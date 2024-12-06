import {ModalType} from "./index.ts";

const modalTitleMap = new Map<ModalType, string>();
modalTitleMap.set("model-selector", "Select large language model");

export const getTitle = (type: ModalType) => {
    return modalTitleMap.get(type) ?? null;
} 