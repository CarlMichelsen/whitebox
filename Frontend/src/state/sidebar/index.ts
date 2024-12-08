import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {
    ConversationOption,
    ConversationOptionSection,
} from "../../model/sidebar/conversationOption.ts";
import {SetSummaryEvent} from "../../model/conversation/dto/conversationStream.ts";
import {distinctAdd, optionsToSections, removeOption, setAlteredNow} from "./optionsToSections.ts";

// Define a type for the slice state
type SidebarState = {
    isOpen: boolean;
    conversationSections: ConversationOptionSection[]|null;
    search: string|null;
}

const initialState: SidebarState = {
    isOpen: window.screen.width > 600,
    conversationSections: null,
    search: null,
}

const sidebarSlice = createSlice({
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
            state.conversationSections = optionsToSections(action.payload);
        },
        removeConversation: (state, action: PayloadAction<string>) => {
            if (state.conversationSections === null) {
                return;
            }
            
            removeOption(state.conversationSections, action.payload);
        },
        upsertSummary: (state, action: PayloadAction<SetSummaryEvent>) => {
            if (state.conversationSections === null) {
                return;
            }

            const conversationOption: ConversationOption = {
                id: action.payload.conversationId,
                summary: action.payload.summary,
                lastAltered: new Date().getTime()
            }
            distinctAdd(state.conversationSections, conversationOption);
        },
        alteredNow: (state, action: PayloadAction<string>) => {
            if (state.conversationSections === null) {
                return;
            }

            setAlteredNow(state.conversationSections, action.payload)
        }
    },
})

export const {
    setOpen,
    setSearch,
    setConversations,
    removeConversation,
    upsertSummary,
    alteredNow,
} = sidebarSlice.actions

export default sidebarSlice.reducer

/*
,
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
 */