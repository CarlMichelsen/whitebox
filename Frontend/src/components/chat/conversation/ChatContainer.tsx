import {FC, useEffect, useState} from "react";
import {useAppSelector} from "../../../hooks.ts";

const ChatContainer: FC = () => {
    const input = useAppSelector(state => state.input);
    const [lastRows, setLastRows] = useState<number>(input.rows);

    const toPx = (rows: number) => {
        return rows * 24;
    }
    
    useEffect(() => {
        const diff = input.rows-lastRows
        if (diff>0) {
            setTimeout(() => window.scrollBy(0, toPx(diff)), 0);
        }
        
        setLastRows(input.rows)
    }, [input.rows])

    return (
        <div
            className="pt-12"
            
            style={{marginBottom: `${toPx(input.rows) + 24*3}px`}}>
            <h1>ChatContainer</h1>

            <br/>

            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed sem commodo, sagittis diam eget,
                porttitor enim. Nam purus felis, molestie vitae scelerisque ut, sagittis interdum dui. Aliquam
                condimentum vulputate neque, id vehicula lectus placerat non. Orci varius natoque penatibus et
                magnis dis parturient montes, nascetur ridiculus mus. Pellentesque volutpat ante interdum lacus
                egestas, eu luctus justo fringilla. Proin libero lacus, tincidunt in quam et, venenatis venenatis
                mi. Aenean scelerisque sem a fringilla malesuada. Integer a nunc sit amet nisi venenatis pharetra
                vel a est. Nam orci magna, molestie et convallis non, semper sed nunc. Quisque sit amet dignissim
                odio. Pellentesque sit amet porttitor velit, at tempus nisl. Quisque risus ante, bibendum eu diam
                in, interdum efficitur lorem. Sed blandit turpis a ipsum sodales, ac rhoncus sem luctus. Quisque ut
                magna non lectus tempus convallis nec vel felis. Cras urna nisl, feugiat sed faucibus gravida,
                tempor at lacus.
            </p>

            <br/>

            <p>
                Fusce quis semper ex. Vestibulum vitae ullamcorper magna, id tincidunt mi. Aenean efficitur eros id
                sagittis aliquam. Fusce mollis odio eu diam ultricies eleifend. Duis nec massa egestas eros faucibus
                viverra. Phasellus at venenatis sem, et sodales massa. Donec eu efficitur leo. Cras sit amet
                pulvinar diam, eu consequat mauris. Ut sit amet sagittis enim. Vestibulum viverra leo in varius
                lobortis. Morbi at consequat odio, id congue odio. Sed imperdiet metus a ex molestie, ac euismod
                diam condimentum. Fusce nibh tortor, imperdiet in pellentesque maximus, facilisis sit amet nulla.
            </p>

            <br/>

            <p>
                Vestibulum sed leo et magna sagittis fringilla. Mauris aliquet convallis fermentum. Nullam nulla
                est, congue sit amet commodo et, sollicitudin et dolor. Sed purus dolor, consequat id sagittis eu,
                scelerisque sit amet tellus. Vivamus quis nisl vitae nisi tempor elementum. Donec quis nibh semper,
                sollicitudin ipsum in, malesuada nisi. Quisque interdum lacinia mauris nec ultricies. Nulla accumsan
                in ante sed molestie. Proin eget libero at ipsum porttitor faucibus. Vestibulum faucibus aliquet
                dictum. Aliquam dictum nisl id dolor placerat mollis. Quisque risus mauris, sagittis eu auctor nec,
                laoreet sit amet nisl. Ut sollicitudin in mi iaculis convallis. Phasellus sed dui massa. In auctor,
                felis id fringilla fermentum, tortor odio porttitor metus, at dapibus enim massa at nisl. Orci
                varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.
            </p>

            <br/>

            <p>
                Aenean varius sollicitudin pharetra. Nulla facilisi. Vestibulum et mauris eleifend, porttitor mauris
                lacinia, laoreet lorem. Duis varius ac risus eu finibus. Quisque neque enim, faucibus sit amet
                lectus et, euismod dictum velit. Cras ullamcorper id augue non commodo. Aliquam nec nisi
                sollicitudin, sodales augue bibendum, facilisis tellus. Aenean gravida aliquam mauris. Integer
                sollicitudin tempor elit, ut blandit ipsum posuere quis. Sed posuere est sit amet urna congue, et
                sagittis ligula venenatis. Praesent nec enim ac ante pharetra bibendum ut sed ex. Duis sollicitudin
                lectus sit amet velit sodales pulvinar. Sed feugiat pellentesque tellus nec pulvinar. Curabitur
                tincidunt consectetur lacus, non vulputate diam elementum vitae. Maecenas id felis feugiat,
                malesuada mauris in, feugiat urna.
            </p>

            <br/>

            <p>
                Integer sed tincidunt lectus, a ultricies felis. Maecenas nec mauris vitae felis commodo ultricies.
                Sed euismod, dolor ut aliquam vehicula, mi nisi egestas justo, id iaculis elit tellus vel eros.
                Vivamus dignissim lacus vitae urna vulputate lobortis semper quis dolor. Phasellus posuere facilisis
                ligula non posuere. Nullam ut mollis ipsum. Donec porttitor dolor ut faucibus pellentesque. In
                pretium sem nec sollicitudin convallis. Sed iaculis malesuada libero mollis convallis. Nullam
                placerat justo nisl, et lobortis purus placerat tempus. Duis accumsan ipsum nisl, id maximus erat
                lacinia id.
            </p>

            <br/>

            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed sem commodo, sagittis diam eget,
                porttitor enim. Nam purus felis, molestie vitae scelerisque ut, sagittis interdum dui. Aliquam
                condimentum vulputate neque, id vehicula lectus placerat non. Orci varius natoque penatibus et
                magnis dis parturient montes, nascetur ridiculus mus. Pellentesque volutpat ante interdum lacus
                egestas, eu luctus justo fringilla. Proin libero lacus, tincidunt in quam et, venenatis venenatis
                mi. Aenean scelerisque sem a fringilla malesuada. Integer a nunc sit amet nisi venenatis pharetra
                vel a est. Nam orci magna, molestie et convallis non, semper sed nunc. Quisque sit amet dignissim
                odio. Pellentesque sit amet porttitor velit, at tempus nisl. Quisque risus ante, bibendum eu diam
                in, interdum efficitur lorem. Sed blandit turpis a ipsum sodales, ac rhoncus sem luctus. Quisque ut
                magna non lectus tempus convallis nec vel felis. Cras urna nisl, feugiat sed faucibus gravida,
                tempor at lacus.
            </p>

            <br/>

            <p>
                Fusce quis semper ex. Vestibulum vitae ullamcorper magna, id tincidunt mi. Aenean efficitur eros id
                sagittis aliquam. Fusce mollis odio eu diam ultricies eleifend. Duis nec massa egestas eros faucibus
                viverra. Phasellus at venenatis sem, et sodales massa. Donec eu efficitur leo. Cras sit amet
                pulvinar diam, eu consequat mauris. Ut sit amet sagittis enim. Vestibulum viverra leo in varius
                lobortis. Morbi at consequat odio, id congue odio. Sed imperdiet metus a ex molestie, ac euismod
                diam condimentum. Fusce nibh tortor, imperdiet in pellentesque maximus, facilisis sit amet nulla.
            </p>

            <br/>

            <p>
                Vestibulum sed leo et magna sagittis fringilla. Mauris aliquet convallis fermentum. Nullam nulla
                est, congue sit amet commodo et, sollicitudin et dolor. Sed purus dolor, consequat id sagittis eu,
                scelerisque sit amet tellus. Vivamus quis nisl vitae nisi tempor elementum. Donec quis nibh semper,
                sollicitudin ipsum in, malesuada nisi. Quisque interdum lacinia mauris nec ultricies. Nulla accumsan
                in ante sed molestie. Proin eget libero at ipsum porttitor faucibus. Vestibulum faucibus aliquet
                dictum. Aliquam dictum nisl id dolor placerat mollis. Quisque risus mauris, sagittis eu auctor nec,
                laoreet sit amet nisl. Ut sollicitudin in mi iaculis convallis. Phasellus sed dui massa. In auctor,
                felis id fringilla fermentum, tortor odio porttitor metus, at dapibus enim massa at nisl. Orci
                varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.
            </p>

            <br/>

            <p>
                Aenean varius sollicitudin pharetra. Nulla facilisi. Vestibulum et mauris eleifend, porttitor mauris
                lacinia, laoreet lorem. Duis varius ac risus eu finibus. Quisque neque enim, faucibus sit amet
                lectus et, euismod dictum velit. Cras ullamcorper id augue non commodo. Aliquam nec nisi
                sollicitudin, sodales augue bibendum, facilisis tellus. Aenean gravida aliquam mauris. Integer
                sollicitudin tempor elit, ut blandit ipsum posuere quis. Sed posuere est sit amet urna congue, et
                sagittis ligula venenatis. Praesent nec enim ac ante pharetra bibendum ut sed ex. Duis sollicitudin
                lectus sit amet velit sodales pulvinar. Sed feugiat pellentesque tellus nec pulvinar. Curabitur
                tincidunt consectetur lacus, non vulputate diam elementum vitae. Maecenas id felis feugiat,
                malesuada mauris in, feugiat urna.
            </p>

            <br/>

            <p>
                Integer sed tincidunt lectus, a ultricies felis. Maecenas nec mauris vitae felis commodo ultricies.
                Sed euismod, dolor ut aliquam vehicula, mi nisi egestas justo, id iaculis elit tellus vel eros.
                Vivamus dignissim lacus vitae urna vulputate lobortis semper quis dolor. Phasellus posuere facilisis
                ligula non posuere. Nullam ut mollis ipsum. Donec porttitor dolor ut faucibus pellentesque. In
                pretium sem nec sollicitudin convallis. Sed iaculis malesuada libero mollis convallis. Nullam
                placerat justo nisl, et lobortis purus placerat tempus. Duis accumsan ipsum nisl, id maximus erat
                lacinia id.
            </p>
        </div>
    );
}

export default ChatContainer;