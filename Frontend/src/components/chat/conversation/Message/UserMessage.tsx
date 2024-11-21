import {FC} from "react";
import {ConversationMessage} from "../../../../model/conversation/conversation.ts";
import {useAppDispatch, useAppSelector, useDarkMode} from "../../../../hooks.ts";
import EditWhite from "../../../../assets/icons/edit-white.svg"
import EditBlack from "../../../../assets/icons/edit-black.svg"
import CrossWhite from "../../../../assets/icons/cross-white.svg"
import CrossBlack from "../../../../assets/icons/cross-black.svg"
import {setEditingMessage} from "../../../../state/input";

type UserMessageProps = {
    message: ConversationMessage;
}

const UserMessage: FC<UserMessageProps> = ({ message }) => {
    const input = useAppSelector(state => state.input);
    const dispatch = useAppDispatch()
    const darkMode = useDarkMode();
    
    
    const isEditingThisMessage = input.editingMessage === message.id;
    return (
        <div className="group flex justify-end">
            <div
                id={"message-"+message.id}
                className="shadow-2xl px-3 py-2 rounded-md bg-neutral-200 dark:bg-neutral-700 ml-auto inline-block relative">
                <button
                    onClick={() => dispatch(setEditingMessage(isEditingThisMessage ? null : message.id))}
                    className="absolute -left-10 top-0 aspect-square w-8 p-1.5 sm:hidden sm:group-hover:block rounded-full hover:bg-neutral-400 dark:hover:bg-neutral-700">
                    <img
                        src={isEditingThisMessage
                            ? (darkMode ? CrossWhite : CrossBlack)
                            : (darkMode ? EditWhite : EditBlack)}
                        alt="edit"/>
                </button>
                <pre className="message-text">{message.text}</pre>
            </div>
        </div>
    )
}

export default UserMessage;