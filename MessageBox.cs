using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Semester2Prototype
{
    internal class MessageBox : Sprite
    {
        List<string> _messages = new List<string>();
        public SpriteFont _messageBoxFont;
        public MessageBox(Texture2D image, Vector2 position, SpriteFont font) : base(image, position)
        {
            _messageBoxFont = font;
        }
        public override void Update(List<Sprite> sprites)
        {
            base.Update(sprites);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
