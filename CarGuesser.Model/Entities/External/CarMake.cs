using Newtonsoft.Json;

namespace CarGuesser.Model.Entities.External;
public class CarMake
{
    [JsonProperty("make_id")]
    public string MakeId { get; set; }
}