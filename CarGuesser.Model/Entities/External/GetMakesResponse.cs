using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarGuesser.Model.Entities.External;
public class GetMakesResponse
{
    [JsonProperty("Makes")]
    public List<CarMake> Makes { get; set; }
}