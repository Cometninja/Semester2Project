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

namespace Semester2Prototype
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        static List<Sprite> _sprites = new List<Sprite>();
        static List<Tile> tiles = new List<Tile>();
        static Random _random = new Random();
        static SpriteFont _mainfont;
        static Player _player;
        static MessageBox _messageBox;
        static Tile _playerPos;
        static Moving _moving = Moving.Still;
        static PlayerFacing _playerFacing = PlayerFacing.Down;
        static int _animationCount = 0, tickCount, testCount;
        static bool _isSpacePressed,_isEPressed;
        static Journal _journal;

        Point _playerPoint = new Point (0, 0);
        Texture2D square,playerSpriteSheet,messageBoxImage,_journalImage, _wallSpriteSheet;
        Point point= new Point(1000,1000);

        Wall _wall;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
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
            _journalImage = Content.Load<Texture2D>("journal");
            _wallSpriteSheet = Content.Load<Texture2D>("walltest");

            for(int col = 0,y = 0; col < point.Y; col += square.Width, y++)
            {
                for(int row = 0,x = 0; row < point.Y; row += square.Width, x++)
                {
                    _sprites.Add(new Tile(square, new Vector2(row, col),new Point(x,y)));
                }
            }

            _wall = new Wall(_wallSpriteSheet, new Vector2(0, 0), new Point(-1, -1));

            _player = new Player(playerSpriteSheet,new Vector2(400,250),_playerPoint);
            _sprites.Add( 
                new MessageBox(messageBoxImage, 
                    new Vector2(_graphics.PreferredBackBufferWidth/2, 
                        _graphics.PreferredBackBufferHeight - messageBoxImage.Height/2),
                    _mainfont));
            _sprites.Add(new Journal(_journalImage,new Vector2(0,0)));
            _player._sourceRect = GetPlayerImage()[0][0];
            _sprites.Add(_player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player = _sprites.OfType<Player>().FirstOrDefault();
            _messageBox = _sprites.OfType<MessageBox>().FirstOrDefault();

            foreach(Sprite sprite in _sprites) 
            {
                sprite.Update(_sprites);
            }

            _playerPos = _sprites.OfType<Tile>().Where(tile => tile._point == _player._point).First();
            tiles = _sprites.OfType<Tile>().ToList();
            
            PlayerMove(_sprites.OfType<Player>().First());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            foreach(Sprite sprite in _sprites) 
            {
                sprite.Draw(_spriteBatch);
            }
            
            base.Draw(gameTime);
            _wall.DrawWall(_spriteBatch);

            _spriteBatch.End();
        }
        static bool CheckNewTile(Point newTilePoint)
        {
            Tile newTile = _sprites.OfType<Tile>().Where(tile => tile._point == newTilePoint).FirstOrDefault();

            if (newTile._tileState != TileState.Empty)
            {
                return false;
            }
            else return true;

        }
        static bool CheckInteractiveTile(Point newTilePoint)
        {
            Tile newTile = _sprites.OfType<Tile>().Where(tile => tile._point == newTilePoint).FirstOrDefault();

            if (newTile._tileState == TileState.Interactive)
            {
                return true;
            }
            else return false;

        }
        static void PlayerControls(Player player)
        {
            Point playerPoint = player._point;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player._sourceRect = GetPlayerImage()[1][0];
                _playerFacing = PlayerFacing.Up;
                if (CheckNewTile(new Point(playerPoint.X, playerPoint.Y - 1)))
                {
                    _moving = Moving.Up;
                }
                else _messageBox.AddMessage("You can't walk through walls......");
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _playerFacing = PlayerFacing.Down;
                player._sourceRect = GetPlayerImage()[0][0];
                if (CheckNewTile(new Point(playerPoint.X, playerPoint.Y + 1)))
                {
                    _moving = Moving.Down;
                }
                else _messageBox.AddMessage("You can't walk through walls......");

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player._sourceRect = GetPlayerImage()[2][0];

                _playerFacing = PlayerFacing.Right;
                if (CheckNewTile(new Point(playerPoint.X+1, playerPoint.Y)))
                {
                    _moving = Moving.Right;
                }
                else _messageBox.AddMessage("You can't walk through walls......");

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                _playerFacing = PlayerFacing.Left;
                player._sourceRect = GetPlayerImage()[3][0];
                
                if (CheckNewTile(new Point(playerPoint.X-1, playerPoint.Y)))
                {
                    _moving = Moving.Left;
                }
                else _messageBox.AddMessage("You can't walk through walls......");
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !_isSpacePressed) 
            {
                _messageBox.AddMessage($"Testing {testCount}.");
                _isSpacePressed = true;
                testCount++;
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.Space) && _isSpacePressed) 
            {
                _isSpacePressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E) && !_isEPressed)
            {
                Point checkPoint = player._point;
                switch (_playerFacing)
                {
                    case PlayerFacing.Up:
                        checkPoint.Y--;
                        break;
                    case PlayerFacing.Down: 
                        checkPoint.Y++;
                        
                        break;
                    case PlayerFacing.Right: 
                        checkPoint.X++;
                        
                        break;
                    case PlayerFacing.Left:
                        checkPoint.X--;
                        
                        break;
                }
                if (CheckInteractiveTile(checkPoint))
                {
                    _messageBox.AddMessage("it an interactive object!!!");
                }
                else
                {
                    _messageBox.AddMessage("its not an interactive object idiot!!!!");
                }
            }
            _journal = _sprites.OfType<Journal>().FirstOrDefault();
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                _journal.DisplayJournal = true;
            }
            else
            {
                _journal.DisplayJournal = false;
            }
        }

        static void PlayerMove(Player player)
        {
            Tile sourceTile = _sprites.OfType<Tile>().FirstOrDefault();
            switch(_moving)
            {
                
                case Moving.Up:
                    
                    player._sourceRect = GetPlayerImage()[1][_animationCount];
                    if (player._position.Y == 150)
                    {
                        foreach (Tile tile in tiles) 
                        { 
                            tile._position.Y++;
                        }
                    }
                    else 
                    {
                        player._position.Y--;
                    }
                    if (player._position.Y % 50 == 0 && sourceTile._position.Y % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[1][0];
                    }
                    break;
                case Moving.Down:
                    player._sourceRect = GetPlayerImage()[0][_animationCount];
                    if (player._position.Y == 350)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.Y--;
                        }
                    }
                    else
                    {
                        player._position.Y++;
                    }
                    if (player._position.Y % 50 == 0 && sourceTile._position.Y % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[0][0];
                    }
                    break;
                case Moving.Right:
                    player._sourceRect = GetPlayerImage()[2][_animationCount];
                    if (player._position.X == 500)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.X--;
                        }
                    }
                    else
                    {
                        player._position.X++;
                    }
                    if (player._position.X % 50 == 0 && sourceTile._position.X % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[2][0];
                    }
                    break;
                case Moving.Left:
                    player._sourceRect = GetPlayerImage()[3][_animationCount];
                    if (player._position.X == 300)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.X++;
                        }
                    }
                    else
                    {
                        player._position.X--;
                    }
                    if (player._position.X % 50 == 0 && sourceTile._position.X % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[3][0];
                    }
                    break;
                    
                default:
                    PlayerControls(player);
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
            { }
        }
        static List<List<Rectangle>> GetPlayerImage()
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
            animations[2].Add(new Rectangle(0, 104, 36, 52));
            animations[2].Add(new Rectangle(36, 104, 36, 52));
            animations[2].Add(new Rectangle(0, 104, 36, 52));
            animations[2].Add(new Rectangle(72, 104, 36, 52));

            // Left animation
            animations[1].Add(new Rectangle(0, 52, 36, 52));
            animations[1].Add(new Rectangle(36, 52, 36, 52));
            animations[1].Add(new Rectangle(0, 52, 36, 52));
            animations[1].Add(new Rectangle(72, 52, 36, 52));
            
            return animations;
        }
    }
    enum Moving { Still,Down,Up,Left,Right }
    enum GameState { GameStart,GamePlaying,JournalScreen}
}