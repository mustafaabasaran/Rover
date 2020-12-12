using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Rover.UnitTest
{
    public class PlateauUnitTest 
    {
        private readonly Plateau _plateau;
        public PlateauUnitTest(Plateau plateau)
        {
            _plateau = plateau;
        }
        [Fact]
        public void Valid_Calc_Test()
        {
            string result = "1 3 N\n5 1 E\n";
            Assert.Equal(_plateau.Initialize("5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM").RunScenario(), result);
        }

        [Fact]
        public void Valid_Calc_Test2()
        {
            string result = "2 0 W\n0 3 W\n";
            Assert.Equal(_plateau.Initialize("4 4\n1 1 E\nMMRMRM\n2 2 W\nRMLMM").RunScenario(), result);
        }
        
        [Fact]
        public void Null_Argument_Test()
        {
            Assert.Throws<ArgumentNullException>(() => _plateau.Initialize(null).RunScenario());
        }
        
        [Fact]
        public void Wrong_Command_Test()
        {
            Assert.Throws<LessCommandCountThanExpectedException>(()=> _plateau.Initialize("5 5\n1 2 N\nLMLMLMLMM\nRMRMMMRMM").RunScenario());
        }


        [Fact]
        public void Wrong_Plateau_Size_Test()
        {
            Assert.Throws<PlateauSizeException>(() => _plateau.Initialize("5 A\n1 2 N\nLMLMLMLMM\n3 3\nRMMRMMM").RunScenario());
        }

        [Fact]
        public void Wrong_Rover_Location_Information()
        {
            Assert.Throws<ExpectedRoverLocationInformationException>(() => _plateau.Initialize("5 5\n1 N\nLMLMLMLMM\n3 3\nRMMRMMM").RunScenario());
        }
        
        [Fact]
        public void Wrong_Rover_Movement_Information()
        {
            Assert.Throws<ExpectedMovementInformationException>(() => _plateau.Initialize("5 5\n1 2 N\nLM3MLM1MM\n3 3 E\nRMMRMMM").RunScenario());
        }
        
        [Fact]
        public void Wrong_Rover_Course_Test()
        {
            Assert.Throws<ExpectedRoverLocationInformationException>(() => _plateau.Initialize("5 5\n1 2 Q\nLMMMLMMMM\n3 3 R\nRMMRMMM").RunScenario());
        }

        [Fact]
        public void Fall_Outside_Of_Plateau_Test()
        {
            Assert.Throws<FallsOutsideOfPlateauException>(() => _plateau.Initialize("5 5\n7 2 W\nLMMMLMMMM\n3 3 E\nRMMRMMM").RunScenario());
        }

        [Fact]
        public void Crash_Test()
        {
            Assert.Throws<RoverCrashException>(() => _plateau.Initialize("4 4\n1 1 E\nMM\n2 2 W\nRRMRM").RunScenario());
        }
        
        [Fact]
        public void Falls_Outside_Of_Plateau_On_Moving_Test()
        {
            Assert.Throws<FallsOutsideOfPlateauException>(() => _plateau.Initialize("4 4\n1 1 E\nLLMM\n2 2 W\nRRMRM").RunScenario()); 
        }
    }
}