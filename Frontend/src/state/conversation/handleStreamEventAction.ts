import {StreamEvent} from "../../model/conversation/dto/conversationStream.ts";
import {ConversationState} from "./index.ts";

export const handleStreamEventAction = (_: ConversationState, action: StreamEvent) => {
    console.log(action);
}