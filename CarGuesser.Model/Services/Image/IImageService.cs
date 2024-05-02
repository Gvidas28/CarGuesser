namespace CarGuesser.Model.Services.Image;

public interface IImageService
{
    string GetFirstGoogleImageUrl(string searchPrompt);
}