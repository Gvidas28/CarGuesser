using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarGuesser.Model.Entities.External;

public class GetTrimsResponse
{
    [JsonProperty("Trims")]
    public List<CarTrim> Trims { get; set; } 
}