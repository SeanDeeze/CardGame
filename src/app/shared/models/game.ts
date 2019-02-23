import { IPlayer } from './player';
import { ICard } from './card';

export interface IGame {
    Id: number;
    name: string;
    active: boolean;
    players: IPlayer[];
}

export interface IPlayerGame {
    game: IGame;
    player: IPlayer;
}

export interface IGameStatus {
    players: IPlayer[];
    cards: ICard[];
}
