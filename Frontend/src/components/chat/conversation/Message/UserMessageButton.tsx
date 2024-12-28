import {FC} from "react";

type UserMessageButtonProps = {
    iconSrc: string;
    onClick: () => void;
}

const UserMessageButton: FC<UserMessageButtonProps> = ({ iconSrc, onClick }) => {
    return (
        <button
            onClick={onClick}
            className="inline aspect-square w-8 p-1.5 rounded-full sm:hover:bg-neutral-400 sm:dark:hover:bg-neutral-700">
            <img
                draggable="false"
                src={iconSrc}
                alt="edit"/>
        </button>
    );
}

export default UserMessageButton;