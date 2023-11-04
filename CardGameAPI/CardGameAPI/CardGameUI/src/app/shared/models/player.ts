import {ICard} from './card';
import {Dice} from './dice';

export interface IUser
{
    id: string;
    userName: string;
    lastActivity: Date;
    admin: boolean;
    wins: number;
}

export interface IGamePlayer
{
    player: IUser;
    gold: number;
    reputationPoints: number;
    cards: ICard[];
    order: number;
    dice: Dice[];
    leader: boolean;
}
