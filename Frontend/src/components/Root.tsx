import {FC, ReactNode} from "react";

type RootProps = {
    children: ReactNode;
}

const Root: FC<RootProps> = ({children}) => {
    return (
        <main
            className="hide-scrollbar overflow-y-hidden min-h-screen">
            {children}
        </main>
    );
}

export default Root;