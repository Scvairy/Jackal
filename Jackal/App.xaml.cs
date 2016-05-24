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
            public enum Type
            {
                water, grass1, grass2, grass3, grass4, astr1, adiag1, adiag2, astr2, a3,
                astr4, adiag4, rum, lab2, lab3, lab4, lab5, ice, hole, croc,
                cannibal, fort, gfort, gold, balloon, cannon, horse, plane
            }

            private Uri ImageUri { get; set; } // relative path to image
            private Type type { get; set; }

            public short gold { get; set; }
            public Team team { get; set; }
            public bool pirate1 { get; set; }
            public bool pirate2 { get; set; }
            public bool pirate3 { get; set; }
            public bool opened { get; set; }

            public enum Direction { up, right, down, left }
            //TODO: Direction of images (arrows)
            public Direction direction { get; set; }


            public Tile(Type type, bool opened = false, short gold = 0, Team team = Team.none, bool pirate1 = false, bool pirate2 = false, bool pirate3 = false)
            {
                Random r = new Random();

                this.type = type;
                this.gold = gold;
                this.team = team;
                this.pirate1 = pirate1;
                this.pirate2 = pirate2;
                this.pirate3 = pirate3;
                this.opened = opened;
                if (opened)
                    switch (type) // setup image paths according to tile types
                    {
                        // TODO: here you have to do
                        // imageUri = new Uri("./tiles/airplane.png",UriKind.Relative);
                        // for each case of tile types, substituting path to image with what you need in particular case

                        case (Type.grass1):
                            ImageUri = new Uri("./tiles/empty-1.png", UriKind.Relative);
                            break;

                        case (Type.grass2):
                            ImageUri = new Uri("./tiles/empty-2.png", UriKind.Relative);
                            break;

                        case (Type.grass3):
                            ImageUri = new Uri("./tiles/empty-3.png", UriKind.Relative);
                            break;

                        case (Type.grass4):
                            ImageUri = new Uri("./tiles/empty-4.png", UriKind.Relative);
                            break;

                        case (Type.rum):
                            ImageUri = new Uri("./tiles/keg-of-rum.png", UriKind.Relative);
                            break;

                        case (Type.ice):
                            ImageUri = new Uri("./tiles/ice.png", UriKind.Relative);
                            break;

                        case (Type.hole):
                            ImageUri = new Uri("./tiles/pitfall.png", UriKind.Relative);
                            break;

                        case (Type.croc):
                            ImageUri = new Uri("./tiles/crocodile.png", UriKind.Relative);
                            break;

                        case (Type.cannibal):
                            ImageUri = new Uri("./tiles/cannibal.png", UriKind.Relative);
                            break;

                        case (Type.fort):
                            ImageUri = new Uri("./tiles/fort.png", UriKind.Relative);
                            break;

                        case (Type.gfort):
                            ImageUri = new Uri("./tiles/fort-w-aborigine.png", UriKind.Relative);
                            break;

                        case (Type.gold):
                            ImageUri = new Uri("./tiles/coins-1.png", UriKind.Relative);
                            break;

                        case (Type.balloon):
                            ImageUri = new Uri("./tiles/balloon.png", UriKind.Relative);
                            break;

                        case (Type.cannon):
                            ImageUri = new Uri("./tiles/gun.png", UriKind.Relative);
                            break;

                        case (Type.water):
                            ImageUri = new Uri("./tiles/water.png", UriKind.Relative);
                            break;

                        case (Type.astr1):
                            ImageUri = new Uri("./tiles/arrow-1.png", UriKind.Relative);
                            direction = (Direction)(r.Next() % 4);
                            break;

                        case (Type.adiag1):
                            ImageUri = new Uri("./tiles/arrow-2.png", UriKind.Relative);
                            direction = (Direction)(r.Next() % 4);
                            break;

                        case (Type.adiag2):
                            ImageUri = new Uri("./tiles/arrow-4.png", UriKind.Relative);
                            direction = (Direction)(r.Next() % 4);
                            break;

                        case (Type.astr2):
                            ImageUri = new Uri("./tiles/arrow-3.png", UriKind.Relative);
                            direction = (Direction)(r.Next() % 4);
                            break;

                        case (Type.a3):
                            ImageUri = new Uri("./tiles/arrow-5.png", UriKind.Relative);
                            direction = (Direction)(r.Next() % 4);
                            break;

                        case (Type.astr4):
                            ImageUri = new Uri("./tiles/arrow-6.png", UriKind.Relative);
                            direction = (Direction)(r.Next() % 4);
                            break;

                        case (Type.adiag4):
                            ImageUri = new Uri("./tiles/arrow-7.png", UriKind.Relative);
                            direction = (Direction)(r.Next() % 4);
                            break;

                        case (Type.lab2):
                            ImageUri = new Uri("./tiles/rotate-2.png", UriKind.Relative);
                            break;

                        case (Type.lab3):
                            ImageUri = new Uri("./tiles/rotate-3.png", UriKind.Relative);
                            break;

                        case (Type.lab4):
                            ImageUri = new Uri("./tiles/rotate-4.png", UriKind.Relative);
                            break;

                        case (Type.lab5):
                            ImageUri = new Uri("./tiles/rotate-5.png", UriKind.Relative);
                            break;

                        case (Type.horse):
                            ImageUri = new Uri("./tiles/horse.png", UriKind.Relative);
                            break;

                        case (Type.plane):
                            ImageUri = new Uri("./tiles/airplane.png", UriKind.Relative);
                            break;

                        default: // TODO: remove this when proper image arranging is done
                            ImageUri = new Uri("./tiles/empty!.png", UriKind.Relative);
                            break;
                    }
                else ImageUri = new Uri("./tiles/closed.png", UriKind.Relative);
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
            public Board()
            {
                var tiles = GenerateAllTiles();
                if (tiles.Count != 117)
                {
                    MessageBox.Show("Количество тайлов не совпадает");
                    return;
                }
                Random rand = new Random();
                Tile[,] board = new Tile[13, 13];

                for (int y = 0; y < 13; y += 12)
                    for (int x = 0; x < 13; x++)
                        SetWater(x, y, board); //0th and last with water
                for (int y = 1; y < 12; y++)
                    for (int x = 0; x < 13; x += 12)
                        SetWater(x, y, board); //left and right with water
                for (int y = 1; y < 12; y += 10)
                    for (int x = 1; x < 12; x += 10)
                        SetWater(x, y, board); //corners with water

                for (int y = 1; y < 12; y += 10)
                    for (int x = 2; x < 11; x++)
                        SetRandomTile(x, y, rand, tiles, board); //1st and last without corners

                for (int y = 2; y < 11; y++)
                    for (int x = 2; x < 11; x++)
                        SetRandomTile(x, y, rand, tiles, board); //2,2 -> 9,

                for (int x = 1; x < 12; x += 10)
                    for (int y = 2; y < 11; y++)
                        SetRandomTile(x, y, rand, tiles, board);

                output = board;
            }

            void MakeAllBoardOpen(Tile[,] board)
            {
                for (int y = 0; y < 13; y++)
                    for (int x = 0; x < 13; x++)
                        board[x, y].opened = true;
            }
            static void SetRandomTile(int x, int y, Random rand, List<Tile> tiles, Tile[,] board)
            {
                int r = rand.Next() % (tiles.Count);
                board[x, y] = tiles[r];
                tiles.RemoveAt(r);
            }
            static void SetWater(int x, int y, Tile[,] board)
            {
                board[x, y] = new Tile(Tile.Type.water, true);
            }

            List<Tile> GenerateAllTiles()
            {
                List<Tile> tiles = new List<Tile>();

                /*for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile(Tile.TileType.grass2, 0, Team.none, false, false, false, false)); //grass
                    */
                for (int g = 1; g < 5; g++)
                    for (int i = 0; i < 10; i++)
                        tiles.Add(new Tile((Tile.Type)g)); //grass

                for (int a = 5; a < 12; a++)
                    for (int i = 0; i < 3; i++)
                        tiles.Add(new Tile((Tile.Type)a)); //arrows

                for (int i = 0; i < 4; i++)
                    tiles.Add(new Tile(Tile.Type.rum));
                for (int i = 0; i < 5; i++)
                    tiles.Add(new Tile(Tile.Type.lab2));
                for (int i = 0; i < 4; i++)
                    tiles.Add(new Tile(Tile.Type.lab3));
                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.Type.lab4));

                tiles.Add(new Tile(Tile.Type.lab5));

                for (int i = 0; i < 6; i++)
                    tiles.Add(new Tile(Tile.Type.ice));
                for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile(Tile.Type.hole));
                for (int i = 0; i < 4; i++)
                    tiles.Add(new Tile(Tile.Type.croc));
                tiles.Add(new Tile(Tile.Type.cannibal));

                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.Type.fort, false));
                tiles.Add(new Tile(Tile.Type.gfort, false));

                for (int i = 0; i < 5; i++)
                    tiles.Add(new Tile(Tile.Type.gold, false, 1));
                for (int i = 0; i < 5; i++)
                    tiles.Add(new Tile(Tile.Type.gold, false, 2));
                for (int i = 0; i < 3; i++)
                    tiles.Add(new Tile(Tile.Type.gold, false, 3));
                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.Type.gold, false, 4));
                tiles.Add(new Tile(Tile.Type.gold, false, 5));

                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.Type.balloon));
                for (int i = 0; i < 2; i++)
                    tiles.Add(new Tile(Tile.Type.cannon));


                tiles.Add(new Tile(Tile.Type.plane));
                tiles.Add(new Tile(Tile.Type.horse));
                tiles.Add(new Tile(Tile.Type.horse));

                return tiles;
            }

            public BitmapImage[,] GetBitmapImages() // returns two-dimensional array of images for tiles at the board
            {
                var imgs = new BitmapImage[13, 13];

                for (int y = 0; y < 13; y++)
                {
                    for (int x = 0; x < 13; x++)
                    {
                        var currentTile = output[x, y];
                        // if image is null, return empty by default
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
