export interface ICard {
    id: number;
    name: string;
    description: string;
    definedDice: ICardRole[];
}

export interface ICardRole {
    id: number;
    name: string;
    diceNumber: string;
}
