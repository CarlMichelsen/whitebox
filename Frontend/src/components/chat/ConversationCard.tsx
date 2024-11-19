import {FC, useState} from "react";
import BurgerMenuBlack from "../../assets/icons/burger-menu-black.svg"
import BurgerMenuWhite from "../../assets/icons/burger-menu-white.svg"
import {useAppDispatch, useDarkMode} from "../../hooks.ts";
import {ConversationOption} from "../../model/sidebar/conversationOption.ts";
import {updateTitle} from "../../state/sidebar";

type ConversationCardProps = {
    option: ConversationOption;
}

const ConversationCard: FC<ConversationCardProps> = ({ option }) => {
    const [open, setOpen] = useState(false);
    const dispatch = useAppDispatch()
    const darkMode = useDarkMode();
    
    const onClick = () => {
        dispatch(updateTitle({id: option.id, newTitle: "I've got a new title!"}))
    }
    
    return (
        <div className="grid grid-cols-[1fr_auto]" id={"option-"+option.id}>
            <button onClick={onClick} className="block text-left hover:underline">
                {option.title}
            </button>

            <button onClick={() => setOpen(!open)} className="block hover:font-bold w-6 h-full">
                <img className="brightness-75" src={darkMode ? BurgerMenuWhite : BurgerMenuBlack} alt="burger-menu"/>
            </button>
        </div>
    );
}

export default ConversationCard;