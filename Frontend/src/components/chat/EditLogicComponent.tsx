import {FC, useEffect, useRef} from "react";
import {useAppDispatch, useAppSelector} from "../../hooks";
import {Position} from "../../model/position.ts";
import {setPreviousMessage, setText} from "../../state/input";
import {findMessage} from "../../util/conversationUtil.ts";

const EditLogicComponent: FC = () => {
    const dispatch = useAppDispatch()
    const input = useAppSelector(state => state.input);
    const conversation = useAppSelector(state => state.conversation);
    const cancelButtonRef = useRef<HTMLButtonElement|null>(null);
    const messageRef = useRef<HTMLDivElement|null>(null);
    const lineRef = useRef<SVGLineElement>(null!);

    const updateLine = () => {
        if (cancelButtonRef.current === null || messageRef.current === null) {
            return;
        }
        
        const cancelRect = cancelButtonRef.current.getBoundingClientRect();
        const messageRect = messageRef.current.getBoundingClientRect();
        
        const above = cancelRect.y > messageRect.y + messageRect.height/2;

        const cancelPos: Position = {
            x: cancelRect.x + cancelRect.width / 2,
            y: above ? cancelRect.y : cancelRect.y + cancelRect.height
        };
        
        const messagePos: Position = {
            x: messageRect.x + messageRect.width / 2,
            y: above ? messageRect.y + messageRect.height : messageRect.y
        };

        lineRef.current.setAttribute('x1', cancelPos.x+"px");
        lineRef.current.setAttribute('y1', cancelPos.y+"px");
        lineRef.current.setAttribute('x2', messagePos.x+"px");
        lineRef.current.setAttribute('y2', messagePos.y+"px");
    }

    useEffect(() => {
        if (input.editingMessage !== null && conversation.selectedConversation !== null)
        {
            const editMessage = findMessage(conversation.selectedConversation, input.editingMessage)
            if (editMessage === null) {
                throw new Error("Edit message not found in selected conversation")
            }
            dispatch(setPreviousMessage(input.text))
            dispatch(setText(editMessage.content.map(c => c.value).join('\n')))
            
            cancelButtonRef.current = document.getElementById("cancel-edit-button") as HTMLButtonElement;
            messageRef.current = document.getElementById("message-"+input.editingMessage) as HTMLDivElement;
            updateLine();
        } else {
            cancelButtonRef.current = null;
            messageRef.current = null;
            dispatch(setText(input.previousMessage ?? ""))
        }
    }, [input.editingMessage]);

    useEffect(() => {
        updateLine();
        window.addEventListener('resize', updateLine);
        window.addEventListener('scroll', updateLine);

        return () => {
            window.removeEventListener('resize', updateLine);
            window.removeEventListener('scroll', updateLine);
        }
    }, []);
    
    return (
        <svg
            className={input.editingMessage === null ? "hidden" : "block"}
            style={{
                position: 'absolute',
                top: 0,
                left: 0,
                width: '100%',
                height: '100%',
                pointerEvents: 'none', // Ensures interaction with underlying elements is not blocked
            }}
        >
            <line ref={lineRef} className="stroke-black dark:stroke-white" strokeWidth="2"/>
        </svg>
    );
}

export default EditLogicComponent;