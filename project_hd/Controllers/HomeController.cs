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
			ViewBag.data = this.ReadFromDB();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		//[HttpGet("Home/ReadApi/{currency}")]
		public IActionResult InsertApi()
		{
			WebClient client = new WebClient();
			var obj = client.DownloadString("https://api.coindesk.com/v1/bpi/currentprice/btc.json");
			var mongo = new MongoClient();
			IMongoDatabase db = mongo.GetDatabase("test");
			IMongoCollection<Object> collection = db.GetCollection<Object>("data");
			dynamic objToSave = new { data = obj };
			collection.InsertOne(objToSave);
			return Ok(obj);
		}

		public IActionResult ClearDB()
		{
			var mongo = new MongoClient();
			IMongoDatabase db = mongo.GetDatabase("test");
			IMongoCollection<Object> collection = db.GetCollection<Object>("data");
			collection.DeleteMany(obj => true);
			return Ok();
		}

		private dynamic ReadFromDB()
		{
			var mongo = new MongoClient();
			IMongoDatabase db = mongo.GetDatabase("test");
			IMongoCollection<Object> collection = db.GetCollection<Object>("data");
			dynamic obj = collection.Find(new BsonDocument()).ToList();
			return obj;
		}

		public IActionResult ReadObject()
		{
			dynamic obj = this.ReadFromDB();
			return Ok(obj);
		}





		[HttpGet]
		public ContentResult Test()
		{
			return Content("Test");
		}

		[HttpGet]



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}