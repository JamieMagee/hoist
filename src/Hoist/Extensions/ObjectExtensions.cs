using System.Text.Json;

namespace Hoist.Extensions
{
    public static class CloningExtensions
    {
        public static T Clone<T>(this T source)
        {
            return ReferenceEquals(source, null) ? default : JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source));
        }
    }
}
