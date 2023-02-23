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
            _point= point;
        }

        public override void Update(List<Sprite> sprites)
        {
            _center =  new Vector2(_position.X+16,_position.Y+16);
        }
    }
}
