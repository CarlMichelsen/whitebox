import {Marked} from "marked";

export const whiteBoxMarked = new Marked({
    renderer: {
        code(data) {
            return `<pre class="dark:bg-neutral-950 bg-neutral-200 shadow-inner rounded-sm w-80 sm:w-[350px] lg:w-[700px] xl:w-[950px] p-1 overflow-x-scroll my-2"><code class="response-code language-${data.lang}">${data.text}</code></pre>`;
        }
    }
});