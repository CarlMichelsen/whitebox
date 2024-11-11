export type SortableConversationOption = {
    id: string;
    title: string;
    lastAltered: Date;
}

export type ConversationOption = {
    id: string;
    title: string;
    lastAltered: number;
}

export type TimeSpans = {
    startOfToday: Date;
    endOfToday: Date;

    startOfYesterday: Date;
    endOfYesterday: Date;

    startOfThisWeek: Date;
    endOfThisWeek: Date;

    startOfThisMonth: Date;
    endOfThisMonth: Date;

    startOfLastMonth: Date;
    endOfLastMonth: Date;
    
    startOfThisYear: Date;
    endOfThisYear: Date;
}

export type ConversationOptionStructure = {
    today: ConversationOption[];
    yesterday: ConversationOption[];
    thisWeek: ConversationOption[];
    thisMonth: ConversationOption[];
    lastMonth: ConversationOption[];
    thisYear: ConversationOption[];
    olderThanAYear: ConversationOption[];
}