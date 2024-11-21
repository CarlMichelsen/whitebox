import {FC} from "react";
import SearchWhite from "../../assets/icons/search-white.svg"
import SearchBlack from "../../assets/icons/search-black.svg"
import {useAppDispatch, useDarkMode} from "../../hooks.ts";
import {setSearch} from "../../state/sidebar";

const SearchBox: FC = () => {
    const dispatch = useAppDispatch()
    const darkMode = useDarkMode();
    
    return (
        <span className="relative mx-1 mb-1">
            <input
                className="block h-8 w-full pl-0.5 py-0.5 pr-7 focus:outline-none transition-colors ease-in-out border border-color rounded-sm bg-neutral-200 focus:bg-white dark:bg-neutral-900 focus:dark:bg-black"
                placeholder="Under construction"
                onChange={e => dispatch(setSearch(e.target.value))}
                type="search" />
            
            <img
                className="absolute aspect-square h-6 right-1 top-1 brightness-75 pointer-events-none"
                src={darkMode ? SearchWhite : SearchBlack}
                alt="search-icon" />
        </span>
    );
}

export default SearchBox;