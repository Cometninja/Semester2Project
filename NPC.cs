using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Semester2Prototype
{
    internal class NPC : Sprite
    {

        List<List<Rectangle>> _rectangles = new List<List<Rectangle>>();
        public Moving _moving = Moving.Still;
        public Moving _lastMove;
        public Facing _facing = Facing.Down;
        public Point _NPCPoint;
        public NPCCharacter _NPCCharacter;
        Random _random = new Random();
        float _speed = 0.5f;
        static int _animationCount = 0, tickCount;
        static List<Sprite> _sprites = new List<Sprite>();
        public bool _dialoge = false;
        static Journal _journal;

        public NPC(Texture2D image, Vector2 position, NPCCharacter character) : base(image, position)
        {
            _NPCCharacter = character;
            _rectangles = GetNPCImage();
            _sourceRect = _rectangles[0][0];
        }
        public override void Update(List<Sprite> sprites)
        {
            _sprites = sprites;
            _center = new Vector2(_position.X + 16, _position.Y + 30);
        }
        static List<List<Rectangle>> GetNPCImage()
        {
            List<List<Rectangle>> animations = new List<List<Rectangle>>();

            for (int i = 0; i < 4; i++)
            {
                animations.Add(new List<Rectangle>());
            }

            // Down animation
            animations[0].Add(new Rectangle(0, 0, 36, 52));
            animations[0].Add(new Rectangle(36, 0, 36, 52));
            animations[0].Add(new Rectangle(0, 0, 36, 52));
            animations[0].Add(new Rectangle(72, 0, 36, 52));

            // Up animation
            animations[3].Add(new Rectangle(0, 156, 36, 52));
            animations[3].Add(new Rectangle(36, 156, 36, 52));
            animations[3].Add(new Rectangle(0, 156, 36, 52));
            animations[3].Add(new Rectangle(72, 156, 36, 52));

            // Right animation
            animations[2].Add(new Rectangle(0, 106, 36, 52));
            animations[2].Add(new Rectangle(36, 106, 36, 52));
            animations[2].Add(new Rectangle(0, 106, 36, 52));
            animations[2].Add(new Rectangle(72, 106, 36, 52));

            // Left animation
            animations[1].Add(new Rectangle(0, 52, 36, 52));
            animations[1].Add(new Rectangle(36, 52, 36, 52));
            animations[1].Add(new Rectangle(0, 52, 36, 52));
            animations[1].Add(new Rectangle(72, 52, 36, 52));

            return animations;
        }
        public void NPC_Move()
        {
            switch (_moving)
            {
                case Moving.Up:
                    _sourceRect = GetNPCImage()[1][_animationCount];
                    _facing = Facing.Up;
                    _position.Y -= _speed;
                    break;
                case Moving.Down:
                    _sourceRect = GetNPCImage()[0][_animationCount];
                    _facing = Facing.Down;
                    _position.Y += _speed;
                    break;
                case Moving.Left:
                    _sourceRect = GetNPCImage()[3][_animationCount];
                    _facing = Facing.Left;
                    _position.X -= _speed;
                    break;
                case Moving.Right:
                    _sourceRect = GetNPCImage()[2][_animationCount];
                    _facing = Facing.Right;
                    _position.X += _speed;
                    break;
                default:
                    break;
            }
            tickCount++;
            if (tickCount % 10 == 0)
            {
                if (_animationCount == 3)
                {
                    _animationCount = 0;
                }
                else
                {
                    _animationCount++;
                }
            }
            NPC_Controls();
        }
        public void NPC_Controls()
        {
            Player player = _sprites.OfType<Player>().FirstOrDefault();
            Tile tile = _sprites.OfType<Tile>().FirstOrDefault();
            //TODO add in NPC Moving
            if ((_position.X - tile._position.X) % 50 == 0 && (_position.Y - tile._position.Y) % 50 == 0)
            {
                CheckNextTile();
            }
        }
        public void CheckNextTile()
        {
            Point nextTilePoint = Point.Zero;
            foreach (Tile tile in _sprites.OfType<Tile>().ToList())
            {
                if (tile._centerBox.Contains(this._center))
                {
                    this._NPCPoint = tile._point;
                    nextTilePoint = _NPCPoint;
                    break;
                }
            }
            switch (this._facing)
            {
                case Facing.Up:
                    nextTilePoint.Y--;
                    break;
                case Facing.Down:
                    nextTilePoint.Y++;
                    break;
                case Facing.Left:
                    nextTilePoint.X--;
                    break;
                case Facing.Right:
                    nextTilePoint.X++;
                    break;
            }

            Tile nextTile = _sprites.OfType<Tile>().Where(tile => tile._point == nextTilePoint).First();

            if (nextTile != null)
            {
                if (nextTile._tileState != TileState.Empty)
                {
                    Moving placeholder = _moving;
                    while (_moving == placeholder)
                    {
                        _moving = (Moving)_random.Next(1, 5);
                    }
                }
            }
        }

        public void StartDialog()
        {
            _dialoge = true;
            _lastMove = _moving;
            this._moving = Moving.Still;
        }

        public List<List<string>> GetDialoge()
        {
            List<string> npcDialog = new List<string>();
            List<string> playerDialog = new List<string>();
            _journal = _sprites.OfType<Journal>().FirstOrDefault();
            switch (_NPCCharacter)
            {
                case NPCCharacter.Manager:
                    if (!_journal._goals["IntroManager"])
                    {
                        playerDialog.Add("Hello may I speak to the manager");


                        npcDialog.Add("ahh good you must be the detective the police department sent, thank you for you timely response.");
                        npcDialog.Add("There has been a terrible incident! one of our guests have been murdered!");
                        npcDialog.Add("I have already put things in place to keep everyone who was here the night of the murder within the hotel.");
                        npcDialog.Add("Talk with the receptionist to get up to speed on what's happened.");
                        npcDialog.Add("I will stay here to await your verdict, good luck detective.");
                        _journal._goals["IntroManager"] = true;
                    }
                    else
                    {
                        playerDialog.Add("May I ask another question?");

                        npcDialog.Add("As I said speak to the receptionist at the front to get up to speed.");
                    }
                        break;
                case NPCCharacter.Receptionist:
                    playerDialog.Add("Hello I am the Detective");
                    if (_journal._goals["IntroManager"])
                    {
                        npcDialog.Add("Hello detective, the manager told me to bring you up to speed so here is some information we know");
                        npcDialog.Add("The person murdered is named Mr Richards and was staying here for one night. They work for the same company that runs the hotel and was here on business talks, and was staying on the 2nd floor, room 4") ;
                        npcDialog.Add("The body was discovered in the morning by the cleaner, so you might want to talk to them first");
                        npcDialog.Add("That's we know. good luck, it would be nice to get some closer");
                        _journal._goals["IntroRecetionist"] = true;
                    }
                    else
                    {
                        npcDialog.Add("Please Speak to the Manager first");
                    }
                    break;
                case NPCCharacter.Cleaner:
                    playerDialog.Add("Hello I am the Detective, I need to ask you a few questions if I may?");
                    if (_journal._goals["IntroReceptionist"])
                    {
                        playerDialog.Add("What where you doing yesterday?");
                        playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                        if (_journal._goals["FoundMasterKey"])
                        {
                            playerDialog.Add("I found this master key in the storage closet, A place I thought only you would access. Can you tell me something?");
                        }
                        playerDialog.Add("GoodBye");
                    

                        npcDialog.Add("I was doing my usual work around the hotel, cleaning rooms and such I didn’t hear or see anything before or after 10pm when I finished my shift and went home I came into work this morning as usual.when I went upstairs, I found Mr Richard’s room door open, and that’s when I found the body");
                    }
                    else
                    {
                        npcDialog.Add("I am sorry but is there someone else you need to talk to first?");
                    }
                    break;
            }


            List<List<string>> result = new List<List<string>>();
            result.Add(npcDialog);
            result.Add(playerDialog);
            return result;
        }
    }
}
