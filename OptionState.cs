﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Semester2Prototype.Controls;

namespace Semester2Prototype.States
{
    internal class OptionState : State
    {
        private List<Component> _components;

        Texture2D _rectangleTxr, _backgroundTxr, _buttonTexture;
        SpriteFont _buttonFont;
        Point _screenSize = new Point(800, 800);
        public OptionState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonTexture = _content.Load<Texture2D>("UI/Controls/Button");
            _buttonFont = _content.Load<SpriteFont>("UI/Fonts/Font");
            _rectangleTxr = _content.Load<Texture2D>("UI/RectangleTxr");
            _backgroundTxr = _content.Load<Texture2D>("UI/Txr_Background");


            var keybindsGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Text = "Keybinds",
                Position = new Vector2((_screenSize.X - _buttonTexture.Width) / 2, 200)
            };


            keybindsGameButton.Click += KeybindsGameButton_Click;

            var SoundGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((_screenSize.X - _buttonTexture.Width) / 2, 250),
                Text = "Sound",
            };

            SoundGameButton.Click += SoundGameButton_Click;

            var exitGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((_screenSize.X - _buttonTexture.Width) / 2, 300),
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
            

            spriteBatch.Draw(_backgroundTxr, new Rectangle(0, 0, _screenSize.X, _screenSize.Y), Color.White);

            Color tDimGrey = new Color(Color.Black, 75);

            int rectWidth = 300; // set the width of the rectangle
            int rectHeight = 500; // set the height of the rectangle

            int rectX = (_screenSize.X - rectWidth) / 2; // calculate the X coordinate to center the rectangle
            int rectY = 0; // set the Y coordinate of the rectangle position

            Rectangle rect = new Rectangle(rectX, rectY, rectWidth, rectHeight); // create the rectangle

            spriteBatch.Draw(_rectangleTxr, rect, tDimGrey); // draw the rectangle centered on the screen along the X-axis



            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            
        }

        private void KeybindsGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new KeybindState(_game, _graphicsDevice, _content));
        }

        private void SoundGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SoundState(_game, _graphicsDevice, _content));
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
            _game.ChangeState(new PauseState(_game, _graphicsDevice, _content));
        }
    }
}


