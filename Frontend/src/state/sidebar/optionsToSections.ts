import {ConversationOption, ConversationOptionSection, NamedTimeSpan} from "../../model/sidebar/conversationOption.ts";

type TimeSpanDefinition = {
    title: string;
    getTimeSpan: (now: Date) => { start: Date; end: Date };
}

const TIME_SPAN_DEFINITIONS: TimeSpanDefinition[] = [
    {
        title: "Today",
        getTimeSpan: (now) => ({
            start: new Date(now.getFullYear(), now.getMonth(), now.getDate()),
            end: new Date(now.getFullYear(), now.getMonth(), now.getDate(), 23, 59, 59, 999)
        })
    },
    {
        title: "Yesterday",
        getTimeSpan: (now) => ({
            start: new Date(now.getFullYear(), now.getMonth(), now.getDate() - 1),
            end: new Date(now.getFullYear(), now.getMonth(), now.getDate() - 1, 23, 59, 59, 999)
        })
    },
    {
        title: "This Week",
        getTimeSpan: (now) => {
            const start = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            start.setDate(start.getDate() - start.getDay());
            const end = new Date(start);
            end.setDate(end.getDate() + 6);
            end.setHours(23, 59, 59, 999);
            return { start, end };
        }
    },
    {
        title: "This Month",
        getTimeSpan: (now) => ({
            start: new Date(now.getFullYear(), now.getMonth(), 1),
            end: new Date(now.getFullYear(), now.getMonth() + 1, 0, 23, 59, 59, 999)
        })
    },
    {
        title: "Last Month",
        getTimeSpan: (now) => ({
            start: new Date(now.getFullYear(), now.getMonth() - 1, 1),
            end: new Date(now.getFullYear(), now.getMonth(), 0, 23, 59, 59, 999)
        })
    },
    {
        title: "This Year",
        getTimeSpan: (now) => ({
            start: new Date(now.getFullYear(), 0, 1),
            end: new Date(now.getFullYear(), 11, 31, 23, 59, 59, 999)
        })
    },
    {
        title: "Older",
        getTimeSpan: (now) => ({
            start: new Date(0),
            end: new Date(now.getFullYear(), 0, 1)
        })
    }
];

const getTimeSpans = (): NamedTimeSpan[] => {
    const now = new Date();

    return TIME_SPAN_DEFINITIONS.map(def => {
        const { start, end } = def.getTimeSpan(now);
        return {
            title: def.title,
            start: start.getTime(),
            end: end.getTime(),
        };
    });
};

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