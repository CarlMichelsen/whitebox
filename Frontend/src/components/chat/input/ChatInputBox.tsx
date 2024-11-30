import {FC} from "react";
import LineHeightEditableTextBox from "../../util/LineHeightEditableTextBox.tsx";
import ChatContentWidth from "../../page/ChatContentWidth.tsx";
import {useAppDispatch, useAppSelector} from "../../../hooks.ts";
import {setInputState, setText} from "../../../state/input";
import MicrophoneButton from "./MicrophoneButton.tsx";
import EditIndicator from "./EditIndicator.tsx";
import {findPreviousMessage, getLatestSelectedMessage} from "../../../util/conversationUtil.ts";
import {ConversationMessage} from "../../../model/conversation/conversation.ts";
import {ConversationClient} from "../../../util/clients/conversationClient.ts";
import {ConversationStreamEvent} from "../../../model/conversation/dto/conversationStream.ts";
import {AppendConversation, ReplyTo} from "../../../model/conversation/dto/appendConversation.ts";
import {handleStreamEvent} from "../../../state/conversation";

const ChatInputBox: FC = () => {
    const input = useAppSelector(state => state.input);
    const conversation = useAppSelector(state => state.conversation);
    const dispatch = useAppDispatch()
    
    const handle = async (chunk: ConversationStreamEvent) => {
        if (input.inputState === "ready") {
            dispatch(setInputState("receiving"));
        }

        dispatch(handleStreamEvent(chunk));
    }
    
    const onSend = async (text: string) => {
        if (input.inputState === "ready" && text.length > 1) {
            let replyToMessage: ConversationMessage | null = null;
            if (input.editingMessage !== null && conversation.selectedConversation !== null) {
                replyToMessage = input.editingMessage
                    ? findPreviousMessage(conversation.selectedConversation, input.editingMessage)
                    : null
            } else {
                replyToMessage = conversation.selectedConversation
                    ? getLatestSelectedMessage(conversation.selectedConversation)
                    : null;
            }
            
            const client = new ConversationClient();
            const replyToObject: ReplyTo|null = !!replyToMessage
                ? { conversationId: conversation.selectedConversation!.id, replyToMessageId: replyToMessage.id }
                : null;
            
            const appendConversation: AppendConversation = {
                replyTo: replyToObject,
                text,
            }
            const appendPromise = client.appendConversation(appendConversation, handle)
            dispatch(setInputState("sending"))

            await appendPromise;
            dispatch(setInputState("ready"));
        }
    }
    
    const getBorderColor = () => {
        switch (input.inputState) {
            case "ready":
                return 'border-color';
            case "sending":
                return 'border-blue-500';
            case "receiving":
                return 'border-blue-300';
            default:
                throw new Error("this is not supposed to happen");
        }
    }
    
    return (
        <ChatContentWidth className="fixed bottom-0">
            <div className="relative mx-2 mb-2 lg:mb-8 lg:mx-4">
                <EditIndicator />
                
                <LineHeightEditableTextBox
                    className={`p-3 focus:outline-none rounded-md w-full shadow-2xl border ${getBorderColor()} transition-colors ease-in-out bg-neutral-200 focus:bg-white dark:bg-neutral-900 focus:dark:bg-black`}
                    disabled={false}
                    rows={input.rows}
                    text={input.text}
                    onChange={text => dispatch(setText(text))}
                    onEnter={onSend}
                    id="chat-input-box"
                    name="chat-input-box"
                    label="Chat Input"/>
                
                <div className="absolute w-8 h-8 z-50 top-2 right-2">
                    <MicrophoneButton onClick={() => alert("Work in progress")} enabled={false} />
                </div>
            </div>
        </ChatContentWidth>
    );
}

export default ChatInputBox;