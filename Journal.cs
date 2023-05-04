using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Semester2Prototype
{
    internal class Journal : Sprite
    {
        static VarCollection varCollection = new VarCollection();

        public bool DisplayJournal = false;
        static MessageBox _messageBox;
        static SpriteFont _font;
        static Point _windowSize = new Point(1000, 500);
        static Vector2 _centerScreen = new Vector2(_windowSize.X / 2, _windowSize.Y / 2);
        static List<string> _journalMessages = new List<string>();
        static string _message = "";
        static List<string> _tasks = new List<string>();


        public Journal(Texture2D image, Vector2 position, SpriteFont font) : base(image, position)
        {
            _font = font;
            _tasks.Add("---TASK---\nSpeak To The Manager\nby Pressing E");

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DisplayJournal)
            {
                spriteBatch.Draw(_image,
                    _centerScreen,
                    null,
                    Color.White,
                    0f,
                    new Vector2(
                        _image.Width / 2,
                        _image.Height / 2),
                    1f,
                    SpriteEffects.None,
                    1f);

                spriteBatch.DrawString(_font,
                    _tasks[0],
                    new Vector2(
                        _centerScreen.X - _image.Width / 2 + 30,
                        _centerScreen.Y - _image.Height / 2 + 30), Color.Black);

                spriteBatch.DrawString(_font,
                    _message,
                    new Vector2(
                        _centerScreen.X - _image.Width / 2 + 30,
                        _centerScreen.Y - _image.Height / 2 + 100), Color.Black);

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
    }
}
