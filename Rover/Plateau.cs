using Rover.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Rover.Helper;

namespace Rover
{
    public class Plateau 
    {
        private ILogger<Plateau> _logger;

        public Plateau(ILogger<Plateau> logger)
        {
            _logger = logger;
        }

        public int Withd { get; private set; }

        public int Height { get; private set; }

        public List<ISpaceCar> SpaceCars { get; private set; }


        public string RunScenario()
        {
            _logger.LogInformation("Plato scenario running.");

            foreach (var car in SpaceCars)
            {
                foreach (var command in car.Commands)
                {
                    if (Enum.TryParse<Constants.Movement>(command.ToString(), out var movement))
                    {
                        car.ExecuteCommandOneByOne(movement);
                        
                        if(car.CarId != 1)
                            CompareRoverLocationToOtherRovers(car);
                        
                        CheckRoverLocationInPlateau(car);
                    }
                }
            }
            _logger.LogInformation("Scenario runned succesfully.");
            return GenerateReturnValue();
        }

        /// <summary>
        /// Uzay aracının platodaki konumunu kontrol eder.  
        /// </summary>
        /// <param name="spaceCar"></param>
        /// <exception cref="FallsOutsideOfPlateauException"> Plato sınırı dışına çıktığında fırlatılır</exception>
        public void CheckRoverLocationInPlateau(ISpaceCar spaceCar)
        {
            if (spaceCar.XAxis > Withd
                || spaceCar.YAxis > Height
                || spaceCar.XAxis < 0
                || spaceCar.YAxis < 0)
            {
                throw new FallsOutsideOfPlateauException($"{spaceCar.GetType().Name} tries to go outside of plateau");
            }
        }

        /// <summary>
        /// Uzay aracının diğer uzay araçları ile olan konumunu kontrol eder.
        /// </summary>
        /// <param name="currentCar"></param>
        /// <exception cref="RoverCrashException"> Çarpışma durumunda fırlatılır </exception>
        public void CompareRoverLocationToOtherRovers(ISpaceCar currentCar)
        {
            //Kendi hariç diğer araçları alalım.
            var otherCars = SpaceCars.Where(x => x.CarId != currentCar.CarId);
                
            foreach (var car in otherCars)
            {
                if (car.XAxis == currentCar.XAxis
                    && car.YAxis == currentCar.YAxis)
                {
                    throw new RoverCrashException($"{car.CarId}. rover and {currentCar.CarId}. rover crashed.");
                }
            }
        }

        public string GenerateReturnValue()
        {
            _logger.LogInformation("Generating a return value");
            var sb = new StringBuilder();
            
            foreach (var car in SpaceCars)
            {
                sb.Append(car.XAxis);
                sb.Append(" ");
                sb.Append(car.YAxis);
                sb.Append(" ");
                sb.Append(car.Course.ToString());
                
                sb.Append("\n");
            }

            _logger.LogInformation("Return value generated.");

            return sb.ToString();
        }
        
        public Plateau Initialize(string input)
        {
            _logger.LogInformation("Plato initialize started.");

            SpaceCars = new List<ISpaceCar>();
            
            Rover rover = null;
            int carCount = 1 ;
            
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(Constants.InputEmpty);

            var inputList = input.Split(Constants.EndLine);
            if (inputList.Length < Constants.ExpectedCommandLineCount)
                throw new LessCommandCountThanExpectedException($"Command line count is less then {Constants.ExpectedCommandLineCount}");

            var plateauMeasurements = inputList[0].Split(' ');
            if (plateauMeasurements.Length != Constants.ExpectedPlateauMeasurementsCount)
                throw new PlateauSizeException($"Plateau measurement count is less then {Constants.ExpectedPlateauMeasurementsCount}");

            if (int.TryParse(plateauMeasurements[0], out var width)) 
                Withd = width;
            else
                throw new PlateauSizeException($"Plateau measurement must be {typeof(int)}");

            if (int.TryParse(plateauMeasurements[1], out var height)) 
                Height = height;
            else
                throw new PlateauSizeException($"Plateau measurement must be {typeof(int)}");

            _logger.LogInformation($"{Withd} x {Height} plateau has created.");
            
            var inputArrayList = inputList.ToList();
            inputArrayList.RemoveAt(0);

            int count = 0;
            
            foreach (var item in inputArrayList) //Rover kontrolleri
            {
                if (count % 2 == 0) //inputtaki 2. satır ile rover komutları başlıyor. ilk satırı saymazsak 0,2,4 gibi çift sayılar rover konum bilgilerini tutar.
                {
                    rover = new Rover() 
                    {
                        CarId = carCount
                    };
                    var roverLocationInformationList = item.Split(Constants.EmptyLine);
                    if (roverLocationInformationList.Length != Constants.ExcpectedRoverLocationInfomationCount )
                        throw new ExpectedRoverLocationInformationException($"Rover location information count must be {Constants.ExpectedPlateauMeasurementsCount}");

                    if (int.TryParse(roverLocationInformationList[0], out int xaxis))
                    {
                        if (xaxis < 0)
                            throw new ExpectedRoverLocationInformationException($"Rover location xaxis information must be greater than 0");
                        
                        rover.XAxis = xaxis;
                    }
                    else
                        throw new ExpectedRoverLocationInformationException($"Rover location xaxis information must be {typeof(int)}");

                    if (int.TryParse(roverLocationInformationList[1], out int yaxis))
                    {
                        if (yaxis < 0)
                            throw new ExpectedRoverLocationInformationException($"Rover location yaxis information must be greater than 0");
                        
                        rover.YAxis = yaxis;
                    }
                    else
                        throw new ExpectedRoverLocationInformationException($"Rover location yaxis information must be {typeof(int)}");

                    if (Enum.TryParse<Constants.Course>(roverLocationInformationList[2], out Constants.Course course))
                        rover.Course = course;
                    else
                        throw new ExpectedRoverLocationInformationException($"Rover location course information must be {typeof(Constants.Course)}");
                }
                else //1.3.5.. gibi tek satırlar roverların hareket bilgilerini tutar.
                {
                    foreach (var command in item)
                    {
                        if (!command.IsAllowedMovements())
                        {
                            throw new ExpectedMovementInformationException(
                                $"Command must be one of {Constants.AllowedMovements}");
                        }
                    }
                    
                    rover.Commands = item;
                    SpaceCars.Add(rover);
                    _logger.LogInformation($"{rover.CarId}. car placed to {rover.XAxis},{rover.YAxis}");
                    
                    carCount++;
                }
                
                count++;
            }

            _logger.LogInformation("plato initialize ended.");
            return this;
        }
    }
}