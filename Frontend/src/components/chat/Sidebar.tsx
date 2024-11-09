import {FC, ReactNode} from "react";
import {useAppSelector} from "../../hooks.ts";
import ToggleSidebarButton from "./ToggleSidebarButton.tsx";
import SidebarBottomBox from "./SidebarBottomBox.tsx";

type SidebarProps = {
    children: ReactNode;
}

const Sidebar: FC<SidebarProps> = ({ children }) => {
    const sidebar = useAppSelector(store => store.sidebar)
    
    const sidebarClasses = (): string => {
        if (sidebar.isOpen) {
            return "block"
        }
        
        return "hidden w-0";
    }

    const backdropClasses = (): string => {
        if (sidebar.isOpen) {
            return "block sm:hidden"
        }

        return "hidden";
    }

    const toggleButtonClasses = (): string => {
        if (sidebar.isOpen) {
            return "hidden sm:block"
        }

        return "block";
    }
    
    return (
        <>
            <div className={`fixed z-30 h-screen w-52 bg-neutral-900 ${sidebarClasses()} grid grid-rows-[auto_1fr_auto]`}>
                <div className="grid grid-cols-[1fr_auto] gap-1">
                    <button className="m-1 bg-blue-800 hover:bg-blue-900 hover:font-bold rounded-sm w-full">New Conversation</button>
                    <ToggleSidebarButton />
                </div>
                
                {children}
                
                <SidebarBottomBox />
            </div>
            
            <div className={`fixed ${toggleButtonClasses()}`}>
                <ToggleSidebarButton />
            </div>
            
            <div className={`absolute z-20 h-full w-full bg-black opacity-80 backdrop-blur-3xl ${backdropClasses()}`}></div>
        </>
    );
}

export default Sidebar;