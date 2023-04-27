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


namespace Semester2Prototype
{
    internal class DanTestingMenu
    {
        static Vector2 mousepos;

        static Texture2D texture;

        GameState gameState;
        Game1 _parent;

        public DanTestingMenu(Game1 parent) 
        {
          _parent = parent;
        }

        public void Update(GameTime gameTime)
        {
            mousepos = Mouse.GetState().Position.ToVector2();
        }

        public void Draw(SpriteBatch spriteBatch) 
        { 
            
        }


    }
}
