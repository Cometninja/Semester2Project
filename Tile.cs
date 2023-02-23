using System.Collections.Generic;
using System.Runtime.Intrinsics;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semester2Prototype
{
    internal class Tile : Sprite
    {
        Rectangle _bounds;
        Rectangle _centerBox;
        Color _origonalColor;

        public Point _point;
        public Tile(Texture2D image, Vector2 position , Point point) : base(image, position)
        {
            _bounds = new Rectangle((int)_position.X, (int)_position.Y, _image.Width, _image.Height);
            _point = point;
            _center = new Vector2(image.Width / 2, image.Height / 2);
            _centerBox = new Rectangle((int)(_position.X + image.Width/2)-20, (int)(_position.Y + image.Width / 2) - 20,40,40);
            _origonalColor = _color;
        }

        public override void Update(List<Sprite> sprites)
        {
            Player player = sprites.OfType<Player>().First();
            if (_centerBox.Contains(player._center))
            {
                _color = Color.Red;
            }
            else
            {
                _color = _origonalColor;
            }
            base.Update(sprites);
        }
    }
}
