type GenericValueObject<T> = {
    Metadata: string;
    OogaList: OogaBooga[];
    Value: T;
}

type OogaBooga = {
    Booga: number;
    Ooga: string;
}