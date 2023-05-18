using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace Semester2Prototype.States
{

    public abstract class State
    {

        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        public Game1 _game;

        public Texture2D _image;

        public Vector2 _position, _center;

        public Rectangle _bounds;

        public Rectangle _sourceRect;

        #endregion


        protected bool _isEscapePressed = true;
        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;

        }

        public abstract void Update(GameTime gameTime);

        #endregion
    }
}
