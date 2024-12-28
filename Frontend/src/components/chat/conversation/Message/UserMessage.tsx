import {FC} from "react";
import {ConversationMessage} from "../../../../model/conversation/conversation.ts";
import {useAppDispatch, useAppSelector, useDarkMode} from "../../../../hooks.ts";
import EditWhite from "../../../../assets/icons/edit-white.svg"
import EditBlack from "../../../../assets/icons/edit-black.svg"
import CrossWhite from "../../../../assets/icons/cross-white.svg"
import CrossBlack from "../../../../assets/icons/cross-black.svg"
import ArrowWhite from "../../../../assets/icons/arrow-white.svg"
import ArrowBlack from "../../../../assets/icons/arrow-black.svg"
import GarbageWhite from "../../../../assets/icons/garbage-white.svg"
import GarbageBlack from "../../../../assets/icons/garbage-black.svg"
import {setEditingMessage} from "../../../../state/input";
import {escapeHTML, whiteBoxMarked} from "../../../../util/helpers/marked.ts";
import UserMessageButton from "./UserMessageButton.tsx";
import {deleteMessage} from "../../../../state/conversation";

type UserMessageProps = {
    branchSelect: (messageId: string) => void;
    branchList: string[];
    message: ConversationMessage;
}

const UserMessage: FC<UserMessageProps> = ({ branchSelect, branchList, message }) => {
    const input = useAppSelector(state => state.input);
    const dispatch = useAppDispatch()
    const darkMode = useDarkMode();
    
    const branchId = branchList.findIndex(b => message.id === b) + 1;
    
    const selectBranchId = (branchId: number) => {
        const selectedMessageId = branchList[branchId-1];
        branchSelect(selectedMessageId);
    } 
    
    
    const isEditingThisMessage = input.editingMessage === message.id;
    const isEditingAMessage = !input.editingMessage;
    const text = escapeHTML(message.content.map(c => c.value).join('\n'));
    return (
        <div className={`flex justify-end ${branchList.length > 1 ? "mb-8" : ""}  ml-10 pt-10 sm:pt-4 sm:ml-24`}>
            <div
                id={"message-"+message.id}
                className="relative shadow-2xl rounded-md bg-neutral-200 dark:bg-neutral-800 ml-auto inline-block">

                <ol className="absolute sm:-left-20 sm:-top-1 left-0 -top-10 h-8 p-1.5 sm:hidden sm:group-hover:block space-x-2 sm:space-x-0">
                    <li className="inline">
                        <UserMessageButton
                            onClick={() => dispatch(setEditingMessage(isEditingThisMessage ? null : message.id))}
                            iconSrc={isEditingThisMessage
                                ? (darkMode ? CrossWhite : CrossBlack)
                                : (darkMode ? EditWhite : EditBlack)}/>
                    </li>

                    <li className={isEditingThisMessage ? "inline" : "hidden"}>
                        <UserMessageButton
                            onClick={() => dispatch(deleteMessage({ conversationId: message.conversationId, messageId: message.id}))}
                            iconSrc={darkMode ? GarbageWhite : GarbageBlack}/>
                    </li>
                </ol>

                <div className="shadow-inner px-2 py-1 sm:px-3 sm:py-2 rounded-md">
                    <span dangerouslySetInnerHTML={({__html: whiteBoxMarked.parse(text) as string})}></span>
                </div>

                {branchList.length > 1 ? (
                    <div className="absolute px-1 right-0 -bottom-10 h-10 w-32 grid grid-rows-1 grid-cols-3">
                    <button
                            disabled={branchId === 1 || !isEditingAMessage}
                            onClick={() => selectBranchId(branchId - 1)}
                            className="m-1 pb-0.5 rounded-md w-6 sm:enabled:hover:bg-neutral-300 sm:enabled:dark:hover:bg-neutral-800 disabled:opacity-10">
                            <img
                                draggable="false"
                                src={darkMode ? ArrowWhite : ArrowBlack}
                                className="aspect-square -rotate-90 p-0.5 pr-1"
                                alt="+"/>
                        </button>
                        
                        <div className="pt-1.5 pr-1.5">
                            <p className="text-center">{branchId}/{branchList.length}</p>
                        </div>
                        
                        <button
                            disabled={branchId === branchList.length || !isEditingAMessage}
                            onClick={() => selectBranchId(branchId + 1)}
                            className="m-1 pb-0.5 rounded-md w-6 sm:enabled:hover:bg-neutral-300 sm:enabled:dark:hover:bg-neutral-800 disabled:opacity-10">
                            <img
                                draggable="false"
                                src={darkMode ? ArrowWhite : ArrowBlack}
                                className="aspect-square rotate-90 p-0.5 pl-1"
                                alt="-"/>
                        </button>
                    </div>
                ) : null}
            </div>
        </div>
    )
}

export default UserMessage;