using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Semester2Prototype.Controls;

namespace Semester2Prototype.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        Texture2D _rectangleTxr, _buttonTexture;
        SpriteFont _titleFont, _buttonFont;

        Rectangle rect;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _titleFont = _content.Load<SpriteFont>("UI/Fonts/TitleMoldyen");

            // Get the screen dimensions
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            // Calculate the rectangle position
            int rectWidth = 300; // set the width of the rectangle
            int rectHeight = 1000; // set the height of the rectangle
            int rectX = screenWidth / 2 - rectWidth / 2;
            int rectY = screenHeight / 2 - rectHeight / 2;

            rect = new Rectangle(rectX, rectY, rectWidth, rectHeight);


            var newGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Text = "New Game",
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 200),
            };


            newGameButton.Click += NewGameButton_Click;

            var optionGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 275),
                Text = "Options",
            };

            optionGameButton.Click += OptionGameButton_Click;

            var quitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((rect.X + _titleFont.MeasureString("New Game").X / 2), 350),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
        {
            newGameButton,
            optionGameButton,
            quitGameButton,
        };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color tDimGrey = new Color(Color.Black, 175);

            spriteBatch.Draw(_rectangleTxr, rect, tDimGrey);

            Vector2 textSize = _titleFont.MeasureString("Title");
            spriteBatch.DrawString(_titleFont,
                "10 SUSpects",
                new Vector2(rect.X + rect.Width / 2 - _titleFont.MeasureString("10 SUSpects").X / 2, 100),
                Color.Red);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        private void OptionGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            _game.ChangeState(new MenuOptionState(_game, _graphicsDevice, _content));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            _game._gameState = GameState.GamePlaying;
            _game.IsMouseVisible = false;
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game._buttonPressInstance.Play();
            _game.Exit();
        }



        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape) && !_game._isEscapedPressed && _game._gameState == GameState.MainMenu)
            {
                _game._menuState = new MenuState(_game, _graphicsDevice, _content);
                _game._isEscapedPressed = true;
                _game._gameState = GameState.MainMenu;
                _game.IsMouseVisible = true;
            }
            else if (!keyboardState.IsKeyDown(Keys.Escape) && _game._isEscapedPressed)
            {
                _game._isEscapedPressed = false;
            }

            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
