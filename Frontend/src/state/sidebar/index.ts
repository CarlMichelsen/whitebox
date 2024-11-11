import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {ConversationOption, ConversationOptionStructure} from "../../model/sidebar/conversationOption.ts";
import {
    addConversationOption,
    mapConversationOptionsToConversationStructure, removeConversationOption, editAndReAddOption
} from "../../util/helpers/conversationStructureMapper.ts";

// Define a type for the slice state
type SidebarState = {
    isOpen: boolean;
    conversationStructure: ConversationOptionStructure|null;
    search: string|null;
}

const initialState: SidebarState = {
    isOpen: window.screen.width > 600,
    conversationStructure: null,
    search: null,
}

export const sidebarSlice = createSlice({
    name: 'sidebar',
    initialState,
    reducers: {
        setOpen: (state, action: PayloadAction<boolean>) => {
            state.isOpen = action.payload;
        },
        setSearch: (state, action: PayloadAction<string>) => {
            if (!action.payload) {
                state.search = null;
                return;
            }
            
            state.search = action.payload;
        },
        setConversations: (state, action: PayloadAction<ConversationOption[]>) => {
            state.conversationStructure = mapConversationOptionsToConversationStructure(action.payload);
        },
        addConversation: (state, action: PayloadAction<ConversationOption>) => {
            if (state.conversationStructure === null) {
                return;
            }
            
            state.conversationStructure = addConversationOption(state.conversationStructure, action.payload);
        },
        removeConversation: (state, action: PayloadAction<string>) => {
            if (state.conversationStructure === null) {
                return;
            }

            removeConversationOption(state.conversationStructure, action.payload);
        },
        notifyAltered: (state, action: PayloadAction<string>) => {
            if (state.conversationStructure === null) {
                return;
            }

            editAndReAddOption(
                state.conversationStructure,
                action.payload,
                (option) => {
                    option.lastAltered = new Date().getTime();
            });
        },
        updateTitle: (state, action: PayloadAction<{id: string, newTitle: string}>) => {
            if (state.conversationStructure === null) {
                return;
            }

            editAndReAddOption(
                state.conversationStructure,
                action.payload.id,
                (option) => {
                    option.title = action.payload.newTitle;
                    option.lastAltered = new Date().getTime();
                });
        },
    },
})

export const {
    setOpen,
    setSearch,
    setConversations,
    addConversation,
    removeConversation,
    notifyAltered,
    updateTitle
} = sidebarSlice.actions

export default sidebarSlice.reducer