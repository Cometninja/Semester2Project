using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype
{
    internal class Sprite
    {
        public Texture2D _image;
        public Vector2 _position,_center;
        public Color _color = Color.White;
        public Rectangle _bounds;
        public Rectangle _sourceRect;


        
        public Sprite(Texture2D image, Vector2 position)
        {
            _image = image;
            _position = position;
            _sourceRect = new Rectangle(0, 0, image.Width, image.Height);
            _bounds = new Rectangle((int)_position.X, (int)_position.Y, _sourceRect.Width, _sourceRect.Height);
            _center = new Vector2(_sourceRect.Width / 2,_sourceRect.Height / 2);

        }


        public virtual void Update(List<Sprite> sprites)
        {
          
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _image, 
                _position, 
                _sourceRect, 
                _color,
                0f,
                new Vector2(_sourceRect.Width/2,_sourceRect.Height/2),
                1f,
                SpriteEffects.None,
                1f);


        }

    }
}
