using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Threading;

namespace Semester2Prototype
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        static VarCollection _varCollection = new VarCollection();
        static List<Sprite> _sprites = new List<Sprite>();
        static Random _random = new Random();
        static SpriteFont _mainfont;
        static Player _player;
        static MessageBox _messageBox;
        static Tile _playerPos;
        static Journal _journal;
        Thread thread;
        static bool _threadStarted = false;

        Point _playerPoint = new Point(0, 0);
        static Texture2D square, playerSpriteSheet, messageBoxImage, _journalImage, _wallSpriteSheet,_floorSpriteSheet, _npcSpriteSheet;

        public Point _windowSize = new Point(1000, 500);
        static Point point = new Point(1500, 1250);

        Wall _wall;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _windowSize.X;
            _graphics.PreferredBackBufferHeight = _windowSize.Y;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            square = Content.Load<Texture2D>("whiteSquare");
            playerSpriteSheet = Content.Load<Texture2D>("DetectiveSpriteSheet");
            messageBoxImage = Content.Load<Texture2D>("MessageBox");
            _mainfont = Content.Load<SpriteFont>("mainFont");
            _journalImage = Content.Load<Texture2D>("LargeJournal");
            _wallSpriteSheet = Content.Load<Texture2D>("walltest");
            _floorSpriteSheet = Content.Load<Texture2D>("FloorTileSpriteSheet");
            _npcSpriteSheet = Content.Load<Texture2D>("NPCspritesheet");

            MakeFloorPlan();
           


            _player = new Player(playerSpriteSheet, new Vector2(400, 250), _playerPoint);
            _sprites.Add(
                new MessageBox(messageBoxImage,
                    new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight - messageBoxImage.Height / 2),
                    _mainfont));
            _sprites.Add(_player);
            _sprites.Add(new NPC(_npcSpriteSheet,new Vector2(600,250)));
            _sprites.Add(new Journal(_journalImage, new Vector2(0, 0),_mainfont));

            _player.GetDebugImage(square);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _messageBox = _sprites.OfType<MessageBox>().FirstOrDefault();

            foreach (Sprite sprite in _sprites)
            {
                sprite.Update(_sprites);
            }
            //_wall.Update(_sprites);

            _playerPos = _sprites.OfType<Tile>().Where(tile => tile._point == _player._point).First();
            //if (!_threadStarted)
            //{
            //    thread.Start();
            //    _threadStarted = true;
            //}
            MoveThePlayer();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw(_spriteBatch);
            }

            base.Draw(gameTime);
            //_wall.DrawWall(_spriteBatch);

            _spriteBatch.End();
        }
        static void MakeFloorPlan()
        {
            for (int col = 0, y = 0; col < point.Y; col += 50, y++)
            {
                for (int row = 0, x = 0; row < point.X; row += 50, x++)
                {
                    _sprites.Add(new Tile(_floorSpriteSheet, new Vector2(row, col), new Point(x, y)));
                }
            }
        }
        static void MoveThePlayer()
        {
            _player.PlayerMove(_player);
        }
    }

    
}