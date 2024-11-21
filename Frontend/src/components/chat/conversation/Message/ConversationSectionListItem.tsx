import {FC} from "react";
import {ConversationSection} from "../../../../model/conversation/conversation";
import UserMessage from "./UserMessage";
import BotMessage from "./BotMessage.tsx";

type ConversationSectionListItemProps = {
    conversationSection: ConversationSection;
}

const ConversationSectionListItem: FC<ConversationSectionListItemProps> = ({ conversationSection }) => {
    const message = conversationSection.messages[conversationSection.selectedMessageId];
    
    const renderSelectedMessage = () => {
        if (message.bot === null) {
            return <UserMessage message={message} />;
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