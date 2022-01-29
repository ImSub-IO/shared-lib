
namespace ImSubShared.Logger.Configuration
{
    public class ImSubLoggerConfiguration
    {
        public string ServiceName { get; set; }

        public bool IsValid()
        { 
            return !string.IsNullOrWhiteSpace(ServiceName);
        }
    }
}
