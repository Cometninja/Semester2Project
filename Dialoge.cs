using System.Collections.Generic;
using System.Diagnostics;
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
        Vector2 _cursorPos = new Vector2(55, 60);
        bool _ButtonPressed = false;
        int _spacing = 15;
        int _answer = 0;
        bool _npcAnswer = false;
        string _npcMessage = string.Empty;
        bool _printing = false;
        string[] splitAnswer;
        int count = 0;
        int tickCount = 0;
        bool _displayEnter;
        public List<List<string>> _dialogs;

        Rectangle _playerDialogBox = new Rectangle(50, 50, 400, 100);
        Rectangle _npcDialogBox = new Rectangle(450, 50, 400, 100);

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
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && !_ButtonPressed)
            {
                _ButtonPressed = true;
                _question++;
                _cursorPos.Y += _spacing;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && !_ButtonPressed)
            {
                _ButtonPressed = true;
                _cursorPos.Y -= _spacing;
                _question--;
            }
            if (Keyboard.GetState().GetPressedKeyCount() == 0 && !_printing)
            {
                _ButtonPressed = false;
            }
            if (_question < 0)
            {
                _question = _playerDialoge.Count;
                _cursorPos.Y += (_spacing * _playerDialoge.Count);
            }
            else if (_question > (_playerDialoge.Count - 1))
            {
                _question = 0;
                _cursorPos.Y -= (_spacing * _playerDialoge.Count);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !_ButtonPressed)
            {
                _ButtonPressed = true;
                string[] responce = _npcDialoge[_question].Split('#');
                if (_answer == responce.Count())
                {
                    if (_playerDialoge[_question] == "Goodbye" || _npcDialoge.Count == 1)
                    {
                        Game1 game = _player._game1;
                        game._gameState = GameState.GamePlaying;
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

            if(_printing) 
            {
                PrintAnswer();
            }
        }

        public void DialogeDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_dialogeBox, _playerDialogBox, Color.White);
            spriteBatch.Draw(_dialogeBox, _npcDialogBox, Color.White);
            int numb = 60;

            if (!_displayEnter)
            {
                foreach (string s in _playerDialoge)
                {
                    spriteBatch.DrawString(_font, s, new Vector2(75, numb), Color.White);
                    numb += _spacing;
                }
            }
            else
            {
                spriteBatch.DrawString(_font, "PRESS ENTER TO CONTINUE...", new Vector2(75, numb), Color.White);
            }

            if (_npcAnswer)
            {
                spriteBatch.DrawString(
                    _font, 
                    _npcMessage, 
                    new Vector2(
                        _npcDialogBox.X + 10, 
                        _npcDialogBox.Y+10), 
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
                    _displayEnter = true;
                    count = 0;
                    _answer++;
                    _printing = false;
                }
            }
        }
    }
}
