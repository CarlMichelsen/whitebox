import {FC, ReactNode} from "react";
import {useAppDispatch, useAppSelector} from "../../hooks.ts";
import ToggleSidebarButton from "./ToggleSidebarButton.tsx";
import SidebarBottomBox from "./SidebarBottomBox.tsx";
import SearchBox from "../util/SearchBox.tsx";
import {selectConversation} from "../../state/conversation";

type SidebarProps = {
    children: ReactNode;
}

const Sidebar: FC<SidebarProps> = ({ children }) => {
    const dispatch = useAppDispatch()
    const sidebar = useAppSelector(store => store.sidebar)
    
    const sidebarClasses = (): string => {
        if (sidebar.isOpen) {
            return "block"
        }
        
        return "hidden w-0";
    }

    const backdropClasses = (): string => {
        if (sidebar.isOpen) {
            return "block sm:hidden opacity-60 dark:opacity-80"
        }

        return "hidden opacity-0";
    }

    const toggleButtonClasses = (): string => {
        if (sidebar.isOpen) {
            return "hidden"
        }

        return "block";
    }
    
    return (
        <aside className={`transition-width duration-100 ease-in-out shadow-2xl ${sidebar.isOpen ? "w-0 sm:w-64" : "w-0"}`}>
            <div className={`fixed z-30 h-screen w-64 dark:bg-neutral-900 bg-neutral-100 ${sidebarClasses()} grid grid-rows-[auto_auto_1fr_auto]`}>
                <div className="grid grid-cols-[1fr_auto] gap-1">
                    <button
                        className="m-1 bg-blue-400 dark:bg-blue-800 hover:font-bold rounded-sm w-full"
                        onClick={() => dispatch(selectConversation(null))}>New Conversation</button>
                    <ToggleSidebarButton />
                </div>
                
                <SearchBox />
                
                <div className="hide-scrollbar overflow-y-scroll">
                    {children}
                </div>
                
                <SidebarBottomBox />
            </div>
            
            <div className={`fixed z-30 ${toggleButtonClasses()}`}>
                <ToggleSidebarButton />
            </div>
            
            <div className={`absolute z-20 h-full w-full bg-black backdrop-blur-3xl ${backdropClasses()}`}></div>
        </aside>
    );
}

export default Sidebar;