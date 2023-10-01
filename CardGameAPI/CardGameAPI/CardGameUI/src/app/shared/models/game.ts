import {ICard} from './card';
import {IGamePlayer, IUser} from './player';

export interface IGame
{
    id: string;
    name: string;
    active: boolean;
    finished: boolean;
    gamePlayers: IUser[];
    cards: ICard[];
}

export interface IPlayerGame
{
    game: IGame;
    player: IUser;
}

export interface IGameState
{
    gamePlayers: IGamePlayer[];
    currentGamePlayerId?: string;
    cards: ICard[];
}
