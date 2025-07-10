using PosterCMS.Models;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PosterCMS;

public class ContentManager
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

    public static async Task<byte[]> GenerateA3Thumbnail(string url)
    {
        using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true
        });
        using var page = await browser.NewPageAsync();
        await page.SetViewportAsync(new ViewPortOptions
        {
            Width = 1588,
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
    public static async Task<Stream> GeneratePDF(PosterModel model)
    {
        using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true
        });

        // Open a new page
        using var page = await browser.NewPageAsync();

        // Navigate to your target URL
        await page.GoToAsync("http://localhost:5299/Poster/Index/" + model.ID);

        var pdfStream = await page.PdfStreamAsync(new PdfOptions
        {
            Format = PaperFormat.A4
        });

        // Return the stream as a downloadable file
        return pdfStream;
    }

    public static async Task<Stream> GeneratePDFA3(PosterModel model)
    {
        using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true
        });

        // Open a new page
        using var page = await browser.NewPageAsync();

        // Navigate to your target URL
        await page.GoToAsync("http://localhost:5299/Poster/Index/" + model.ID);

        var pdfStream = await page.PdfStreamAsync(new PdfOptions
        {
            Format = PaperFormat.A3,
            Landscape = true
        });

        // Return the stream as a downloadable file
        return pdfStream;
    }
}


