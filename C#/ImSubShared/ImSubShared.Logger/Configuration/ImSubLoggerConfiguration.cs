
namespace ImSubShared.Logger.Configuration
{
    internal class ImSubLoggerConfiguration
    {
        public string ServiceName { get; set; }

        public bool IsValid()
        { 
            return !string.IsNullOrWhiteSpace(ServiceName);
        }
    }
}
