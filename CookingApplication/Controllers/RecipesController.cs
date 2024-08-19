using CookingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CookingApplication.Controllers
{
    public class RecipesController : Controller
    {
        public IActionResult Index()
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("cooking_application");//vad vi döper DB till
            var collection = database.GetCollection<Recipe>("recipes");//vad vi vill spara(Recipe), var vi vill spara(recipe)

            List<Recipe> recipes = collection.Find(r => true).ToList();

            return View(recipes);//skickar in recipes till vår vy

        }
        

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]//för att webbsidan ska veta vilken vy vi vill visa
        public IActionResult Create(Recipe recipe)//kunna ta emot ett recept
        {
            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("cooking_application");//vad vi döper DB till
            var collection = database.GetCollection<Recipe>("recipes");//vad vi vill spara(Recipe), var vi vill spara(recipe)
            collection.InsertOne(recipe);//läga till recept i vår collection/lista

            return Redirect("/Recipes");//vart vi vill redirecta
        }

        public IActionResult Show(string Id)//denna action tar emot Id på det recept vi vill titta på
        {
            ObjectId recipeId=new ObjectId(Id);

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("cooking_application");//vad vi döper DB till
            var collection = database.GetCollection<Recipe>("recipes");//vad vi vill spara(Recipe), var vi vill spara(recipe)

            Recipe recipe=collection.Find(r => r.Id == recipeId).FirstOrDefault();//hämta spec recept med id från DB

            return View(recipe);//skicka receptet till vår vy.

        }

        public IActionResult Edit(string Id)//vi tar emot ett id
        {
            ObjectId recipeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("cooking_application");//vad vi döper DB till
            var collection = database.GetCollection<Recipe>("recipes");//vad vi vill spara(Recipe), var vi vill spara(recipe)

            Recipe recipe = collection.Find(r => r.Id == recipeId).FirstOrDefault();//hämta spec recept med id från DB

            return View(recipe);//skicka receptet till vår vy.

        }
        [HttpPost]//eftersom detta är en action som körs av att vi skickar in ett formulär
        public IActionResult Edit(string Id, Recipe recipe)//denna action tar emot 2 värden: Id och recept
        {   //koden som behövs för att uppdatera info när vi skickat in den
            ObjectId recipeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("cooking_application");//vad vi döper DB till
            var collection = database.GetCollection<Recipe>("recipes");//vad vi vill spara(Recipe), var vi vill spara(recipe)

            recipe.Id = recipeId;//gör så att vårt recepts id sätts till samma id som vi tagit emot. annars knas i DB
            collection.ReplaceOne(r => r.Id == recipeId, recipe);//ersätta rec med nytt rec inkl ändrdingar

            return Redirect("/Recipes");
            
        }
    }
}
