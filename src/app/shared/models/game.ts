import { IPlayer } from './player';
import { ICard } from './card';

export interface IGame {
    id: number;
    name: string;
    active: boolean;
    players: IPlayer[];
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
