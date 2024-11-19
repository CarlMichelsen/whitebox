import WhiteMicrophone from "../../../assets/icons/microphone-white.svg"
import BlackMicrophone from "../../../assets/icons/microphone-black.svg"
import {FC} from "react";
import {useDarkMode} from "../../../hooks.ts";

type MicrophoneButtonProps = {
    className?: string
    enabled: boolean;
    onClick: () => void;
}

/*const getConnection = () => {
    return new HubConnectionBuilder()
        .withUrl(hostUrl()+"/api/v1/speechToTextHub")
        .build();
}*/

const MicrophoneButton: FC<MicrophoneButtonProps> = ({ className = "", enabled, onClick }) => {
    const darkMode = useDarkMode()
    
    return (
        <button
            className={`w-full h-full rounded-full p-2 shadow-2xl ${enabled ? "bg-green-800" : "dark:bg-black bg-white"} ${className}`}
            onClick={onClick}>
            <img
                className="block pointer-events-none aspect-square"
                src={darkMode ? WhiteMicrophone : BlackMicrophone}
                alt="microphone"/>
        </button>
    )
}

export default MicrophoneButton;