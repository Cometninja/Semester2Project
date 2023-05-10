namespace Semester2Prototype
{
    public enum Facing { Up, Down, Left, Right }
    public enum Moving { Still, Down, Up, Left, Right }
    public enum GameState { GameStart, GamePlaying, JournalScreen, Dialoge, MainMenu, Pause, Options, Keybinds, Sound}
    public enum FloorLevel { GroundFLoor, FirstFloor, SecondFLoor }
    enum TileState { Empty, Interactive, Wall }
    public enum NPCCharacter { Manager,Receptionist,Cleaner,Chef,Cook,MrMontgomery,MrsPark,MsMayflower,MrSanders,MrRoss}
}
