using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype
{
    internal class Journal : Sprite
    {
        public bool _isJournalDisplayed = false;
        static SpriteFont _font;
        static Vector2 _windowSize;
        static List<string> _journalMessages = new List<string>();
        static string _message = "";
        public List<string> _journalTasks = new List<string>();
        public List<string> _journalClues = new List<string>();
        public List<string> _journalSuspects = new List<string>();
        public List<string> _journalPage4 = new List<string>();
        public Dictionary<string, bool> _goals;
        public Game1 _game1;
        Rectangle _windowBounds, _leftPage, _rightPage;
        JournalPage _journalPage;
        public bool _isKeysPressed;
        public int _cluesFound = 0;
        string[] _titles = new string[] { "Tasks", "Clues", "Suspects", "page4" };

        public string[] _tasks;
        public List<string> _clueMessages = new List<string>();


        public Journal(Texture2D image, Vector2 position, SpriteFont font,Game1 game1) : base(image, position)
        {
            _windowSize = new Vector2(game1.GraphicsDevice.Viewport.Width, game1.GraphicsDevice.Viewport.Height);
            _font = font;
            _goals = SetGoals();
            _game1= game1;
            _windowBounds = _game1.GraphicsDevice.Viewport.Bounds;
            _journalPage = JournalPage.Tasks;
            _tasks   = new string[] {
            "Speak to the Manager",
            "Speak to the Receptionist",
            "Speak to the Cleaner",
            $"find clues {_cluesFound}"};
            _journalTasks.Add(_tasks[0]);
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
                    _titles[(int)_journalPage],
                    new Vector2(
                            _leftPage.X + 
                            _leftPage.Width/2 - 
                            _font.MeasureString(_titles[(int)_journalPage]).X/2,
                        _leftPage.Y), 
                    Color.Black); 
                
                int spacing = 20;
                Rectangle page = _leftPage;
                switch (_journalPage)
                {
                    case JournalPage.Tasks:
                        foreach(string s in _journalTasks)
                        {
                            spriteBatch.DrawString(_font, 
                                s, 
                                new Vector2(page.X,page.Y+spacing), 
                                Color.Black);
                            spacing += 20;
                        }
                        break;
                    case JournalPage.Clues:
                        spriteBatch.DrawString(_font,
                                $"Found Clues {_cluesFound}/8",
                                new Vector2(page.X, page.Y + spacing),
                                Color.Black);
                        float number = 20;
                        int entryCount = 0;
                        bool newPage = false;
                        foreach(string s in _journalClues)
                        {
                            entryCount ++;
                            if (entryCount > 3 && !newPage)
                            {
                                newPage = true; 
                                page = _rightPage;
                                number = 20;
                            }
                            string[] split = s.Split(' ');
                            string result = string.Empty;
                            foreach(string s2 in split)
                            {
                                if(_font.MeasureString(result + s2 +" ").X > page.Width)
                                {
                                    result += "\n" + s2 + " ";
                                }
                                else result += s2 + " ";

                            }

                            spriteBatch.DrawString(_font,
                                result,
                                new Vector2(page.X, page.Y + spacing + number),
                                Color.Black);
                            number += _font.MeasureString(result).Y + spacing;
                        }
                        break;
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
            Dictionary<string, bool> goals = new Dictionary<string, bool>
            {
                { "Test", false },
                { "IntroManager", false },
                { "IntroReceptionist", false },

                { "FoundMasterKey", false },
                { "FoundKnife", false },
                { "ChangingRoomClue", false },
                { "MsMayflowerPhoto", false },
                { "HotelReceptionLogs", false },
                { "KitchenChecks", false },
                { "FinancialDocuments", false },
                { "VictimsDocuments", false },

                { "lockedRecepionist", false },
                { "CookLocked", false }
            };
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
                (int)(_windowBounds.Width / 1.625f),
                (int)(_windowBounds.Height / 12.5f),
                _windowBounds.Width / 3,
                (int)(_windowBounds.Height / 1.15f));


            switch (_journalPage)
            {
                case JournalPage.Tasks:
                    
                    break;
                case JournalPage.Clues:
                    
                    break;
                case JournalPage.Suspects: 
                    break;
                case JournalPage.page4:
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
