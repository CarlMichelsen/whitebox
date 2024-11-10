import {FC} from "react";
import Sidebar from "../components/chat/Sidebar.tsx";
import ChatContainer from "../components/chat/ChatContainer.tsx";
import PageContent from "../components/page/PageContent.tsx";
import {useAppSelector} from "../hooks.ts";
import ConversationSelector from "../components/chat/ConversationSelector.tsx";

const Chat: FC = () => {
    const sidebar = useAppSelector(store => store.sidebar)
    
    return (
        <div className="relative h-full block sm:grid sm:grid-cols-[auto_1fr]">
            <Sidebar>
                <ConversationSelector />
            </Sidebar>
            
            <div className={sidebar.isOpen ? "w-52" : "w-0"}
                 aria-hidden>
            </div>
            
            <PageContent>
                <ChatContainer />
            </PageContent>
        </div>
    )
}

export default Chat;