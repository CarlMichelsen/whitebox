import {FC} from "react";
import {ModalContentProps} from "./ModalContentProps.ts";
import SystemMessageEditor from "../../chat/system/SystemMessageEditor.tsx";
import {useAppDispatch, useAppSelector} from "../../../hooks.ts";
import {ConversationState, setConversationSystemMessage} from "../../../state/conversation";
import {ConversationClient} from "../../../util/clients/conversationClient.ts";
import {SetConversationSystemMessage} from "../../../model/conversation/dto/setConversationSystemMessage.ts";

const ConversationSystemMessageContent: FC<ModalContentProps> = () => {
    const dispatch = useAppDispatch()
    const conversation = useAppSelector(state => state.conversation as ConversationState);
    
    return <SystemMessageEditor
        label="Default system message editor"
        initialMessage={conversation.selectedConversation?.systemMessage ?? ""}
        saveChanges={async (m) => {
            const client = new ConversationClient();
            const payload: SetConversationSystemMessage = {systemMessage: m};
            const res = await client.setConversationSystemMessage(
                conversation.selectedConversation!.id,
                payload);
            if (res.ok && res.value) {
                dispatch(setConversationSystemMessage(res.value.currentSystemMessage));
            }
        }}/>
}

export default ConversationSystemMessageContent;