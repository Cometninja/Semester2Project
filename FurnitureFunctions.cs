using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
            //CounterTop 1
            //chair left 2
            //chair right 3
            //chair down 4
            //chair up 5
            //Sofa 6
            //Toilet 7 
            //Cabnet 8
            //shelfs 9
            //Locker 10
            //Bed 11
            //Shower 12
            //CoffeeTable 13 
            //Wardrobe 14 
            //Sink 15
            //cupboard
            //stairs 17
            //
            for (int i = 0; i <= 17; i++)
            {
                furniture.Add(new List<Point>());
            }

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
                    furniture[1].Add(new Point(15, 5));
                    furniture[1].Add(new Point(16, 5));
                    furniture[1].Add(new Point(16, 6));
                    furniture[1].Add(new Point(8, 7));
                    furniture[1].Add(new Point(10, 7));
                    furniture[1].Add(new Point(11, 7));
                    furniture[1].Add(new Point(13, 7));
                    furniture[1].Add(new Point(14, 7));
                    furniture[1].Add(new Point(16, 7));
                    furniture[1].Add(new Point(7, 9));
                    furniture[1].Add(new Point(8, 9));
                    furniture[1].Add(new Point(10, 9));
                    furniture[1].Add(new Point(14, 10));
                    furniture[1].Add(new Point(14, 11));
                    furniture[1].Add(new Point(14, 12));
                    furniture[1].Add(new Point(14, 13));

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

                    //Toilet
                    furniture[7].Add(new Point(23, 1));
                    furniture[7].Add(new Point(27, 1));

                    //Cabnet 8
                    furniture[8].Add(new Point(1, 1));
                    furniture[8].Add(new Point(1, 6));
                    furniture[8].Add(new Point(5, 6));
                    furniture[8].Add(new Point(15, 15));
                    furniture[8].Add(new Point(16, 15));
                    furniture[8].Add(new Point(28, 21));
                    furniture[8].Add(new Point(28, 22));
                    //shelfs 9
                    furniture[9].Add(new Point(4, 3));
                    furniture[9].Add(new Point(4, 3));
                    furniture[9].Add(new Point(7, 5));
                    furniture[9].Add(new Point(16, 7));
                    furniture[9].Add(new Point(5, 9));
                    furniture[9].Add(new Point(12, 13));
                    furniture[9].Add(new Point(14, 13));
                    furniture[9].Add(new Point(13, 15));
                    furniture[9].Add(new Point(25, 21));
                    furniture[9].Add(new Point(26, 21));
                    //Locker 10
                    furniture[10].Add(new Point(8, 1));
                    furniture[10].Add(new Point(9, 1));
                    furniture[10].Add(new Point(10, 1));
                    furniture[10].Add(new Point(7, 11));
                    furniture[10].Add(new Point(8, 11));
                    furniture[10].Add(new Point(26, 23));

                    //stairs
                    furniture[17].Add(new Point(25, 17));
                    furniture[17].Add(new Point(25, 18));
                    furniture[17].Add(new Point(25, 19));
                    furniture[17].Add(new Point(26, 18));
                    furniture[17].Add(new Point(26, 19));
                    furniture[17].Add(new Point(27, 19));


                    break;
                case FloorLevel.FirstFloor:



                    //table 0
                    furniture[0].Add(new Point(1, 4));
                    furniture[0].Add(new Point(9, 5));
                    furniture[0].Add(new Point(9, 6));
                    furniture[0].Add(new Point(9, 7));
                    furniture[0].Add(new Point(11, 4));
                    furniture[0].Add(new Point(19, 5));
                    furniture[0].Add(new Point(19, 6));
                    furniture[0].Add(new Point(19, 7));
                    furniture[0].Add(new Point(1, 17));
                    furniture[0].Add(new Point(1, 18));
                    furniture[0].Add(new Point(1, 19));
                    furniture[0].Add(new Point(9, 20));
                    furniture[0].Add(new Point(11, 17));
                    furniture[0].Add(new Point(11, 18));
                    furniture[0].Add(new Point(11, 19));
                    furniture[0].Add(new Point(19, 20));
                    furniture[0].Add(new Point(25, 1));
                    furniture[0].Add(new Point(26, 1));
                    furniture[0].Add(new Point(25, 6));
                    furniture[0].Add(new Point(26, 6));
                    furniture[0].Add(new Point(25, 22));
                    furniture[0].Add(new Point(25, 23));
                    furniture[0].Add(new Point(28, 22));
                    furniture[0].Add(new Point(28, 23));

                    //chair left 2
                    furniture[2].Add(new Point(2, 18));
                    furniture[2].Add(new Point(12, 18));
                    //chair right 3
                    furniture[3].Add(new Point(8, 6));
                    furniture[3].Add(new Point(18, 6));
                    //chair up 5
                    furniture[5].Add(new Point(26, 7));
                    //sofa 6
                    furniture[6].Add(new Point(7, 1));
                    furniture[6].Add(new Point(8, 1));
                    furniture[6].Add(new Point(17, 1));
                    furniture[6].Add(new Point(18, 1));
                    furniture[6].Add(new Point(2, 23));
                    furniture[6].Add(new Point(3, 23));
                    furniture[6].Add(new Point(12, 23));
                    furniture[6].Add(new Point(13, 23));
                    //toilet 7
                    furniture[7].Add(new Point(3, 6));
                    furniture[7].Add(new Point(13, 6));
                    furniture[7].Add(new Point(7, 15));
                    furniture[7].Add(new Point(17, 15));
                    //shelf 9
                    furniture[9].Add(new Point(9, 8));
                    furniture[9].Add(new Point(9, 9));
                    furniture[9].Add(new Point(19, 8));
                    furniture[9].Add(new Point(19, 9));
                    furniture[9].Add(new Point(1, 15));
                    furniture[9].Add(new Point(1, 16));
                    furniture[9].Add(new Point(11, 15));
                    furniture[9].Add(new Point(11, 16));

                    furniture[9].Add(new Point());
                    furniture[9].Add(new Point());
                    furniture[9].Add(new Point());

                    //bed 11
                    furniture[11].Add(new Point(1, 2));
                    furniture[11].Add(new Point(2, 2));
                    furniture[11].Add(new Point(3, 2));
                    furniture[11].Add(new Point(1, 3));
                    furniture[11].Add(new Point(2, 3));
                    furniture[11].Add(new Point(3, 3));

                    furniture[11].Add(new Point(11, 2));
                    furniture[11].Add(new Point(12, 2));
                    furniture[11].Add(new Point(13, 2));
                    furniture[11].Add(new Point(11, 3));
                    furniture[11].Add(new Point(12, 3));
                    furniture[11].Add(new Point(13, 3));

                    furniture[11].Add(new Point(7, 21));
                    furniture[11].Add(new Point(8, 21));
                    furniture[11].Add(new Point(9, 21));
                    furniture[11].Add(new Point(7, 22));
                    furniture[11].Add(new Point(8, 22));
                    furniture[11].Add(new Point(9, 22));

                    furniture[11].Add(new Point(17, 21));
                    furniture[11].Add(new Point(18, 21));
                    furniture[11].Add(new Point(19, 21));
                    furniture[11].Add(new Point(17, 22));
                    furniture[11].Add(new Point(18, 22));
                    furniture[11].Add(new Point(19, 22));

                    //shower 12
                    furniture[12].Add(new Point(1, 6));
                    furniture[12].Add(new Point(1, 7));
                    furniture[12].Add(new Point(2, 6));
                    furniture[12].Add(new Point(2, 7));

                    furniture[12].Add(new Point(11, 6));
                    furniture[12].Add(new Point(11, 7));
                    furniture[12].Add(new Point(12, 6));
                    furniture[12].Add(new Point(12, 7));

                    furniture[12].Add(new Point(8, 17));
                    furniture[12].Add(new Point(9, 17));
                    furniture[12].Add(new Point(8, 18));
                    furniture[12].Add(new Point(9, 18));

                    furniture[12].Add(new Point(18, 17));
                    furniture[12].Add(new Point(19, 17));
                    furniture[12].Add(new Point(18, 18));
                    furniture[12].Add(new Point(19, 18));

                    //coffeetable 13
                    furniture[13].Add(new Point(7, 3));
                    furniture[13].Add(new Point(8, 3));
                    furniture[13].Add(new Point(17, 3));
                    furniture[13].Add(new Point(18, 3));
                    furniture[13].Add(new Point(2, 21));
                    furniture[13].Add(new Point(3, 21));
                    furniture[13].Add(new Point(12, 21));
                    furniture[13].Add(new Point(13, 21));

                    //wardrobe 14
                    furniture[14].Add(new Point(6, 5));
                    furniture[14].Add(new Point(6, 6));

                    furniture[14].Add(new Point(28, 6));
                    furniture[14].Add(new Point(28, 7));

                    //sink 15
                    furniture[15].Add(new Point(1, 9));
                    furniture[15].Add(new Point(7, 15));
                    furniture[15].Add(new Point(13, 9));
                    furniture[15].Add(new Point(17, 15));

                    //cuboard 16
                    furniture[16].Add(new Point(1, 1));
                    furniture[16].Add(new Point(11, 1));
                    furniture[16].Add(new Point(4, 6));
                    furniture[16].Add(new Point(14, 6));
                    furniture[16].Add(new Point(6, 18));
                    furniture[16].Add(new Point(16, 18));
                    furniture[16].Add(new Point(19, 23));

                    furniture[16].Add(new Point(28, 1));
                    furniture[16].Add(new Point(28, 2));
                    furniture[16].Add(new Point(25, 17));
                    furniture[16].Add(new Point(26, 17));
                    furniture[16].Add(new Point(28, 21));






                    break;
                case FloorLevel.SecondFLoor:

                    //table 0
                    furniture[0].Add(new Point(1, 4));
                    furniture[0].Add(new Point(9, 5));
                    furniture[0].Add(new Point(9, 6));
                    furniture[0].Add(new Point(9, 7));
                    furniture[0].Add(new Point(11, 4));
                    furniture[0].Add(new Point(19, 5));
                    furniture[0].Add(new Point(19, 6));
                    furniture[0].Add(new Point(19, 7));
                    furniture[0].Add(new Point(1, 17));
                    furniture[0].Add(new Point(1, 18));
                    furniture[0].Add(new Point(1, 19));
                    furniture[0].Add(new Point(9, 20));
                    furniture[0].Add(new Point(11, 17));
                    furniture[0].Add(new Point(11, 18));
                    furniture[0].Add(new Point(11, 19));
                    furniture[0].Add(new Point(19, 20));

                    //chair left 2
                    furniture[2].Add(new Point(2, 18));
                    furniture[2].Add(new Point(12, 18));
                    //chair right 3
                    furniture[3].Add(new Point(8, 6));
                    furniture[3].Add(new Point(18, 6));

                    //sofa 6
                    furniture[6].Add(new Point(7, 1));
                    furniture[6].Add(new Point(8, 1));
                    furniture[6].Add(new Point(17, 1));
                    furniture[6].Add(new Point(18, 1));
                    furniture[6].Add(new Point(2, 23));
                    furniture[6].Add(new Point(3, 23));
                    furniture[6].Add(new Point(12, 23));
                    furniture[6].Add(new Point(13, 23));

                    // toilet 7
                    furniture[7].Add(new Point(3, 6));
                    furniture[7].Add(new Point(13, 6));
                    furniture[7].Add(new Point(7, 15));
                    furniture[7].Add(new Point(17, 15));

                    //shelf 9
                    furniture[9].Add(new Point(9, 8));
                    furniture[9].Add(new Point(9, 9));
                    furniture[9].Add(new Point(19, 8));
                    furniture[9].Add(new Point(19, 9));
                    furniture[9].Add(new Point(1, 15));
                    furniture[9].Add(new Point(1, 16));
                    furniture[9].Add(new Point(11, 15));
                    furniture[9].Add(new Point(11, 16));

                    //Bed 11
                    furniture[11].Add(new Point(1, 2));
                    furniture[11].Add(new Point(2, 2));
                    furniture[11].Add(new Point(3, 2));
                    furniture[11].Add(new Point(1, 3));
                    furniture[11].Add(new Point(2, 3));
                    furniture[11].Add(new Point(3, 3));

                    furniture[11].Add(new Point(11, 2));
                    furniture[11].Add(new Point(12, 2));
                    furniture[11].Add(new Point(13, 2));
                    furniture[11].Add(new Point(11, 3));
                    furniture[11].Add(new Point(12, 3));
                    furniture[11].Add(new Point(13, 3));

                    furniture[11].Add(new Point(7, 21));
                    furniture[11].Add(new Point(8, 21));
                    furniture[11].Add(new Point(9, 21));
                    furniture[11].Add(new Point(7, 22));
                    furniture[11].Add(new Point(8, 22));
                    furniture[11].Add(new Point(9, 22));

                    furniture[11].Add(new Point(17, 21));
                    furniture[11].Add(new Point(18, 21));
                    furniture[11].Add(new Point(19, 21));
                    furniture[11].Add(new Point(17, 22));
                    furniture[11].Add(new Point(18, 22));
                    furniture[11].Add(new Point(19, 22));

                    //shower 12
                    furniture[12].Add(new Point(1, 6));
                    furniture[12].Add(new Point(1, 7));
                    furniture[12].Add(new Point(2, 6));
                    furniture[12].Add(new Point(2, 7));

                    furniture[12].Add(new Point(11, 6));
                    furniture[12].Add(new Point(11, 7));
                    furniture[12].Add(new Point(12, 6));
                    furniture[12].Add(new Point(12, 7));

                    furniture[12].Add(new Point(8, 17));
                    furniture[12].Add(new Point(9, 17));
                    furniture[12].Add(new Point(8, 18));
                    furniture[12].Add(new Point(9, 18));

                    furniture[12].Add(new Point(18, 17));
                    furniture[12].Add(new Point(19, 17));
                    furniture[12].Add(new Point(18, 18));
                    furniture[12].Add(new Point(19, 18));

                    //coffeetable 13
                    furniture[13].Add(new Point(7, 3));
                    furniture[13].Add(new Point(8, 3));
                    furniture[13].Add(new Point(17, 3));
                    furniture[13].Add(new Point(18, 3));
                    furniture[13].Add(new Point(2, 21));
                    furniture[13].Add(new Point(3, 21));
                    furniture[13].Add(new Point(12, 21));
                    furniture[13].Add(new Point(13, 21));

                    //wardrobe 14
                    furniture[14].Add(new Point(6, 5));
                    furniture[14].Add(new Point(6, 6));

                    //sink 15
                    furniture[15].Add(new Point(1, 9));
                    furniture[15].Add(new Point(7, 15));
                    furniture[15].Add(new Point(13, 9));
                    furniture[15].Add(new Point(17, 15));

                    //cuboard 16
                    furniture[16].Add(new Point(1, 1));
                    furniture[16].Add(new Point(11, 1));
                    furniture[16].Add(new Point(4, 6));
                    furniture[16].Add(new Point(14, 6));
                    furniture[16].Add(new Point(6, 18));
                    furniture[16].Add(new Point(16, 18));
                    furniture[16].Add(new Point(19, 23));

                    //table 0
                    //chair left 2
                    //chair right 3
                    //sofa 6
                    //toilet 7
                    //shelf 9
                    //bed 11
                    //shower 12
                    //coffeetable 13
                    //wardrobe 14
                    //sink 15
                    //cuboard 16


                    break;

            }
            return furniture;
        }



    }
}
