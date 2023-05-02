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

        static List<Sprite> _sprites = new List<Sprite>();
        static Random _random = new Random();
        static SpriteFont _mainfont;
        static Player _player;
        static MessageBox _messageBox;
        static Tile _playerPos;
        static Journal _journal;
        static bool _isEscapedPressed;
        static MessageBox _dialogeBox;
        static DanTestingMenu _danTestingMenu;

        static GameState _gameState = GameState.GamePlaying;


        Point _playerPoint = new Point(0, 0);
        
        public FloorLevel _floorLevel = FloorLevel.GroundFLoor;
        static Texture2D square, playerSpriteSheet, messageBoxImage, _journalImage, _wallSpriteSheet,_floorSpriteSheet, _npcSpriteSheet;
        public Point _windowSize = new Point(1000, 500);
        static Point point = new Point(1500, 1250);

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
            _dialogeBox = new MessageBox(messageBoxImage,
                    new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight - messageBoxImage.Height / 2),
                    _mainfont);


            _player = new Player(playerSpriteSheet, new Vector2(400, 250), _playerPoint);
            _sprites.Add(
                new MessageBox(messageBoxImage,
                    new Vector2(_graphics.PreferredBackBufferWidth / 2,
                        _graphics.PreferredBackBufferHeight - messageBoxImage.Height / 2),
                    _mainfont));
            _sprites.Add(_player);


            _sprites.Add(new NPC(_npcSpriteSheet,new Vector2(600,250),"bob"));
            _sprites.Add(new NPC(_npcSpriteSheet,new Vector2(650,250),"Dan"));
            _sprites.Add(new NPC(_npcSpriteSheet,new Vector2(700,250),"Linus"));
            _sprites.Add(new NPC(_npcSpriteSheet,new Vector2(750,250),"Kyle"));
            _sprites.Add(new NPC(_npcSpriteSheet,new Vector2(800,250),"Oscar"));
           // _sprites.Add(new NPC(_npcSpriteSheet,new Vector2(850,250),"Nick"));






            _sprites.Add(new Journal(_journalImage, new Vector2(0, 0),_mainfont));


            _danTestingMenu = new DanTestingMenu(this);

            _player.GetDebugImage(square);
        }

        protected override void Update(GameTime gameTime)
        {

            _messageBox = _sprites.OfType<MessageBox>().FirstOrDefault();
            
            foreach (Sprite sprite in _sprites)
            {
                sprite.Update(_sprites);
            }

            switch (_gameState)
            {
                case GameState.GameStart:
                    _danTestingMenu.Update(gameTime);
                    break;
                case GameState.GamePlaying:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_isEscapedPressed)
                        Exit();
                    else if (Keyboard.GetState().IsKeyUp(Keys.Escape) && _isEscapedPressed)
                    {
                        _isEscapedPressed = false;
                    }
                    _playerPos = _sprites.OfType<Tile>().Where(tile => tile._point == _player._point).First();
                    MoveThePlayer();
                    CheckChangeLevel();
                    break;
                case GameState.JournalScreen:
                    break;
                case GameState.Dialoge:
                    _player._dialoge.DialogeUpdate();
                    DialogueControls();
                    break;
            }
            GameStateChange(_sprites);
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
            switch (_gameState)
            {
                case GameState.GameStart:
                    break;
                case GameState.GamePlaying:
                    break;
                case GameState.JournalScreen:
                    break;
                case GameState.Dialoge:
                    _player._dialoge.DialogeDraw(_spriteBatch);


                    break;
            }


            base.Draw(gameTime);
            //_wall.DrawWall(_spriteBatch);

            _spriteBatch.End();
        }
        public void MakeFloorPlan()
        {
            for (int col = 0, y = 0; col < point.Y; col += 50, y++)
            {
                for (int row = 0, x = 0; row < point.X; row += 50, x++)
                {
                    _sprites.Add(new Tile(_floorSpriteSheet, new Vector2(row, col), new Point(x, y),this));
                }
            }
        }
        static void MoveThePlayer()
        {
            _player.PlayerMove(_player);
        }

        static void GameStateChange(List<Sprite> sprites)
        {
            if (_player._changeGameState)
            {
                _gameState = _player._gameState;
                _player._changeGameState = false;
            }
        }
        static void DialogueControls()
        {
            NPC npc = _sprites.OfType<NPC>().Where(x => x._dialoge == true).FirstOrDefault();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)&& !_isEscapedPressed)
            {
                _isEscapedPressed = true;
                npc._moving = npc._lastMove;
                _gameState = GameState.GamePlaying;
            }
        }

        public void CheckChangeLevel()
        {
            switch (_floorLevel)
            {
                case FloorLevel.GroundFLoor:
                    int[] groundUp = new int[] { 17, 18, 19 };
                    if (_player._point.X == 24 && groundUp.Contains(_player._point.Y))
                    {
                        _messageBox.AddMessage("going UP");
                        _floorLevel = FloorLevel.FirstFloor;
                    }
                    
                    break;
                case FloorLevel.FirstFloor:
                    int[] firstDown = new int[] { 13,14,15 };
                    int[] firstUp = new int[] { 9,10,11 };
                    if (_player._point.X == 24 && firstDown.Contains(_player._point.Y))
                    {
                        _messageBox.AddMessage("going Down");
                        _floorLevel = FloorLevel.GroundFLoor;
                    }
                    else if (_player._point.X == 24 && firstUp.Contains(_player._point.Y))
                    {
                        _messageBox.AddMessage("going UP");
                        _floorLevel = FloorLevel.SecondFLoor;
                    }
                    break;

                case FloorLevel.SecondFLoor:
                    int[] SecondDown = new int[] { 13, 14, 15 };

                    if (_player._point.X == 24 && SecondDown.Contains(_player._point.Y))
                    {
                        _messageBox.AddMessage("going Down");
                        _floorLevel = FloorLevel.FirstFloor;
                    }
                    break;
            }



        }
    
    }
}