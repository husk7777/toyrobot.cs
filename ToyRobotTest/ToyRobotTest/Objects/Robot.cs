using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotTest.Data;
using System.Linq;

namespace ToyRobotTest.Objects
{
    public class Robot
    {
        private int _direction;
        private int _xpos;
        private int _ypos;
        private bool _placed = false;

        public int Direction
        {
            get
            {
                if (Placed) return _direction;
                throw new InvalidOperationException("Robot has not been placed yet");
            }
            set {
                if(Enum.IsDefined(typeof(Enums.Direction), value))
                {
                    _direction = value;
                }
                else
                {
                    throw new ArgumentException("Value provided does not correspond with a cardinal direction");
                }
            }
        }

        public int XPos
        {
            get
            {
                if (Placed) return _xpos;
                throw new InvalidOperationException("Robot has not been placed yet");
            }
            set 
            { 
               if(value > 0 && value <= SetupParameters.BoardWidth)
                {
                    _xpos = value;
                } 
               else
                {
                    throw new ArgumentException("New x position is outside the board boundary");
                }
            }
        }

        public int YPos
        {
            get
            {
                if (Placed) return _ypos;
                throw new InvalidOperationException("Robot has not been placed yet");
            }
            set
            {
                if (value > 0 && value <= SetupParameters.BoardHeight)
                {
                    _ypos = value;
                }
                else
                {
                    throw new ArgumentException("New y position is outside the board boundary");
                }
            }
        }

        public bool Placed
        {
            get { return _placed; }
            set
            {
                if (value)
                {
                    _placed = value;
                }
            }
        }

        /// <summary>
        /// Accepts a positive or negative integer to turn the robot. +ve = right, -ve = left
        /// </summary>
        /// <param name="direction"></param>
        public void Turn(int direction)
        {
            int currentDirection = this.Direction;
            int newDirection = (currentDirection + 4 + (direction > 0?1:-1)) % 4;
            this.Direction = newDirection;
        }

        public void Move()
        {
            switch (Enum.Parse<Enums.Direction>(this.Direction.ToString()))
            {
                case Enums.Direction.NORTH:
                    this.YPos += 1;
                    break;
                case Enums.Direction.SOUTH:
                    this.YPos -= 1;
                    break;
                case Enums.Direction.EAST:
                    this.XPos += 1;
                    break;
                case Enums.Direction.WEST:
                    this.XPos -= 1; ;
                    break;
                default:
                    throw new ArgumentException("Robot's direction has not been set. Use the place command before using any other commands");
            }
        }

        public string Report()
        {
            return string.Format("{0},{1},{2}", this.XPos, this.YPos, (Enums.Direction)this.Direction);
        }

        public void Place(string[] args)
        {
            this.Placed = true;
            this.Direction = (int)Enum.Parse<Enums.Direction>(args[2]);
            this.XPos = int.Parse(args[0]);
            this.YPos = int.Parse(args[1]);
        }
    }
}
