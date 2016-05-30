﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace Jackal
{
    public class Board
    {
        public Tile[,] boardArray { get; set; }
        public ObservableCollection<Tile> TilesColl = new ObservableCollection<Tile>();
        public ObservableCollection<Pirate> PiratesColl = new ObservableCollection<Pirate>();
        public static Random rand = new Random();

        public Board()
        {
            var tiles = GenerateAllTiles();
            if (tiles.Count != 117)
            {
                MessageBox.Show("Количество тайлов не совпадает");
                return;
            }

            boardArray = new Tile[13, 13];

            for (int y = 0; y < 13; y += 12)
                for (int x = 0; x < 13; x++)
                    SetWater(x, y, boardArray); //0th and last with water
            for (int y = 1; y < 12; y++)
                for (int x = 0; x < 13; x += 12)
                    SetWater(x, y, boardArray); //left and right with water
            for (int y = 1; y < 12; y += 10)
                for (int x = 1; x < 12; x += 10)
                    SetWater(x, y, boardArray); //corners with water

            for (int y = 1; y < 12; y += 10)
                for (int x = 2; x < 11; x++)
                    SetRandomTile(x, y, tiles, boardArray); //1st and last without corners

            for (int y = 2; y < 11; y++)
                for (int x = 2; x < 11; x++)
                    SetRandomTile(x, y, tiles, boardArray); //2,2 -> 9,

            for (int x = 1; x < 12; x += 10)
                for (int y = 2; y < 11; y++)
                    SetRandomTile(x, y, tiles, boardArray);

            for (int y = 0; y < 13; y++)
                for (int x = 0; x < 13; x++)
                {
                    var tmp = boardArray[x, y];
                    tmp.Pos = new Point(x, y);
                    TilesColl.Add(tmp);
                }

            PiratesColl.Add(new Pirate(PirateId.first, Player.red, 7, 7));

        }

        static void SetRandomTile(int x, int y, List<Tile> tiles, Tile[,] board)
        {
            int r = rand.Next() % (tiles.Count);
            board[x, y] = tiles[r];
            tiles.RemoveAt(r);
        }
        static void SetWater(int x, int y, Tile[,] board)
        {
            board[x, y] = new Tile(TileType.water, x, y, true);
        }

        List<Tile> GenerateAllTiles()
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < 3; i++)
                tiles.Add(new Tile(TileType.grass2)); //grass

            for (int g = 1; g < 5; g++)
                for (int i = 0; i < 10; i++)
                    tiles.Add(new Tile((TileType)g)); //grass

            for (int a = 5; a < 12; a++)
                for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile((TileType)a)); //arrows

            for (int i = 0; i < 4; i++)
                tiles.Add(new Tile(TileType.rum));
            for (int i = 0; i < 5; i++)
                tiles.Add(new Tile(TileType.lab2));
            for (int i = 0; i < 4; i++)
                tiles.Add(new Tile(TileType.lab3));
            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(TileType.lab4));

            tiles.Add(new Tile(TileType.lab5));

            for (int i = 0; i < 6; i++)
                tiles.Add(new Tile(TileType.ice));
            for (int i = 0; i < 3; i++)
                tiles.Add(new Tile(TileType.hole));
            for (int i = 0; i < 4; i++)
                tiles.Add(new Tile(TileType.croc));
            tiles.Add(new Tile(TileType.cannibal));

            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(TileType.fort));
            tiles.Add(new Tile(TileType.gfort));

            for (int i = 0; i < 5; i++)
                tiles.Add(new Tile(1, TileType.coins1));
            for (int i = 0; i < 5; i++)
                tiles.Add(new Tile(2, TileType.coins1));
            for (int i = 0; i < 3; i++)
                tiles.Add(new Tile(3, TileType.coins1));
            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(4, TileType.coins1));
            tiles.Add(new Tile(5, TileType.coins1));

            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(TileType.balloon));
            for (int i = 0; i < 2; i++)
                tiles.Add(new Tile(TileType.cannon));

            /*
            tiles.Add(new Tile(TileType.plane));
            tiles.Add(new Tile(TileType.horse));
            tiles.Add(new Tile(TileType.horse));
            */
            return tiles;
        }
    }
}