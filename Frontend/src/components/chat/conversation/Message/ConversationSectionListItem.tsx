import {FC, useEffect, useState} from "react";
import {ConversationSection} from "../../../../model/conversation/conversation";
import UserMessage from "./UserMessage";
import BotMessage from "./BotMessage.tsx";
import {useAppDispatch} from "../../../../hooks.ts";
import {setSectionSelectedMessage} from "../../../../state/conversation";

type ConversationSectionListItemProps = {
    conversationSectionIndex: number;
    conversationSection: ConversationSection;
}

const getBranchList = (conversationSection: ConversationSection) => {
    return Object.values(conversationSection.messages)
        .sort((a, b) => a.created-b.created)
        .map(m => m.id);
}

const ConversationSectionListItem: FC<ConversationSectionListItemProps> = ({ conversationSection, conversationSectionIndex }) => {
    const dispatch = useAppDispatch()
    
    const [branchList, setBranchList] = useState<string[]>(getBranchList(conversationSection));
    const message = conversationSection.messages[conversationSection.selectedMessageId!];
    
    const branchSelect = (messageId: string) => {
        dispatch(setSectionSelectedMessage({
            sectionIndex: conversationSectionIndex,
            messageId: messageId,
        }))
    }
    

    useEffect(() => {
        setBranchList(getBranchList(conversationSection))
    }, [conversationSection.messages]);
    
    const renderSelectedMessage = () => {
        if (message.bot === null) {
            return <UserMessage branchSelect={branchSelect} branchList={branchList} message={message} />;
        } else {
            return <BotMessage message={message} />;
        }
    }
    
    return (
        <li>
            {renderSelectedMessage()}
        </li>
    )
}

export default ConversationSectionListItem