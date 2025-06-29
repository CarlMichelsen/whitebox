import {FC, useState} from "react";
import BurgerMenuBlack from "../../assets/icons/burger-menu-black.svg"
import BurgerMenuWhite from "../../assets/icons/burger-menu-white.svg"
import {useAppDispatch, useAppSelector, useDarkMode} from "../../hooks.ts";
import {ConversationOption} from "../../model/sidebar/conversationOption.ts";
import {ConversationClient} from "../../util/clients/conversationClient.ts";
import {selectConversation} from "../../state/conversation";
import {setIsSidebarOpen} from "../../state/sidebar";
import {DomIdentifiers} from "../util/domIdentifiers.ts";
import {BreakPoints} from "../util/breakPoints.ts";

type ConversationCardProps = {
    option: ConversationOption;
}

const ConversationCard: FC<ConversationCardProps> = ({ option }) => {
    const [open, setOpen] = useState(false);
    const conversation = useAppSelector(store => store.conversation)
    const dispatch = useAppDispatch()
    const darkMode = useDarkMode();
    
    const onClick = async () => {
        const client = new ConversationClient();
        const res = await client.getConversation(option.id);
        if (res.ok && res.value) {
            dispatch(selectConversation(res.value));
            if (window.innerWidth < BreakPoints.mobileWidthBreakPoint) {
                dispatch(setIsSidebarOpen(false));
                const inputElement = document.getElementById(DomIdentifiers.inputElementId) as HTMLInputElement;
                inputElement?.focus();
            }
        }
    }
    
    const selected = conversation.selectedConversation?.id === option.id;
    
    return (
        <div className={`grid grid-cols-[1fr_auto] h-6 ${selected ? "dark:bg-neutral-800 bg-neutral-300 grid-cols-[1fr_auto]" : "grid-cols-1"}`} id={"option-"+option.id}>
            <button
                onClick={onClick}
                className="inline-block text-left rounded-xs hover:underline ml-1 overflow-hidden text-ellipsis whitespace-nowrap text-sm">
                {option.summary}
            </button>

            {selected ? (
                <button onClick={() => setOpen(!open)} className="block hover:font-bold w-6 h-full dark:hover:bg-neutral-900 hover:bg-neutral-400">
                    <img className="brightness-75" src={darkMode ? BurgerMenuWhite : BurgerMenuBlack} alt="burger-menu"/>
                </button>
            ): null}
        </div>
    );
}

export default ConversationCard;