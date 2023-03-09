using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2Prototype
{
    internal class Enums
    {
    }
    enum Facing { Up, Down, Left, Right }
    enum Moving { Still, Down, Up, Left, Right }
    enum GameState { GameStart, GamePlaying, JournalScreen }
    enum FloorLevel { GroundFLoor, FirstFloor, SecondFLoor }
    enum TileState { Empty, Interactive, Wall }


}
