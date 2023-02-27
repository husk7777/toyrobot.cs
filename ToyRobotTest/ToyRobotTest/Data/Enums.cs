using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotTest.Data
{
    public class Enums
    {
        public enum Direction
        {
            NORTH = 0,
            EAST = 1,
            SOUTH = 2,  
            WEST = 3,
        }

        public enum Commands
        {
            PLACE = 1,
            MOVE = 2,
            LEFT = 3,
            RIGHT = 4,
            REPORT = 5
        }
    }
}
