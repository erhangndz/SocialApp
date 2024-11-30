namespace ServerApp.Helpers
{
    public static class ExtensionMethods
    {
        public static int CalculateAge(this DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;

            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
            {
                age--;
            }
            return age;
        }
    }
}
