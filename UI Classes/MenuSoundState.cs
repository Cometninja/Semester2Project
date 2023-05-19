using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Semester2Prototype.Controls;

namespace Semester2Prototype.States
{
    internal class MenuSoundState : State
    {
        private List<Component> _components;


        SpriteFont _smallFont, _titleFont, _buttonFont, _largeButtonFont;
        Texture2D _rectangleTxr, _buttonTexture, _smallButtonTexture;

        static Point _screenSize = new Point(800, 800);


        Rectangle rect;
        public MenuSoundState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {

            // Get the screen dimensions
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            // Calculate the rectangle position
            int rectWidth = 300; // set the width of the rectangle
            int rectHeight = 1000; // set the height of the rectangle
            int rectX = screenWidth / 2 - rectWidth / 2;
            int rectY = screenHeight / 2 - rectHeight / 2;

            rect = new Rectangle(rectX, rectY, rectWidth, rectHeight);

            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");
            _smallButtonTexture = _content.Load<Texture2D>("UI/Controls/Small Button");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _largeButtonFont = _content.Load<SpriteFont>("UI/Fonts/LargeFont");
            _smallFont = _content.Load<SpriteFont>("UI/Fonts/Moldyen");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _titleFont = _content.Load<SpriteFont>("UI/Fonts/TitleMoldyen");

            var plusSongGameButton = new Button(_smallButtonTexture, _largeButtonFont)
            {
                Text = "+",
                Position = new Vector2((_screenSize.X - _smallButtonTexture.Width) / 1.6f, 225)
            };

            plusSongGameButton.Click += PlusSongGameButton_Click;

            var minusSongGameButton = new Button(_smallButtonTexture, _largeButtonFont)
            {
                Text = "-",
                Position = new Vector2((_screenSize.X - _smallButtonTexture.Width) / 2.6f, 225)
            };

            minusSongGameButton.Click += MinusSongGameButton_Click;

            var plusSoundGameButton = new Button(_smallButtonTexture, _largeButtonFont)
            {
                Text = "+",
                Position = new Vector2((_screenSize.X - _smallButtonTexture.Width) / 1.6f, 350)
            };

            plusSoundGameButton.Click += PlusSoundGameButton_Click;

            var minusSoundGameButton = new Button(_smallButtonTexture, _largeButtonFont)
            {
                Text = "-",
                Position = new Vector2((_screenSize.X - _smallButtonTexture.Width) / 2.6f, 350)
            };

            minusSoundGameButton.Click += MinusSoundGameButton_Click;

            var exitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 425),
                Text = "Back",
            };

            exitGameButton.Click += ExitGameButton_Click;

            _components = new List<Component>()
      {
        exitGameButton,
        plusSongGameButton,
        minusSongGameButton,
        plusSoundGameButton,
        minusSoundGameButton,

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

            textSize = _smallFont.MeasureString("Song Volume");
            spriteBatch.DrawString(_smallFont,
                "Song Volume",
                new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("Sounds").X / 2, 175),
                Color.White);

            textSize = _smallFont.MeasureString("Sound Volume");
            spriteBatch.DrawString(_smallFont,
                "Sound Volume",
                new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("Sounds").X / 2, 300),
                Color.White);

            spriteBatch.DrawString(_smallFont, (_game._volume * 100).ToString("0") + "%", new Vector2(_screenSize.X / 2.1f, 225), Color.White);

            spriteBatch.DrawString(_smallFont, (_game._masterVolume * 100).ToString("0") + "%", new Vector2(_screenSize.X / 2.1f, _screenSize.Y / 2.25f), Color.White);



            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            _game.ChangeState(new MenuOptionState(_game, _graphicsDevice, _content));
        }

        private void PlusSongGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            Debug.WriteLine("+");
            _game.AdjustSongVolume(0.1f);
        }

        private void MinusSongGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            Debug.WriteLine("-");
            _game.AdjustSongVolume(-0.1f);
        }

        private void PlusSoundGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            Debug.WriteLine("+");
            _game.AdjustSoundVolume(0.1f);
        }

        private void MinusSoundGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            Debug.WriteLine("-");
            _game.AdjustSoundVolume(-0.1f);
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

