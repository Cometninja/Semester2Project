using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Semester2Prototype.Controls;

namespace Semester2Prototype.States
{
    public class PauseState : State
    {
        private List<Component> _components;

        Texture2D _rectangleTxr, _backgroundTxr, _buttonTexture;
        SpriteFont _buttonFont, _titleFont;

        static Point _screenSize = new Point(800, 800);

        static int _rectWidth = 300; // set the width of the rectangle
        static int _rectHeight = 1000; // set the height of the rectangle

        static int _centerX = _screenSize.X / 2; // calculate the X coordinate of the center of the screen

        Rectangle rect = new Rectangle(_centerX - _rectWidth / 2, 0, _rectWidth, _rectHeight); // create the rectangle

        public PauseState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _titleFont = _content.Load<SpriteFont>("UI/Fonts/TitleMoldyen");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");


            var resumeGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Text = "Resume",
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 200),
            };


            resumeGameButton.Click += ResumeGameButton_Click;

            var optionGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 250),
                Text = "Options",
            };

            optionGameButton.Click += OptionGameButton_Click;

            var exitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 300),
                Text = "Main menu",
            };

            exitGameButton.Click += ExitGameButton_Click;

            _components = new List<Component>()
      {
        resumeGameButton,
        optionGameButton,
        exitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {


            Color tDimGrey = new Color(Color.Black, 175);

            int rectWidth = 300; // set the width of the rectangle
            int rectHeight = _screenSize.Y; // set the height of the rectangle

            int rectX = (_screenSize.X - rectWidth) / 2; // calculate the X coordinate to center the rectangle
            int rectY = 0; // set the Y coordinate of the rectangle position

            Rectangle rect = new Rectangle(rectX, rectY, rectWidth, rectHeight); // create the rectangle

            spriteBatch.Draw(_rectangleTxr, rect, tDimGrey); // draw the rectangle centered on the screen along the X-axis

            Vector2 textSize = _titleFont.MeasureString("Title");
            spriteBatch.DrawString(_titleFont,
                "Paused",
                new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("Paused").X / 2, 100),
                Color.White);


            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);


        }

        private void OptionGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new OptionState(_game, _graphicsDevice, _content));
        }

        private void ResumeGameButton_Click(object sender, EventArgs e)
        {
            _game._gameState = GameState.GamePlaying;
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();

            /*if (keyboardState.IsKeyDown(Keys.Escape) && !_isEscapePressed)
            {
                _isEscapePressed = true;
                _game.ChangeState(new GameStatePlaying(_game, _graphicsDevice, _content));
            }

            else if (!keyboardState.IsKeyDown(Keys.Escape) && _isEscapePressed)
            {
                _isEscapePressed = false;
            }*/

            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
    }
}
