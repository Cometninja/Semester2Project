using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2Prototype
{

    internal class Button
    {

        public Button(Texture2D texture, SpriteFont font)
        {

        }
        public string Text { get; set; }

        public Vector2 Position { get; set; }

        public event EventHandler Click;
    }
}
