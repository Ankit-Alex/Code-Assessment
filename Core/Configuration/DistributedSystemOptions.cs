namespace Core.Configuration
{
    public class DistributedSystemOptions
    {
        public const string ConfigurationKey = "DistributedSystem";
        
        /// <summary>
        /// The unique identifier for this node in the distributed system (0-9)
        /// </summary>
        public int NodeId { get; set; }
    }
} 