import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {countOccurrences} from "../../util/helpers/textRows.ts";

// Define a type for the slice state
type InputState = {
    text: string;
    rows: number;
}

const initialState: InputState = {
    text: "",
    rows: 1,
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
    },
})

export const { setText } = authSlice.actions

export default authSlice.reducer