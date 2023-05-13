using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool _spokenTo = false;

        public NPC(Texture2D image, Vector2 position, NPCCharacter character) : base(image, position)
        {
            _NPCCharacter = character;
            _rectangles = GetNPCImage();
            _sourceRect = _rectangles[0][0];
            _center = new Vector2(_position.X + 16, _position.Y + 30);
        }
        public override void Update(List<Sprite> sprites)
        {
            _center = new Vector2(_position.X + 16, _position.Y + 30);
            _sprites = sprites;
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
            _spokenTo = true;
            
            switch (_NPCCharacter)
            {
                case NPCCharacter.Manager:
                    if (!_journal._goals["IntroManager"])
                    {
                        playerDialog.Add("Hello may I speak to the manager");


                        npcDialog.Add("ahh good you must be the detective the police department sent, thank you for you timely response.#" +
                            "There has been a terrible incident! one of our guests have been murdered!#" +
                            "I have already put things in place to keep everyone who was here the night of the murder within the hotel.#" +
                            "Talk with the receptionist to get up to speed on what's happened.#" +
                            "I will stay here to await your verdict, good luck detective.");
                        _journal._goals["IntroManager"] = true;
                        _journal._journalTasks.Add(_journal._tasks[1]);
                    }
                    else
                    {
                        playerDialog.Add("May I ask another question?");

                        npcDialog.Add("As I said speak to the receptionist at the front to get up to speed.");
                    }
                    break;
                case NPCCharacter.Receptionist:
                    if (_journal._goals["lockedRecepionist"])
                    {
                        playerDialog.Add("Hello");
                        npcDialog.Add("I am not talking to you!!");
                    }
                    else if (_journal._goals["IntroReceptionist"])
                    {
                        playerDialog.Add("What where you doing yesterday?");
                        npcDialog.Add("I was working behind the reception desk all evening, only going to the managers office a few times before clocking out at 10pm");

                        playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                        npcDialog.Add("I did see Ms Mayflower talk to Mr Richards the victim yesterday at around 6pm#" +
                                "Most people where their usual self's yesterday, but Mr Montgomery was looking worried pacing in the lounge.#" +
                                "And the Manager too was looking more stressed than usual at 7pm when I went into his office");

                        if (_journal._goals["ChangingRoomClue"])
                        {
                            playerDialog.Add("You said that you didn't leave the reception desk all evening, yet I found this piece of your uniform showing you left the desk at least once yesterday.");
                            npcDialog.Add("Oh- umm… that…#" +
                                "I was umm…#" +
                                "OK YES I LEFT THE DESK AT 9PM TO GO TO THE CHANGING ROOM WITH THE COOK! -OK!#" +
                                "NOW LEAVE ME AND MY PRIVATE LIFE ALONE!");
                            _journal._goals["lockedRecepionist"] = false;
                        }

                        playerDialog.Add("Goodbye");
                        npcDialog.Add("Goodbye");
                    }
                    else if (_journal._goals["IntroManager"])
                    {
                        playerDialog.Add("Hello I am the Detective");
                        npcDialog.Add("Hello detective, the manager told me to bring you up to speed so here is some information we know#" +
                            "The person murdered is named Mr Richards and was staying here for one night. They work for the same company that runs the hotel and was here on business talks, and was staying on the 2nd floor, room 4#" +
                            "The body was discovered in the morning by the cleaner, so you might want to talk to them first#" +
                            "That's we know. good luck, it would be nice to get some closer");
                        _journal._goals["IntroReceptionist"] = true;
                    }
                    else
                    {
                        playerDialog.Add("Hello I am the Detective");
                        npcDialog.Add("Please Speak to the Manager first");
                    }
                    break;
                case NPCCharacter.Cleaner:
                    if (_journal._goals["IntroReceptionist"])
                    {
                        playerDialog.Add("What where you doing yesterday?");
                        npcDialog.Add("I was doing my usual work around the hotel, cleaning rooms and such#" +
                            "I didn't hear or see anything before or after 10pm when I finished my shift and went home#" +
                            "I came into work this morning as usual. when I went upstairs, I found Mr Richard's room door open, and that's when I found the body");

                        playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                        npcDialog.Add("I saw little of other people yesterday, I barely talked or saw anyone, I was off working the whole evening#" +
                            "Though I did catch a glimpse Mr Montgomery come back to the hotel at 1am");
                        if (_journal._goals["FoundMasterKey"])
                        {
                            playerDialog.Add("I found this master key in the storage closet, A place I thought only you would access. Can you tell me something?");
                            npcDialog.Add("Wait… what?#" +
                                "the Manager said they had lost it?#" +
                                "I didn't know anything about the key in there I swear");
                        }
                        playerDialog.Add("Goodbye");
                        npcDialog.Add("Goodbye");
                    }
                    else
                    {
                        playerDialog.Add("Hello");
                        npcDialog.Add("I am sorry but is there someone else you need to talk to first?");
                    }
                    break;
                case NPCCharacter.Chef:

                    playerDialog.Add("What where you doing yesterday?");
                    npcDialog.Add("I was in the kitchen all day, I served the guests that came to the cafeteria, and I locked up and when home at 11pm");

                    playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                    npcDialog.Add("Now I did see a lot of people yesterday given I work in the kitchen to here's all I saw#" +
                        "At I think 7pm, I served Mrs Park, Mr Richards, and Mrs Mayflower#" +
                        "At 9pm my assistant cook ran off somewhere, so I had to have the Manager help me.#" +
                        "At around 10pm I served Mr Montgomery#" +
                        "And at 11pm I closed up and went home");
                    if (_journal._goals["FoundKnife"])
                    {
                        playerDialog.Add("I found this kitchen knife at the crime scene, mind filling me in?");
                        npcDialog.Add("yea that's from the kitchen alright, but not one of mine#" +
                            "That's one of the cooks knifes, I keep mine out of sight and reach in a bag");
                    }
                    playerDialog.Add("Goodbye");
                    npcDialog.Add("Goodbye");
                    break;

                case NPCCharacter.Cook:
                    if (_journal._goals["CookLocked"])
                    {
                        playerDialog.Add("Hello");
                        npcDialog.Add("I already said I need to get back to work");
                    }
                    else
                    {
                        playerDialog.Add("What where you doing yesterday?");
                        npcDialog.Add("I was working in the kitchen all evening, not much apart from that");

                        playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                        npcDialog.Add("I didn't really see much as I just work in making ingredients and dishes, so I won't see anyone as I don't serve");

                        if (_journal._goals["ChangingRoomClue"])
                        {
                            playerDialog.Add("");
                            npcDialog.Add("Oh - that… Hehe#" +
                                "Yea I wasn't entirely honest about staying in the kitchen all evening#" +
                                "I went to the changing room at 9pm with the receptionist, for you know, privacy#" +
                                "I'm going to just go back to work, please don't tell anyone else about this");
                            _journal._goals["CookLocked"] = true;
                        }
                        playerDialog.Add("Goodbye");
                        npcDialog.Add("Goodbye");
                    }

                    break;
                case NPCCharacter.MrMontgomery:
                    playerDialog.Add("What where you doing yesterday?");
                    npcDialog.Add("I-I was in the hotel for most of the evening, in the lounge#" +
                        "But I went out on private business at about 11pm, and didn't return until 1am");

                    playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                    npcDialog.Add("I'm not sure on saying who was where, I kept mostly to myself yesterday");

                    playerDialog.Add("Goodbye");
                    npcDialog.Add("Goodbye");

                    break;
                case NPCCharacter.MrsPark:
                    playerDialog.Add("What where you doing yesterday?");
                    npcDialog.Add("I was out for most of the day but came back to the hotel for dinner at 7pm.#" +
                        "after dinner I played some board games with Ms Mayflower before going to bed");
                    playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                    npcDialog.Add("Guess I saw the cook and chef in the kitchen at 7pm when I went to get my dinner#" +
                        "But other than that, I can't say where other people were or did");
                    playerDialog.Add("Goodbye");
                    npcDialog.Add("Goodbye");

                    break;
                case NPCCharacter.MsMayflower:
                    playerDialog.Add("What where you doing yesterday?");
                    npcDialog.Add("In the evening I got dinner at 7pm, and played board games with Mrs Park after#" +
                        "I was in the lounge for a bit longer before going to bed at 10pm");
                    playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                    npcDialog.Add("I can say for sure for everyone#" +
                        "but I know Mr Montgomery was in the lounge from 6 to 8pm");
                    if (_journal._goals["MsMayflowerPhoto"])
                    {
                        playerDialog.Add("I'm sorry to barge in on your privacy but I found a picture of you and the Victim in your room, can you elaborate on it please?");
                        npcDialog.Add("Ok yes, I knew Mr Richards, we were in a relationship. But it ended bitterly#" +
                            "We hadn't seen or spoken to each other for some time#" +
                            "We had a brief talk at 6pm yesterday, but where both still bitter from each other");
                    }
                    playerDialog.Add("Goodbye");
                    npcDialog.Add("Goodbye");

                    break;
                case NPCCharacter.MrSanders:
                    playerDialog.Add("What where you doing yesterday?");
                    npcDialog.Add("I was in the small lounge from 6 to 8pm, when I went out with Mr Ross#" +
                        "I was out until 12pm when I returned to the hotel and went to bed");

                    playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                    npcDialog.Add("I saw a whole lot of nothing yesterday to be honest#" +
                        "I know the receptionist was in when I was in the lounge, but other than that I'm blind to other events");

                    playerDialog.Add("Goodbye");
                    npcDialog.Add("Goodbye");

                    break;
                case NPCCharacter.MrRoss:
                    playerDialog.Add("What where you doing yesterday?");
                    npcDialog.Add("I was in my hotel room from 6 to 8pm when I went out, And I didn't return to the hotel until 12pm which is when I went to sleep");

                    playerDialog.Add("What did you see other people do yesterday, anything unusual?");
                    npcDialog.Add("Nothing really, I was with Mr Sanders for the evening and didn't see a whole lot");

                    playerDialog.Add("Goodbye");
                    npcDialog.Add("Goodbye");

                    break;
            }

            List<List<string>> result = new List<List<string>>();
            result.Add(npcDialog);
            result.Add(playerDialog);
            return result;
        }
    }
}
