import {FC, ReactNode} from "react";
import {useAppSelector} from "../../hooks.ts";

type PageContentProps = {
    children: ReactNode;
    className?: string;
}

const ChatPageContent: FC<PageContentProps> = ({ children, className = "" }) => {
    const sidebar = useAppSelector(store => store.sidebar)
    
    return (
        <div className={`mx-auto ${sidebar.isOpen ? "chat-container" : "container"} min-h-screen ${className}`}>
            {children}
        </div>
    );
}

export default ChatPageContent;