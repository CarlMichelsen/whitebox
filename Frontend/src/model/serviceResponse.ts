type OkServiceResponse<T> = {
    ok: true;
    value?: T;
    errors: [];
    nowUtc: string;
}

type NotOkServiceResponse = {
    ok: false;
    errors: [string, ...string[]];
    nowUtc: string;
}

export type ServiceResponse<T> = OkServiceResponse<T> | NotOkServiceResponse;

export const errorResponse = <T>(): ServiceResponse<T> =>
{
    return {
        ok: false,
        errors: ["error"],
        nowUtc: (new Date()).toString(),
    }
}