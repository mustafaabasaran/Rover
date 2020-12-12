namespace Rover.Helper
{
    public static class Extensions
    {
        public static bool IsAllowedCourse(this char course)
        {
            if (!Helper.Constants.AllowedCourses.Contains(course))
                return false;
            
            return true;
        }

        public static bool IsAllowedMovements(this char move)
        {
            if (!Helper.Constants.AllowedMovements.Contains(move))
                return false;
            
            return true;
        }
        
    }
}