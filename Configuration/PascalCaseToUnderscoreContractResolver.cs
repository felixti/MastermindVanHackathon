using Newtonsoft.Json.Serialization;
using Humanizer;

namespace MastermindVanHackathon.Configuration
{
    public class PascalCaseToUnderscoreContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName) => propertyName.Underscore();
    }
}
