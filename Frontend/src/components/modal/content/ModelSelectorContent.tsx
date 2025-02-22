import {FC} from "react";
import {ModalContentProps} from "./ModalContentProps.ts";
import ConnectedModelProviderSelector from "../../chat/config/ConnectedModelProviderSelector.tsx";

const ModelSelectorContent: FC<ModalContentProps> = ( { closeModal }) => {
    return <ConnectedModelProviderSelector onNewSelection={closeModal} />;
}

export default ModelSelectorContent;