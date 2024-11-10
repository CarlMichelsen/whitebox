import { createSlice, PayloadAction } from '@reduxjs/toolkit'

// Define a type for the slice state
type SidebarState = {
    isOpen: boolean;
}

const initialState: SidebarState = {
    isOpen: window.screen.width > 600,
}

export const sidebarSlice = createSlice({
    name: 'sidebar',
    initialState,
    reducers: {
        setOpen: (state, action: PayloadAction<boolean>) => {
            state.isOpen = action.payload;
        },
    },
})

export const { setOpen } = sidebarSlice.actions

export default sidebarSlice.reducer