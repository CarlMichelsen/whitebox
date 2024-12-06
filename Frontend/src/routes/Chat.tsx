import {FC} from "react";
import Sidebar from "../components/chat/Sidebar.tsx";
import ConversationSelector from "../components/chat/ConversationSelector.tsx";
import ChatContentWidth from "../components/page/ChatContentWidth.tsx";
import ChatContainer from "../components/chat/conversation/ChatContainer.tsx";
import ChatInputBox from "../components/chat/input/ChatInputBox.tsx";
import EditLogicComponent from "../components/chat/EditLogicComponent.tsx";
import ModelSelectorButton from "../components/chat/ModelSelectorButton.tsx";
import {useLocation} from "react-router-dom";

const Chat: FC = () => {
    const location = useLocation();
    const pathSplit = location.pathname.split('/');
    const conversationId: string|null = pathSplit[2] ?? null;
    
    console.log("path selected conversation", conversationId);
    
    return (
        <div className="relative h-full grid grid-cols-[auto_1fr]">
            <Sidebar>
                <ConversationSelector />
            </Sidebar>
            
            <ChatContentWidth className="min-h-screen">
                <ChatContainer />
                <ChatInputBox />
                <EditLogicComponent />
            </ChatContentWidth>
            
            <ModelSelectorButton />
        </div>
    )
}

export default Chat;