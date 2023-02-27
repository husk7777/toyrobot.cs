using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotTest.Data;
using ToyRobotTest.Objects;

namespace ToyRobotTest.Functions
{
    public static class ToyRobotWorker
    {
        public static void Main()
        {
            string fileName = "instructions.txt";
            var commands = ImportFile(fileName);
            var robot = new Robot();
            foreach (var commandString in commands)
            {
                try
                {
                    var validatedCommand = ValidateCommand(commandString, out string[] args);
                    var result = ExecuteCommand(robot, validatedCommand, args);
                    Console.WriteLine(commandString + (string.IsNullOrWhiteSpace(result) ? "" : ": " + result));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(commandString + ": " + ex.Message);
                }
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Program complete, press any key to exit...");
            Console.ReadKey(false);
        }

        private static string[] ImportFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadAllLines(fileName);
            }
            throw new InvalidOperationException("File was not provided");
        }

        private static Enums.Commands ValidateCommand(string command, out string[] args)
        {
            var splitCommand = command.Split(' ');
            args = new string[0];
            if (splitCommand.Count() == 1)
            {
                try
                {
                    if (Enum.Parse<Enums.Commands>(command) != Enums.Commands.PLACE)
                        return Enum.Parse<Enums.Commands>(command);
                }
                catch
                {
                    throw new ArgumentException("Invalid command provided");
                }
            }
            else if (splitCommand.Count() == 2)
            {
                if (Enum.TryParse<Enums.Commands>(splitCommand[0], out Enums.Commands commandEnum) && commandEnum == Enums.Commands.PLACE)
                {
                    if (ValidatePlaceArgs(splitCommand[1], out string[] validArgs))
                    {
                        args = validArgs;
                        return Enums.Commands.PLACE;
                    }
                    else
                    {
                        throw new ArgumentException("Place command was sumbitted with invalid arguments");
                    }
                }
            }
            throw new ArgumentException("Invalid command provided");
        }

        private static bool ValidatePlaceArgs(string argString, out string[] args)
        {
            args = argString.Split(',');
            if (args.Count() != 3) return false;
            if (!(int.TryParse(args[0], out int xpos) && xpos > 0 && xpos <= SetupParameters.BoardWidth)) return false;
            if (!(int.TryParse(args[1], out int ypos) && ypos > 0 && ypos <= SetupParameters.BoardHeight)) return false;
            if (!Enum.TryParse<Enums.Direction>(args[2], out Enums.Direction direction) || !Enum.IsDefined<Enums.Direction>(direction)) return false;
            return true;
        }


        //Using this approach of executing the commands and using the setter validation to stop invalid commands only works because each command only
        //updates one property at a time (besides place, but the ValidateCommand method handles incorrect parameters). If any command was added/modified
        //to update more than one parameter per operation, this would need to be reworked
        private static string ExecuteCommand(Robot robot, Enums.Commands command, string[] args)
        {
            string result = "";

            switch (command)
            {
                case Enums.Commands.PLACE:
                    robot.Place(args);
                    break;
                case Enums.Commands.MOVE:
                    robot.Move();
                    break;
                case Enums.Commands.RIGHT:
                    robot.Turn(1);
                    break;
                case Enums.Commands.LEFT:
                    robot.Turn(-1);
                    break;
                case Enums.Commands.REPORT:
                    result = robot.Report();
                    break;
                default:
                    throw new InvalidOperationException("Invalid command provided");
            }
            return result;
        }





    }
}
