﻿using Humanizer;
using Newtonsoft.Json.Serialization;

namespace MastermindVanHackathon.Configuration
{
    public class PascalCaseToUnderscoreContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName) => propertyName.Underscore();
    }
}