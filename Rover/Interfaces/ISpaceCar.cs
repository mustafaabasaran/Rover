using System;
using System.Collections.Generic;
using System.Text;
using static Rover.Helper.Constants;

namespace Rover.Interfaces
{
    public interface ISpaceCar 
    {
        int CarId { get; set; }
        
        int XAxis { get; set; }

        int YAxis { get; set; }

        Course Course { get; set; }

        string Commands { get; set; }

        public void Move(Movement movement);
        
        public void ExecuteCommandOneByOne(Movement movement);
    }
}
