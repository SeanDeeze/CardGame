import { IPlayer } from './player';

export interface IGame {
    Id: number;
    Name: string;
    active: boolean;
    players: IPlayer[];
}

export interface IPlayerGame {
    game: IGame;
    player: IPlayer;
}
