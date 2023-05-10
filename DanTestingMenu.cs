using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


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
