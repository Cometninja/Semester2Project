using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Semester2Prototype
{

    internal class Player : Sprite
    {
        public Point _point;
        public Facing _playerFacing = Facing.Down;
        public Moving _moving = Moving.Still;
        static List<Sprite> _sprites;
        static List<NPC> _npcList;
        bool DebugBounds;
        Texture2D _debugImage;
        static Rectangle _detection;
        static int _animationCount = 0, tickCount;
        public MessageBox _messageBox;
        static bool _isSpacePressed, _isQPressed, _isKeysPressed;
        public Journal _journal;
        static List<Tile> tiles;
        public GameState _gameState = GameState.GamePlaying;
        public Dialoge _dialoge;
        public Game1 _game1;
        SoundEffectInstance _clueSound;

        public List<string> _playerDialoge = new List<string>();

        public Player(Texture2D image, Vector2 position, Point point, Game1 game1, SoundEffectInstance clueSound) : base(image, position)
        {
            _clueSound = clueSound;
            _game1 = game1;
            _point = point;
            _sourceRect = GetPlayerImage()[0][0];
        }
        public override void Update(List<Sprite> sprites)
        {
            _bounds = new Rectangle((int)_position.X, (int)_position.Y, _sourceRect.Width, _sourceRect.Height);
            _sprites = sprites;
            _center = new Vector2(_position.X + 16, _position.Y + 30);
        }
        public void PlayerControls(Player player)
        {
            _messageBox = _sprites.OfType<MessageBox>().FirstOrDefault();
            Point playerPoint = player._point;
            _journal = _sprites.OfType<Journal>().FirstOrDefault();

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                player._sourceRect = GetPlayerImage()[1][0];
                _playerFacing = Facing.Up;
                if (CheckNewTile(new Point(playerPoint.X, playerPoint.Y - 1)))
                {
                    _moving = Moving.Up;
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _playerFacing = Facing.Down;
                player._sourceRect = GetPlayerImage()[0][0];
                if (CheckNewTile(new Point(playerPoint.X, playerPoint.Y + 1)))
                {
                    _moving = Moving.Down;
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player._sourceRect = GetPlayerImage()[2][0];

                _playerFacing = Facing.Right;
                if (CheckNewTile(new Point(playerPoint.X + 1, playerPoint.Y)))
                {
                    _moving = Moving.Right;
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {

                _playerFacing = Facing.Left;
                player._sourceRect = GetPlayerImage()[3][0];

                if (CheckNewTile(new Point(playerPoint.X - 1, playerPoint.Y)))
                {
                    _moving = Moving.Left;
                }
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.Space) && _isSpacePressed)
            {
                _isSpacePressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Point checkPoint = player._point;
                switch (_playerFacing)
                {
                    case Facing.Up:
                        checkPoint.Y--;
                        break;
                    case Facing.Down:
                        checkPoint.Y++;

                        break;
                    case Facing.Right:
                        checkPoint.X++;

                        break;
                    case Facing.Left:
                        checkPoint.X--;

                        break;
                }
                if (CheckInteractiveTile(checkPoint))
                {
                    _journal._goals["Test"] = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && !_isQPressed && !_journal._isJournalDisplayed)
            {
                _journal._isKeysPressed = true;
                _journal.DisplayJournal();

                _isQPressed = true;
                _game1._gameState = GameState.JournalScreen;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q) && !_isQPressed && _journal._isJournalDisplayed)
            {
                _journal._isJournalDisplayed = false;
                _isQPressed = true;
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.Q) && _isQPressed)
            {
                _isQPressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E) && !_isKeysPressed)
            {
                DebugBounds = false;
                _detection = _bounds;
                _isKeysPressed = true;

                switch (_playerFacing)
                {
                    case Facing.Up:
                        _detection.Height *= 2;
                        _detection.Y -= 100;
                        break;
                    case Facing.Down:
                        _detection.Y += 50;
                        break;
                    case Facing.Right:
                        _detection.X += 50;
                        break;
                    case Facing.Left:
                        _detection.X -= 50;
                        break;
                    default:
                        break;
                }
                List<NPC> npcList = _sprites.OfType<NPC>().ToList();
                foreach (NPC npc in npcList)
                {
                    if (_detection.Contains(npc._center))
                    {
                        npc.StartDialog();
                        _dialoge = new Dialoge(player, npc, _messageBox._image, _messageBox._messageBoxFont);
                        _game1._gameState = GameState.Dialoge;
                        break;
                    }
                }
                List<Clue> clues = _sprites.OfType<Clue>().ToList();
                foreach (Clue clue in clues)
                {
                    if (_detection.Contains(clue._center))
                    {
                        clue.FoundClue(_messageBox, _journal);
                        _clueSound.Play();
                        _journal._newInfo = true;
                        Debug.WriteLine("you found a clue!!!!");
                    }
                }

            }
            else if (Keyboard.GetState().IsKeyUp(Keys.E))
            {
                DebugBounds = false;
            }
            if (Keyboard.GetState().GetPressedKeyCount() == 0)
            {
                _isKeysPressed = false;
            }
        }
        public void PlayerMove(Player player)
        {
            Tile sourceTile = _sprites.OfType<Tile>().FirstOrDefault();
            tiles = _sprites.OfType<Tile>().ToList();
            _npcList = _sprites.OfType<NPC>().ToList();
            List<Clue> clues = _sprites.OfType<Clue>().ToList();
            int speed = 2;

            switch (_moving)
            {

                case Moving.Up:

                    player._sourceRect = GetPlayerImage()[1][_animationCount];
                    if (player._position.Y == 150)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.Y += speed;
                        }
                        foreach (NPC npc in _npcList)
                        {
                            npc._position.Y += speed;
                        }
                        foreach (Clue clue in clues)
                        {
                            clue._position.Y += speed;
                        }
                    }
                    else
                    {
                        player._position.Y -= speed;
                    }
                    if (player._position.Y % 50 == 0 && sourceTile._position.Y % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[1][0];
                    }
                    break;
                case Moving.Down:
                    player._sourceRect = GetPlayerImage()[0][_animationCount];
                    if (player._position.Y == 350)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.Y -= speed;
                        }
                        foreach (NPC npc in _npcList)
                        {
                            npc._position.Y -= speed;
                        }
                        foreach (Clue clue in clues)
                        {
                            clue._position.Y -= speed;
                        }
                    }
                    else
                    {
                        player._position.Y += speed;
                    }
                    if (player._position.Y % 50 == 0 && sourceTile._position.Y % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[0][0];
                    }
                    break;
                case Moving.Right:
                    player._sourceRect = GetPlayerImage()[2][_animationCount];
                    if (player._position.X >= 500)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.X -= speed;
                        }
                        foreach (NPC npc in _npcList)
                        {
                            npc._position.X -= speed;
                        }
                        foreach (Clue clue in clues)
                        {
                            clue._position.X -= speed;
                        }
                    }
                    else
                    {
                        player._position.X += speed;
                    }
                    if (player._position.X % 50 == 0 && sourceTile._position.X % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[2][0];
                    }
                    break;
                case Moving.Left:
                    player._sourceRect = GetPlayerImage()[3][_animationCount];
                    if (player._position.X <= 300)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.X += speed;
                        }

                        foreach (NPC npc in _npcList)
                        {
                            npc._position.X += speed;
                        }
                        foreach (Clue clue in clues)
                        {
                            clue._position.X += speed;
                        }
                    }
                    else
                    {
                        player._position.X -= speed;
                    }
                    if (player._position.X % 50 == 0 && sourceTile._position.X % 50 == 0)
                    {
                        if (_moving == Moving.Left || _moving == Moving.Right)
                        {
                            player._sourceRect = GetPlayerImage()[3][0];
                        }
                        else
                        {
                            player._sourceRect = GetPlayerImage()[3][0];
                        }
                        _moving = Moving.Still;
                    }
                    break;

                default:
                    PlayerControls(player);
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
        }
        static List<List<Rectangle>> GetPlayerImage()
        {
            // 32x,64x,96x
            // 32y,64y,96y,128y

            List<List<Rectangle>> animations = new List<List<Rectangle>>();


            for (int i = 0; i < 4; i++)
            {
                animations.Add(new List<Rectangle>());
            }
            // Down animation
            animations[0].Add(new Rectangle(0, 0, 36, 54));
            animations[0].Add(new Rectangle(36, 0, 36, 54));
            animations[0].Add(new Rectangle(0, 0, 36, 54));
            animations[0].Add(new Rectangle(72, 0, 36, 54));

            // Up animation
            animations[1].Add(new Rectangle(0, 53, 36, 53));
            animations[1].Add(new Rectangle(36, 53, 36, 53));
            animations[1].Add(new Rectangle(0, 53, 36, 53));
            animations[1].Add(new Rectangle(72, 53, 36, 53));

            // Right animation
            animations[2].Add(new Rectangle(0, 106, 36, 52));
            animations[2].Add(new Rectangle(36, 106, 36, 52));
            animations[2].Add(new Rectangle(0, 106, 36, 52));
            animations[2].Add(new Rectangle(72, 106, 36, 52));

            // Left animation
            animations[3].Add(new Rectangle(0, 159, 36, 52));
            animations[3].Add(new Rectangle(36, 159, 36, 52));
            animations[3].Add(new Rectangle(0, 159, 36, 52));
            animations[3].Add(new Rectangle(72, 159, 36, 52));

            return animations;
        }
        static bool CheckNewTile(Point newTilePoint)
        {
            Tile newTile = _sprites.OfType<Tile>().Where(tile => tile._point == newTilePoint).FirstOrDefault();

            if (newTile._tileState != TileState.Empty)
            {
                return false;
            }
            else return true;

        }
        static bool CheckInteractiveTile(Point newTilePoint)
        {
            Tile newTile = _sprites.OfType<Tile>().Where(tile => tile._point == newTilePoint).FirstOrDefault();

            if (newTile._tileState == TileState.Interactive)
            {
                return true;
            }
            else return false;

        }

        public void GetDebugImage(Texture2D image)
        {
            _debugImage = image;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DebugBounds)
            {
                spriteBatch.Draw(_debugImage, _detection, Color.Red);
            }
            base.Draw(spriteBatch);
        }
    }
}
