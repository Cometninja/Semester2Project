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
        private List<Sprite> _sprites = new List<Sprite>();

        static Random random = new Random();
        static Moving moving = Moving.Still;
        static int animationCount = 0, tickCount;
        static SpriteFont _mainfont;
        static Player player;
        static MessageBox messageBox;

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
            player = new Player(playerSpriteSheet,new Vector2(50,50),_playerPoint);
            _sprites.Add( new MessageBox(messageBoxImage, new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight / 2),_mainfont));

            
            player._sourceRect = GetPlayerImage()[0][0];
            _sprites.Add(player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player = _sprites.OfType<Player>().FirstOrDefault();
            messageBox = _sprites.OfType<MessageBox>().FirstOrDefault();
            
            
            

            foreach(Sprite sprite in _sprites) 
            {
                sprite.Update(_sprites);
            }
            
            Tile playerPos = _sprites.OfType<Tile>().Where(tile => tile._point == player._point).First();
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
        static void PlayerControls(Player player)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                moving = Moving.Up;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                moving = Moving.Down;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                moving = Moving.Right;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                moving = Moving.Left;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space)) 
            {
                messageBox.AddMessage("Testing");
            }
        }

        static void PlayerMove(Player player)
        {
            switch(moving)
            {
                
                case Moving.Up:
                    
                    player._sourceRect = GetPlayerImage()[1][animationCount];
                    player._position.Y--;
                    if (player._position.Y % 50 == 0)
                    {
                        moving = Moving.Still;
                    }
                    break;
                case Moving.Down:
                    player._sourceRect = GetPlayerImage()[0][animationCount];

                    player._position.Y++;
                    if (player._position.Y % 50 == 0)
                    {
                        moving = Moving.Still;
                    }
                    break;
                case Moving.Right:
                    player._sourceRect = GetPlayerImage()[2][animationCount];
                    
                    player._position.X++;
                    if (player._position.X % 50 == 0)
                    {
                        moving = Moving.Still;
                    }
                    break;
                case Moving.Left:
                    player._sourceRect = GetPlayerImage()[3][animationCount];
                    
                    player._position.X--;
                    if (player._position.X % 50 == 0)
                    {
                        moving = Moving.Still;
                    }
                    break;
                    
                default:
                    PlayerControls(player);
                    break;
            }
            
            tickCount++;
            if (tickCount % 10 == 0)
            {

                if (animationCount == 3)
                {
                    animationCount = 0;
                }
                else
                {
                    animationCount++;
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