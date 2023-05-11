namespace Semester2Prototype
{
    internal class Menus
    {
        Game1 _gameParent;


        /* public GameState _gameState = GameState.MainMenu;

         public void Update(GameTime gameTime)
         {
             _gameState = GameState.MainMenu;

             KeyboardState keyboardState = Keyboard.GetState();

             if (keyboardState.IsKeyDown(Keys.Escape) && !_gameParent._isEscapedPressed)
             {
                 _gameParent._isEscapedPressed = true;
                 _gameState = GameState.GamePlaying;
             }

             else if (!keyboardState.IsKeyDown(Keys.Escape) && _gameParent._isEscapedPressed)
             {
                 _gameParent._isEscapedPressed = false;
             }

         }

         public Menus(Game1 gameParent) { _gameParent = gameParent;

             var newGameButton = new Button(_gameParent.buttonTexture, _gameParent.buttonFont)
             {
                 Text = "New Game",
                 Position = new Vector2((_gameParent._windowSize.X - _gameParent.buttonTexture.Width) / 2, 200)
             };


             newGameButton.Click += NewGameButton_Click;

             var optionGameButton = new Button(_gameParent.buttonTexture, _gameParent.buttonFont)
             {
                 Position = new Vector2((_gameParent._windowSize.X - _gameParent.buttonTexture.Width) / 2, 250),
                 Text = "Options",
             };

             optionGameButton.Click += OptionGameButton_Click;

             var quitGameButton = new Button(_gameParent.buttonTexture, _gameParent.buttonFont)
             {
                 Position = new Vector2((_gameParent._windowSize.X - _gameParent.buttonTexture.Width) / 2, 300),
                 Text = "Quit Game",
             };

             quitGameButton.Click += QuitGameButton_Click;

             {
                 newGameButton,
         optionGameButton,
         quitGameButton,
       };
         }



         private void NewGameButton_Click(object sender, EventArgs e)
         {
             _gameState = GameState.GamePlaying;

         }

         private void OptionGameButton_Click(object sender, EventArgs e)
         {
             _gameState = GameState.Options;
         }



         private void QuitGameButton_Click(object sender, EventArgs e)
         {
             _gameParent.Exit();
         }




         public void Draw(SpriteBatch spriteBatch)
         {


             spriteBatch.Draw(_gameParent._backgroundTxr, new Rectangle(0, 0, _gameParent._windowSize.X, _gameParent._windowSize.Y), Color.White);

             Color tDimGrey = new Color(Color.Black, 75);

             int rectWidth = 300; // set the width of the rectangle
             int rectHeight = _gameParent._windowSize.Y; // set the height of the rectangle

             int rectX = (_gameParent._windowSize.X - rectWidth) / 2; // calculate the X coordinate to center the rectangle
             int rectY = 0; // set the Y coordinate of the rectangle position

             Rectangle rect = new Rectangle(rectX, rectY, rectWidth, rectHeight); // create the rectangle

             spriteBatch.Draw(_gameParent._rectangleTxr, rect, tDimGrey); // draw the rectangle centered on the screen along the X-axis

             Vector2 textSize = _gameParent._mainfont.MeasureString("The Killer");
             spriteBatch.DrawString(_gameParent._mainfont,
                 "The Killer!",
                 new Vector2(_gameParent._windowSize.X / 2f - textSize.X / 2, _gameParent._windowSize.Y / 10 - textSize.Y / 2),
                 Color.Red);



         }

         }
         */
    }
}
