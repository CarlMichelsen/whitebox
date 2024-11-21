import {useAppSelector} from "../../../hooks.ts";
import {FC} from "react";
import ConversationSectionListItem from "./Message/ConversationSectionListItem.tsx";
import {ConversationSection} from "../../../model/conversation/conversation.ts";

const SelectedConversation: FC = () => {
    const conversation = useAppSelector(state => state.conversation);
    //const dispatch = useAppDispatch()
    
    return (
        <ol className="space-y-2 p-1 sm:p-0">
            {conversation.selectedConversation?.sections.map((cs: ConversationSection) => <ConversationSectionListItem
                conversationSection={cs}
                key={cs.selectedMessageId}/>)}
        </ol>
    );
}

export default SelectedConversation;