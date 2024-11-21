import {ChangeEventHandler, KeyboardEvent, FC} from "react";

type EditableTextBoxProps = {
    className?: string;
    rows: number;
    name: string;
    label: string;
    id: string;
    text: string;
    disabled: boolean;
    onChange: (text: string) => void;
    onEnter?: (text: string) => void;
}

const LineHeightEditableTextBox: FC<EditableTextBoxProps> = ({ className, rows, name, label, id, text, disabled, onChange, onEnter }) => {
    
    const handleOnChange: ChangeEventHandler<HTMLTextAreaElement> = (event) => {
        let text = event.target.value;
        onChange(text)
    }

    const handleSendOnEnter = (keyEvent: KeyboardEvent<HTMLTextAreaElement>) => {
        if (!keyEvent.shiftKey && keyEvent.key === "Enter") {
            keyEvent.preventDefault();
            let text = (keyEvent.target as HTMLTextAreaElement).value;
            onEnter && onEnter(text)
        }
    }

    return (
        <>
            <label htmlFor={id} className="sr-only">{label}</label>
            <textarea
                className={`hide-scrollbar resize-none block w-full p-1 ${className ?? ""}`}
                disabled={disabled}
                name={name}
                id={id}
                value={text}
                rows={rows}
                onChange={handleOnChange}
                onKeyDown={handleSendOnEnter}>
            </textarea>
        </>
    );
}

export default LineHeightEditableTextBox;