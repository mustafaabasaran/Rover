namespace Rover.Helper
{
    public static class Constants
    {
        public static readonly int ExpectedCommandLineCount = 5;
        public static readonly int ExpectedPlateauMeasurementsCount = 2;
        public static readonly int ExcpectedRoverLocationInfomationCount = 3;
        
        public static readonly string InputEmpty = "Input can not be empty.";
        public static readonly string EndLine = "\n";
        public static readonly string EmptyLine = " ";
        public static readonly string AllowedCourses = "WSNE";
        public static readonly string AllowedMovements = "MRL";
        
        public enum Course
        {
            /// <summary>
            /// Kuzey
            /// </summary>
            N,

            /// <summary>
            /// Doğu
            /// </summary>
            E,

            /// <summary>
            /// Güney
            /// </summary>
            S,

            /// <summary>
            /// Batı
            /// </summary>
            W
        }

        public enum Movement
        {
            L,
            R,
            M
        }
    }
}
