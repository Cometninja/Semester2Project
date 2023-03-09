using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;



namespace Semester2Prototype
{
    internal class Player : Sprite
    { 
        public Point _point;
        static PlayerFacing _playerFacing = PlayerFacing.Down;
        static Moving _moving = Moving.Still;
        static List<Sprite> _sprites;
        static int _animationCount = 0, tickCount, testCount;
        MessageBox _messageBox;
        static bool _isSpacePressed, _isEPressed,_isPPressed;
        static Journal _journal;
        static List<Tile> tiles;
        Dictionary<string, bool> _goals;

        static VarCollection varCollection = new VarCollection();


        public Player(Texture2D image, Vector2 position, Point point):base(image, position) 
        { 
            _point= point;
            _sourceRect = GetPlayerImage()[0][0];
            _goals = SetGoals();
        }

        public override void Update(List<Sprite> sprites)
        {
            _sprites = sprites;
            tiles = _sprites.OfType<Tile>().ToList();
            _messageBox = _sprites.OfType<MessageBox>().FirstOrDefault();
            _center =  new Vector2(_position.X+16,_position.Y+30);
        }

        public void PlayerControls(Player player)
        {
            Point playerPoint = player._point;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player._sourceRect = GetPlayerImage()[1][0];
                _playerFacing = PlayerFacing.Up;
                if (CheckNewTile(new Point(playerPoint.X, playerPoint.Y - 1)))
                {
                    _moving = Moving.Up;
                }
                else _messageBox.AddMessage("You can't walk through walls......");

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _playerFacing = PlayerFacing.Down;
                player._sourceRect = GetPlayerImage()[0][0];
                if (CheckNewTile(new Point(playerPoint.X, playerPoint.Y + 1)))
                {
                    _moving = Moving.Down;
                }
                else _messageBox.AddMessage("You can't walk through walls......");

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player._sourceRect = GetPlayerImage()[2][0];

                _playerFacing = PlayerFacing.Right;
                if (CheckNewTile(new Point(playerPoint.X + 1, playerPoint.Y)))
                {
                    _moving = Moving.Right;
                }
                else _messageBox.AddMessage("You can't walk through walls......");

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                _playerFacing = PlayerFacing.Left;
                player._sourceRect = GetPlayerImage()[3][0];

                if (CheckNewTile(new Point(playerPoint.X - 1, playerPoint.Y)))
                {
                    _moving = Moving.Left;
                }
                else _messageBox.AddMessage("You can't walk through walls......");
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !_isSpacePressed)
            {
                _messageBox.AddMessage($"Testing {testCount}.");
                _isSpacePressed = true;
                testCount++;
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.Space) && _isSpacePressed)
            {
                _isSpacePressed = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E) && !_isEPressed)
            {
                Point checkPoint = player._point;
                switch (_playerFacing)
                {
                    case PlayerFacing.Up:
                        checkPoint.Y--;
                        break;
                    case PlayerFacing.Down:
                        checkPoint.Y++;

                        break;
                    case PlayerFacing.Right:
                        checkPoint.X++;

                        break;
                    case PlayerFacing.Left:
                        checkPoint.X--;

                        break;
                }
                if (CheckInteractiveTile(checkPoint))
                {
                    _messageBox.AddMessage("it an interactive object!!!");
                    _goals["Test"] = true;
                }
                else
                {
                    _messageBox.AddMessage("its not an interactive object idiot!!!!");
                }
                
            }
            _journal = _sprites.OfType<Journal>().FirstOrDefault();
            if (Keyboard.GetState().IsKeyDown(Keys.P) && !_isPPressed && !_journal.DisplayJournal)
            {
                if (_goals["Test"])
                {
                    _journal.CurrentMessage(1);
                }
                else
                {
                    _journal.CurrentMessage(0);
                }
                varCollection.test++;
                _journal.DisplayJournal = true;
                _isPPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P) && !_isPPressed && _journal.DisplayJournal)
            {
                _journal.DisplayJournal = false;
                _isPPressed = true;
            }
            else if (!Keyboard.GetState().IsKeyDown(Keys.P) && _isPPressed)
            {
                _isPPressed = false;
            }
        }

        public void PlayerMove(Player player)
        {
            Tile sourceTile = _sprites.OfType<Tile>().FirstOrDefault();
            switch (_moving)
            {

                case Moving.Up:

                    player._sourceRect = GetPlayerImage()[1][_animationCount];
                    if (player._position.Y == 150)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.Y++;
                        }
                    }
                    else
                    {
                        player._position.Y--;
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
                            tile._position.Y--;
                        }
                    }
                    else
                    {
                        player._position.Y++;
                    }
                    if (player._position.Y % 50 == 0 && sourceTile._position.Y % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[0][0];
                    }
                    break;
                case Moving.Right:
                    player._sourceRect = GetPlayerImage()[2][_animationCount];
                    if (player._position.X == 500)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.X--;
                        }
                    }
                    else
                    {
                        player._position.X++;
                    }
                    if (player._position.X % 50 == 0 && sourceTile._position.X % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[2][0];
                    }
                    break;
                case Moving.Left:
                    player._sourceRect = GetPlayerImage()[3][_animationCount];
                    if (player._position.X == 300)
                    {
                        foreach (Tile tile in tiles)
                        {
                            tile._position.X++;
                        }
                    }
                    else
                    {
                        player._position.X--;
                    }
                    if (player._position.X % 50 == 0 && sourceTile._position.X % 50 == 0)
                    {
                        _moving = Moving.Still;
                        player._sourceRect = GetPlayerImage()[3][0];
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
            { }
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
            animations[2].Add(new Rectangle(0, 104, 36, 52));
            animations[2].Add(new Rectangle(36, 104, 36, 52));
            animations[2].Add(new Rectangle(0, 104, 36, 52));
            animations[2].Add(new Rectangle(72, 104, 36, 52));

            // Left animation
            animations[1].Add(new Rectangle(0, 52, 36, 52));
            animations[1].Add(new Rectangle(36, 52, 36, 52));
            animations[1].Add(new Rectangle(0, 52, 36, 52));
            animations[1].Add(new Rectangle(72, 52, 36, 52));

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
        static Dictionary<string,bool> SetGoals()
        {
            Dictionary<string, bool> goals = new Dictionary<string, bool>();
            
            goals.Add("Test", false);
            
            return goals;
        }
    }
    enum PlayerFacing { Up,Down,Left,Right }
}
