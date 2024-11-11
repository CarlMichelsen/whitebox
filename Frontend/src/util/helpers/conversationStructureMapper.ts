import {
    ConversationOption,
    ConversationOptionStructure,
    SortableConversationOption, TimeSpans
} from "../../model/sidebar/conversationOption.ts";

const toConversationOption = (sco: SortableConversationOption) => ({...sco, lastAltered: sco.lastAltered.getTime()} as ConversationOption)

const toSortableConversationOption = (sco: ConversationOption) => ({...sco, lastAltered: new Date(sco.lastAltered)} as SortableConversationOption)

const getCurrentTimeSpans = (): TimeSpans => {
    const now = new Date();
    
    const startOfToday = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    const endOfToday = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 23, 59, 59, 999);

    const startOfYesterday = new Date(now.getFullYear(), now.getMonth(), now.getDate() - 1);
    const endOfYesterday = new Date(now.getFullYear(), now.getMonth(), now.getDate() - 1, 23, 59, 59, 999);
    
    const startOfThisWeek = new Date(startOfToday);
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

    return {
        startOfToday,
        endOfToday,
        startOfYesterday,
        endOfYesterday,
        startOfThisWeek,
        endOfThisWeek,
        startOfThisMonth,
        endOfThisMonth,
        startOfLastMonth,
        endOfLastMonth,
        startOfThisYear,
        endOfThisYear,
    };
};

export const mapConversationOptionsToConversationStructure =
    (conversationOptions: ConversationOption[]): ConversationOptionStructure => {

    const validDateConversationOptions: SortableConversationOption[] = conversationOptions
        .map(toSortableConversationOption);

    const uniqueOptionsMap = new Map<string, SortableConversationOption>();
    validDateConversationOptions.forEach(option => {
        if (!uniqueOptionsMap.has(option.id)) {
            uniqueOptionsMap.set(option.id, option);
        }
    });

    const uniqueOptions = Array.from(uniqueOptionsMap.values());
    
    uniqueOptions.sort((a, b) => b.lastAltered.getTime() - a.lastAltered.getTime());

    const spans = getCurrentTimeSpans();
    
    const today: SortableConversationOption[] = [];
    const yesterday: SortableConversationOption[] = [];
    const thisWeek: SortableConversationOption[] = [];
    const thisMonth: SortableConversationOption[] = [];
    const lastMonth: SortableConversationOption[] = [];
    const thisYear: SortableConversationOption[] = [];
    const olderThanAYear: SortableConversationOption[] = [];

    
    uniqueOptions.forEach(option => {
        if (option.lastAltered <= spans.endOfToday && option.lastAltered >= spans.startOfToday) {
            today.push(option);
        } else if (option.lastAltered <= spans.endOfYesterday && option.lastAltered >= spans.startOfYesterday) {
            yesterday.push(option);
        } else if (option.lastAltered <= spans.endOfThisWeek && option.lastAltered >= spans.startOfThisWeek) {
            thisWeek.push(option);
        } else if (option.lastAltered <= spans.endOfThisMonth && option.lastAltered >= spans.startOfThisMonth) {
            thisMonth.push(option);
        } else if (option.lastAltered <= spans.endOfLastMonth && option.lastAltered >= spans.startOfLastMonth) {
            lastMonth.push(option);
        } else if (option.lastAltered <= spans.endOfThisYear && option.lastAltered >= spans.startOfThisYear) {
            thisYear.push(option);
        } else {
            olderThanAYear.push(option);
        }
    });

    return {
        today: today.map(toConversationOption),
        yesterday: yesterday.map(toConversationOption),
        thisWeek: thisWeek.map(toConversationOption),
        thisMonth: thisMonth.map(toConversationOption),
        lastMonth: lastMonth.map(toConversationOption),
        thisYear: thisYear.map(toConversationOption),
        olderThanAYear: olderThanAYear.map(toConversationOption),
    };
}

export const addConversationOption = (
    structure: ConversationOptionStructure,
    option: ConversationOption
): ConversationOptionStructure => {
    const spans = getCurrentTimeSpans();
    
    const lastAltered = new Date(option.lastAltered);
    if (lastAltered<= spans.endOfToday && lastAltered>= spans.startOfToday) {
        structure.today.unshift(option);
    } else if (lastAltered <= spans.endOfYesterday && lastAltered >= spans.startOfYesterday) {
        structure.yesterday.unshift(option);
    } else if (lastAltered <= spans.endOfThisWeek && lastAltered >= spans.startOfThisWeek) {
        structure.thisWeek.unshift(option);
    } else if (lastAltered <= spans.endOfThisMonth && lastAltered >= spans.startOfThisMonth) {
        structure.thisMonth.unshift(option);
    } else if (lastAltered <= spans.endOfLastMonth && lastAltered >= spans.startOfLastMonth) {
        structure.lastMonth.unshift(option);
    } else if (lastAltered <= spans.endOfThisYear && lastAltered >= spans.startOfThisYear) {
        structure.thisYear.unshift(option);
    } else {
        structure.olderThanAYear.unshift(option);
    }
    
    return structure;
}

export const removeConversationOption = (structure: ConversationOptionStructure, optionId: string) => {
    const convKeys = Object.keys(structure) as (keyof ConversationOptionStructure)[];
    for (const key of convKeys) {
        const sectionConversations = structure[key];
        if (sectionConversations === null) {
            continue;
        }

        const idx = sectionConversations.findIndex(co => co.id === optionId);
        if (idx !== -1) {
            const removed = sectionConversations.splice(idx, 1);
            return removed[0];
        }
    }
    
    return null;
}

export const editAndReAddOption = (structure: ConversationOptionStructure, optionId: string, edit: (option: ConversationOption) => void) => {
    const conversationToReAdd = removeConversationOption(structure, optionId);
    if (conversationToReAdd === null) {
        return;
    }

    edit(conversationToReAdd);

    addConversationOption(structure, conversationToReAdd)
}