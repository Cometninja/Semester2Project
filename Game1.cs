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
using Microsoft.Xna.Framework.Media;
using Semester2Prototype.States;
using System.Xml.Linq;

namespace Semester2Prototype
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        static List<Sprite> _sprites = new List<Sprite>();
        static Random _random = new Random();
        public SpriteFont _mainfont, buttonFont;
        static Player _player;
        static MessageBox _messageBox;
        static Tile _playerPos;
        static Journal _journal;
        public bool _isEscapedPressed;
        static MessageBox _dialogeBox;

        public State _state;

        //
        protected Song song;

        private State _currentState;

        private State _nextState;

        public float _volume = 1f;
        public State _menuState;

        public void ChangeState(State state)
        {
            _menuState = state;
        }
        //


        public GameState _gameState = GameState.MainMenu;


        Point _playerPoint = new Point(0, 0);
        static Texture2D square, playerSpriteSheet, messageBoxImage, _journalImage, _wallSpriteSheet,_floorSpriteSheet, _npcSpriteSheet;
        public Texture2D _rectangleTxr, _backgroundTxr, buttonTexture;
        public Point _windowSize = new Point(1000, 500);
        static Point point = new Point(1500, 1250);

        string Text;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //
            IsMouseVisible = true;
            //
            _graphics.PreferredBackBufferWidth = _windowSize.X;
            _graphics.PreferredBackBufferHeight = _windowSize.Y;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _menuState = new KeybindState(this, GraphicsDevice, Content);
            //
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            song = Content.Load<Song>("Song");

            MediaPlayer.Play(song);

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            //
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            square = Content.Load<Texture2D>("whiteSquare");
            playerSpriteSheet = Content.Load<Texture2D>("DetectiveSpriteSheet");
            messageBoxImage = Content.Load<Texture2D>("MessageBox");
            _mainfont = Content.Load<SpriteFont>("mainFont");
            _journalImage = Content.Load<Texture2D>("LargeJournal");
            _wallSpriteSheet = Content.Load<Texture2D>("walltest");
            _floorSpriteSheet = Content.Load<Texture2D>("FloorTileSpriteSheet");
            _npcSpriteSheet = Content.Load<Texture2D>("NPCspritesheet");

            var buttonTexture = Content.Load<Texture2D>("UI/Controls/Button");
            var buttonFont = Content.Load<SpriteFont>("UI/Fonts/Font");
            _rectangleTxr = Content.Load<Texture2D>("UI/RectangleTxr");
            _backgroundTxr = Content.Load<Texture2D>("UI/Txr_Background");

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
            _sprites.Add(new Journal(_journalImage, new Vector2(0, 0),_mainfont));

            _player.GetDebugImage(square);

            
        }
        //
        void MediaPlayer_MediaStateChanged(object sender, System.
                                         EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            //MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
        }

        public void AdjustVolume(float change)
        {

            _volume += change;

            // Constrain volume to range [0, 1]
            if (_volume < 0)
            {
                _volume = 0;
            }
            else if (_volume > 1)
            {
                _volume = 1;
            }

            // Set volume for MediaPlayer
            MediaPlayer.Volume = _volume;

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //
        protected override void Update(GameTime gameTime)
        {
            //
            KeyboardState keyboardState = Keyboard.GetState();
            //

            

            
            _messageBox = _sprites.OfType<MessageBox>().FirstOrDefault();
            
            foreach (Sprite sprite in _sprites)
            {
                sprite.Update(_sprites);
            }

            switch (_gameState)
            {
                case GameState.MainMenu:
                    _menuState.Update(gameTime);
                    break;
                case GameState.GameStart:
                    break;
                case GameState.GamePlaying:
                    _playerPos = _sprites.OfType<Tile>().Where(tile => tile._point == _player._point).First();
                    MoveThePlayer();
                   
                    break;
                case GameState.JournalScreen:
                    break;
                case GameState.Dialoge:
                    _player._dialoge.DialogeUpdate();
                    DialogueControls();
                    break;
                
            }
           

            //
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);
            //

            //
            if (keyboardState.IsKeyDown(Keys.Escape) && !_isEscapedPressed)
            {
                _menuState = new PauseState(this, GraphicsDevice, Content);
                _isEscapedPressed = true;
                _gameState = GameState.MainMenu;
            }
            else if (!keyboardState.IsKeyDown(Keys.Escape) && _isEscapedPressed)
            {
                _isEscapedPressed = false;
            }
            //
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
                case GameState.MainMenu:
                    _menuState.Draw(gameTime,_spriteBatch);
                    break;
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

     
        public void DialogueControls()
        {
            NPC npc = _sprites.OfType<NPC>().Where(x => x._dialoge == true).FirstOrDefault();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)&& !_isEscapedPressed)
            {
                _isEscapedPressed = true;
                npc._moving = npc._lastMove;
                _gameState = GameState.GamePlaying;
            }

            
        }


    }

    
}
