using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype
{
    internal class Journal : Sprite
    {
        public bool _isJournalDisplayed = false;
        static SpriteFont _font;
        static Vector2 _windowSize;
        static Vector2 _centerScreen;
        static List<string> _journalMessages = new List<string>();
        static string _message = "";
        static List<string> _tasks = new List<string>();
        public Dictionary<string, bool> _goals;
        public Game1 _game1;
        Rectangle _windowBounds, _leftPage, _rightPage;
        JournalPage _journalPage;
        public bool _isKeysPressed;


        public Journal(Texture2D image, Vector2 position, SpriteFont font,Game1 game1) : base(image, position)
        {
            _windowSize = new Vector2(game1.GraphicsDevice.Viewport.Width, game1.GraphicsDevice.Viewport.Height);
            _centerScreen = _windowSize / 2;
                //new Vector2(game1.GraphicsDevice.Viewport.Width/2, game1.GraphicsDevice.Viewport.Height/2);
            _font = font;
            _goals = SetGoals();
            _game1= game1;
            _windowBounds = _game1.GraphicsDevice.Viewport.Bounds;
            _journalPage = JournalPage.Tasks;

        }
        public override void Update(List<Sprite> sprites)
        {
            if (_isJournalDisplayed)
            {
                JournalControls();
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isJournalDisplayed)
            {
                spriteBatch.Draw(_image,
                    _windowBounds,
                    null,
                    Color.White,
                    0f,
                    new Vector2(0,0),
                    SpriteEffects.None,
                    1f);
                
                spriteBatch.DrawString(_font,
                    _tasks[0],
                    new Vector2(
                        _leftPage.X,
                        _leftPage.Y), Color.Black); 
                
                switch (_journalPage)
                {
                    case JournalPage.Tasks:



                        spriteBatch.DrawString(_font,
                            _message,
                            new Vector2(
                                _leftPage.X,
                                _leftPage.Y + _font.MeasureString(_tasks[0]).Y), 
                            Color.Black);
                        break;
                    case JournalPage.Clues: break;
                    case JournalPage.Suspects: break;
                    case JournalPage.page4: break;
                }
            }
            else
            {
                spriteBatch.Draw(_image, new Rectangle(0, 0, 50, 50), null, Color.White);
            }
        }

        public void LoadJournalMessages()
        {
            //TODO add in file read system
            _journalMessages.Clear();
            _journalMessages.Add("Task not Completed");
            _journalMessages.Add("task Complete");
        }

        public void CurrentMessage(int number)
        {
            this.LoadJournalMessages();
            switch (number)
            {
                case 0:
                    _message = _journalMessages[0];
                    break;
                default:
                    _message = _journalMessages[1];
                    break;
            }
        }

        static Dictionary<string, bool> SetGoals()
        {
            Dictionary<string, bool> goals = new Dictionary<string, bool>();

            goals.Add("Test", false);
            goals.Add("IntroManager", false);
            goals.Add("IntroReceptionist", false);

            goals.Add("FoundMasterKey", false);
            goals.Add("FoundKnife", false);
            goals.Add("ChangingRoomClue", false);
            goals.Add("MsMayflowerPhoto", false);
            goals.Add("HotelReceptionLogs", false);
            goals.Add("KitchenChecks", false);
            goals.Add("FinancialDocuments", false);
            goals.Add("VictimsDocuments", false);


            goals.Add("lockedRecepionist", false);
            goals.Add("CookLocked", false);
            return goals;
        }

        public void DisplayJournal()
        {
            _windowBounds = _game1.GraphicsDevice.Viewport.Bounds;
            _leftPage = new Rectangle(
                _windowBounds.Width / 16,
                (int)(_windowBounds.Height / 12.5f),
                _windowBounds.Width / 3,
                (int)(_windowBounds.Height / 1.15f));
            _rightPage = new Rectangle(
                (int)(_windowBounds.Width / 1.71f),
                (int)(_windowBounds.Height / 12.5f),
                _windowBounds.Width / 3,
                (int)(_windowBounds.Height / 1.15f));


            switch (_journalPage)
            {
                case JournalPage.Tasks:
                    _tasks.Clear();
                    _tasks.Add("---TASK---\nSpeak To The Manager\nby Pressing E");
                    
                    break;
                case JournalPage.Clues:
                    _tasks.Clear();
                    _tasks.Add("---Clues---");
                    break;
                case JournalPage.Suspects: 
                    _tasks.Clear();
                    _tasks.Add("---Suspects---");
                    break;
                case JournalPage.page4:
                    _tasks.Clear();
                    _tasks.Add("---Page 4---");
                    break;
            }

            

            if (_goals["IntroManager"])
            {
                CurrentMessage(1);
            }
            else
            {
                CurrentMessage(0);
            }
            _isJournalDisplayed = true;
        }
        public void JournalControls()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !_isKeysPressed)
            {
                _isKeysPressed = true;
                _journalPage++;
                if ((int)_journalPage == 4)
                {
                    _journalPage = 0;
                }
                DisplayJournal();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && !_isKeysPressed)
            {
                _isKeysPressed = true;
                _journalPage--;
                if((int)_journalPage < 0) 
                {
                    _journalPage = (JournalPage)3;
                }
                DisplayJournal();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P) && !_isKeysPressed)
            {
                _isKeysPressed = true;
                _isJournalDisplayed = false;
                _game1._gameState = GameState.GamePlaying;
            }
            if (Keyboard.GetState().GetPressedKeyCount() == 0)
            {
                _isKeysPressed = false;
            }
        }
    }
}
