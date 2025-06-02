using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PosterCMS;

public class ImageManager
{
    public static void readyUp()
    {
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

        await page.SetCacheEnabledAsync(false);

        await page.EmulateMediaTypeAsync(MediaType.Print);

        await page.GoToAsync(url, WaitUntilNavigation.Load);
        return await page.ScreenshotDataAsync(new ScreenshotOptions
        {
            Type = ScreenshotType.Jpeg,
            Quality = 70
        });
    }

    public static void SaveImage(byte[] imageBytes, String path)
    {
        // Ensure the directory exists
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        // Save the byte array as a JPEG
        System.IO.File.WriteAllBytes(filePath, imageBytes);

        return;
    }

    public static void DeleteImage(String path)
    {
        // Ensure the directory exists
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        return;
    }
}

