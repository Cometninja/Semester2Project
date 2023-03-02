using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.OpenGL;

namespace Semester2Prototype
{
    internal class Journal :Sprite
    {
        public bool DisplayJournal = true;
        
        static MessageBox _messageBox;
        static SpriteFont _font;

        
        public Journal(Texture2D image, Vector2 position, SpriteFont font) :base(image, position) 
        {
            _font = font;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DisplayJournal) 
            {
                spriteBatch.Draw(_image,new Vector2(600/2,400/2),null, Color.White,0f,new Vector2(_image.Width/2,_image.Height/2),1f,SpriteEffects.None,0f);
                spriteBatch.DrawString(_font, "This is in the \njournal", new Vector2(140, 100), Color.Black);
            
            }
            else
            {
                spriteBatch.Draw(_image, new Rectangle(0,0, 50,50),null, Color.White);
            }
        }
    }
}
