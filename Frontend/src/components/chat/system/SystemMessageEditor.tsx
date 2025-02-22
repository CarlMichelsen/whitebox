import {FC, useState} from "react";

type SystemMessageEditorProps = {
    initialMessage?: string;
}

const SystemMessageEditor: FC<SystemMessageEditorProps> = ({ initialMessage }) => {
    const [message, setMessage] = useState<string|null>(initialMessage ?? null);
    
    return <textarea
        className="resize-none w-full focus:outline-none p-1"
        value={message ?? ""}
        onChange={event => setMessage(event.target.value)}
    ></textarea>
}

export default SystemMessageEditor;