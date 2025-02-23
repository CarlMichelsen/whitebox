import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {getTitle} from "./modalTitleMap.ts";

export type ModalType = "model-selector"|"conversation-system-message";

type ModalState = {
    type: ModalType|null;
    title: string|null;
}

const initialState: ModalState = {
    type: null,
    title: null,
}

const modalSlice = createSlice({
    name: 'modal',
    initialState,
    reducers: {
        openModal: (state, action: PayloadAction<ModalType>) => {
            state.type = action.payload;
            const title = getTitle(action.payload);
            if (title) {
                state.title = title;
            } else {
                throw new Error("Missing title for modal");
            }
        },
        closeActiveModal: (state) => {
            state.type = null;
            state.title = null;
        }
    },
})

export const {
    openModal,
    closeActiveModal
} = modalSlice.actions

export default modalSlice.reducer