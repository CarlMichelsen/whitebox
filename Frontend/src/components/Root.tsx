import {FC, ReactNode} from "react";
import ModalLogicComponent from "./modal/ModalLogicComponent.tsx";

type RootProps = {
    children: ReactNode;
}

const Root: FC<RootProps> = ({children}) => {
    return (
        <main
            className="relative overflow-y-hidden min-h-screen">
            {children}
            <ModalLogicComponent />
        </main>
    );
}

export default Root;