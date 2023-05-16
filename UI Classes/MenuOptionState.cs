using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Semester2Prototype.Controls;

namespace Semester2Prototype.States
{
    internal class MenuOptionState : State
    {
        private List<Component> _components;

        Texture2D _rectangleTxr, _backgroundTxr, _buttonTexture;
        SpriteFont _buttonFont, _titleFont;
        static Point _screenSize = new Point(800, 800);

        static int _rectWidth = 300; // set the width of the rectangle
        static int _rectHeight = 1000; // set the height of the rectangle

        static int _centerX = _screenSize.X / 2; // calculate the X coordinate of the center of the screen

        Rectangle rect = new Rectangle(_centerX - _rectWidth / 2, 0, _rectWidth, _rectHeight); // create the rectangle
        public MenuOptionState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _titleFont = _content.Load<SpriteFont>("UI/Fonts/TitleMoldyen");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _backgroundTxr = _content.Load<Texture2D>("UI/Txr_Background");


            var keybindsGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Text = "Keybinds",
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 200),
            };


            keybindsGameButton.Click += KeybindsGameButton_Click;

            var SoundGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 250),
                Text = "Sound",
            };

            SoundGameButton.Click += SoundGameButton_Click;

            var exitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 300),
                Text = "Back",
            };

            exitGameButton.Click += ExitGameButton_Click;

            _components = new List<Component>()
      {
        keybindsGameButton,
        SoundGameButton,
        exitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            Color tDimGrey = new Color(Color.Black, 175);



            spriteBatch.Draw(_rectangleTxr, rect, tDimGrey); // draw the rectangle centered on the screen along the X-axis

            Vector2 textSize = _titleFont.MeasureString("Title");
            spriteBatch.DrawString(_titleFont,
                "Options",
                new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("Options").X / 2, 100),
                Color.White);
            // 800 x 600 window size



            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);


        }

        private void KeybindsGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            _game.ChangeState(new MenuKeybindState(_game, _graphicsDevice, _content));
        }

        private void SoundGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            _game.ChangeState(new MenuSoundState(_game, _graphicsDevice, _content));
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
                _game.ChangeState(new PauseState(_game, _graphicsDevice, _content));
            }

            else if (!keyboardState.IsKeyDown(Keys.Escape) && _isEscapePressed)
            {
                _isEscapePressed = false;
            }

            foreach (var component in _components)
                component.Update(gameTime);

        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
    }
}



