﻿using System;
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

        Texture2D _rectangleTxr, _buttonTexture;
        SpriteFont _titleFont, _buttonFont;

        Point _screenSize = new Point(800, 800);

        int rectWidth = 300; // set the width of the rectangle
        int rectHeight = 1000; // set the height of the rectangle

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _titleFont = _content.Load<SpriteFont>("UI/Fonts/TitleMoldyen");


            var newGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Text = "New Game",
                Position = new Vector2((_screenSize.X - rectWidth) / 1.65f, 200)
            };


            newGameButton.Click += NewGameButton_Click;

            var optionGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((_screenSize.X - rectWidth) / 1.65f, 250),
                Text = "Options",
            };

            optionGameButton.Click += OptionGameButton_Click;

            var quitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((_screenSize.X - rectWidth) / 1.65f, 300),
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


            int rectX = (0 + _screenSize.X / 2 - rectWidth /2); // calculate the X coordinate to center the rectangle
            int rectY = 0; // set the Y coordinate of the rectangle position


            Rectangle rect = new Rectangle(rectX, rectY, rectWidth, rectHeight); // create the rectangle

            spriteBatch.Draw(_rectangleTxr, rect, tDimGrey); // draw the rectangle centered on the screen along the X-axis

            Vector2 textSize = _titleFont.MeasureString("Title");
            spriteBatch.DrawString(_titleFont,
                "10 SUSpects",
                new Vector2(rect.X + rect.Width/2 - _titleFont.MeasureString("10 SUSpects").X / 2 , 100 ),
                Color.Red);
            // 800 x 600 window size

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
