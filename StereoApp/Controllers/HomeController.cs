using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StereoApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Blobs.Models;

namespace StereoApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IConfiguration _configuration;
    

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        _configuration = builder.Build();
    }

    public IActionResult Home()
    {
        return View();
    }

    public IActionResult Towers()
    {
        return View();
    }

    public IActionResult Bookshelves()
    {
        return View();
    }

    public IActionResult Subwoofers()
    {
        return View();
    }

    public IActionResult Amplifiers()
    {
        string? blobconnection = _configuration.GetConnectionString("BLOBConnString");
        string? blobAccessKey = _configuration.GetConnectionString("BLOBAccessKey");
        BlobContainerClient client = new BlobContainerClient(blobconnection, "amplifierphotos");

        List<Amplifier> amplifiers = new List<Amplifier>();

        Amplifier amplifier = new Amplifier();
        amplifier.Name = "Yamaha Amplifier";
        amplifier.Price = 50000;        

        amplifiers.Add(amplifier);

        return View(amplifiers);
    }

    public IActionResult CDPlayers()
    {
        return View();
    }   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public FileContentResult GetImage()
    {
        string? blobconnection = _configuration.GetConnectionString("BLOBConnString");
        string? blobAccessKey = _configuration.GetConnectionString("BLOBAccessKey");

        BlobServiceClient blobServiceClient = new BlobServiceClient(blobconnection);
        BlobClient blobClient = new BlobClient(blobconnection, "amplifierphotos", "Yamaha Amplifier.jpg");

        FileStream stream = new FileStream("Temp/Blob1.jpg", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        blobClient.DownloadTo(stream);
        stream.Close();

        byte[] bytes;
        FileStream fileStream = new FileStream("Temp/Blob1.jpg", FileMode.Open, FileAccess.Read); ;

        using (BinaryReader reader = new BinaryReader(fileStream))
        {
            bytes = reader.ReadBytes((int)fileStream.Length);
        }
        fileStream.Close();

        return File(bytes, "image/jpg");
    }
}

