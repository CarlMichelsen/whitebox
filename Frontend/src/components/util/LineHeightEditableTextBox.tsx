import {ChangeEventHandler, FC} from "react";

type EditableTextBoxProps = {
    className?: string;
    rows: number;
    name: string;
    id: string;
    text: string;
    onChange: (text: string) => void;
}

const LineHeightEditableTextBox: FC<EditableTextBoxProps> = ({ className, rows, name, id, text, onChange }) => {
    
    const handleOnChange: ChangeEventHandler<HTMLTextAreaElement> = (event) => {
        let text = event.target.value;
        onChange(text)
    }

    return (
        <textarea
            className={`hide-scrollbar resize-none block w-full p-1 ${className ?? ""}`}
            name={name}
            id={id}
            value={text}
            rows={rows}
            onChange={handleOnChange}>
        </textarea>
    );
}

export default LineHeightEditableTextBox;