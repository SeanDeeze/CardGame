import {ICard} from './card';
import {IGame} from './game';

export interface IPlayer
{
    id: string;
    userName: string;
    lastActivity: Date;
    admin: boolean;
    currentGame: IGame;
    wins: number;
    isSelectedPlayer: boolean;
    dice: number[];
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
