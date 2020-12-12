using System;
using System.Diagnostics.CodeAnalysis;

namespace Rover
{

    
    public class LessCommandCountThanExpectedException : Exception
    {
        public LessCommandCountThanExpectedException(string message) : base(message)
        {
            
        }
    }
    
    public class PlateauSizeException : Exception
    {
        public PlateauSizeException(string message) : base(message)
        {
            
        }
    }
    
    public class ExpectedRoverLocationInformationException : Exception
    {
        public ExpectedRoverLocationInformationException(string message) : base(message)
        {
               
        }
    }
    
    public class ExpectedMovementInformationException : Exception
    {
        public ExpectedMovementInformationException(string message) : base(message)
        {
            
        }
    }

    public class FallsOutsideOfPlateauException : Exception
    {
        public FallsOutsideOfPlateauException(string message) : base(message)
        {
            
        }
    }

    public class RoverCrashException : Exception
    {
        public RoverCrashException(string message) : base(message)
        {
            
        }
    }
    
}