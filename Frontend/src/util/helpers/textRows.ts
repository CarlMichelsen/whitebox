const replaceCharacterAtIndex = (originalString: string, index: number, replacement: string): string => {
    if (index < 0 || index >= originalString.length || replacement.length !== 1) {
        throw new Error("Invalid index or replacement string. Ensure the index is within bounds and replacement is a single character.");
    }

    return originalString.substring(0, index) + replacement + originalString.substring(index + 1);
}

console.warn(replaceCharacterAtIndex("testing things is f", 8, "\n"))


export const insertLinebreaks = (text: string, maxLineLength: number): string => {
    let count = 0
    
    let adjustedText = text;
    let iterator = 0;
    
    while (true) {
        const textWindowEndIndex = iterator + maxLineLength;
        const nextLinebreak = adjustedText.indexOf("\n", iterator) + 1;
        const textWindow = adjustedText.substring(iterator, textWindowEndIndex);
        
        console.log(count, textWindow)
        if (adjustedText.length-iterator < maxLineLength) {
            break;
        }
        
        const lastSpaceLocalIndex = textWindow.lastIndexOf(' ');
        
        if (nextLinebreak !== -1) {
            iterator = nextLinebreak;
        } else if (lastSpaceLocalIndex !== -1 && textWindow.length !== maxLineLength) {
            adjustedText = replaceCharacterAtIndex(adjustedText, lastSpaceLocalIndex+iterator, "\n");
            iterator = lastSpaceLocalIndex + iterator + 2;
        } else {
            iterator = textWindowEndIndex + 1
        }
        
        count++
        if (count > 500) {
            console.error("too many loops...")
            break;
        }
    }
    
    return adjustedText.replace(/^\s+/gm, '');
};

export const countOccurrences = (str: string, char: string): number => {
    const regex = new RegExp(char, 'g');
    const matches = str.match(regex);
    return matches ? matches.length : 0;
}