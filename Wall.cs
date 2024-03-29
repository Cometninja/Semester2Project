﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype
{
    internal class Wall : Tile
    {
        Texture2D _wallSpriteSheet, leftWall, rightWall;
        int number = 0;
        public Wall(Texture2D wallSpriteSheet, Vector2 position, Point point) : base(wallSpriteSheet, position, point, new Game1())
        {
            _wallSpriteSheet = wallSpriteSheet;
        }


        public void DrawWall(SpriteBatch spriteBatch)
        {
            //left
            spriteBatch.Draw(_wallSpriteSheet, new Rectangle((int)_position.X, (int)_position.Y, 25 - number, 50), new Rectangle(25, 0, 25, 50), Color.White);
            //right
            spriteBatch.Draw(_wallSpriteSheet, new Rectangle((int)_position.X + 25 - number, (int)_position.Y, 25 + number, 50), new Rectangle(50, 0, 25, 50), Color.White);
            //top
            // spriteBatch.Draw(_wallSpriteSheet, new Rectangle((int)_position.X - number,(int)_position.Y,50 + (number *2),50),new Rectangle(0,0,25,50), Color.White);
        }

        public override void Update(List<Sprite> sprites)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                number++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                number--;
            }
        }
    }
}
