namespace AgriToolWebApi.Common.Settings
{
    public class SecuritySettings
    {
        public int SaltSize { get; set; }
        public int Iterations { get; set; }
        public int KeySize { get; set; }
    }
}
