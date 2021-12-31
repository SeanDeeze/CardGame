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
    cardpiles: [ICard[]];
}

export interface IPlayerGame
{
    game: IGame;
    player: IPlayer;
}

export interface IDice
{
    diceValue: number;
}

export interface IGameState
{
    gamePlayers: IGamePlayer[];
    currentGamePlayer: IGamePlayer;
    cardPiles: ICard[];
}
