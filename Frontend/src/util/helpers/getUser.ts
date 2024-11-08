import {UserAccessor} from "../userAccessor.ts";
import {errorResponse, ServiceResponse} from "../../model/serviceResponse.ts";
import {AuthenticatedUser} from "../../model/user.ts";

export const getUser = async (): Promise<ServiceResponse<AuthenticatedUser>> => {
    const userResponse = await UserAccessor.getUser();
    if (userResponse.ok) {
        return userResponse
    } else {
        const refreshResponse = await UserAccessor.refresh();
        if (refreshResponse.ok) {
            const secondUserResponse = await UserAccessor.getUser();
            if (secondUserResponse.ok) {
                return secondUserResponse
            }
        }

        return errorResponse<AuthenticatedUser>();
    }
}