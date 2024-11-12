import {FC, ReactNode} from "react";

type RootProps = {
    children: ReactNode;
}

const Root: FC<RootProps> = ({children}) => {
    return (
        <main
            className="overflow-y-hidden min-h-screen">
            {children}
        </main>
    );
}

export default Root;