import {ConversationOption, ConversationOptionSection, NamedTimeSpan} from "../../model/sidebar/conversationOption.ts";

const getTimeSpans = (): NamedTimeSpan[] => {
    const now = new Date();
    
    const startOfToday = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    const endOfToday = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 23, 59, 59, 999);

    const startOfYesterday = new Date(now.getFullYear(), now.getMonth(), now.getDate() - 1);
    const endOfYesterday = new Date(now.getFullYear(), now.getMonth(), now.getDate() - 1, 23, 59, 59, 999);

    const startOfThisWeek = endOfYesterday;
    startOfThisWeek.setDate(startOfThisWeek.getDate() - startOfThisWeek.getDay());

    const endOfThisWeek = new Date(startOfThisWeek);
    endOfThisWeek.setDate(endOfThisWeek.getDate() + 6);
    endOfThisWeek.setHours(23, 59, 59, 999);

    const startOfThisMonth = new Date(now.getFullYear(), now.getMonth(), 1);
    const endOfThisMonth = new Date(now.getFullYear(), now.getMonth() + 1, 0, 23, 59, 59, 999);

    const startOfLastMonth = new Date(now.getFullYear(), now.getMonth() - 1, 1);
    const endOfLastMonth = new Date(now.getFullYear(), now.getMonth(), 0, 23, 59, 59, 999);

    const startOfThisYear = new Date(now.getFullYear(), 0, 1);
    const endOfThisYear = new Date(now.getFullYear(), 11, 31, 23, 59, 59, 999);
    
    return [
        {
            title: "Today",
            start: startOfToday.getTime(),
            end: endOfToday.getTime(),
        },
        {
            title: "Yesterday",
            start: startOfYesterday.getTime(),
            end: endOfYesterday.getTime(),
        },
        {
            title: "This Week",
            start: startOfThisWeek.getTime(),
            end: endOfThisWeek.getTime(),
        },
        {
            title: "This Month",
            start: startOfThisMonth.getTime(),
            end: endOfThisMonth.getTime(),
        },
        {
            title: "Last Month",
            start: startOfLastMonth.getTime(),
            end: endOfLastMonth.getTime(),
        },
        {
            title: "This Year",
            start: startOfThisYear.getTime(),
            end: endOfThisYear.getTime(),
        },
        {
            title: "Older",
            start: new Date(0).getTime(),
            end: startOfThisYear.getTime(),
        },
    ] satisfies NamedTimeSpan[];
}

const inTimeSpan = (epoch: number, span: NamedTimeSpan): boolean => {
    return epoch >= span.start && epoch <= span.end;
}

export const optionsToSections = (options: ConversationOption[]): ConversationOptionSection[] => {
    let consumableOptionsList = [...options];
    const optionSections: ConversationOptionSection[] = getTimeSpans()
        .map(s => ({...s, options: []}) satisfies ConversationOptionSection);
    optionSections.sort((a, b) => b.start - a.start);
    
    for (let i = 0; i < optionSections.length; i++) {
        const section = optionSections[i];
        const optionsToConsume: string[] = []
        
        for (const option of consumableOptionsList) {
            const isInSection = inTimeSpan(option.lastAltered, section);
            if (isInSection) {
                optionsToConsume.push(option.id);
                section.options.push(option);
            }
        }

        consumableOptionsList = consumableOptionsList
            .filter(o => optionsToConsume.indexOf(o.id) === -1);
    }
    
    return optionSections;
}

export const removeOption = (sections: ConversationOptionSection[], optionId: string)=> {
    for (const section of sections) {
        const idx = section.options.findIndex(o => o.id === optionId);
        const option = section.options[idx];
        if (idx !== -1) {
            section.options = section.options.filter(o => o.id !== optionId);
            return option;
        }
    }
    
    return null;
}

export const distinctAdd = (sections: ConversationOptionSection[], option: ConversationOption)=> {
    removeOption(sections, option.id);
    
    for (let i = 0; i < sections.length; i++) {
        const section = sections[i];

        const isInSection = inTimeSpan(option.lastAltered, section);
        if (isInSection) {
            section.options.unshift(option);
            section.options.sort((a, b) => b.lastAltered - a.lastAltered);
            break;
        }
    }
}

export const setAlteredNow = (sections: ConversationOptionSection[], optionId: string)=> {
    const option = removeOption(sections, optionId);
    
    if (option) {
        option.lastAltered = new Date().getTime();
        distinctAdd(sections, option);
    }
}