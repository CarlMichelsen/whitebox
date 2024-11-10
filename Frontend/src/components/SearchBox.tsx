import {FC} from "react";
import SearchWhite from "../assets/search-white.svg"
import SearchBlack from "../assets/search-black.svg"
import {useDarkMode} from "../hooks.ts";

const SearchBox: FC = () => {
    const darkMode = useDarkMode();
    
    return (
        <span className="relative mx-1 mb-1">
            <input
                className="block h-8 w-full pl-0.5 py-0.5 pr-7 border dark:border-neutral-700 border-neutral-300 rounded-sm"
                type="search" />
            
            <img
                className="absolute aspect-square h-6 right-1 top-1 brightness-75 pointer-events-none"
                src={darkMode ? SearchWhite : SearchBlack}
                alt="search-icon" />
        </span>
    );
}

export default SearchBox;