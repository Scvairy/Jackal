﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace Jackal
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public enum Team { none, red, black, yellow, white }
        public class Tile
        {
            public enum TileType { water, grass1, grass2, grass3, grass4, astr1, adiag1, adiag2, astr2, a3, astr4, adiag4, rum, lab2, lab3, lab4, lab5, ice, hole, croc, cannibal, fort, gfort, gold, balloon, cannon/*, horse, plane*/}

            [JsonProperty("type")]
            public TileType type { get; set; }

            [JsonProperty("gold")]
            public short gold { get; set; }

            [JsonProperty("team")]
            public Team team { get; set; }

            [JsonProperty("pirate1")]
            public bool pirate1 { get; set; }

            [JsonProperty("pirate2")]
            public bool pirate2 { get; set; }

            [JsonProperty("pirate3")]
            public bool pirate3 { get; set; }

            [JsonProperty("opened")]
            public bool opened { get; set; }
            
            
            public Tile(TileType type, short gold, Team team, bool pirate1, bool pirate2, bool pirate3, bool opened)
            {
                this.type = type;
                this.gold = gold;
                this.team = team;
                this.pirate1 = pirate1;
                this.pirate2 = pirate2;
                this.pirate3 = pirate3;
                this.opened = opened;
            }
        }
        public class Board
        {
            public static Tile[,] output { get; set; }
            public Board()
            {
                Random rand = new Random();
                var tiles = GenerateAllTiles();
                if (tiles.Count != 117)
                {
                    MessageBox.Show("Количество тайлов не совпадает");
                }
                Tile[,] board = new Tile[12, 12];

                for (int y = 0; y < 12; y += 11)
                    for (int x = 0; x < 11; x++)
                        Fill(x, y, Tile.TileType.water, board); //0th and last with water
                for (int y = 2; y < 10; y++)
                    for (int x = 0; x < 12; x += 11)
                        Fill(x, y, Tile.TileType.water, board); //left and right with water
                for (int y = 1; y < 11; y += 9)
                    for (int x = 1; x < 11; x += 9)
                        Fill(x, y, Tile.TileType.water, board); //corners with water

                for (int y = 1; y < 11; y += 9)
                    for (int x = 2; x < 10; x++)
                        Fill(x, y, rand, tiles, board); //1st and last without corners

                for (int y = 2; y < 10; y++)
                    for (int x = 2; x < 10; x++)
                        Fill(x, y, rand, tiles, board); //2,2 -> 9,9
                

                for (int y = 0; y < 12; y++)
                    for (int x = 0; x < 12; x += 11)


                        output = board;
            }

            void MakeAllBoardOpen(Tile[,] board)
            {
                for (int y = 0; y < 12; y++)
                    for (int x = 0; x < 12; x++)
                        board[x, y].opened = true;
            }
            static void Fill(int x, int y, Random rand, List<Tile> tiles, Tile[,] board)
            {
                int r = rand.Next() % tiles.Count;
                board[x, y] = tiles[r];
                tiles.RemoveAt(r);
            }
            static void Fill(int x, int y, Tile.TileType type, Tile[,] board)
            {
                board[x, y] = new Tile(type, 0, Team.none, false, false, false, true);
            }

            List<Tile> GenerateAllTiles()
            {
                List<Tile> tiles = new List<Tile>();

                for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile(Tile.TileType.grass2, 0, Team.none, false, false, false, false)); //grass

                for (int g = 1; g < 5; g++)
                    for (int i = 0; i < 10; i++)
                        tiles.Add(new Tile((Tile.TileType)g, 0, Team.none, false, false, false, false)); //grass

                for (int a = 5; a < 12; a++)
                    for (int i = 0; i < 3; i++)
                        tiles.Add(new Tile((Tile.TileType)a, 0, Team.none, false, false, false, false)); //arrows

                for (int i = 0; i < 4; i++)
                    tiles.Add(new Tile(Tile.TileType.rum, 0, Team.none, false, false, false, false));
                for (int i = 0; i < 5; i++)
                    tiles.Add(new Tile(Tile.TileType.lab2, 0, Team.none, false, false, false, false));
                for (int i = 0; i < 4; i++)
                    tiles.Add(new Tile(Tile.TileType.lab3, 0, Team.none, false, false, false, false));
                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.TileType.lab4, 0, Team.none, false, false, false, false));

                tiles.Add(new Tile(Tile.TileType.lab5, 0, Team.none, false, false, false, false));

                for (int i = 0; i < 6; i++)
                    tiles.Add(new Tile(Tile.TileType.ice, 0, Team.none, false, false, false, false));
                for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile(Tile.TileType.hole, 0, Team.none, false, false, false, false));
                for (int i = 0; i < 4; i++)
                    tiles.Add(new Tile(Tile.TileType.croc, 0, Team.none, false, false, false, false));
                tiles.Add(new Tile(Tile.TileType.cannibal, 0, Team.none, false, false, false, false));

                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.TileType.fort, 0, Team.none, false, false, false, false));
                tiles.Add(new Tile(Tile.TileType.gfort, 0, Team.none, false, false, false, false));

                for (int i = 0; i < 5; i++)
                    tiles.Add(new Tile(Tile.TileType.gold, 1, Team.none, false, false, false, false));
                for (int i = 0; i < 5; i++)
                    tiles.Add(new Tile(Tile.TileType.gold, 2, Team.none, false, false, false, false));
                for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile(Tile.TileType.gold, 3, Team.none, false, false, false, false));
                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.TileType.gold, 4, Team.none, false, false, false, false));
                tiles.Add(new Tile(Tile.TileType.gold, 5, Team.none, false, false, false, false));

                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.TileType.balloon, 0, Team.none, false, false, false, false));
                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.TileType.cannon, 0, Team.none, false, false, false, false));

                return tiles;
            }
        }

        class Pirate
        {
            public enum id { first, second, third }

        }
        class Ship
        {

        }
    }
}
