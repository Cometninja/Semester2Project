using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
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
        Vector2 _cursorPos= new Vector2(-20,0);
        List<Vector2> _namePos = new List<Vector2>();
        int _charSelect = 0;
        bool _isButtonDown,_charSelecting,_confirmingChoice;
        string _text;
        List<string> options = new List<string>();

        public Accusation(Texture2D messageBoxImage,SpriteFont font,Game1 game1,List<NPC> npcs) 
        {
            Rectangle window = game1.GraphicsDevice.Viewport.Bounds;
            _textBoxRect = new Rectangle(window.X, (int)(window.Height * 0.6), window.Width, (int)(window.Height *0.4));
            _box = messageBoxImage;
            _font = font;
            _npcs = npcs;
            int ySpacing = 20;
            int xSpacing = 50;
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
                options.Add(npc._NPCCharacter.ToString());
            }
            _text = GetText(0);

        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !_isButtonDown)
            {
                _isButtonDown = true;
                _charSelect--;
                if (_charSelect < 0)
                {
                    _charSelect = options.Count -1;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !_isButtonDown)
            {
                _charSelect++;
                _isButtonDown = true;
                if (_charSelect == options.Count)
                {
                    _charSelect = 0;
                }
            }
            if (Keyboard.GetState().GetPressedKeyCount() == 0) 
            { 
                _isButtonDown = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !_isButtonDown)
            {
                _isButtonDown = true;
                _text = GetText(1);

                if (!_confirmingChoice)
                {
                    _charSelect = 0;
                    _confirmingChoice = true;
                    options.Clear();
                    options.Add("yes");
                    options.Add("no");
                }   
                else if (_charSelect == 0)
                {
                    options.Clear();

                }
                else if (_charSelect == 1)
                {
                    _confirmingChoice = false;
                    _text = GetText(0);
                    options.Clear();
                    foreach (NPC npc in _npcs)
                    {
                        options.Add(npc._NPCCharacter.ToString());
                    }
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(_box, _textBoxRect, Color.White);
            spriteBatch.DrawString(
                _font,
                _text, 
                new Vector2(_textBoxRect.Width/2 - _font.MeasureString(_text).X/2,_textBoxRect.Top + 10),
                Color.White);
            
            for (int i =0;i < options.Count;i++ )
            {
                spriteBatch.DrawString(
                    _font, 
                    options[i],
                    _namePos[i], 
                    Color.White) ;
            }
            spriteBatch.DrawString(_font, _cursor, _namePos[_charSelect] + _cursorPos, Color.White);
        }


        public string GetText(int index)
        {
            string[] messages = new string[]
            {
                "You now have to choose who you beleive the murderer is.",
                $"Are you sure you want to accuse {options[_charSelect]}???"
                
           
            };

            return messages[index];
        }

    }
}
