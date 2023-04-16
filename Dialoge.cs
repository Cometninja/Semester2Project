using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Net;

namespace Semester2Prototype
{
    internal class Dialoge
    {
        int _question = 0;
        Player _player;
        NPC _npc;
        List<string> _playerDialoge;
        List<string> _npcDialoge;
        string _cursor = ">>"; 
        SpriteFont _font;
        Vector2 _cursorPos = new Vector2(55, 60);
        bool _ButtonPressed = false;
        int _spacing = 15;
        int _answer;
        bool _npcAnswer = false;

        Texture2D _dialogeBox;

        public Dialoge(Player player, NPC npc, Texture2D texture, SpriteFont font)
        {
            _dialogeBox = texture;
            _player = player;
            _npc = npc;
            _playerDialoge = player._playerDialoge;
            _npcDialoge = npc._npcDialoge.ToList();
            _font = font;
        }

        public void DialogeUpdate()
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
            if(Keyboard.GetState().GetPressedKeyCount() == 0) 
            {
                _ButtonPressed = false;
            }
            if (_question < 0) 
            {
                _question = 3;
                _cursorPos.Y += (_spacing*_playerDialoge.Count);
            }
            else if (_question > (_playerDialoge.Count -1))
            {
                _question = 0;
                _cursorPos.Y -= (_spacing*_playerDialoge.Count);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _answer = _question;
                _npcAnswer = true;
            }
            
        }

        public void DialogeDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_dialogeBox, new Rectangle(50, 50, 400, 100),Color.White);   
            spriteBatch.Draw(_dialogeBox, new Rectangle(450, 50, 200, 100),Color.White);
            int numb = 60;
            foreach (string s in _playerDialoge)
            {
                spriteBatch.DrawString(_font, s, new Vector2(75, numb), Color.White);
                numb += _spacing;
            }
            
            if(_npcAnswer)
            {
                spriteBatch.DrawString(_font, _npcDialoge[_answer], new Vector2(460, 60), Color.White);

            }

            spriteBatch.DrawString(_font, _cursor,_cursorPos,Color.White);
            

        }


        

    }
}
