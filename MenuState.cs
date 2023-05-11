using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Semester2Prototype.Controls;

namespace Semester2Prototype.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        Texture2D _rectangleTxr, _backgroundTxr, _buttonTexture;
        SpriteFont _titleFont, _buttonFont;

        Point _screenSize = new Point(800, 800);
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _backgroundTxr = _content.Load<Texture2D>("UI/Txr_Background");
            _titleFont = _content.Load<SpriteFont>("UI/Fonts/TitleMoldyen");


            var newGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Text = "New Game",
                Position = new Vector2((_screenSize.X - _buttonTexture.Width) / 2, 200)
            };


            newGameButton.Click += NewGameButton_Click;

            var optionGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((_screenSize.X - _buttonTexture.Width) / 2, 250),
                Text = "Options",
            };

            optionGameButton.Click += OptionGameButton_Click;

            var quitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((_screenSize.X - _buttonTexture.Width) / 2, 300),
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

            spriteBatch.Draw(_backgroundTxr, new Rectangle(0, 0, _screenSize.X, _screenSize.Y), Color.White);

            Color tDimGrey = new Color(Color.Black, 75);

            int rectWidth = 300; // set the width of the rectangle
            int rectHeight = 500; // set the height of the rectangle

            int rectX = (_screenSize.X - rectWidth) / 2; // calculate the X coordinate to center the rectangle
            int rectY = 0; // set the Y coordinate of the rectangle position

            Rectangle rect = new Rectangle(rectX, rectY, rectWidth, rectHeight); // create the rectangle

            spriteBatch.Draw(_rectangleTxr, rect, tDimGrey); // draw the rectangle centered on the screen along the X-axis

            Vector2 textSize = _titleFont.MeasureString("The Killer");
            spriteBatch.DrawString(_titleFont,
                "The Killer!",
                new Vector2(_screenSize.X / 2f - textSize.X / 2, _screenSize.Y / 10 - textSize.Y / 2),
                Color.Red);


            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

        }

        private void OptionGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new OptionState(_game, _graphicsDevice, _content));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game._gameState = GameState.GamePlaying;
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
