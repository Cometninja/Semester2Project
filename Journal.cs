using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semester2Prototype
{
    internal class Journal :Sprite
    {
        public bool DisplayJournal = false;

        static MessageBox _messageBox;
        public Journal(Texture2D image, Vector2 position) :base(image, position) 
        { 
        
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DisplayJournal) 
            {
                spriteBatch.Draw(_image, new Rectangle(0,0, 300,300), Color.White);
            }
            else
            {
                spriteBatch.Draw(_image, new Rectangle(0,0, 50,50), Color.White);
            }
        }
    }
}
