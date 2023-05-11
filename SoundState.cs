using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Semester2Prototype.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Semester2Prototype.States
{
    internal class SoundState : State
    {
        private List<Component> _components;


        SpriteBatch _spriteBatch;
        SpriteFont _smallFont, _titleFont, _buttonFont;
        Texture2D _rectangleTxr, _backgroundTxr, _buttonTexture, _smallButtonTexture;
        Point _screenSize = new Point(800, 800);
        public SoundState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");
            _smallButtonTexture = _content.Load<Texture2D>("UI/Controls/Small Button");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _smallFont = _content.Load<SpriteFont>("UI/Fonts/Moldyen");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _titleFont = _content.Load<SpriteFont>("UI/Fonts/TitleMoldyen");

            var plusGameButton = new Button(_smallButtonTexture, _buttonFont)
            {
                Text = "+",
                Position = new Vector2((_screenSize.X - _smallButtonTexture.Width) / 1.6f, 250)
            };

            plusGameButton.Click += PlusGameButton_Click;

            var minusGameButton = new Button(_smallButtonTexture, _buttonFont)
            {
                Text = "-",
                Position = new Vector2((_screenSize.X - _smallButtonTexture.Width) / 2.5f, 250)
            };

            minusGameButton.Click += MinusGameButton_Click;

            var exitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((_screenSize.X - _buttonTexture.Width) / 2, 300),
                Text = "Back",
            };

            exitGameButton.Click += ExitGameButton_Click;

            _components = new List<Component>()
      {
        exitGameButton,
        plusGameButton,
        minusGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            

            Color tDimGrey = new Color(Color.Black, 175);

            int rectWidth = 400; // set the width of the rectangle
            int rectHeight = _screenSize.Y; // set the height of the rectangle

            int rectX = (_screenSize.X - rectWidth) / 2; // calculate the X coordinate to center the rectangle
            int rectY = 0; // set the Y coordinate of the rectangle position

            Rectangle rect = new Rectangle(rectX, rectY, rectWidth, rectHeight); // create the rectangle

            spriteBatch.Draw(_rectangleTxr, rect, tDimGrey); // draw the rectangle centered on the screen along the X-axis

            Vector2 textSize = _titleFont.MeasureString("Sounds");
            spriteBatch.DrawString(_titleFont,
                "Sounds",
                new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("Sounds").X / 2, 100),
                Color.White);

            textSize = _smallFont.MeasureString("Master Volume");
            spriteBatch.DrawString(_smallFont,
                "Master Volume",
                new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("Master Volume").X / 2, 200),
                Color.White);

            spriteBatch.DrawString(_smallFont, (_game._volume * 100).ToString() + "%", new Vector2(_screenSize.X / 2.1f, _screenSize.Y / 3.15f), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new OptionState(_game, _graphicsDevice, _content));
        }

        private void PlusGameButton_Click(object sender, EventArgs e)
        {

            Debug.WriteLine("+");
            _game.AdjustVolume(0.25f);          
        }

        private void MinusGameButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("-");
            _game.AdjustVolume(-0.25f);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape) && !_isEscapePressed)
            {
                _isEscapePressed = true;
                _game.ChangeState(new OptionState(_game, _graphicsDevice, _content));
            }

            else if (!keyboardState.IsKeyDown(Keys.Escape) && _isEscapePressed)
            {
                _isEscapePressed = false;
            }

            foreach (var component in _components)
                component.Update(gameTime);

        }
    }
}

