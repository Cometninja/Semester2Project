﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections;
using System;
using System.Diagnostics;
using System.IO;

namespace Semester2Prototype
{
    internal class NPC : Sprite
    {

        List<List<Rectangle>> _rectangles = new List<List<Rectangle>>();
        Vector2 _startingPosition;
        public Moving _moving = Moving.Still;
        public Moving _lastMove;
        public Facing _facing = Facing.Down;
        public Point _NPCPoint;
        public string _name;
        Random _random = new Random();
        float _speed = 0.5f;
        static int _animationCount = 0, tickCount, testCount;
        List<Sprite> _sprites = new List<Sprite>();
        public bool _dialoge = false;
        public string[] _npcDialoge;

        public NPC(Texture2D image, Vector2 position,string name) : base(image, position) 
        {
            _name = name;
            _startingPosition = position;
            _rectangles = GetNPCImage();
            _sourceRect = _rectangles[0][0];
            _npcDialoge = new string[] { $"{_name}", "nothing", "Something","Nick Did it!!"};
            
        }
        public override void Update(List<Sprite> sprites)
        {
            _center = new Vector2(_position.X + 16, _position.Y + 30);

            _sprites = sprites;
            //NPC_Controls();
            //NPC_Move();


            base.Update(sprites);
        }
        static List<List<Rectangle>> GetNPCImage()
        {
            // 32x,64x,96x
            // 32y,64y,96y,128y

            List<List<Rectangle>> animations = new List<List<Rectangle>>();


            for (int i = 0; i < 4; i++)
            {
                animations.Add(new List<Rectangle>());
            }
            // Down animation
            animations[0].Add(new Rectangle(0, 0, 36, 52));
            animations[0].Add(new Rectangle(36, 0, 36, 52));
            animations[0].Add(new Rectangle(0, 0, 36, 52));
            animations[0].Add(new Rectangle(72, 0, 36, 52));

            // Up animation
            animations[3].Add(new Rectangle(0, 156, 36, 52));
            animations[3].Add(new Rectangle(36, 156, 36, 52));
            animations[3].Add(new Rectangle(0, 156, 36, 52));
            animations[3].Add(new Rectangle(72, 156, 36, 52));

            // Right animation
            animations[2].Add(new Rectangle(0, 106, 36, 52));
            animations[2].Add(new Rectangle(36, 106, 36, 52));
            animations[2].Add(new Rectangle(0, 106, 36, 52));
            animations[2].Add(new Rectangle(72, 106, 36, 52));

            // Left animation
            animations[1].Add(new Rectangle(0, 52, 36, 52));
            animations[1].Add(new Rectangle(36, 52, 36, 52));
            animations[1].Add(new Rectangle(0, 52, 36, 52));
            animations[1].Add(new Rectangle(72, 52, 36, 52));

            return animations;
        }
        public void NPC_Move()
        {
            switch (_moving)
            {
                case Moving.Up:
                    _sourceRect = GetNPCImage()[1][_animationCount];
                    _facing = Facing.Up; 
                    _position.Y -= _speed;
                    break;
                case Moving.Down:
                    _sourceRect = GetNPCImage()[0][_animationCount];
                    _facing = Facing.Down; 
                    _position.Y += _speed;
                    break;
                case Moving.Left:
                    _sourceRect = GetNPCImage()[3][_animationCount];
                    _facing = Facing.Left; 
                    _position.X -= _speed;
                    break;
                case Moving.Right:
                    _sourceRect = GetNPCImage()[2][_animationCount];
                    _facing = Facing.Right; 
                    _position.X += _speed;
                    break;
                default:
                    
                    break;

            }

            
            tickCount++;
            if (tickCount % 10 == 0)
            {
                if (_animationCount == 3)
                {
                    _animationCount = 0;
                }
                else
                {
                    _animationCount++;
                }
            }
            NPC_Controls();
        }
        public void NPC_Controls()
        {
            Player player = _sprites.OfType<Player>().FirstOrDefault();
            Tile tile = _sprites.OfType<Tile>().FirstOrDefault();
            //TODO add in NPC Moving
            if ( (_position.X - tile._position.X) % 50 == 0 && (_position.Y - tile._position.Y) % 50 == 0) 
            {
                CheckNextTile();
            }
        }
        public void CheckNextTile()
        {
            Point nextTilePoint = Point.Zero;
            foreach(Tile tile in _sprites.OfType<Tile>().ToList())
            {
                if (tile._centerBox.Contains(this._center))
                {
                    this._NPCPoint = tile._point;
                    nextTilePoint = _NPCPoint;
                    break;
                }
            }
            switch (this._facing)
            {
                case Facing.Up:
                    nextTilePoint.Y--;
                    break;
                case Facing.Down: 
                    nextTilePoint.Y++;
                    break;
                case Facing.Left:
                    nextTilePoint.X--;
                    break;
                case Facing.Right:
                    nextTilePoint.X++;
                    break;
            }

            Tile nextTile = _sprites.OfType<Tile>().Where(tile => tile._point == nextTilePoint).First();

            if (nextTile != null) 
            { 
                if (nextTile._tileState != TileState.Empty) 
                {
                    Moving placeholder = _moving;
                    while(_moving == placeholder) 
                    { 
                        _moving = (Moving)_random.Next(1,5);
                    }
                }
            }
        }

        public void StartDialog()
        {
            _dialoge = true;
            _lastMove = _moving;
            this._moving = Moving.Still;
        }

        static string[] GetDialoge()
        {


            //string data = File.ReadAllText("./Content/NPCDialoge.txt");


            //string[] results = data.Split('-');


            return new string[] { "1","2","3","4"};
        }
    }
}