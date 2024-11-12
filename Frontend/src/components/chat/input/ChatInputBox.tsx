import {FC, useState} from "react";
import LineHeightEditableTextBox from "../../util/LineHeightEditableTextBox.tsx";
import ChatContentWidth from "../../page/ChatContentWidth.tsx";
import {useAppDispatch, useAppSelector} from "../../../hooks.ts";
import {setText} from "../../../state/input";
import MicrophoneButton from "./MicrophoneButton.tsx";

type ChatInputBoxProps = {
    name: string;
    id: string;
}

const ChatInputBox: FC<ChatInputBoxProps> = ({ name, id }) => {
    const input = useAppSelector(state => state.input);
    const dispatch = useAppDispatch()
    
    const [placeholder, setPlaceholder] = useState<boolean>(false);
    
    return (
        <ChatContentWidth className="fixed bottom-0">
            <div className="relative mx-2 mb-2 lg:mb-8 lg:mx-4">
                <LineHeightEditableTextBox
                    className="p-3 rounded-lg w-full shadow-2xl border border-color bg-neutral-100 dark:bg-neutral-900"
                    rows={input.rows}
                    text={input.text}
                    id={id}
                    onChange={text => dispatch(setText(text))}
                    name={name}/>
                
                <div className="absolute w-8 h-8 z-50 top-2 right-2">
                    <MicrophoneButton onClick={() => setPlaceholder(!placeholder)} enabled={placeholder} />
                </div>
            </div>
        </ChatContentWidth>
    );
}

export default ChatInputBox;