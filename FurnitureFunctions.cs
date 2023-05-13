using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Semester2Prototype
{
    public class FurnitureFunctions
    {
        Game1 _game1;
        public FurnitureFunctions(Game1 game1) 
        {
            _game1 = game1;
        }

        public List<List<Point>> PlaceFurniture()
        {
            List<List<Point>> furniture = new List<List<Point>>();

            //tables 0
            furniture.Add(new List<Point>());
            //CounterTop 1
            furniture.Add(new List<Point>());
            //chair 2
            furniture.Add(new List<Point>());
            //Sofa 3
            furniture.Add(new List<Point>());
            //Toilet 4
            furniture.Add(new List<Point>());
            //Cabnet 5
            furniture.Add(new List<Point>());
            //shelfs 6
            furniture.Add(new List<Point>());
            //Locker 7
            furniture.Add(new List<Point>());

            switch (_game1._floorLevel)
            {
                case FloorLevel.GroundFLoor:
                    furniture[0].Add(new Point(5, 1));
                    furniture[0].Add(new Point(13, 1));
                    furniture[0].Add(new Point(14, 1));
                    furniture[0].Add(new Point(2, 7));
                    furniture[0].Add(new Point(3, 7));
                    furniture[0].Add(new Point(4, 7));
                    furniture[0].Add(new Point(21, 8));
                    furniture[0].Add(new Point(21, 9));
                    furniture[0].Add(new Point(27, 8));
                    furniture[0].Add(new Point(27, 9));
                    furniture[0].Add(new Point(1, 12));
                    furniture[0].Add(new Point(20, 12));
                    furniture[0].Add(new Point(21, 12));
                    furniture[0].Add(new Point(26, 13));
                    furniture[0].Add(new Point(27, 13));
                    furniture[0].Add(new Point(6, 16));
                    furniture[0].Add(new Point(6, 17));
                    furniture[0].Add(new Point(8, 16));
                    furniture[0].Add(new Point(8, 17));
                    furniture[0].Add(new Point(8, 17));
                    furniture[0].Add(new Point(3, 20));
                    furniture[0].Add(new Point(3, 21));

                    break;
                case FloorLevel.FirstFloor:

                    break;
                case FloorLevel.SecondFLoor:

                    break;

            }
            return furniture;
        }
    }
}
