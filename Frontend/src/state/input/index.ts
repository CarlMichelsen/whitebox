import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {countOccurrences} from "../../util/helpers/textRows.ts";

// Define a type for the slice state
type InputState = {
    text: string;
    rows: number;
    previousMessage: string|null;
    editingMessage: string|null;
    inputState: "ready"|"sending"|"receiving";
}

const initialState: InputState = {
    text: "",
    rows: 1,
    previousMessage: null,
    editingMessage: null,
    inputState: "ready"
}

export const authSlice = createSlice({
    name: 'input',
    initialState,
    reducers: {
        setInputState: (state, action: PayloadAction<InputState["inputState"]>) => {
            state.inputState = action.payload;
            
            if (action.payload === "receiving") {
                state.previousMessage = action.payload;
                state.editingMessage = null;
                state.text = "";
                state.rows = 1;
            }
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

export const { setText, setEditingMessage, setPreviousMessage, setInputState } = authSlice.actions

export default authSlice.reducer