import {FC, useState} from "react";
import BurgerMenuBlack from "../../assets/icons/burger-menu-black.svg"
import BurgerMenuWhite from "../../assets/icons/burger-menu-white.svg"
import {useAppDispatch, useAppSelector, useDarkMode} from "../../hooks.ts";
import {ConversationOption} from "../../model/sidebar/conversationOption.ts";
import {ConversationClient} from "../../util/clients/conversationClient.ts";
import {selectConversation} from "../../state/conversation";
import {useNavigate} from "react-router-dom";

type ConversationCardProps = {
    option: ConversationOption;
}

const ConversationCard: FC<ConversationCardProps> = ({ option }) => {
    const [open, setOpen] = useState(false);
    const conversation = useAppSelector(store => store.conversation)
    const dispatch = useAppDispatch()
    const darkMode = useDarkMode();
    const navigate = useNavigate();

    const setCPath = (id: string|null) => {
        if (id) {
            navigate(`/c/${id}`, { replace: true });
        } else {
            navigate('/c', { replace: true });
        }
    };
    
    const onClick = async () => {
        const client = new ConversationClient();
        const res = await client.getConversation(option.id);
        if (res.ok && res.value) {
            dispatch(selectConversation(res.value));
            setCPath(res.value.id)
        }
    }
    
    const selected = conversation.selectedConversation?.id === option.id;
    
    return (
        <div className={`grid grid-cols-[1fr_auto] h-6 ${selected ? "dark:bg-neutral-800 bg-neutral-300 grid-cols-[1fr_auto]" : "grid-cols-1"}`} id={"option-"+option.id}>
            <button
                onClick={onClick}
                className="inline-block text-left rounded-sm hover:underline ml-1 overflow-hidden text-ellipsis whitespace-nowrap text-sm">
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