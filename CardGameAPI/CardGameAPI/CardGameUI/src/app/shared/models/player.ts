import {ICard} from './card';

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
    dice: number[];
    leader: boolean;
}
