import { IPlayer } from './player';

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
