import {FC} from "react";

type MicrophoneListenerProps = {
    isListening: boolean;
    worklet: AudioWorklet;
}

const MicrophoneListener: FC<MicrophoneListenerProps> = (/*{ isListening = false, worklet }*/) => {
    return (<div>mic</div>)
}

export default MicrophoneListener