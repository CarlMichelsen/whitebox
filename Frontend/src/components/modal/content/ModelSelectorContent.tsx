import {FC} from "react";
import {ModalContentProps} from "./ModalContentProps.ts";

const ModelSelectorContent: FC<ModalContentProps> = ( { onClose }) => {
    return (
        <>
            <p>ModelSelectorContent</p>
            <button onClick={onClose}>Close</button>
        </>
    );
}

export default ModelSelectorContent;