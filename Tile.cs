using System.Collections.Generic;
using System.Runtime.Intrinsics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semester2Prototype
{
    internal class Tile : Sprite
    {
        Rectangle _bounds;
        public Point _point;
        public Tile(Texture2D image, Vector2 position , Point point) : base(image, position)
        {
            _bounds = new Rectangle((int)_position.X, (int)_position.Y, _image.Width, _image.Height);
            _point = point;
            _center = new Vector2(position.X+ (image.Width/2), position.Y + (image.Height / 2));
        }

        public override void Update(List<Sprite> sprites)
        {
            base.Update(sprites);
        }
    }
}
