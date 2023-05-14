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
            //CounterTop 
            furniture.Add(new List<Point>());
            //chair left 
            furniture.Add(new List<Point>());
            //chair right 
            furniture.Add(new List<Point>());
            //chair down
            furniture.Add(new List<Point>());
            //chair up
            furniture.Add(new List<Point>());
            //Sofa 
            furniture.Add(new List<Point>());
            //Toilet 
            furniture.Add(new List<Point>());
            //Cabnet 
            furniture.Add(new List<Point>());
            //shelfs 
            furniture.Add(new List<Point>());
            //Locker 
            furniture.Add(new List<Point>());

            switch (_game1._floorLevel)
            {
                case FloorLevel.GroundFLoor:
                    //tables
                    furniture[0].Add(new Point(5, 1));
                    furniture[0].Add(new Point(13, 1));
                    furniture[0].Add(new Point(14, 1));
                    furniture[0].Add(new Point(2, 7));
                    furniture[0].Add(new Point(3, 7));
                    furniture[0].Add(new Point(4, 7));
                    furniture[0].Add(new Point(21, 8));
                    furniture[0].Add(new Point(21, 9));
                    furniture[0].Add(new Point(26, 8));
                    furniture[0].Add(new Point(26, 9));
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

                    //CounterTop
                    furniture[1].Add(new Point(8, 5));
                    furniture[1].Add(new Point(9, 5));
                    furniture[1].Add(new Point(11, 5));
                    furniture[1].Add(new Point(14, 5));
                    furniture[1].Add(new Point(15,5));
                    furniture[1].Add(new Point(16,5));
                    furniture[1].Add(new Point(16,6));
                    furniture[1].Add(new Point(8,7));
                    furniture[1].Add(new Point(10,7));
                    furniture[1].Add(new Point(11,7));
                    furniture[1].Add(new Point(13,7));
                    furniture[1].Add(new Point(14,7));
                    furniture[1].Add(new Point(16,7));
                    furniture[1].Add(new Point(7,9));
                    furniture[1].Add(new Point(8,9));
                    furniture[1].Add(new Point(10,9));
                    furniture[1].Add(new Point(14,10));
                    furniture[1].Add(new Point(14,11));
                    furniture[1].Add(new Point(14,12));
                    furniture[1].Add(new Point(14,13));

                    //ChairLeft
                    furniture[2].Add(new Point(22, 8));
                    furniture[2].Add(new Point(22, 9));
                    furniture[2].Add(new Point(27, 8));
                    furniture[2].Add(new Point(27, 9));

                    //ChairRight
                    furniture[3].Add(new Point(20, 8));
                    furniture[3].Add(new Point(20, 9));
                    furniture[3].Add(new Point(25, 8));
                    furniture[3].Add(new Point(25, 9));


                    //chair down
                    furniture[4].Add(new Point(20, 11));
                    furniture[4].Add(new Point(21, 11));
                    //chair up
                    furniture[5].Add(new Point(20, 13));
                    furniture[5].Add(new Point(21, 13));

                    //Sofa
                    furniture[6].Add(new Point(26, 11));
                    furniture[6].Add(new Point(27, 11));
                    furniture[6].Add(new Point(24, 12));
                    furniture[6].Add(new Point(24, 13));
                    furniture[6].Add(new Point(26, 15));
                    furniture[6].Add(new Point(27, 15));


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
