import {FC, useEffect, useState} from "react";
import {ConversationSection} from "../../../../model/conversation/conversation";
import UserMessage from "./UserMessage";
import BotMessage from "./BotMessage.tsx";
import {useAppDispatch, useAppSelector} from "../../../../hooks.ts";
import {ConversationState, setSectionSelectedMessage} from "../../../../state/conversation";

type ConversationSectionListItemProps = {
    conversationSectionIndex: number;
    conversationSection: ConversationSection;
}

const getBranchList = (previousSelectedMessage: string|null, conversationSection: ConversationSection) => {
    return Object.values(conversationSection.messages)
        .filter(m => m.previousMessageId === previousSelectedMessage)
        .sort((a, b) => a.createdUtc-b.createdUtc)
        .map(m => m.id);
}

const ConversationSectionListItem: FC<ConversationSectionListItemProps> = ({ conversationSection, conversationSectionIndex }) => {
    const conversation = useAppSelector(state => state.conversation as ConversationState);
    const dispatch = useAppDispatch()
    
    const [branchList, setBranchList] = useState<string[]>(getBranchList(null, conversationSection));
    const message = conversationSection.messages[conversationSection.selectedMessageId!];
    
    const branchSelect = (messageId: string) => {
        dispatch(setSectionSelectedMessage({
            sectionIndex: conversationSectionIndex,
            messageId: messageId,
        }))
    }
    

    useEffect(() => {
        const previousSelectedMessage =
            conversation.selectedConversation?.sections[conversationSectionIndex - 1]?.selectedMessageId ?? null;
        setBranchList(getBranchList(previousSelectedMessage, conversationSection));
    }, [conversationSection.messages]);
    
    const renderSelectedMessage = () => {
        if (message.aiModel === null) {
            return <UserMessage branchSelect={branchSelect} branchList={branchList} message={message} />;
        } else {
            return <BotMessage message={message} />;
        }
    }
    
    return (
        <li key={conversationSectionIndex}>
            {renderSelectedMessage()}
        </li>
    )
}

export default ConversationSectionListItem