using System;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

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
            private Uri ImageUri { get; set; } // relative path to image

            public enum TileType { water, grass1, grass2, grass3, grass4, astr1, adiag1, adiag2, astr2, a3, astr4, adiag4, rum, lab2, lab3, lab4, lab5, ice, hole, croc, cannibal, fort, gfort, gold, balloon, cannon, horse, plane}

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
            
            public enum Direction { up, right, down, left }
            //TODO: Direction of arrows
            public Direction direction { get; set; }


            public Tile(TileType type, short gold, Team team, bool pirate1, bool pirate2, bool pirate3, bool opened)
            {
                this.type = type;
                this.gold = gold;
                this.team = team;
                this.pirate1 = pirate1;
                this.pirate2 = pirate2;
                this.pirate3 = pirate3;
                this.opened = opened;

                switch (type) // setup image paths according to tile types
                {
                    // TODO: here you have to do
                    // imageUri = new Uri("./tiles/airplane.png",UriKind.Relative);
                    // for each case of tile types, substituting path to image with what you need in particular case

                    case (TileType.grass1):
                        ImageUri = new Uri("./tiles/empty-1.png", UriKind.Relative);
                        break;

                    case (TileType.grass2):
                        ImageUri = new Uri("./tiles/empty-2.png", UriKind.Relative);
                        break;

                    case (TileType.grass3):
                        ImageUri = new Uri("./tiles/empty-3.png", UriKind.Relative);
                        break;

                    case (TileType.grass4):
                        ImageUri = new Uri("./tiles/empty-4.png", UriKind.Relative);
                        break;

                    case (TileType.rum):
                        ImageUri = new Uri("./tiles/keg-of-rum.png", UriKind.Relative);
                        break;

                    case (TileType.ice):
                        ImageUri = new Uri("./tiles/ice.png", UriKind.Relative);
                        break;

                    case (TileType.hole):
                        ImageUri = new Uri("./tiles/pitfall.png", UriKind.Relative);
                        break;

                    case (TileType.croc):
                        ImageUri = new Uri("./tiles/crocodile.png", UriKind.Relative);
                        break;

                    case (TileType.cannibal):
                        ImageUri = new Uri("./tiles/cannibal.png", UriKind.Relative);
                        break;

                    case (TileType.fort):
                        ImageUri = new Uri("./tiles/fort.png", UriKind.Relative);
                        break;

                    case (TileType.gfort):
                        ImageUri = new Uri("./tiles/fort-w-aborigine.png", UriKind.Relative);
                        break;

                    case (TileType.gold):
                        ImageUri = new Uri("./tiles/coins-1.png", UriKind.Relative);
                        break;

                    case (TileType.balloon):
                        ImageUri = new Uri("./tiles/baloon.png", UriKind.Relative);
                        break;

                    case (TileType.cannon):
                        ImageUri = new Uri("./tiles/gun.png", UriKind.Relative);
                        break;

                    case (TileType.water):
                        ImageUri = new Uri("./tiles/water.png", UriKind.Relative);
                        break;

                    case (TileType.astr1):
                        ImageUri = new Uri("./tiles/arrow-1.png", UriKind.Relative);
                        break;

                    case (TileType.adiag1):
                        ImageUri = new Uri("./tiles/arrow-2.png", UriKind.Relative);
                        break;

                    case (TileType.adiag2):
                        ImageUri = new Uri("./tiles/arrow-4.png", UriKind.Relative);
                        break;

                    case (TileType.astr2):
                        ImageUri = new Uri("./tiles/arrow-3.png", UriKind.Relative);
                        break;

                    case (TileType.a3):
                        ImageUri = new Uri("./tiles/arrow-5.png", UriKind.Relative);
                        break;

                    case (TileType.astr4):
                        ImageUri = new Uri("./tiles/arrow-6.png", UriKind.Relative);
                        break;

                    case (TileType.adiag4):
                        ImageUri = new Uri("./tiles/arrow-7.png", UriKind.Relative);
                        break;

                    case (TileType.lab2):
                        ImageUri = new Uri("./tiles/rotate-2.png", UriKind.Relative);
                        break;

                    case (TileType.lab3):
                        ImageUri = new Uri("./tiles/rotate-3.png", UriKind.Relative);
                        break;

                    case (TileType.lab4):
                        ImageUri = new Uri("./tiles/rotate-4.png", UriKind.Relative);
                        break;

                    case (TileType.lab5):
                        ImageUri = new Uri("./tiles/rotate-5.png", UriKind.Relative);
                        break;

                    case (TileType.horse):
                        ImageUri = new Uri("./tiles/horse.png", UriKind.Relative);
                        break;

                    case (TileType.plane):
                        ImageUri = new Uri("./tiles/airplane.png", UriKind.Relative);
                        break;

                    default: // TODO: remove this when proper image arranging is done
                        ImageUri = new Uri("./tiles/empty!.png", UriKind.Relative);
                        break;
                }
            }

            public BitmapImage GetBitmapImage() // returns assigned bitmap image for tile
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = ImageUri;
                img.EndInit();
                return img;
            }
        }

        public class Board
        {
            public static Tile[,] output { get; set; }
            public static int r = 117;
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
                    for (int x = 0; x < 12; x++)
                        Fill(x, y, Tile.TileType.water, board); //0th and last with water
                for (int y = 1; y < 11; y++)
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
                        Fill(x, y, rand, tiles, board); //2,2 -> 9,

                for (int x = 1; x < 11; x+=9)
                    for (int y = 2; y < 10; y++)

                /*for (int y = 2; y < 10; y++)
                    for (int x = 1; x < 12; x += 10)*/
                        Fill(x, y, rand, tiles, board);

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
                //int r = rand.Next() % (tiles.Count - 1);
                board[x, y] = tiles[--Board.r];
                tiles.RemoveAt(Board.r);
            }
            static void Fill(int x, int y, Tile.TileType type, Tile[,] board)
            {
                board[x, y] = new Tile(type, 0, Team.none, false, false, false, true);
            }

            List<Tile> GenerateAllTiles()
            {
                List<Tile> tiles = new List<Tile>();

                /*for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile(Tile.TileType.grass2, 0, Team.none, false, false, false, false)); //grass
                    */
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


                tiles.Add(new Tile(Tile.TileType.plane, 0, Team.none, false, false, false, false));
                tiles.Add(new Tile(Tile.TileType.horse, 0, Team.none, false, false, false, false));
                tiles.Add(new Tile(Tile.TileType.horse, 0, Team.none, false, false, false, false));

                return tiles;
            }

            public BitmapImage[,] GetBitmapImages() // returns two-dimensional array of images for tiles at the board
            {
                var imgs = new BitmapImage[12, 12];

                for (int y = 0; y < 12; y++)
                {
                    for (int x = 0; x < 12; x++)
                    {
                        var currentTile = output[x, y];
                        // if image is null, return empty-1 by default
                        // TODO: change this later
                        var currentImage = currentTile == null ?
                            new BitmapImage(new Uri("./tiles/empty.png", UriKind.Relative))
                            : currentTile.GetBitmapImage();
                        imgs[x, y] = currentImage;
                    }
                }

                return imgs;
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
