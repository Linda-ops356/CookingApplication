using MongoDB.Bson;

namespace CookingApplication.Models
{
    public class Recipe
    {
        public ObjectId Id { get; set; }
        public string Name {  get; set; }
        public string Ingredient {  get; set; }

        public string Description { get; set; }
    }
}
