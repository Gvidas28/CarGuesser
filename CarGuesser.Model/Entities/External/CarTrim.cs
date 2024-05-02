using Newtonsoft.Json;

namespace CarGuesser.Model.Entities.External;

public class CarTrim
{
    [JsonProperty("model_id")]
    public string ModelId { get; set; }

    [JsonProperty("model_make_id")]
    public string ModelMakeId { get; set; }

    [JsonProperty("model_trim")]
    public string ModelTrim { get; set; }

    [JsonProperty("model_name")]
    public string ModelName { get; set; }

    [JsonProperty("model_year")]
    public string ModelYear { get; set; }

    [JsonProperty("model_engine_position")]
    public string ModelEnginePosition { get; set; }

    [JsonProperty("model_engine_type")]
    public string ModelEngineType { get; set; }

    [JsonProperty("model_drive")]
    public string ModelDrive { get; set; }

    [JsonProperty("model_doors")]
    public string ModelDoors { get; set; }

    [JsonProperty("make_country")]
    public string MakeCountry { get; set; }

    [JsonProperty("max_top_speed")]
    public string MaxTopSpeed { get; set; }

    [JsonProperty("make_display")]
    public string MakeDisplay { get; set; }

    [JsonProperty("model_engine_power_ps")]
    public string ModelEnginePowerPs { get; set; }

    [JsonProperty("model_transmission_type")]
    public string ModelTransmissionType { get; set; }
}