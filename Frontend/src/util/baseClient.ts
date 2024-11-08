import { v4 as uuidV4 } from 'uuid';
import { errorResponse, type ServiceResponse } from "../model/serviceResponse";
import { UserAccessor } from './userAccessor';

export type Method = "GET"
    | "HEAD"
    | "POST"
    | "PUT"
    | "DELETE"
    | "CONNECT"
    | "OPTIONS"
    | "TRACE"
    | "PATCH";

export abstract class BaseClient
{
    protected abstract host: string;

    protected async request<T>(
        method: Method,
        path: string,
        body?: object,
        headers?: { [key: string]: string },
        traceId?: string)
    {
        try {
            return await this.internalRequest<T>(method, path, body, headers, traceId);
        } catch (error) {
            const userRes = await UserAccessor.getUser();
            if (userRes.ok) {
                // User is logged in so this must be a genuine error.
                return errorResponse<T>();
            }

            const refreshRes = await UserAccessor.refresh();
            if (!refreshRes.ok) {
                return errorResponse<T>();
            }
        }

        try {
            return await this.internalRequest<T>(method, path, body, headers, traceId);
        } catch (error) {
            return errorResponse<T>();
        }
    }

    private async internalRequest<T>(
        method: Method,
        path: string,
        body?: object,
        headers?: { [key: string]: string },
        traceId?: string,): Promise<ServiceResponse<T>>
    {
        const actualTraceId = traceId ?? uuidV4();
        const init: RequestInit = {
            method: method,
            credentials: 'include',
            headers: {
                "Content-Type": "application/json",
                "X-Trace-Id": actualTraceId,
                ...headers },
        };

        if (body) {
            init.body = JSON.stringify(body);
        }

        const fullDestination = this.host + this.ensureLeadingSlash(path);
        const response = await fetch(fullDestination, init);

        if (response.ok) {
            return (await response.json()) as ServiceResponse<T>;
        } else {
            throw new Error(`${method} "${fullDestination}" request failed with status: ${response.status}`);
        }
    }

    private ensureLeadingSlash(input: string): string {
        if (!input.startsWith("/")) {
            return "/" + input;
        }

        return input;
    }
}