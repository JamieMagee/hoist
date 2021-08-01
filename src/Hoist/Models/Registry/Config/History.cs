using System;
using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Config
{
    public class History
    {
        [JsonPropertyName("created")]
        public string Created { get; set; }
        
        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
        
        [JsonPropertyName("empty_layer")]
        public bool? EmptyLayer { get; set; }
        
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((History)obj);
        }

        protected bool Equals(History other)
        {
            return Created == other.Created && CreatedBy == other.CreatedBy;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Created, CreatedBy);
        }
    }
}
