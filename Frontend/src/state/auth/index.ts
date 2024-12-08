import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import {AuthenticatedUser} from "../../model/user.ts";

// Define a type for the slice state
type AuthState = {
    status: "pending"|"loggedIn"|"loggedOut";
    user: AuthenticatedUser|null;
}

const initialState: AuthState = {
    status: "pending",
    user: null,
}

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        login: (state, action: PayloadAction<AuthenticatedUser>) => {
            state.status = "loggedIn";
            state.user = action.payload;
        },
        logout: (state) => {
            state.status = "loggedOut";
            state.user = null;
        },
    },
});

export const { login, logout } = authSlice.actions

export default authSlice.reducer;