import {FC, ReactNode} from "react";
import { createPortal } from "react-dom";
import CrossWhite from "../../assets/icons/cross-white.svg"

type DialogProps = {
    title?: string;
    isOpen: boolean;
    onClose: () => void;
    children: ReactNode;
}

const Dialog: FC<DialogProps> = ({ title, isOpen, onClose, children }) => {
    if (!isOpen) return null;

    const handleDialogClick = (event: React.MouseEvent<HTMLDialogElement>) => {
        event.stopPropagation();
    };

    return createPortal(
        <div
            className="fixed top-0 left-0 w-full h-full bg-black bg-opacity-70 flex items-center justify-center z-50 backdrop-blur-sm shadow-inner"
            onMouseDown={onClose}
        >
            <dialog
                onMouseDown={handleDialogClick}
                className="grid grid-rows-[1.5rem_1fr] bg-white dark:bg-neutral-800 sm:rounded-sm w-full h-[750px] md:w-[750px]"
                open
            >
                <div className="grid grid-cols-[1fr_1.5rem]">
                    <h1 className="mt-1 ml-2 italic text-lg">{title ?? "no title"}</h1>

                    <div>
                        <button
                            className="bg-red-600 hover:bg-red-800 hover:underline text-white w-6 h-6 sm:rounded-tr-sm rounded-bl-sm"
                            onClick={onClose}
                            onMouseDown={onClose}>
                            <img
                                draggable="false"
                                src={CrossWhite}
                                className="p-0.5"
                                alt="edit"/>
                        </button>
                    </div>
                </div>

                <div className="mx-2 my-4">
                    {children}
                </div>
            </dialog>
        </div>,
        document.body
    );
}

export default Dialog;