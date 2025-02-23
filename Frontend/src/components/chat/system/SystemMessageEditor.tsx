import {FC, useEffect, useState} from "react";
import {delay} from "../../../util/delay.ts";

type SystemMessageEditorProps = {
    initialMessage?: string;
    saveChanges: (message: string) => Promise<void>;
}

const SystemMessageEditor: FC<SystemMessageEditorProps> = ({ initialMessage, saveChanges }) => {
    const [status, setStatus] = useState<string|null>(null);
    const [timeoutId, setTimeoutId] = useState<number|null>(null);
    const [message, setMessage] = useState<string|null>(initialMessage ?? null);
    
    const onChange = (content: string) => {
        setMessage(content);
        if (timeoutId !== null) {
            clearTimeout(timeoutId);
        }
        
        setTimeoutId(setTimeout(async () => {
            setTimeoutId(null);
            setStatus("saving...");
            await saveChanges(content);
            setStatus("saved");
            await delay(700);
            setStatus(null);
        }, 800));
    }

    useEffect(() => {
        return () => {
            if (timeoutId !== null) {
                clearTimeout(timeoutId);
                if (message !== null) {
                    saveChanges(message).catch(console.error);
                }
            }
        }
    }, []);
    
    return (
        <div className="my-1">
            <textarea
                className="resize-none w-full h-48 focus:outline-none p-1 rounded-sm transition-colors ease-in-out bg-neutral-100 focus:bg-white dark:bg-black dark:focus:bg-black"
                value={message ?? ""}
                onChange={event => onChange(event.target.value)}
            ></textarea>
            <p className="text-xs h-4 italic">{status}</p>
        </div>
    );
}

export default SystemMessageEditor;