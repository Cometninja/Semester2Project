using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2Prototype
{
    public enum Facing { Up, Down, Left, Right }
    public enum Moving { Still, Down, Up, Left, Right }
    public enum GameState { GameStart, GamePlaying, JournalScreen, Dialoge }
    public enum FloorLevel { GroundFLoor, FirstFloor, SecondFLoor }
    enum TileState { Empty, Interactive, Wall }
}
