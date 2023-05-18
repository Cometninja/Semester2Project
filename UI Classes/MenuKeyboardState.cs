using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Semester2Prototype.Controls;

namespace Semester2Prototype.States
{
    internal class MenuKeybindState : State
    {
        private List<Component> _components;

        Texture2D _rectangleTxr, _backgroundTxr, _buttonTexture, _keybindTxr;
        SpriteFont _titleFont, _buttonFont;

        static Point _screenSize = new Point(800, 800);
 

        Rectangle rect;

        public MenuKeybindState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {

            // Get the screen dimensions
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            // Calculate the rectangle position
            int rectWidth = 600; // set the width of the rectangle
            int rectHeight = 1000; // set the height of the rectangle
            int rectX = screenWidth / 2 - rectWidth / 2;
            int rectY = screenHeight / 2 - rectHeight / 2;

            rect = new Rectangle(rectX, rectY, rectWidth, rectHeight);

            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _keybindTxr = _content.Load<Texture2D>("UI/KeybindTxr");

            _titleFont = _content.Load<SpriteFont>("UI/Fonts/TitleMoldyen");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");


            var exitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("Options").X / 2, 350),
                Text = "Back",
            };


            exitGameButton.Click += ExitGameButton_Click;

            _components = new List<Component>()
          {
            exitGameButton,
          };
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color tDimGrey = new Color(Color.Black, 175);


            spriteBatch.Draw(_rectangleTxr, rect, tDimGrey); // draw the rectangle centered on the screen along the X-axis

            Vector2 textSize = _titleFont.MeasureString("Title");
            spriteBatch.DrawString(_titleFont,
                "Keybinds",
                new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("Keybinds").X / 2, 100),
                Color.White);

            spriteBatch.Draw(_keybindTxr, new Vector2(rect.X, 150), Color.White);


            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            _game.ChangeState(new MenuOptionState(_game, _graphicsDevice, _content));
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

