import {FC} from "react";
import {setEditingMessage} from "../../../state/input";
import {useAppDispatch, useAppSelector} from "../../../hooks";

const EditIndicator: FC = () => {
    const input = useAppSelector(state => state.input);
    const dispatch = useAppDispatch()
    
    return input.editingMessage !== null ? (
        <div className="absolute -top-8 right-2 h-8 w-44">
            <button
                id="cancel-edit-button"
                onClick={() => dispatch(setEditingMessage(null))}
                className="h-full w-full rounded-t-sm dark:bg-neutral-700 bg-neutral-100 border-t border-x border-color hover:underline dark:hover:bg-neutral-800 hover:bg-neutral-100">
                Cancel Edit
            </button>
        </div>
    ) : null;
}

export default EditIndicator;