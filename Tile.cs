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
        MessageBox _messageBox;
        bool _messageSent;

        public Point _point;
        public TileState _tileState = TileState.Empty;
        public Tile(Texture2D image, Vector2 position , Point point) : base(image, position)
        {
            _sourceRect = new Rectangle(1, 1, 50, 50);

            _bounds = new Rectangle((int)_position.X, (int)_position.Y, _image.Width, _image.Height);
            _point = point;
            _center = new Vector2(image.Width / 2, image.Height / 2);
            _centerBox = new Rectangle((int)(_position.X), (int)(_position.Y) - 20,40,40);
            _origonalColor = _color;
            if (_point.X == 0 || _point.X == 16 || _point.Y == 0 || _point.Y == 10)
            {
                _tileState = TileState.Wall;
                _sourceRect = new Rectangle(1, 52, 50, 50);
            }

            if (_point.X == 10 && _point.Y == 5)
            {
                _tileState = TileState.Interactive;
                _origonalColor = Color.Blue;
            }
        }

        public override void Update(List<Sprite> sprites)
        {
            _centerBox = new Rectangle((int)(_position.X + _image.Width / 2) - 20, (int)(_position.Y + _image.Width / 2) - 20, 40, 40);
            _messageBox = sprites.OfType<MessageBox>().First();
            Player player = sprites.OfType<Player>().First();
            if (_centerBox.Contains(player._center))
            {
                _color = Color.Red;
                if (!_messageSent)
                {
                    player._point = _point; 
                    _messageBox.AddMessage(_point.ToString());
                    _messageSent = true;
                }
            }
            else
            {
                _color = _origonalColor;
                _messageSent = false;
            }
            base.Update(sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _image,
                _position,
                _sourceRect,
                _color,
                0f,
                new Vector2(50 / 2, 50 / 2),
                1f,
                SpriteEffects.None,
                1f);

        }
    }
    enum TileState { Empty,Interactive,Wall}
}
