using System.Collections.Generic;
using System.Runtime.Intrinsics;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Semester2Prototype
{
    internal class Tile : Sprite
    {
        List<List<int>> ints = new List<List<int>>();

        Rectangle _bounds;
        Rectangle _centerBox;
        Color _origonalColor;
        MessageBox _messageBox;
        bool _messageSent;
        public FloorLevel _floorLevel = FloorLevel.GroundFLoor;

        public Point _point;
        public TileState _tileState = TileState.Empty;
        public Tile(Texture2D image, Vector2 position , Point point) : base(image, position)
        {
            _sourceRect = new Rectangle(1, 1, 50, 50);

            _bounds = new Rectangle((int)_position.X, (int)_position.Y, _image.Width, _image.Height);
            _point = point;
            _center = new Vector2(image.Width / 2, image.Height / 2);
            _centerBox = new Rectangle((int)(_position.X), (int)(_position.Y),50,50);
            _origonalColor = _color;

            ints = LayoutRoom();
        
            SetUpFLoorPlan();
            
        }

        public override void Update(List<Sprite> sprites)
        {

            _centerBox = new Rectangle((int)(_position.X), (int)(_position.Y),50,50);
            Player player = sprites.OfType<Player>().First();
            if (_centerBox.Contains(player._center))
            {
                player._point = _point; 
            }
            base.Update(sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_position.X < 800 && _position.X > 0 && _position.Y < 800 && _position.Y > 0)
            {
                spriteBatch.Draw(
                    _image,
                    _position,
                    _sourceRect,
                    _color,
                    0f,
                    new Vector2(50 / 2, 50 / 2),
                    1f,
                    SpriteEffects.None,
                    1f);
            }
        }

        public void SetUpFLoorPlan()
        {
            switch (_floorLevel)
            {
                case FloorLevel.GroundFLoor:

                    if (_point.X == 0 || _point.X == 29 || _point.Y == 0 || _point.Y == 24)
                    {
                        _tileState = TileState.Wall;
                        _sourceRect = new Rectangle(1, 52, 50, 50);
                    }

                    if (ints[_point.Y].Contains(_point.X))
                    {   
                        _tileState = TileState.Wall;
                        _sourceRect = new Rectangle(1, 52, 50, 50);
                    }



                    if (_point.X == 10 && _point.Y == 5)
                    {
                        _tileState = TileState.Interactive;
                        _origonalColor = Color.Blue;
                        _sourceRect = new Rectangle(52,1,50,50);
                        
                    }
                break;
                case FloorLevel.FirstFloor:
                    break;
                case FloorLevel.SecondFLoor:
                    break;
            }
        }
        public List<List<int>> LayoutRoom()
        {
            List<List<int>> ints = new List<List<int>>();

            int[,] XYWalls = new int[,] { { 6,17,21,25} };

            int[] X0 = new int[] { 0 }; 
            int[] X1 = new int[] { 6,17,21,25};
            int[] X2 = new int[] { 21,25};
            int[] X3 = new int[] {6,17,21,25 };
            int[] X4 = new int[] {1,2,3,4,5,6,7,8,9,10,11,13,14,15,16,17,21,22,24,25,26,28 };
            int[] X5 = new int[] {6,17};
            int[] X6 = new int[] { 6,17};
            int[] X7 = new int[] { 6,17};
            int[] X8 = new int[] { 6,17};
            int[] X9 = new int[] { 6,17};
            int[] X10 = new int[] { 1,2,4,5,6,7,8,9,10,12,13,17,};
            int[] X11 = new int[] {13,17 };
            int[] X12 = new int[] { 13,17};
            int[] X13 = new int[] {13,17 };
            int[] X14 = new int[] { 1,2,4,5,6,7,8,10,11,12,13,14,15,16,17,};
            int[] X15 = new int[] {7,17 };
            int[] X16 = new int[] {7,17,18,19,24,25,26,27,28 };
            int[] X17 = new int[] {7,17 };
            int[] X18 = new int[] {7,8,9,10,11,12,13,14,15,16,17};
            int[] X19 = new int[] { 0};
            int[] X20 = new int[] {25,26,27,28 };
            int[] X21 = new int[] {24 };
            int[] X22 = new int[] { 0};
            int[] X23 = new int[] { 24};
            int[] X24 = new int[] { 0};
            
            
            


            for(int X = 0; X < 30; X++)
            {
                ints.Add(new List<int>());
            }
            

            for(int i  = 1; i < 29; i++)
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
    }
    enum FloorLevel { GroundFLoor,FirstFloor,SecondFLoor}
    enum TileState { Empty,Interactive,Wall}
}
