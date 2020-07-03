import { IPlayer } from './player';
import { ICard } from './card';

export interface IGame {
    id: number;
    name: string;
    active: boolean;
    finished: boolean;
    players: IPlayer[];
    cards: ICard[];
    cardpiles: [ICard[]];
}

export interface IPlayerGame {
    game: IGame;
    player: IPlayer;
}

export interface IDice {
    diceValue: number;
}

export interface IGameStatus {
    players: IPlayer[];
    cards: ICard[];
    dice: IDice[];
}
