﻿using System.Text.Json.Serialization;

namespace JsonSerializerOptionsExample;

[JsonSerializable(typeof(WeatherForecast))]
internal partial class WeatherSerializerContext : JsonSerializerContext
{
}
