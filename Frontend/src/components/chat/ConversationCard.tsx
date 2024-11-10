import {FC, useState} from "react";
import BurgerMenuBlack from "../../assets/burger-menu-black.svg"
import BurgerMenuWhite from "../../assets/burger-menu-white.svg"
import {useDarkMode} from "../../hooks.ts";

type ConversationCardProps = {
    tempTitle: string;
}

const ConversationCard: FC<ConversationCardProps> = ({ tempTitle }) => {
    const [open, setOpen] = useState(false);
    const darkMode = useDarkMode();
    
    
    return (
        <div className="grid grid-cols-[1fr_auto] border dark:border-neutral-700 border-neutral-300 rounded-sm">
            <button className="block text-left hover:underline pl-1">
                <p>{tempTitle}</p>
            </button>

            <button onClick={() => setOpen(!open)} className="block hover:font-bold w-6 h-full">
                <img className="brightness-75" src={darkMode ? BurgerMenuWhite : BurgerMenuBlack} alt="burger-menu"/>
            </button>
        </div>
    );
}

export default ConversationCard;