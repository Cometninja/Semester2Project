using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Semester2Prototype
{
    internal class Tile : Sprite
    {
        public Rectangle _centerBox;
        public FloorLevel _floorLevel;
        public Point _point;
        public TileState _tileState = TileState.Empty;
        static Game1 _game1;
        Texture2D _furnitureSheet;
        public Furniture _furniture = Furniture.None;
        public Rectangle _furnitureImage;
        public bool _flipped;
        public Rectangle _drawRect;
        int _imageDrawHeight = 50;
        int _imageDrawWidth = 50;
        float _imageDepth = 0f;
        int _offSet = 0;

        public Tile(Texture2D image, Texture2D furnitureSheet, Vector2 position, Point point, Game1 game1) : base(image, position)
        {
            _drawRect = new Rectangle((int)_position.X - 25, (int)_position.Y - 25, 50, 50);
            _game1 = game1;
            _sourceRect = new Rectangle(45, 55, 8, 8);
            _bounds = new Rectangle((int)_position.X, (int)_position.Y, _image.Width, _image.Height);
            _point = point;
            _center = new Vector2(image.Width / 2, image.Height / 2);
            _centerBox = new Rectangle((int)(_position.X), (int)(_position.Y), 50, 50);
            _floorLevel = _game1._floorLevel;
            _furnitureSheet = furnitureSheet;
        }
        public override void Update(List<Sprite> sprites)
        {
            _drawRect = new Rectangle((int)_position.X - 25, (int)_position.Y - 25 + _offSet, _imageDrawWidth, _imageDrawHeight);
            _centerBox = new Rectangle((int)(_position.X), (int)(_position.Y), 50, 50);
            Player player = sprites.OfType<Player>().First();
            if (_centerBox.Contains(player._center))
            {
                player._point = _point;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_position.X < 1050 && _position.X > -50 && _position.Y < 700 && _position.Y > -150)
            {
                spriteBatch.Draw(
                    _image,
                    new Rectangle((int)_position.X,(int)_position.Y,52,52),
                    _sourceRect,
                    _color,
                    0f,
                    new Vector2(4,4),
                    SpriteEffects.None,
                    0f);
                if (_furniture != Furniture.None)
                {
                    SpriteEffects flipped = SpriteEffects.None;

                    if (_flipped)
                        flipped = SpriteEffects.FlipHorizontally;

                    if (_furniture == Furniture.StairsUp)
                    {
                        Debug.WriteLine(_imageDepth);
                    }
                    spriteBatch.Draw(_furnitureSheet,
                        _drawRect,
                        _furnitureImage,
                        Color.White,
                        0f,
                        new Vector2(0, 0),
                        flipped,
                        _imageDepth);
                }
            }
        }
        public void SetUpFLoorPlan(List<Sprite> sprites)
        {
            List<Clue> clues = sprites.OfType<Clue>().ToList();
            List<NPC> npcs = sprites.OfType<NPC>().ToList();
            _floorLevel = _game1._floorLevel;
            List<List<int>> ints = LayoutRoom(_floorLevel);
            _tileState = TileState.Empty;
            _sourceRect = new Rectangle(45, 55, 8, 8);


            foreach (NPC npc in npcs)
            {
                if (npc._position == _position)
                {
                    _tileState = TileState.Wall;
                    if (_point == new Point(19, 20))
                        Debug.WriteLine(npc._NPCCharacter.ToString());

                }
            }

            foreach (Clue clue in clues)
            {
                if (_centerBox.Contains(clue._center))
                {
                    if (_point == new Point(19, 20))
                        Debug.WriteLine(clue._clueType.ToString());
                    _tileState = TileState.Wall;
                }
            }


            if (_point.X == 0 || _point.X == 29 || _point.Y == 0 || _point.Y == 24)
            {
                _tileState = TileState.Wall;
                _sourceRect = new Rectangle(9, 1, 8, 8);
            }

            if (ints[_point.Y].Contains(_point.X))
            {
                _tileState = TileState.Wall;
                _sourceRect = new Rectangle(9, 1, 8, 8);
            }



            if (_furniture != Furniture.None)
            {
                _tileState = TileState.Wall;
            }
        }

        public void ContainsNPC(List<Sprite> sprites)
        {
            List<Clue> clues = sprites.OfType<Clue>().ToList();

            List<NPC> npcs = sprites.OfType<NPC>().ToList();
            foreach (NPC npc in npcs)
            {
                if (_centerBox.Contains(npc._center))
                {
                    _tileState = TileState.Wall;
                }
            }

            foreach (Clue clue in clues)
            {
                if (_centerBox.Contains(clue._center))
                {
                    _tileState = TileState.Wall;
                }
            }
        }

        public List<List<int>> LayoutRoom(FloorLevel level)
        {
            List<List<int>> ints = new List<List<int>>();

            int[] X0;
            int[] X1;
            int[] X2;
            int[] X3;
            int[] X4;
            int[] X5;
            int[] X6;
            int[] X7;
            int[] X8;
            int[] X9;
            int[] X10;
            int[] X11;
            int[] X12;
            int[] X13;
            int[] X14;
            int[] X15;
            int[] X16;
            int[] X17;
            int[] X18;
            int[] X19;
            int[] X20;
            int[] X21;
            int[] X22;
            int[] X23;
            int[] X24;
            switch (level)
            {
                case FloorLevel.FirstFloor:
                    X0 = new int[] { 0 };
                    X1 = new int[] { 10, 20, 24 };
                    X2 = new int[] { 10, 20, 24 };
                    X3 = new int[] { 10, 20, 24 };
                    X4 = new int[] { 10, 20 };
                    X5 = new int[] { 1, 2, 3, 4, 5, 10, 11, 12, 13, 14, 15, 20, 24 };
                    X6 = new int[] { 5, 10, 15, 20, 24 };
                    X7 = new int[] { 10, 20, 24 };
                    X8 = new int[] { 5, 10, 15, 20, 24, 25, 26, 27, 28 };
                    X9 = new int[] { 5, 10,15,20 };
                    X10 = new int[] { 1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 19, 20 };
                    X11 = new int[] { };
                    X12 = new int[] { 24, 25, 26, 27, 28 };
                    X13 = new int[] { };
                    X14 = new int[] { 1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17, 18, 19, 20 };
                    X15 = new int[] { 5, 10, 15, 20 };
                    X16 = new int[] { 5, 10, 15, 20, 24, 25, 26, 27, 28 };
                    X17 = new int[] { 10, 20, 24 };
                    X18 = new int[] { 5, 10, 15, 20, 24 };
                    X19 = new int[] { 5, 6, 7, 8, 9, 10, 15, 16, 17, 18, 19, 20, 24 };
                    X20 = new int[] { 10, 20 };
                    X21 = new int[] { 10, 20, 24 };
                    X22 = new int[] { 10, 20, 24 };
                    X23 = new int[] { 10, 20, 24 };
                    X24 = new int[] { 0 };
                    break;
                case FloorLevel.SecondFLoor:
                    X0 = new int[] { 0 };
                    X1 = new int[] { 10, 20, 24 };
                    X2 = new int[] { 10, 20, 24 };
                    X3 = new int[] { 10, 20, 24 };
                    X4 = new int[] { 10, 20 };
                    X5 = new int[] { 1, 2, 3, 4, 5, 10, 11, 12, 13, 14, 15, 20, 24 };
                    X6 = new int[] { 5, 10, 15, 20, 24 };
                    X7 = new int[] { 10, 20, 24 };
                    X8 = new int[] { 5, 10, 15, 20, 24, 25, 26, 27, 28 };
                    X9 = new int[] { 5, 10,15,20,24 };
                    X10 = new int[] { 1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 19, 20, 24 };
                    X11 = new int[] { 24 };
                    X12 = new int[] { 24, 25, 26, 27, 28 };
                    X13 = new int[] { };
                    X14 = new int[] { 1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17, 18, 19, 20 };
                    X15 = new int[] { 5, 10, 15, 20 };
                    X16 = new int[] { 5, 10, 15, 20, 24, 25, 26, 27, 28 };
                    X17 = new int[] { 10, 20, 24 };
                    X18 = new int[] { 5, 10, 15, 20, 24 };
                    X19 = new int[] { 5, 6, 7, 8, 9, 10, 15, 16, 17, 18, 19, 20, 24 };
                    X20 = new int[] { 10, 20 };
                    X21 = new int[] { 10, 20, 24 };
                    X22 = new int[] { 10, 20, 24 };
                    X23 = new int[] { 10, 20, 24 };
                    X24 = new int[] { 0 };
                    break;
                default:
                    X0 = new int[] { 0 };
                    X1 = new int[] { 6, 17, 21, 25 };
                    X2 = new int[] { 21, 25 };
                    X3 = new int[] { 6, 17, 21, 25 };
                    X4 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 16, 17, 21, 22, 24, 25, 26, 28 };
                    X5 = new int[] { 6, 17 };
                    X6 = new int[] { 6, 17 };
                    X7 = new int[] { 6, 17 };
                    X8 = new int[] { 6, 17 };
                    X9 = new int[] { 6, 17 };
                    X10 = new int[] { 1, 2, 4, 5, 6, 7, 8, 9, 10, 12, 13, 17, };
                    X11 = new int[] { 13, 17 };
                    X12 = new int[] { 13, 17 };
                    X13 = new int[] { 13, 17 };
                    X14 = new int[] { 1, 2, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15, 16, 17, };
                    X15 = new int[] { 7, 17 };
                    X16 = new int[] { 7, 17, 18, 19, 24, 25, 26, 27, 28 };
                    X17 = new int[] { 7, 17 };
                    X18 = new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                    X19 = new int[] { 0 };
                    X20 = new int[] { 24,25, 26, 27, 28 };
                    X21 = new int[] { 24 };
                    X22 = new int[] { 0 };
                    X23 = new int[] { 24 };
                    X24 = new int[] { 0 };
                    break;
            }

            for (int X = 0; X < 30; X++)
            {
                ints.Add(new List<int>());
            }

            for (int i = 1; i < 29; i++)
            {
                if (X1.Contains(i))
                {
                    ints[1].Add(i);
                }
                if (X2.Contains(i))
                {
                    ints[2].Add(i);
                }
                if (X3.Contains(i))
                {
                    ints[3].Add(i);
                }
                if (X4.Contains(i))
                {
                    ints[4].Add(i);
                }
                if (X5.Contains(i))
                {
                    ints[5].Add(i);
                }
                if (X6.Contains(i))
                {
                    ints[6].Add(i);
                }
                if (X7.Contains(i))
                {
                    ints[7].Add(i);
                }
                if (X8.Contains(i))
                {
                    ints[8].Add(i);
                }
                if (X9.Contains(i))
                {
                    ints[9].Add(i);
                }
                if (X10.Contains(i))
                {
                    ints[10].Add(i);
                }
                if (X11.Contains(i))
                {
                    ints[11].Add(i);
                }
                if (X12.Contains(i))
                {
                    ints[12].Add(i);
                }
                if (X13.Contains(i))
                {
                    ints[13].Add(i);
                }
                if (X14.Contains(i))
                {
                    ints[14].Add(i);
                }
                if (X15.Contains(i))
                {
                    ints[15].Add(i);
                }
                if (X16.Contains(i))
                {
                    ints[16].Add(i);
                }
                if (X17.Contains(i))
                {
                    ints[17].Add(i);
                }
                if (X18.Contains(i))
                {
                    ints[18].Add(i);
                }
                if (X19.Contains(i))
                {
                    ints[19].Add(i);
                }
                if (X20.Contains(i))
                {
                    ints[20].Add(i);
                }
                if (X21.Contains(i))
                {
                    ints[21].Add(i);
                }
                if (X22.Contains(i))
                {
                    ints[22].Add(i);
                }
                if (X23.Contains(i))
                {
                    ints[23].Add(i);
                }
                if (X24.Contains(i))
                {
                    ints[24].Add(i);
                }
            }
            return ints;
        }

        public void SetFurniture()
        {

            switch (_furniture)
            {
                case Furniture.Table:
                    _furnitureImage = new Rectangle(67, 33, 17, 17);
                    break;
                case Furniture.ChairLeft:
                    _flipped = true;
                    _furnitureImage = new Rectangle(272, 205, 18, 18);
                    break;
                case Furniture.ChairRight:
                    _furnitureImage = new Rectangle(272, 205, 18, 18);
                    break;
                case Furniture.ChairDown:
                    _furnitureImage = new Rectangle(272, 238, 18, 18);
                    break;
                case Furniture.ChairUp:
                    _furnitureImage = new Rectangle(272, 222, 18, 18);
                    break;
                case Furniture.Sofa:
                    _furnitureImage = new Rectangle(255, 51, 31, 15);
                    break;
                case Furniture.Toilet:
                    _furnitureImage = new Rectangle(544, 300, 14, 23);
                    break;
                case Furniture.Cabnet:
                    _furnitureImage = new Rectangle(0, 187, 14, 31);
                    break;
                case Furniture.Shelves:
                    _furnitureImage = new Rectangle(102, 307, 15, 31);
                    break;
                case Furniture.Locker:
                    _furnitureImage = new Rectangle(17, 68, 15, 31);
                    break;
                case Furniture.CounterTop:
                    _furnitureImage = new Rectangle(68, 119, 14, 14);
                    break;
                case Furniture.Bed:
                    _furnitureImage = new Rectangle(169, 35, 33, 32);
                    break;
                case Furniture.Shower:
                    _furnitureImage = new Rectangle(407, 289, 14, 33);
                    break;
                case Furniture.CoffeeTable:
                    _furnitureImage = new Rectangle(50, 51, 17, 16);
                    break;
                case Furniture.Wardrobe:
                    _furnitureImage = new Rectangle(101, 34, 17, 33);
                    break;
                case Furniture.Sink:
                    _furnitureImage = new Rectangle(101, 254, 16, 17);
                    break;
                case Furniture.Cuboard:
                    _furnitureImage = new Rectangle();
                    break;
                case Furniture.StairsUp:
                    _flipped = true;
                    _imageDepth = 0.1f;
                    _offSet = -10;
                    _imageDrawHeight = 150 - _offSet;
                    _imageDrawWidth = 200;
                    _furnitureImage = new Rectangle(578,0,40,50);
                    break;
                case Furniture.StairsDown:
                    _imageDepth = 0.1f;
                    _offSet = 0;
                    _imageDrawHeight = 150 - _offSet;
                    _imageDrawWidth = 200;
                    _furnitureImage = new Rectangle(578, 0, 40, 50);
                    break;
                default:
                    _furnitureImage = Rectangle.Empty;
                    break;
            }
        }
    }
}
