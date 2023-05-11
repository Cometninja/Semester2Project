using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype.States
{
    public class GameStatePlaying : State
    {

        Texture2D journalImage, _rectangleTxr, _backgroundTxr;
        SpriteFont smallJournalFont;
        Point _screenSize = new Point(800, 800);
        bool isPauseState;

        string[] journalFeatures = { "Feature 1", "Feature 2", "Feature 3" };
        string[] journalFeatures2 = { "Feature 4", "Feature 5", "Feature 6" };

        public GameStatePlaying(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            journalImage = _content.Load<Texture2D>("LargeJournal");
            smallJournalFont = _content.Load<SpriteFont>("Ui/Fonts/SmallMoldyen");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _backgroundTxr = _content.Load<Texture2D>("UI/Txr_Background");


            // Draw journal image
            spriteBatch.Draw(_backgroundTxr, new Rectangle(0, 0, _screenSize.X, _screenSize.Y), Color.White);
            spriteBatch.Draw(journalImage, new Vector2(200, 50), Color.White);

            // Draw journal features
            Vector2 featurePosition = new Vector2(250, 275);
            foreach (string feature in journalFeatures)
            {
                spriteBatch.DrawString(smallJournalFont, feature, featurePosition, Color.Black);
                featurePosition.Y += smallJournalFont.LineSpacing * 2;
            }

            Vector2 featurePosition2 = new Vector2(475, 275);
            foreach (string feature in journalFeatures2)
            {
                spriteBatch.DrawString(smallJournalFont, feature, featurePosition2, Color.Black);
                featurePosition2.Y += smallJournalFont.LineSpacing * 2;
            }

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape) && !_isEscapePressed)
            {
                _isEscapePressed = true;
                _game.ChangeState(new PauseState(_game, _graphicsDevice, _content));
            }

            else if (!keyboardState.IsKeyDown(Keys.Escape) && _isEscapePressed)
            {
                _isEscapePressed = false;
            }
        }
    }
}
