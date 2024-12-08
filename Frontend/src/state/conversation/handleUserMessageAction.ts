import {ConversationState} from "./index";
import {UserMessageEvent} from "../../model/conversation/dto/conversationStream";

export const handleUserMessageAction = (state: ConversationState, payload: UserMessageEvent): void => {
    if (state.selectedConversation?.id !== payload.conversationId) {
        return;
    }
    
}