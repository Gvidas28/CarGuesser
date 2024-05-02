using CarGuesser.Model.Entities.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Client;

public class ClientService : IClientService
{
    private readonly IConfiguration _configuration;

    public ClientService(
        IConfiguration configuration
        )
    {
        _configuration = configuration;
    }

    public async Task<Response> SendRequestAsync<Response>(Dictionary<string, string> queryParams)
    {
        var url = GetFullUrl(queryParams);

        var client = new RestClient();
        var request = new RestRequest(url, Method.Get);

        var res = await client.ExecuteAsync(request);
        if (res?.StatusCode != HttpStatusCode.OK)
            throw new InternalException($"Failed to communicate with the CarQuery API! {res?.StatusCode}: {res?.Content}");

        return JsonConvert.DeserializeObject<Response>(res.Content);
    }

    private string GetFullUrl(Dictionary<string, string> queryParams)
    {
        var baseUrl = _configuration["CarQuery:BaseUrl"];
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new InternalException("BaseUrl setting not configured for the CarQuery API!");

        var url = new UriBuilder(baseUrl);
        var paramList = new List<string>();

        foreach (var param in queryParams)
            paramList.Add($"{param.Key}={param.Value}");

        url.Query += string.Join("&", paramList);
        return url.Uri.AbsoluteUri;
    }
}