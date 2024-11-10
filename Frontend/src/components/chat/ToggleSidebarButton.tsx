import {FC} from "react";
import {useAppDispatch, useAppSelector} from "../../hooks.ts";
import {setOpen} from "../../state/sidebar";

const ToggleSidebarButton: FC = () => {
    const sidebar = useAppSelector(store => store.sidebar)
    const dispatch = useAppDispatch()
    
    const onClick = () => {
        dispatch(setOpen(!sidebar.isOpen));
    }
    
    return (
        <button
            className="w-10 h-10 dark:bg-green-900 bg-green-600 rounded-sm text-xl hover:font-bold m-1"
            onClick={onClick}>{sidebar.isOpen ? "<" : ">"}</button>
    )
}

export default ToggleSidebarButton;