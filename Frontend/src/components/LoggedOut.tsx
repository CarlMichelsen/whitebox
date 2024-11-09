import {FC} from "react";
import {UserClient} from "../util/clients/userClient.ts";
const LoggedOut: FC = () => {
    const login = () => {
        const userClient = new UserClient();
        window.location.replace(userClient.getLoginUrl());
    }
    
    return (
        <div>
            <button onClick={login}>Login</button>
        </div>
    );
}

export default LoggedOut;