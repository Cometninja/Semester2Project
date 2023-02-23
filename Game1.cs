using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Sprite> _sprites = new List<Sprite>(); 

        Point _playerPoint = new Point (0, 0);
        Player player;
        Texture2D square,playerSpriteSheet;
        Point point= new Point(1000,1000);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth= point.X;
            _graphics.PreferredBackBufferHeight = point.Y;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            square = Content.Load<Texture2D>("whiteSquare");
            playerSpriteSheet = Content.Load<Texture2D>("chpepper1squirePNG");

            for(int col = 0,y = 0; col < point.Y; col += square.Width, y++)
            {
                for(int row = 0,x = 0; row < point.Y; row += square.Width, x++)
                {
                    _sprites.Add(new Tile(square, new Vector2(row, col),new Point(x,y)));
                }
            }
            player = new Player(playerSpriteSheet,new Vector2(50,50),_playerPoint);
            player._sourceRect = GetPlayerImage()[0];
            _sprites.Add(player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach(Sprite sprite in _sprites) 
            {
                sprite.Update(_sprites);
            }
            Tile playerPos = _sprites.OfType<Tile>().Where(tile => tile._point == player._point).First();
            PlayerControls(_sprites.OfType<Player>().First());
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
                player._position.Y--;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player._position.Y++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player._position.X++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player._position.X--;
            }
        }
        static List<Rectangle> GetPlayerImage()
        {
            // 32x,64x,96x
            // 32y,64y,96y,128y
           
            List<Rectangle> downAnimation = new List<Rectangle>();

            downAnimation.Add(new Rectangle(32, 0, 32, 32));
            downAnimation.Add(new Rectangle(0, 0, 32, 32));
            downAnimation.Add(new Rectangle(32, 0, 32, 32));
            downAnimation.Add(new Rectangle(64, 0, 32, 32));
            return downAnimation;
        }
    }

    enum Moving { Still,Down,Up,Left,Right }
}