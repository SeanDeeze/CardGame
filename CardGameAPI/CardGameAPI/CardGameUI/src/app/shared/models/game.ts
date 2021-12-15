import { ICard } from './card';
import { IPlayer } from './player';

export interface IGame
{
    id: number;
    name: string;
    active: boolean;
    finished: boolean;
    players: IPlayer[];
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

export interface IGameStatus
{
    players: IPlayer[];
    cards: ICard[];
    dice: IDice[];
}
