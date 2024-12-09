import {useAppDispatch, useAppSelector} from "../../../hooks.ts";
import {FC, useEffect} from "react";
import ConversationSectionListItem from "./Message/ConversationSectionListItem.tsx";
import {ConversationSection} from "../../../model/conversation/conversation.ts";
import {ConversationState, selectConversation} from "../../../state/conversation";
import {useLocation} from "react-router-dom";
import {ConversationClient} from "../../../util/clients/conversationClient.ts";

const SelectedConversation: FC = () => {
    const dispatch = useAppDispatch()
    const conversation = useAppSelector(state => state.conversation as ConversationState);
    const location = useLocation();

    const getConversation = async (conversationId: string|null) => {
        if (conversationId === null) {
            dispatch(selectConversation(null));
            return;
        }
        
        const client = new ConversationClient();
        const res = await client.getConversation(conversationId);
        if (res.ok && res.value) {
            dispatch(selectConversation(res.value));
        }
    }
    
    useEffect(() => {
        const pathSplit = location.pathname.split('/');
        let pathConversationId: string|null = pathSplit[2] ?? null;
        if (pathConversationId?.length === 0) {
            pathConversationId = null;
        }
        
        if (pathConversationId) {
            getConversation(pathConversationId)
                .catch(err => console.error(err));
        }
    }, [location.pathname]);
    
    return conversation.selectedConversation !== null ? (
        <ol className="space-y-4 p-1 sm:p-0 mb-36">
            {conversation.selectedConversation.sections
                .filter((cs: ConversationSection) => cs.selectedMessageId !== null)
                .map((cs: ConversationSection, index: number) => <ConversationSectionListItem
                    conversationSectionIndex={index}
                    conversationSection={cs}
                    key={cs.selectedMessageId}/>)}
        </ol>
    ) : <p>No conversation selected.</p>
}

export default SelectedConversation;