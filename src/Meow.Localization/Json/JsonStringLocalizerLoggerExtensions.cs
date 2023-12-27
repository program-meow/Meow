﻿namespace Meow.Localization.Json;

/// <summary>
/// Json本地化日志扩展
/// </summary>
internal static class JsonStringLocalizerLoggerExtensions {
    private static readonly Action<ILogger , string , string , CultureInfo , System.Exception> _searchedLocation;

    static JsonStringLocalizerLoggerExtensions() {
        _searchedLocation = LoggerMessage.Define<string , string , CultureInfo>(
            LogLevel.Debug ,
            1 ,
            $"{nameof( JsonStringLocalizer )} searched for '{{Key}}' in '{{LocationSearched}}' with culture '{{Culture}}'." );
    }

    public static void SearchedLocation( this ILogger logger , string key , string searchedLocation , CultureInfo culture ) {
        _searchedLocation( logger , key , searchedLocation , culture , null );
    }
}