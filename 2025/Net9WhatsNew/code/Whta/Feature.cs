﻿
namespace Whta;

public class Feature
{
    [FeatureSwitchDefinition("Feature.IsSupported")]
    internal static bool IsSupported => AppContext.TryGetSwitch("Feature.IsSupported", out bool isEnabled) ? isEnabled : true;

    internal static void Implementation() => Console.WriteLine("Hello, Implementation!");

    [FeatureGuard(typeof(RequiresDynamicCodeAttribute))]
    internal static bool IsSupported1 => RuntimeFeature.IsDynamicCodeSupported;

    [RequiresDynamicCode("Feature requires dynamic code support.")]
    internal static void Implementation1() => Console.WriteLine("Hello, Implementation!");
}
