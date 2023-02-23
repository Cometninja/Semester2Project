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
        static Random _random = new Random();
        static Moving _moving = Moving.Still;
        static int _animationCount = 0, tickCount, testCount;
        static SpriteFont _mainfont;
        static Player _player;
        static MessageBox _messageBox;
        static bool _isSpacePressed;
        static Tile _playerPos;

        Point _playerPoint = new Point (0, 0);
        Texture2D square,playerSpriteSheet,messageBoxImage;
        Point point= new Point(1000,1000);
        List<Tile> tiles = new List<Tile>();

        
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
            playerSpriteSheet = Content.Load<Texture2D>("chpepper1squirePNG");
            messageBoxImage = Content.Load<Texture2D>("MessageBox");
            _mainfont = Content.Load<SpriteFont>("mainFont");

            for(int col = 0,y = 0; col < point.Y; col += square.Width, y++)
            {
                for(int row = 0,x = 0; row < point.Y; row += square.Width, x++)
                {
                    _sprites.Add(new Tile(square, new Vector2(row, col),new Point(x,y)));
                }
            }
            _player = new Player(playerSpriteSheet,new Vector2(400,250),_playerPoint);
            _sprites.Add( 
                new MessageBox(messageBoxImage, 
                    new Vector2(_graphics.PreferredBackBufferWidth/2, 
                        _graphics.PreferredBackBufferHeight - messageBoxImage.Height/2),
                    _mainfont));
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
            _spriteBatch.End();
        }
        static bool CheckNewTile(Point newTilePoint)
        {
            Tile newTile = _sprites.OfType<Tile>().Where(tile => tile._point == newTilePoint).FirstOrDefault();

            if (newTile._tileState == TileState.Wall)
            {
                return false;
            }
            else return true;

        }
        static void PlayerControls(Player player)
        {
            Point playerPoint = player._point;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (CheckNewTile(new Point(playerPoint.X, playerPoint.Y - 1)))
                {
                    _moving = Moving.Up;
                }
                else _messageBox.AddMessage("You can't walk through walls......");
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (CheckNewTile(new Point(playerPoint.X, playerPoint.Y + 1)))
                {
                    _moving = Moving.Down;
                }
                else _messageBox.AddMessage("You can't walk through walls......");

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (CheckNewTile(new Point(playerPoint.X+1, playerPoint.Y)))
                {
                    _moving = Moving.Right;
                }
                else _messageBox.AddMessage("You can't walk through walls......");

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
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
        }

        static void PlayerMove(Player player)
        {
            switch(_moving)
            {
                
                case Moving.Up:
                    
                    player._sourceRect = GetPlayerImage()[1][_animationCount];
                    player._position.Y--;
                    if (player._position.Y % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[1][0];
                    }
                    break;
                case Moving.Down:
                    player._sourceRect = GetPlayerImage()[0][_animationCount];

                    player._position.Y++;
                    if (player._position.Y % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[0][0];
                    }
                    break;
                case Moving.Right:
                    player._sourceRect = GetPlayerImage()[2][_animationCount];
                    
                    player._position.X++;
                    if (player._position.X % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[2][0];
                    }
                    break;
                case Moving.Left:
                    player._sourceRect = GetPlayerImage()[3][_animationCount];
                    
                    player._position.X--;
                    if (player._position.X % 50 == 0)
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
            animations[0].Add(new Rectangle(32, 0, 32, 32));
            animations[0].Add(new Rectangle(0, 0, 32, 32));
            animations[0].Add(new Rectangle(32, 0, 32, 32));
            animations[0].Add(new Rectangle(64, 0, 32, 32));
            
            // Up animation
            animations[1].Add(new Rectangle(32, 96, 32, 32));
            animations[1].Add(new Rectangle(0, 96, 32, 32));
            animations[1].Add(new Rectangle(32, 96, 32, 32));
            animations[1].Add(new Rectangle(64, 96, 32, 32));
            
            // Right animation
            animations[2].Add(new Rectangle(32, 64, 32, 32));
            animations[2].Add(new Rectangle(0, 64, 32, 32));
            animations[2].Add(new Rectangle(32, 64, 32, 32));
            animations[2].Add(new Rectangle(64, 64, 32, 32));

            // Left animation
            animations[3].Add(new Rectangle(32, 32, 32, 32));
            animations[3].Add(new Rectangle(0, 32, 32, 32));
            animations[3].Add(new Rectangle(32, 32, 32, 32));
            animations[3].Add(new Rectangle(64, 32, 32, 32));
            
            return animations;
        }
    }

    enum Moving { Still,Down,Up,Left,Right }
}