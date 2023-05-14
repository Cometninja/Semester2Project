namespace Semester2Prototype
{
    public enum Facing 
    { 
        Up, 
        Down, 
        Left, 
        Right 
    }
    public enum Moving
    { 
        Still, 
        Down, 
        Up, 
        Left, 
        Right 
    }
    public enum GameState 
    { 
        GameStart, 
        GamePlaying, 
        JournalScreen, 
        Dialoge, 
        MainMenu, 
        Pause, 
        Options, 
        Keybinds, 
        Sound 
    }
    public enum FloorLevel 
    { 
        GroundFLoor, 
        FirstFloor, 
        SecondFLoor 
    }
    public enum TileState 
    { 
        Empty, 
        Interactive, 
        Wall 
    }
    public enum NPCCharacter 
    { 
        Manager, 
        Receptionist, 
        Cleaner, 
        Chef, 
        Cook, 
        MrMontgomery, 
        MrsPark, 
        MsMayflower, 
        MrSanders, 
        MrRoss 
    }
    public enum ClueType 
    { 
        ChefKnife,
        MayFlowerPhoto,
        DiscardedClothing,
        HotelMasterKey,
        HotelReceptionLogs,
        KitchenChecks,
        FinancialDocuments,
        VictimsDocuments
    }
    public enum JournalPage 
    { 
        Tasks, 
        Clues, 
        Suspects, 
        page4 
    }
    public enum Furniture 
    { 
        None,
        Table,
        CounterTop,
        ChairLeft, 
        ChairRight,
        ChairDown,
        ChairUp,
        Sofa,
        Toilet,
        Cabnet,
        Shelves,
        Locker
    } //TODO add in more furniture

}
