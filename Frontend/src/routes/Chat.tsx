import {FC} from "react";
import Sidebar from "../components/chat/Sidebar.tsx";
import ConversationSelector from "../components/chat/ConversationSelector.tsx";
import ChatContentWidth from "../components/page/ChatContentWidth.tsx";
import ChatContainer from "../components/chat/conversation/ChatContainer.tsx";
import ChatInputBox from "../components/chat/input/ChatInputBox.tsx";

const Chat: FC = () => {
    return (
        <div className="relative h-full grid grid-cols-[auto_1fr]">
            <Sidebar>
                <ConversationSelector />
            </Sidebar>
            
            <ChatContentWidth className="min-h-screen">
                <ChatContainer />
                <ChatInputBox />
            </ChatContentWidth>
        </div>
    )
}

export default Chat;