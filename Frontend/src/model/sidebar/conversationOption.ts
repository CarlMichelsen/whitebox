export type SortableConversationOption = {
    id: string;
    title: string;
    lastAltered: Date;
}

export type ConversationOption = {
    id: string;
    summary: string;
    lastAltered: number;
}

export type NamedTimeSpan = {
    title: string;
    start: number;
    end: number;
}

export type ConversationOptionSection = NamedTimeSpan & {
    options: ConversationOption[];
}