import {FC, ReactNode} from "react";

type PageContentProps = {
    children: ReactNode;
    className?: string;
}

const PageContent: FC<PageContentProps> = ({ children, className = "" }) => {
    return (
        <div className={`mx-auto container ${className}`}>
            {children}
        </div>
    );
}

export default PageContent;