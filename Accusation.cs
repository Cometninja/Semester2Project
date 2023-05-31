using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype
{
    internal class Accusation
    {
        Texture2D _box;
        SpriteFont _font;
        Rectangle _textBoxRect;
        string _cursor = ">>";
        List<NPC> _npcs;
        Vector2 _cursorPos = new Vector2(-20, 0);
        List<Vector2> _namePos = new List<Vector2>();
        int _charSelect = 0;
        bool _isButtonDown = true, _confirmingChoice;
        string _text;
        private bool _startTimer;
        bool _decisionMade;
        List<string> _options = new List<string>();
        Journal _journal;
        Game1 _game1;
        NPC _accused;
        int _tickCount = 0;
        bool _printing;
        bool _displayFinalMessage;
        bool _centerText;
        public Accusation(Texture2D messageBoxImage, SpriteFont font, Game1 game1, List<NPC> npcs, Journal journal)
        {
            _journal = journal;
            Rectangle window = game1.GraphicsDevice.Viewport.Bounds;
            _game1 = game1;
            _textBoxRect = new Rectangle(window.X, (int)(window.Height * 0.6), window.Width, (int)(window.Height * 0.4));
            _box = messageBoxImage;
            _font = font;
            _npcs = npcs;
            int ySpacing = 20;
            int xSpacing = 50;
            _accused = npcs.First();
            for (int i = 0, j = 0; i < _npcs.Count; i++, j++)
            {
                if (i == 5)
                {
                    xSpacing += 150;
                    j = 0;
                }
                _namePos.Add(new Vector2(_textBoxRect.X + xSpacing + 10, _textBoxRect.Y + (j * ySpacing) + 30));
            }
            foreach (NPC npc in npcs)
            {
                _options.Add(AddSpace(npc._NPCCharacter.ToString()));
            }
            _text = GetText(0);
        }

        public void Update(GameTime gameTime)
        {
            Rectangle window = _game1.GraphicsDevice.Viewport.Bounds;
            _textBoxRect = new Rectangle(window.X, (int)(window.Height * 0.6), window.Width, (int)(window.Height * 0.4));
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !_isButtonDown && !_decisionMade)
            {
                _isButtonDown = true;
                _charSelect--;
                if (_charSelect < 0)
                {
                    _charSelect = _options.Count - 1;

                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !_isButtonDown && !_decisionMade)
            {
                _charSelect++;
                _isButtonDown = true;
                if (_charSelect >= _options.Count)
                {
                    _charSelect = 0;
                }
            }
            if (Keyboard.GetState().GetPressedKeyCount() == 0)
            {
                _isButtonDown = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !_isButtonDown && !_decisionMade)
            {
                _isButtonDown = true;
                _text = GetText(1);

                if (!_confirmingChoice)
                {
                    _accused = _npcs.First(npc => npc._NPCCharacter.ToString() == RemoveSpace(_options[_charSelect]));
                    _charSelect = 0;
                    _confirmingChoice = true;
                    _options.Clear();
                    _options.Add("yes");
                    _options.Add("no");

                }
                else if (_charSelect == 0)
                {
                    _options.Clear();
                    _decisionMade = true;

                    //run function to show if player got the answer right......
                    //the if player hasnt found all clues then the accused gets away for lack of evidence
                    //when the player gets all the clues then they can only accuse manager
                }
                else if (_charSelect == 1)
                {
                    _confirmingChoice = false;
                    _text = GetText(0);
                    _options.Clear();
                    foreach (NPC npc in _npcs)
                    {
                        _options.Add(npc._NPCCharacter.ToString());
                    }
                }
            }
            if (_printing)
            {
                _tickCount++;
                _text = PrintString(GetText(4), _tickCount);
            }
            if (_displayFinalMessage)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !_isButtonDown)
                {
                    _isButtonDown = true;
                    if (!_displayRestart) 
                    { 
                        _centerText = true;
                        numb = 0;
                        _text = GetText(5);
                        _startTimer = true;
                    }
                    else
                    {
                        _game1.ResetGame();
                    }

                }
            }
            if (_startTimer)
            {
                if(TimePassed(gameTime, 5))
                {
                    _displayRestart = true;
                    _text = "Press Enter to Restart Game";

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        _isButtonDown = true;
                        _game1.ResetGame();
                    }

                }
            }
            else if (_decisionMade)
            {
                switch (_accused._NPCCharacter)
                {
                    case NPCCharacter.Manager:
                        if (!_journal._allFound)
                        {
                            _text = GetText(3);
                            _startTimer = true;
                        }
                        else
                        {
                            if (!_displayFinalMessage)
                            {
                                _printing = true;
                            }
                        }
                        break;
                    default:
                        _text = GetText(3);
                        _startTimer = true;
                        break;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_box, _textBoxRect, Color.White);
            if (!_decisionMade || _centerText)
            {
                spriteBatch.DrawString(
                    _font,
                    _text,
                    new Vector2(_textBoxRect.Width / 2 - _font.MeasureString(_text).X / 2, _textBoxRect.Top + 10),
                    Color.White);
                spriteBatch.DrawString(_font, _cursor, _namePos[_charSelect] + _cursorPos, Color.White);
            }
            else
            {
                spriteBatch.DrawString(
                    _font,
                    WrapAround(_text),
                    new Vector2(_textBoxRect.Left + 10, _textBoxRect.Top + 10),
                    Color.White);
            }
            for (int i = 0; i < _options.Count; i++)
            {
                spriteBatch.DrawString(
                    _font,
                    _options[i],
                    _namePos[i],
                    Color.White);
            }
        }
        public string GetText(int index)
        {
            string ManagerAccusation = "Manager:wha- what? NO!\n " +
                "Detective: Yes, you murdered him because he was going to expose you for defrauding the company and stealing fund meant for the hotel!!\n" +
                "Manager:But how!?!?\n" +
                "Detective: I found the account records in your office and the documents in Mr Richards brief case. The Cleaner Mentioned you were the one who \"Lost\" the Master Key and you are the only one with a true motive and no alibi.\n" +
                "Manager: I'm sorry, I didnt know what to do!!!!!";
            string FinalMessage =
                "_________ Congradulations ________\n" +
                "______You found the Murderer______";
            string[] messages = new string[]
            {
                "You now have to choose who you beleive the murderer is.",
                $"Are you sure you want to accuse {AddSpace(_npcs[_charSelect]._NPCCharacter.ToString())}???",
                "You have choose wisely",
                $"You dont have enough evidence to convict {AddSpace(_accused._NPCCharacter.ToString())}\nYOU HAVE FAILED!!!!",
                ManagerAccusation,
                FinalMessage
            };
            return messages[index];
        }

        public string AddSpace(string text)
        {
            bool first = false;
            string result = string.Empty;

            foreach (char c in text)
            {
                if (c.ToString() == c.ToString().ToUpper())
                {
                    if (!first)
                    {
                        first = true;
                        result += c;
                    }
                    else
                        result += " " + c;
                }
                else result += c;
            }
            return result;
        }

        public string RemoveSpace(string text)
        {
            string result = string.Empty;
            foreach (char c in text)
            {
                if (c != ' ')
                {
                    result += c;
                }
            }
            return result;
        }

        public string WrapAround(string text)
        {
            string[] strings = text.Split(' ');
            string result = string.Empty;

            foreach (string s in strings)
            {
                if (_font.MeasureString(result + s).X > _textBoxRect.Width)
                {
                    result += "\n" + s + " ";
                }
                else result += s + " ";
            }
            return result;
        }
        int _printingindex = 0;
        string _displayText = string.Empty;
        public string PrintString(string text, int tickCount)
        {
            string[] split = text.Split(' ');

            if (tickCount % 5 == 0)
            {
                _printingindex++;
                if (_printingindex < split.Count())
                {
                    _displayText = string.Empty;
                    for (int i = 0; i < _printingindex; i++)
                    {
                        _displayText += split[i] + " ";
                    }
                }
                else
                {
                    _printing = false;
                    _displayText = text;
                    _displayFinalMessage = true;
                }
            }
            return _displayText;
        }

        double numb = 0;
        private bool _displayRestart;

        public bool TimePassed(GameTime gameTime, int seconds)
        {
            numb += gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds < numb) 
                return true;
            else return false;
        }
    }
}
