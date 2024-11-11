import {FC} from "react";
import Sidebar from "../components/chat/Sidebar.tsx";
import ChatContainer from "../components/chat/ChatContainer.tsx";
import {useAppSelector} from "../hooks.ts";
import ConversationSelector from "../components/chat/ConversationSelector.tsx";
import ChatPageContent from "../components/page/ChatPageContent.tsx";

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
            
            <ChatPageContent>
                <ChatContainer />
            </ChatPageContent>
        </div>
    )
}

export default Chat;