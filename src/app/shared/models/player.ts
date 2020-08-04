import { IGame } from './game';

export interface IPlayer {
    id: number;
    userName: string;
    lastActivity: Date;
    admin: boolean;
    currentGame: IGame;
    wins: number;
    points: number;
    gold: number;
    isSelectedPlayer: boolean;
}
