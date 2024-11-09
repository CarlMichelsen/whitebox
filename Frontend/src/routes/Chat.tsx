import {FC} from "react";
import Sidebar from "../components/chat/Sidebar.tsx";
import ChatContainer from "../components/chat/ChatContainer.tsx";
import PageContent from "../components/page/PageContent.tsx";

const Chat: FC = () => {
    
    return (
        <div className="relative h-full">
            <Sidebar>
                hello
            </Sidebar>
            
            <PageContent>
                <ChatContainer />
            </PageContent>
        </div>
    )
}

export default Chat;