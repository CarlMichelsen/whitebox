import {useAppSelector} from "../../../hooks.ts";
import {FC} from "react";
import ConversationSectionListItem from "./Message/ConversationSectionListItem.tsx";
import {ConversationSection} from "../../../model/conversation/conversation.ts";

const SelectedConversation: FC = () => {
    const conversation = useAppSelector(state => state.conversation);
    
    return conversation.selectedConversation !== null ? (
        <ol className="space-y-4 p-1 sm:p-0">
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