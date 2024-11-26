namespace ServerApp.Settings
{
    public class JwtOptions
    {

        public string Key { get; set; }
        public int ExpirationMinute { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
