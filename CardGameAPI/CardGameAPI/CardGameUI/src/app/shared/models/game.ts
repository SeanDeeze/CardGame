import {ICard} from './card';
import {IGamePlayer, IPlayer} from './player';

export interface IGame
{
    id: number;
    name: string;
    active: boolean;
    finished: boolean;
    gamePlayers: IPlayer[];
    cards: ICard[];
}

export interface IPlayerGame
{
    game: IGame;
    player: IPlayer;
}

export interface IGameState
{
    gamePlayers: IGamePlayer[];
    currentGamePlayerId?: string;
    cards: ICard[];
}
