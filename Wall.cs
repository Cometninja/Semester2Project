using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype
{
    internal class Wall : Tile
    {
        Texture2D _wallSpriteSheet,leftWall,rightWall;

        public Wall(Texture2D wallSpriteSheet, Vector2 position, Point point) :base(wallSpriteSheet, position, point) 
        {
            _wallSpriteSheet= wallSpriteSheet;
        }


        public void DrawWall(SpriteBatch spriteBatch)
        {
            //top
            spriteBatch.Draw(_wallSpriteSheet, new Rectangle((int)_position.X,(int)_position.Y,25,50),new Rectangle(0,0,25,50), Color.White);
            //left
            spriteBatch.Draw(_wallSpriteSheet, new Rectangle((int)_position.X + 25,(int)_position.Y,25,50),new Rectangle(25,0,25,50), Color.White);
            //right
            spriteBatch.Draw(_wallSpriteSheet, new Rectangle((int)_position.X + 50,(int)_position.Y,25,50),new Rectangle(50,0,25,50), Color.White);
        }
        




    }
}
