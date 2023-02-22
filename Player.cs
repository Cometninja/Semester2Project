using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;




namespace Semester2Prototype
{
    internal class Player : Sprite
    {
        public Point _point;

        public Player(Texture2D image, Vector2 position, Point point):base(image, position) 
        { 
            _center =  new Vector2(position.X+16,position.Y+16);
            _point= point;
        }







    }
}
