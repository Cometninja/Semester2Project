using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Semester2Prototype.States;

namespace Semester2Prototype
{

    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private State _currentState;
        private State _nextState;

        static List<Sprite> _sprites = new List<Sprite>();
        static Player _player;
        static Texture2D _square,
            playerSpriteSheet,
            _messageBoxImage,
            _journalImage, 
            _floorSpriteSheet, 
            _npcSpriteSheet,
            _furnitureSheet;
        static Point _point = new Point(1500, 1250);

        public FurnitureFunctions _furnitureFunctions;
        public bool _isEscapedPressed;
        public float _volume = 1f;
        public State _state;
        public State _menuState;
        public SpriteFont _mainFont, buttonFont;
        public GameState _gameState = GameState.MainMenu;
        public FloorLevel _floorLevel = FloorLevel.GroundFLoor;
        public Point _windowSize = new Point(800, 500);
        public Texture2D _rectangleTxr, _backgroundTxr, buttonTexture;
        public List<List<Point>> _furnitureLocations;


        protected Song song;

        Point _playerPoint = new Point(0, 0);
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = _windowSize.X;
            _graphics.PreferredBackBufferHeight = _windowSize.Y;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _menuState = new MenuState(this, GraphicsDevice, Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _furnitureFunctions = new FurnitureFunctions(this);
            _furnitureLocations = _furnitureFunctions.PlaceFurniture();
            

            song = Content.Load<Song>("Song");
            MediaPlayer.Play(song);
            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _square = Content.Load<Texture2D>("whiteSquare");
            playerSpriteSheet = Content.Load<Texture2D>("DetectiveSpriteSheet");
            _mainFont = Content.Load<SpriteFont>("mainFont");
            _journalImage = Content.Load<Texture2D>("LargeJournal");
            _floorSpriteSheet = Content.Load<Texture2D>("FloorTileSpriteSheet");
            _npcSpriteSheet = Content.Load<Texture2D>("NPCspritesheet");
            _furnitureSheet = Content.Load<Texture2D>("furniture spritesheet");
            _messageBoxImage = Content.Load<Texture2D>("MessageBox");


            var buttonTexture = Content.Load<Texture2D>("UI/Controls/Button");
            var buttonFont = Content.Load<SpriteFont>("UI/Fonts/Font");
            _rectangleTxr = Content.Load<Texture2D>("UI/RectangleTxr");
            _backgroundTxr = Content.Load<Texture2D>("UI/Txr_Background");

            MakeFloorPlan();

            _player = new Player(playerSpriteSheet, new Vector2(400, 250), _playerPoint, this);

            _sprites.Add(new MessageBox(_messageBoxImage, new Vector2(0, 0), _mainFont));
            _sprites.Add(_player);

            // add in the NPCs

            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(600, 250), NPCCharacter.Manager));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(650, 250), NPCCharacter.Receptionist));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(700, 250), NPCCharacter.Cleaner));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(750, 250), NPCCharacter.Chef));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(800, 250), NPCCharacter.Cook));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(850, 250), NPCCharacter.MrMontgomery));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(900, 250), NPCCharacter.MrsPark));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(950, 250), NPCCharacter.MsMayflower));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(1000, 250), NPCCharacter.MrSanders));
            _sprites.Add(new NPC(_npcSpriteSheet, new Vector2(1050, 250), NPCCharacter.MrRoss));

            _sprites.Add(new Clue(_square, new Vector2(600, 350), ClueType.ChefKnife));
            _sprites.Add(new Clue(_square, new Vector2(650, 350), ClueType.MayFlowerPhoto));
            _sprites.Add(new Clue(_square, new Vector2(700, 350), ClueType.DiscardedClothing));
            _sprites.Add(new Clue(_square, new Vector2(750, 350), ClueType.HotelMasterKey));
            _sprites.Add(new Clue(_square, new Vector2(800, 350), ClueType.HotelReceptionLogs));
            _sprites.Add(new Clue(_square, new Vector2(850, 350), ClueType.KitchenChecks));
            _sprites.Add(new Clue(_square, new Vector2(900, 350), ClueType.FinancialDocuments));
            _sprites.Add(new Clue(_square, new Vector2(950, 350), ClueType.VictimsDocuments));
            
            
            SetTileFurniture(_sprites ,_furnitureLocations);

            

            _sprites.Add(new Journal(_journalImage, new Vector2(0, 0), _mainFont,this));
            _player.GetDebugImage(_square);
        }
        void MediaPlayer_MediaStateChanged(object sender, System.
                                         EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            //MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
        }

        public void AdjustVolume(float change)
        {
            _volume += change;
            // Constrain volume to range [0, 1]
            if (_volume < 0)
            {
                _volume = 0;
            }
            else if (_volume > 1)
            {
                _volume = 1;
            }
            // Set volume for MediaPlayer
            MediaPlayer.Volume = _volume;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            List<Sprite> sprites = _sprites.Where(sprite => sprite.GetType() != _sprites.OfType<Tile>().First().GetType()).ToList();
            foreach (Sprite sprite in _sprites)
            {
                sprite.Update(_sprites);
            }

            switch (_gameState)
            {
                case GameState.MainMenu:
                    IsMouseVisible = true;
                    _menuState.Update(gameTime);
                    break;
                case GameState.GameStart:
                    break;
                case GameState.GamePlaying:
                    IsMouseVisible= false;
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_isEscapedPressed)
                        Exit();
                    else if (Keyboard.GetState().IsKeyUp(Keys.Escape) && _isEscapedPressed)
                    {
                        _isEscapedPressed = false;
                    }
                    if (_player._position.X % 50 == 0 && _sprites.OfType<Tile>().FirstOrDefault()._position.X % 50 == 0)
                    {
                        CheckChangeLevel();
                    }
                    MoveThePlayer();
                    break;
                case GameState.JournalScreen:
                    _sprites.OfType<Journal>().FirstOrDefault().Update(_sprites);
                    break;
                case GameState.Dialoge:
                    _player._dialoge.DialogeUpdate(gameTime);
                    DialogueControls();
                    break;
            }
            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            if (keyboardState.IsKeyDown(Keys.Escape) && !_isEscapedPressed)
            {
                _menuState = new PauseState(this, GraphicsDevice, Content);
                _isEscapedPressed = true;
                _gameState = GameState.MainMenu;
            }
            else if (!keyboardState.IsKeyDown(Keys.Escape) && _isEscapedPressed)
            {
                _isEscapedPressed = false;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SandyBrown);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw(_spriteBatch);
            }
            switch (_gameState)
            {
                case GameState.MainMenu:
                    _menuState.Draw(gameTime, _spriteBatch);
                    break;
                case GameState.GameStart:
                    break;
                case GameState.GamePlaying:
                    break;
                case GameState.JournalScreen:
                    break;
                case GameState.Dialoge:
                    _player._dialoge.DialogeDraw(_spriteBatch);
                    break;
                case GameState.Accusation:

                    Debug.WriteLine("You Have Been Accused");
                    
                    break;
            }
            base.Draw(gameTime);
            _spriteBatch.End();
        }
        public void MakeFloorPlan()
        {
            Vector2 adjustment = new Vector2(0, 750);
            for (int col = 0, y = 0; col < _point.Y; col += 50, y++)
            {
                for (int row = 0, x = 0; row < _point.X; row += 50, x++)
                {
                    _sprites.Add(new Tile(_floorSpriteSheet,_furnitureSheet, new Vector2(row, col) - adjustment, new Point(x, y), this));
                }
            }
        }
        static void MoveThePlayer()
        {
            _player.PlayerMove(_player);
        }
        public void DialogueControls()
        {
            NPC npc = _sprites.OfType<NPC>().Where(x => x._dialoge == true).FirstOrDefault();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_isEscapedPressed)
            {
                _isEscapedPressed = true;
                npc._moving = npc._lastMove;
                _gameState = GameState.GamePlaying;
            }
        }
        public void CheckChangeLevel()
        {
            List<Tile> tiles = _sprites.OfType<Tile>().ToList();
            switch (_floorLevel)
            {
                case FloorLevel.GroundFLoor:
                    int[] groundUp = new int[] { 17, 18, 19 };
                    if (_player._point.X == 24 && groundUp.Contains(_player._point.Y))
                    {
                        _floorLevel = FloorLevel.FirstFloor;
                        _player._position.X -= 50; 
                        _player._point.X -= 1;
                        _player.Update(_sprites);

                        foreach (Tile tile in tiles)
                        {
                            tile._position.Y += 200;
                            tile._furniture = Furniture.None;
                            tile.SetFurniture();
                        }

                        SetTileFurniture(_sprites,_furnitureFunctions.PlaceFurniture());
                        ChangeLevel();
                    }

                    break;
                case FloorLevel.FirstFloor:
                    int[] firstDown = new int[] { 13, 14, 15 };
                    int[] firstUp = new int[] { 9, 10, 11 };
                    if (_player._point.X == 24 && firstDown.Contains(_player._point.Y))
                    {
                        _floorLevel = FloorLevel.GroundFLoor;
                        _player._position.X -= 50;
                        _player._point.X -= 1;
                        _player.Update(_sprites);
                        foreach (Tile tile in tiles)
                        {
                            tile._position.Y -= 200;
                            tile._furniture = Furniture.None;
                            tile.SetFurniture();
                        }
                        SetTileFurniture(_sprites, _furnitureFunctions.PlaceFurniture());

                        ChangeLevel();
                    }
                    else if (_player._point.X == 24 && firstUp.Contains(_player._point.Y))
                    {
                        _floorLevel = FloorLevel.SecondFLoor;
                        _player._position.X -= 50;
                        _player._point.X -= 1;
                        _player.Update(_sprites);
                        foreach (Tile tile in tiles)
                        {
                            tile._position.Y -= 200;
                            tile._furniture = Furniture.None;
                            tile.SetFurniture();
                        }
                        SetTileFurniture(_sprites, _furnitureFunctions.PlaceFurniture());
                        ChangeLevel();
                    }
                    break;
                case FloorLevel.SecondFLoor:
                    int[] SecondDown = new int[] { 13, 14, 15 };

                    if (_player._point.X == 24 && SecondDown.Contains(_player._point.Y))
                    {
                        _floorLevel = FloorLevel.FirstFloor;
                        _player._position.X -= 50;
                        _player._point.X -= 1;
                        _player.Update(_sprites);
                        foreach (Tile tile in tiles)
                        {
                            tile._position.Y += 200;
                            tile._furniture = Furniture.None;
                            tile.SetFurniture();
                        }
                        SetTileFurniture(_sprites, _furnitureFunctions.PlaceFurniture());

                        ChangeLevel();
                    }
                    break;
            }
        }
        public void ChangeLevel()
        {
            _player._position.X -= 50;
            _player._moving = Moving.Still;
            _player._playerFacing = Facing.Left;
            _player._sourceRect = new Rectangle(0, 104, 36, 52);

            List<Tile> tiles = _sprites.OfType<Tile>().ToList();
            foreach (Tile t in tiles)
            {
                t.SetUpFLoorPlan();
            }
        }
        public void ChangeState(State state)
        {
            _menuState = state;
            _currentState = state;
        }
        static void SetTileFurniture(List<Sprite> sprites, List<List<Point>> furnitureLocations)
        {
            foreach (Tile tile in sprites.OfType<Tile>().ToList())
            {
                for (int i = 0; i < furnitureLocations.Count; i++) 
                {
                    if (furnitureLocations[i].Contains(tile._point))
                    {
                        tile._furniture = (Furniture)i;
                    }
                }

                tile.SetFurniture();
                tile.SetUpFLoorPlan();
                tile.ContainsNPC(sprites);
            }
            

        }

    }
}
