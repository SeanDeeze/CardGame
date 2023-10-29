import {ICard} from './card';
import {IGamePlayer, IUser} from './player';

export interface IGame
{
    id: string;
    name: string;
    active: boolean;
    engine: IGameEngine;
}

export interface IGameEngine 
{
    gamePlayers: IUser[];
    CurrentGamePlayerID?: string;
    cards: ICard[];
    started: boolean;
    finished: boolean;
}

export interface IPlayerGame
{
    gameID: string;
    playerID: string;
}

export interface IGameState
{
    gamePlayers: IGamePlayer[];
    currentGamePlayerId?: string;
    cards: ICard[];
}
