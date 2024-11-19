import {FC, useEffect, useState} from "react";
import LineHeightEditableTextBox from "../../util/LineHeightEditableTextBox.tsx";
import ChatContentWidth from "../../page/ChatContentWidth.tsx";
import {useAppDispatch, useAppSelector} from "../../../hooks.ts";
import {setText} from "../../../state/input";
import MicrophoneButton from "./MicrophoneButton.tsx";
import {AudioCapture} from "../../../util/audio/audioCapture.ts";

type ChatInputBoxProps = {
    name: string;
    id: string;
}

const capture = new AudioCapture();

const ChatInputBox: FC<ChatInputBoxProps> = ({ name, id }) => {
    const input = useAppSelector(state => state.input);
    const dispatch = useAppDispatch()
    
    const [recording, setRecording] = useState<boolean>(false);
    
    useEffect(() => {
        if (recording) {
            capture.start().catch(console.error);
        } else {
            capture.stop().catch(console.error);
        }
    }, [recording])
    
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
                    <MicrophoneButton onClick={() => setRecording(!recording)} enabled={recording} />
                </div>
            </div>
        </ChatContentWidth>
    );
}

export default ChatInputBox;