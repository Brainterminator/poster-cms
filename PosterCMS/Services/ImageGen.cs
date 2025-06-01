using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PosterCMS;

public class ImageGen
{
    public static void readyUp(){
        var browserFetcher = new BrowserFetcher();
        browserFetcher.DownloadAsync();
    }

    public static async Task<byte[]> GenerateA4Thumbnail(string url)
    {
        using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true
        });
        using var page = await browser.NewPageAsync();
        await page.SetViewportAsync(new ViewPortOptions
        {
            Width = 794,
            Height = 1122
        });

        await page.EmulateMediaTypeAsync(MediaType.Print);

        await page.GoToAsync(url);
        return await page.ScreenshotDataAsync(new ScreenshotOptions
        {
            Type = ScreenshotType.Jpeg,
            Quality = 70
        });
    }
}

