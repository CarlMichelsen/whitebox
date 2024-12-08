import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {countOccurrences} from "../../util/helpers/textRows.ts";
import {ChatConfiguration} from "../../model/chatConfiguration/chatConfiguration.ts";
import {LlmModel} from "../../model/conversation/llmModel.ts";

export type InputStatus = "ready"|"sending"|"receiving";

// Define a type for the slice state
type InputState = {
    text: string;
    rows: number;
    previousMessage: string|null;
    editingMessage: string|null;
    chatConfiguration: ChatConfiguration|null;
    inputState: InputStatus;
}

const initialState: InputState = {
    text: "",
    rows: 1,
    previousMessage: null,
    editingMessage: null,
    chatConfiguration: null,
    inputState: "ready"
}

export const inputSlice = createSlice({
    name: 'input',
    initialState,
    reducers: {
        setInputState: (state, action: PayloadAction<InputStatus>) => {
            if (state.inputState === action.payload) {
                return;
            }
            
            state.inputState = action.payload;
            if (action.payload === "receiving") {
                state.previousMessage = action.payload;
                state.editingMessage = null;
                state.text = "";
                state.rows = 1;
            }
        },
        setChatConfiguration: (state, action: PayloadAction<ChatConfiguration>) => {
            state.chatConfiguration = action.payload;
        },
        selectModel: (state, action: PayloadAction<LlmModel>) => {
            if (!state.chatConfiguration) {
                return;
            }
            
            state.chatConfiguration.selectedModel = action.payload;
        },
        setEditingMessage: (state, action: PayloadAction<string|null>) => {
            state.editingMessage = action.payload;
        },
        setPreviousMessage: (state, action: PayloadAction<string|null>) => {
            state.previousMessage = action.payload;
        },
        setText: (state, action: PayloadAction<string>) => {
            if (state.inputState === "sending") {
                return;
            }
            
            state.text = action.payload;

            // TODO: Handle word-wrapping
            state.rows = Math.min(countOccurrences(state.text, '\n'), 10) + 1;
        }
    },
})

export const {
    setText,
    setChatConfiguration,
    selectModel,
    setEditingMessage,
    setPreviousMessage,
    setInputState
} = inputSlice.actions

export default inputSlice.reducer;