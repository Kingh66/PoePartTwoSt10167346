using System;
using System.Collections.Generic;
using System.Linq;

// Represents an ingredient in a recipe
class Ingredient
{
    public string Name { get; set; }        // The name of the ingredient
    public double Quantity { get; set; }    // The quantity of the ingredient
    public string Measurement { get; set; } // The measurement unit of the ingredient
    public double Calories { get; set; }    // The calorie content of the ingredient
    public string FoodGroup { get; set; }   // The food group the ingredient belongs to
    public double OriginalQuantity { get; set; } // The original quantity of the ingredient (used for scaling)
}

// Represents a recipe
class Recipe
{
    public string Name { get; set; }                    // The name of the recipe
    public List<Ingredient> Ingredients { get; set; }    // The list of ingredients in the recipe
    public List<string> Steps { get; set; }              // The list of steps in the recipe
    public double TotalCalories                          // The total calorie content of the recipe
    {
        get
        {
            double total = 0;
            foreach (Ingredient ingredient in Ingredients)
            {
                total += ingredient.Calories * ingredient.Quantity;
            }
            return total;
        }
    }
}

// Manages the recipes and provides functionality to add, modify, and display recipes
class RecipeManager
{
    private List<Recipe> recipes;   // The list of recipes

    public RecipeManager()
    {
        recipes = new List<Recipe>();
    }

    // Adds a new recipe with the given name to the list of recipes
    public void AddRecipe(string name)
    {
        var recipe = new Recipe
        {
            Name = name,
            Ingredients = new List<Ingredient>(),
            Steps = new List<string>()
        };
        recipes.Add(recipe);
    }

    // Adds an ingredient to the specified recipe
    public void AddIngredient(string recipeName)
    {
        Recipe recipe = GetRecipe(recipeName);
        if (recipe != null)
        {
            Console.Write("Enter ingredient name: ");
            string ingredientName = Console.ReadLine();
            double quantity;
            if (!Double.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid quantity. Please try again.");
                return;
            }

            Console.Write("Enter measurement: ");
            string measurement = Console.ReadLine();
            double calories;
            if (!Double.TryParse(Console.ReadLine(), out calories) || calories < 0)
            {
                Console.WriteLine("Invalid calories. Please try again.");
                return;
            }

            Console.Write("Enter food group: ");
            string foodGroup = Console.ReadLine();

            var ingredient = new Ingredient
            {
                Name = ingredientName,
                Quantity = quantity,
                Measurement = measurement,
                Calories = calories,
                FoodGroup = foodGroup
            };

            recipe.Ingredients.Add(ingredient);

            // Invoke the RecipeExceedsCalories event if the total calories exceed 300
            if (recipe.TotalCalories > 300)
            {
                RecipeExceedsCalories?.Invoke(recipe);
            }
        }
        else
        {
            Console.WriteLine("Recipe name not found. Please try another one.");
        }
    }

    // Adds a step to the specified recipe
    public void AddStep(string recipeName, string step)
    {
        Recipe recipe = GetRecipe(recipeName);
        if (recipe != null)
        {
            recipe.Steps.Add(step);
        }
    }

    // Retrieves the recipe with the specified name from the list of recipes
    public Recipe GetRecipe(string name)
    {
        return recipes.FirstOrDefault(recipe => recipe.Name == name);
    }

    // Displays all the recipes and their details
    public void Display()
    {
        Console.WriteLine("Recipe List:");
        Console.WriteLine("------------");

        foreach (Recipe recipe in recipes)
        {
            Console.WriteLine(recipe.Name);
            Console.WriteLine($"Total Calories: {recipe.TotalCalories}");
            Console.WriteLine("Ingredients:");
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"- {ingredient.Name}: {ingredient.Quantity} {ingredient.Measurement}, {ingredient.Calories} Calories, {ingredient.FoodGroup}");
            }
            Console.WriteLine("Steps:");
            foreach (string step in recipe.Steps)
            {
                Console.WriteLine($"- {step}");
            }
            Console.WriteLine();
        }
    }

    // Scales the quantities of ingredients in all recipes by the specified factor
    public void Scale(double factor)
    {
        foreach (Recipe recipe in recipes)
        {
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity * factor;
            }
        }
    }

    // Resets the quantities of ingredients in all recipes to their original values
    public void ResetQuantities()
    {
        foreach (Recipe recipe in recipes)
        {
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
        }
    }

    // Clears all the recipes
    public void Clear()
    {
        recipes.Clear();
    }

    // Saves the original quantities of ingredients in all recipes
    public void SaveOriginalQuantities()
    {
        foreach (Recipe recipe in recipes)
        {
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                ingredient.OriginalQuantity = ingredient.Quantity;
            }
        }
    }

    // Gets the names of all recipes in alphabetical order
    public List<string> GetRecipeNames()
    {
        return recipes.Select(recipe => recipe.Name).OrderBy(name => name).ToList();
    }

    // Event handler for the RecipeExceedsCalories event
    public delegate void RecipeExceedsCaloriesHandler(Recipe recipe);
    public event RecipeExceedsCaloriesHandler RecipeExceedsCalories;
}

// Entry point of the program
class Program
{
    static RecipeManager recipeManager = new RecipeManager();

    static void Main(string[] args)
    {
        string input;

        do
        {
            Console.WriteLine("\nCommands:");
            Console.WriteLine("1 - Add recipe");
            Console.WriteLine("2 - Add ingredient");
            Console.WriteLine("3 - Add step");
            Console.WriteLine("4 - Display recipe");
            Console.WriteLine("5 - Scale recipe");
            Console.WriteLine("6 - Reset quantities");
            Console.WriteLine("7 - Clear recipe");
            Console.WriteLine("0 - Exit");

            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter recipe name: ");
                    var recipeName = Console.ReadLine();
                    recipeManager.AddRecipe(recipeName);
                    break;

                case "2":
                    Console.Write("Enter recipe name: ");
                    recipeName = Console.ReadLine();
                    recipeManager.AddIngredient(recipeName);
                    recipeManager.SaveOriginalQuantities();
                    break;

                case "3":
                    Console.Write("Enter recipe name: ");
                    recipeName = Console.ReadLine();
                    Console.Write("Enter step description: ");
                    var step = Console.ReadLine();

                    recipeManager.AddStep(recipeName, step);
                    break;

                case "4":
                    recipeManager.Display();
                    break;

                case "5":
                    double factor;
                    Console.Write("Enter scaling factor (0.5, 2, or 3): ");
                    if (!double.TryParse(Console.ReadLine(), out factor))
                    {
                        Console.WriteLine("Invalid scaling factor. Please try again.");
                        break;
                    }

                    recipeManager.Scale(factor);
                    break;

                case "6":
                    recipeManager.ResetQuantities();
                    break;

                case "7":
                    recipeManager.Clear();
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }

        } while (input != "0");
    }
}
