﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Semester2Prototype
{
    internal class Dialoge
    {
        Player _player;
        int _question = 0;
        List<string> _playerDialoge;
        List<string> _npcDialoge;
        string _cursor = ">>";
        SpriteFont _font;
        bool _ButtonPressed = false;
        int _answer = 0;
        bool _npcAnswer = false;
        string _npcMessage = string.Empty;
        bool _printing = false;
        string[] splitAnswer;
        int count = 0;
        int tickCount = 0;
        bool _displayEnter;
        public List<List<string>> _dialogs;
        static Point _dialogPos = new Point(0, 325);
        Vector2 _cursorPos = new Vector2(_dialogPos.X + 5, _dialogPos.Y + 10);
        static Point _dialogWindowSize = new Point(400, 175);
        bool _finalQuestion;
        List<Vector2> _optionPos = new List<Vector2>();

        static Rectangle _playerDialogBox = new Rectangle(
            _dialogPos,
            _dialogWindowSize);
        static Rectangle _npcDialogBox = new Rectangle(
            _playerDialogBox.Width,
            _dialogPos.Y,
            _dialogWindowSize.X,
            _dialogWindowSize.Y);

        Texture2D _dialogeBox;

        public Dialoge(Player player, NPC npc, Texture2D texture, SpriteFont font)
        {
            _dialogs = npc.GetDialoge();
            _player = player;
            _dialogeBox = texture;

            _playerDialoge = _dialogs[1];
            _npcDialoge = _dialogs[0];
            _font = font;
        }

        public void DialogeUpdate(GameTime gameTime)
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
                && !_ButtonPressed)
            {
                _ButtonPressed = true;
                _question++;
                if (_question > (_playerDialoge.Count - 1))
                {
                    _question = 0;
                }
                _cursorPos = _optionPos[_question];
            }
            else if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
                && !_ButtonPressed)
            {
                _ButtonPressed = true;
                _question--;
                if (_question < 0)
                {
                    _question = _playerDialoge.Count - 1;
                }
                _cursorPos = _optionPos[_question];

            }
            if (Keyboard.GetState().GetPressedKeyCount() == 0 && !_printing)
            {
                _ButtonPressed = false;
            }
            if (_finalQuestion)
            {
                _displayEnter = false;
                _playerDialoge.Clear();
                _npcDialoge.Clear();
                _playerDialoge.Add("yes");
                _npcDialoge.Add("Very well I will Gather the Guests in the Lounge.");
                _playerDialoge.Add("no");
                _npcDialoge.Add("Please come see me when you are ready.");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !_ButtonPressed)
            {
                if (_playerDialoge[_question] == "You said that you didn't leave the Kitchen desk all evening, yet I found this piece of your uniform showing you left the kitchen at least once yesterday.")
                {
                    _player._journal._goals["CookLocked"] = true;
                }
                if (_playerDialoge[_question] == "You said that you didn't leave the reception desk all evening, yet I found this piece of your uniform showing you left the desk at least once yesterday.")
                {
                    _player._journal._goals["lockedRecepionist"] = true;

                }
                _ButtonPressed = true;
                string[] responce = _npcDialoge[_question].Split('#');
                if (_answer == responce.Count())
                {
                    Game1 game = _player._game1;
                    if (_playerDialoge[_question] == "Goodbye" || _npcDialoge.Count == 1)
                    {
                        game._gameState = GameState.GamePlaying;
                    }
                    if (_playerDialoge[0] == "I am ready to make my Decision!")
                    {
                        _finalQuestion = true;
                    }
                    if (_playerDialoge[_question] == "yes")
                    {
                        game._gameState = GameState.Accusation;
                    }
                    else
                    {
                        _answer = 0;
                        _displayEnter = false;
                        _printing = false;
                        _npcAnswer = false;
                    }
                }
                else
                {
                    _npcMessage = string.Empty;
                    _ButtonPressed = true;
                    _printing = true;
                    _npcAnswer = true;

                    splitAnswer = responce[_answer].Split(' ');
                }
            }

            if (_printing)
            {
                PrintAnswer();
            }
        }

        public void DialogeDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_dialogeBox, _playerDialogBox, Color.White);
            spriteBatch.Draw(_dialogeBox, _npcDialogBox, Color.White);
            int numb = _playerDialogBox.Y + 10;

            if (!_displayEnter)
            {
                _optionPos.Clear();
                foreach (string s in _playerDialoge)
                {
                    string[] splitQuestion = s.Split(' ');
                    string result = string.Empty;
                    foreach (string s2 in splitQuestion)
                    {
                        if (_font.MeasureString(result + " " + s2).X > (_playerDialogBox.Width - 50))
                        {
                            result += ("\n" + s2 + " ");
                        }
                        else
                        {
                            result += s2 + " ";
                        }
                    }
                    _optionPos.Add(new Vector2(_playerDialogBox.X + 5, numb));

                    spriteBatch.DrawString(_font, result, new Vector2(_playerDialogBox.X + 25, numb), Color.White);
                    numb += (int)_font.MeasureString(result).Y;
                }
            }
            else
            {
                _cursorPos = new Vector2(_playerDialogBox.X + 5, numb);
                spriteBatch.DrawString(_font, "PRESS ENTER TO CONTINUE...", new Vector2(_playerDialogBox.X + 25, numb), Color.White);
            }

            if (_npcAnswer)
            {
                spriteBatch.DrawString(
                    _font,
                    _npcMessage,
                    new Vector2(
                        _npcDialogBox.X + 10,
                        _npcDialogBox.Y + 10),
                    Color.White);
            }

            spriteBatch.DrawString(_font, _cursor, _cursorPos, Color.White);
        }

        public void PrintAnswer()
        {
            tickCount++;
            if (tickCount >= 5)
            {
                if (_font.MeasureString(_npcMessage + " " + splitAnswer[count]).X > (_npcDialogBox.X - 50))
                {
                    _npcMessage += ("\n" + splitAnswer[count] + " ");
                }
                else
                {
                    _npcMessage += splitAnswer[count] + " ";
                }
                count++;
                tickCount = 0;
                if (count == splitAnswer.Length)
                {
                    if (!_finalQuestion)
                    {
                        _displayEnter = true;
                    }
                    else
                    {
                        _displayEnter = false;
                    }
                    count = 0;
                    _answer++;
                    _printing = false;
                }
            }
        }
    }
}
