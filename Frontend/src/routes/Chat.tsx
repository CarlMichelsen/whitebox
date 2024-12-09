import {FC, useEffect} from "react";
import Sidebar from "../components/chat/Sidebar.tsx";
import ConversationSelector from "../components/chat/ConversationSelector.tsx";
import ChatContentWidth from "../components/page/ChatContentWidth.tsx";
import ChatContainer from "../components/chat/conversation/ChatContainer.tsx";
import ChatInputBox from "../components/chat/input/ChatInputBox.tsx";
import EditLogicComponent from "../components/chat/EditLogicComponent.tsx";
import ModelSelectorButton from "../components/chat/ModelSelectorButton.tsx";
import {ChatConfigurationClient} from "../util/clients/chatConfigurationClient.ts";
import {useQuery} from "react-query";
import {ConversationClient} from "../util/clients/conversationClient.ts";
import {useAppDispatch} from "../hooks.ts";
import {setChatConfiguration} from "../state/input";
import {setConversations} from "../state/sidebar";

const Chat: FC = () => {
    const dispatch = useAppDispatch()
    
    const getConversationOptions = async () => {
        const client = new ConversationClient();
        const conversationOptionsResponse = await client.getConversationOptions();

        if (!conversationOptionsResponse.ok || !conversationOptionsResponse.value) {
            throw new Error(conversationOptionsResponse.errors.join('\n'));
        }

        return conversationOptionsResponse.value;
    }

    const getChatConfiguration = async () => {
        const client = new ChatConfigurationClient();
        const chatConfigurationResponse = await client.getChatConfiguration();

        if (!chatConfigurationResponse.ok || !chatConfigurationResponse.value) {
            throw new Error(chatConfigurationResponse.errors.join('\n'));
        }

        return chatConfigurationResponse.value;
    }

    const conversationOptionsQuery = useQuery(
        {
            queryKey: ['conversation-options'],
            queryFn: getConversationOptions,
            staleTime: 1000 * 60 * 5,
        });
    
    const chatConfigurationQuery = useQuery(
        {
            queryKey: ['chat-configuration'],
            queryFn: getChatConfiguration,
            staleTime: 1000 * 60 * 5,
        });

    useEffect(() => {
        if (conversationOptionsQuery.data) {
            dispatch(setConversations(conversationOptionsQuery.data))
        }
    }, [conversationOptionsQuery.data]);
    
    useEffect(() => {
        if (chatConfigurationQuery.data) {
            dispatch(setChatConfiguration(chatConfigurationQuery.data));
        }
    }, [chatConfigurationQuery.data]);
    
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