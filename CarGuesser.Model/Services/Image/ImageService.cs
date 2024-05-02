using CarGuesser.Model.Entities.Exceptions;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace CarGuesser.Model.Services.Image;

public class ImageService : IImageService
{
    private readonly ILogger<ImageService> _logger;
    private readonly IConfiguration _configuration;

    public ImageService(
        ILogger<ImageService> logger,
        IConfiguration configuration
        )
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string GetFirstGoogleImageUrl(string searchPrompt)
    {
        try
        {
            var imageSearchUrl = string.Format(_configuration["Image:GoogleImageSeachUrl"], searchPrompt);

            var web = new HtmlWeb();
            var doc = web.Load(imageSearchUrl);

            var imageNodes = doc.DocumentNode.SelectNodes("//img[@data-src]");
            if (imageNodes is null || imageNodes.Count < 1)
                throw new InternalException("Could not find any images according to the search prompt!");

            return WebUtility.HtmlDecode(imageNodes[0].Attributes["data-src"].Value);
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{nameof(GetFirstGoogleImageUrl)}: Error ({ex.Message}) while trying to get image, using the default image instead!");
            return _configuration["Image:DefaultImage"];
        }
    }
}