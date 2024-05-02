using Newtonsoft.Json;
using System;

namespace CarGuesser.Model.Helpers;

public static class LogHelper
{
    public static string JsonToString(this object entity)
    {
        try
        {
            return JsonConvert.SerializeObject(entity);
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}