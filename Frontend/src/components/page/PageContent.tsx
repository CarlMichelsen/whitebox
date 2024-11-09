import {FC, ReactNode} from "react";

type PageContentProps = {
    children: ReactNode;
}

const PageContent: FC<PageContentProps> = ({ children }) => {
    
    return (
        <div className="mx-auto container min-h-screen">
            {children}
        </div>
    );
}

export default PageContent;