import {FC, ReactNode} from "react";

type RootProps = {
    children: ReactNode;
}

const Root: FC<RootProps> = ({ children }) => {
    return (
        <main className="hide-scrollbar overflow-y-hidden overflow-x-scroll h-screen">
            <h1 className="text-2xl">Root</h1>
            {children}
        </main>
    );
}

export default Root;