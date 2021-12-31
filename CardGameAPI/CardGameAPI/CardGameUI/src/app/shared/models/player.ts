import {ICard} from './card';
import {IGame} from './game';

export interface IPlayer
{
    id: number;
    userName: string;
    lastActivity: Date;
    admin: boolean;
    currentGame: IGame;
    wins: number;
    isSelectedPlayer: boolean;
}

export interface IGamePlayer
{
    player: IPlayer;
    gold: number;
    reputationPoints: number;
    cards: ICard[];
    order: number;
    isCurrent: boolean;
}
