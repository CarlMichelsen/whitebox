import {FC, ReactNode} from "react";

type LoggedInProps = {
    children: ReactNode;
}


const LoggedIn: FC<LoggedInProps> = ({ children }) => {
    return children;
}

export default LoggedIn;