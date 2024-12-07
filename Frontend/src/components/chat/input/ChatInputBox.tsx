import {FC, useEffect, useRef} from "react";
import LineHeightEditableTextBox from "../../util/LineHeightEditableTextBox.tsx";
import ChatContentWidth from "../../page/ChatContentWidth.tsx";
import {useAppDispatch, useAppSelector} from "../../../hooks.ts";
import {setText} from "../../../state/input";
import MicrophoneButton from "./MicrophoneButton.tsx";
import EditIndicator from "./EditIndicator.tsx";
import ConversationResponseLogicComponent, {
    ConversationResponseLogicComponentProps
} from "../ConversationResponseLogicComponent.tsx";

const ChatInputBox: FC = () => {
    const input = useAppSelector(state => state.input);
    const dispatch = useAppDispatch();
    const logicComponentRef = useRef<ConversationResponseLogicComponentProps>(null);
    const inputElementId = "chat-input-box";
    
    const onSend = async (text: string) => {
        if (logicComponentRef.current) {
            await logicComponentRef.current.onSend(text);
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

    useEffect(() => {
        if (!!input.editingMessage) {
            const inputElement = document.getElementById(inputElementId) as HTMLInputElement;
            if (inputElement) {
                inputElement.focus();
            }
        }
    }, [input.editingMessage]);
    
    return (
        <ChatContentWidth className="fixed bottom-0">
            <div className="relative mx-2 mb-2 lg:mb-8 lg:mx-4 shadow-2xl">
                <EditIndicator />
                
                <LineHeightEditableTextBox
                    className={`p-3 focus:outline-none rounded-md w-full shadow-inner border ${getBorderColor()} transition-colors ease-in-out bg-neutral-100 focus:bg-white dark:bg-neutral-900 focus:dark:bg-black`}
                    disabled={false}
                    rows={input.rows}
                    text={input.text}
                    onChange={text => dispatch(setText(text))}
                    onEnter={onSend}
                    id={inputElementId}
                    name="chat-box"
                    label="Chat Input"/>
                
                <div className="absolute w-8 h-8 z-50 top-2 right-2">
                    <MicrophoneButton onClick={() => alert("Work in progress")} enabled={false} />
                </div>
            </div>

            <ConversationResponseLogicComponent ref={logicComponentRef} />
        </ChatContentWidth>
    );
}

export default ChatInputBox;