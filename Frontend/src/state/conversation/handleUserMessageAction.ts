import {ConversationState} from "./index";
import {UserMessageEvent} from "../../model/conversation/dto/conversationStream";
import {appendMessage} from "./shared.ts";

export const handleUserMessageAction = (state: ConversationState, payload: UserMessageEvent): void => {
    if (state.selectedConversation?.id !== payload.conversationId) {
        return;
    }

    appendMessage(state.selectedConversation, payload.message);
}