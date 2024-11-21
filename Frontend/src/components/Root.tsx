import {FC, ReactNode} from "react";
import EditLogicComponent from "./chat/EditLogicComponent.tsx";

type RootProps = {
    children: ReactNode;
}

const Root: FC<RootProps> = ({children}) => {
    return (
        <main
            className="relative overflow-y-hidden min-h-screen">
            {children}
            <EditLogicComponent />
        </main>
    );
}

export default Root;