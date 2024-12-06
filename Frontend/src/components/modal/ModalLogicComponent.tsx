import {FC} from "react";
import {useAppDispatch, useAppSelector} from "../../hooks";
import Dialog from "./Dialog";
import {closeActiveModal, ModalType} from "../../state/modal";
import ModelSelectorContent from "./content/ModelSelectorContent.tsx";

const ModalLogicComponent: FC = () => {
    const modal = useAppSelector(store => store.modal);
    const dispatch = useAppDispatch();
    
    const closeModal = () => {
        dispatch(closeActiveModal());
    }
    
    const getContent = () => {
        if (!modal.type) {
            return null;
        }
        
        const type: ModalType = modal.type;
        switch (type) {
            case "model-selector": return <ModelSelectorContent onClose={closeModal} />
            default:
                return null;
        }
    }
    
    return <Dialog title={modal.title ?? undefined} isOpen={modal.type !== null} onClose={closeModal}>
        {getContent()}
    </Dialog>;
}

export default ModalLogicComponent;