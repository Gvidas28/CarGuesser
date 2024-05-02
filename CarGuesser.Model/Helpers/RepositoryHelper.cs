using System.Collections.Generic;
using System.Linq;

namespace CarGuesser.Model.Helpers;

public static class RepositoryHelper
{
    public static dynamic MapToDynamic(this object entity)
    {
        var variables = new Dictionary<string, object>();
        var properties = entity.GetType().GetProperties().ToList();
        properties.ForEach(x => variables.Add(x.Name, x.GetValue(entity)));
        return variables;
    }
}