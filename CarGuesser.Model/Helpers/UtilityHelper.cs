using System;

namespace CarGuesser.Model.Helpers;

public static class UtilityHelper
{
    private static readonly Random _random = new Random();

    public static int GetRandomNumber(int minValue, int maxValue) => _random.Next(minValue, --maxValue);
}