import {FC, useEffect, useState} from "react";
import {useAppSelector} from "../../../hooks.ts";
import SelectedConversation from "./SelectedConversation.tsx";

const ChatContainer: FC = () => {
    const input = useAppSelector(state => state.input);
    const [lastRows, setLastRows] = useState<number>(input.rows);

    const toPx = (rows: number) => {
        return rows * 24;
    }
    
    useEffect(() => {
        const diff = input.rows-lastRows
        if (diff>0) {
            setTimeout(() => window.scrollBy(0, toPx(diff)), 0);
        }
        
        setLastRows(input.rows)
    }, [input.rows])

    return (
        <div
            className="pt-12"
            style={{marginBottom: `${toPx(input.rows) + 24*3}px`}}>

            <SelectedConversation />
        </div>
    );
}

export default ChatContainer;