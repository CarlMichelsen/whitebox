import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {countOccurrences} from "../../util/helpers/textRows.ts";

// Define a type for the slice state
type InputState = {
    text: string;
    rows: number;
    speechToText: "connected"|"disconnected"|"pending";
}

const initialState: InputState = {
    text: "",
    rows: 1,
    speechToText: "disconnected",
}

export const authSlice = createSlice({
    name: 'input',
    initialState,
    reducers: {
        setText: (state, action: PayloadAction<string>) => {
            state.text = action.payload;

            // TODO: Handle word-wrapping
            state.rows = Math.min(countOccurrences(state.text, '\n'), 10) + 1;
        },
        setVoice: (state, action: PayloadAction<InputState["speechToText"]>) => {
            if (state.speechToText !== action.payload) {
                return;
            }
            state.speechToText = action.payload;
        }
    },
})

export const { setText, setVoice } = authSlice.actions

export default authSlice.reducer