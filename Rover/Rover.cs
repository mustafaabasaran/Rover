using Rover.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Rover.Helper;
using static Rover.Helper.Constants;

namespace Rover
{
    public class Rover : ISpaceCar
    {

        public Rover(int xAxis, int yAxis, Course course, string commands, int carId)
        {
            XAxis = xAxis;
            YAxis = yAxis;
            Course = course;
            Commands = commands;
            CarId = carId;
        }

        public Rover()
        {
            
        }

        public int CarId { get; set; }
        
        public int XAxis { get; set; }

        public int YAxis { get; set; }

        public Course Course { get; set; }

        public string Commands { get; set; }

        public void Move(Movement movement)
        {
            if (movement == Movement.M)
                MoveForward();
            else
                Turn(movement);
        }

        public void Turn(Movement movement)
        {
            if (movement == Movement.L)
            {
                if (Course == Course.N)
                    Course = Course.W;
                else
                    Course -= 1;
            }
            else if (movement == Movement.R)
            {
                if (Course == Course.W)
                    Course = Course.N;
                else
                    Course += 1;
            }
        }

        public void MoveForward()
        {
            switch (Course)
            {
                case Course.N:
                    YAxis += 1;
                    break;

                case Course.E:
                    XAxis += 1;
                    break;

                case Course.S:
                    YAxis -= 1;
                    break;

                case Course.W:
                    XAxis -= 1;
                    break;

                default:
                    break;
            }
        }
        
        public void ExecuteCommandOneByOne(Movement movement)
        {
            Move(movement);
        }

    }
}
