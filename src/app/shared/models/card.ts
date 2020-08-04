export interface ICard {
    id: number;
    name: string;
    description: string;
    reputationPoints: number;
    gold: number;
    image: string;
    definedDice: ICardRole[];
    cardpile: number;
}

export interface ICardRole {
    id: number;
    name: string;
    diceNumber: string;
}
