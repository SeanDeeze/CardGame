import { IGame } from './game';

export interface IPlayer {
    id: number;
    userName: string;
    LastActivity: Date;
    admin: boolean;
    currentgame: IGame;
    wins: number;
    points: number;
}
