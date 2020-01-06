using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using project_hd.Models;

namespace project_hd.Controllers
{
	public class HomeController : Controller
	{

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult InsertObject()
		{
			var obj = new { Name = "Konrad", Surname = "Flaka" };
			var mongo = new MongoClient();
			IMongoDatabase db = mongo.GetDatabase("test");
			IMongoCollection<Object> collection = db.GetCollection<Object>("currency");
			collection.InsertOne(obj);
			return Ok();
		}

		public IActionResult ReadObject()
		{
			var mongo = new MongoClient();
			IMongoDatabase db = mongo.GetDatabase("test");
			IMongoCollection<Object> collection = db.GetCollection<Object>("currency");
			var obj = collection.Find(new BsonDocument());
			var result = obj.ToEnumerable();
			return Ok(result);
		}

		[HttpGet("Home/ReadApi/{currency}")]
		public IActionResult ReadApi([FromRoute]string currency)
		{
			WebClient client = new WebClient();
			var obj = client.DownloadString("https://api.coindesk.com/v1/bpi/currentprice/" + currency + ".json");
			return Ok(obj);
		}

		public IActionResult ReadBtc()
		{
			WebClient client = new WebClient();
			var obj = client.DownloadString("https://api.coindesk.com/v1/bpi/currentprice/" + "btc" + ".json");
			return Ok(obj);
		}


		[HttpGet]
		public ContentResult Test()
		{
			return Content("Test");
		}

		[HttpGet]
		public IActionResult Get()
		{
			var obj = new { Name = "Konrad", Surname = "Flaka" };
			return Ok(obj);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}