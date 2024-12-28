import {Marked, Tokens} from "marked";
import {hostUrl} from "../endpoints.ts";

const htmlEntities: {[key: string]: string} = {
    '&': '&amp;',
    '<': '&lt;',
    '>': '&gt;',
    '"': '&quot;',
    "'": '&apos;',
    '¢': '&cent;',
    '£': '&pound;',
    '¥': '&yen;',
    '€': '&euro;',
    '©': '&copy;',
    '®': '&reg;',
    '™': '&trade;',
    '°': '&deg;',
    '±': '&plusmn;',
    '×': '&times;',
    '÷': '&divide;',
    '¼': '&frac14;',
    '½': '&frac12;',
    '¾': '&frac34;',
    'µ': '&micro;',
    '¶': '&para;',
    '·': '&middot;',
    '§': '&sect;',
    '←': '&larr;',
    '→': '&rarr;'
};

const escapeHTML = (str: string) => {
    return str.replace(/[&<>"'¢£¥€©®™°±×÷¼½¾µ¶·§←→]/g, char => htmlEntities[char]);
}

const linkRedirect = (href: string): string => {
    const base64Url = btoa(encodeURIComponent(href));
    return `${hostUrl()}/api/v1/redirect/chatLink/${base64Url}`;
}

const whiteBoxMarked = new Marked({
    renderer: {
        link(link: Tokens.Link) {
            const href = linkRedirect(link.href);
            return `<a class="underline dark:text-neutral-400 dark:visited:text-yellow-800 text-neutral-600 visited:text-yellow-600" href="${href}" data-redirect="true" target="_blank">${link.text}</a>`;
        },
        code(data) {
            const code = escapeHTML(data.text);
            return `<pre class="dark:bg-neutral-950 bg-neutral-200 shadow-inner rounded-sm w-full sm:w-[350px] lg:w-[700px] xl:w-[950px] p-1 overflow-x-scroll my-2"><code class="response-code language-${data.lang}">${code}</code></pre>`;
        }
    }
});

export { whiteBoxMarked };

