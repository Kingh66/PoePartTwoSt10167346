# PoePartTwoSt10167346
Recipe Manager
This is a recipe manager application that allows you to create and manage recipes with their respective ingredients and steps.

Instructions
To compile and run the software, follow these steps:

•	Ensure that you have the .NET Core SDK installed on your machine.
•	Clone the GitHub repository: Link to Repository
•	Open a command prompt or terminal and navigate to the project directory.
•	Run the following command to compile the application:
dotnet build
•	Once the build process is complete, run the following command to start the application:
dotnet run
•	Follow the on-screen instructions to interact with the recipe manager.
Description
In Part 2 of the recipe manager, several improvements and enhancements have been made based on the feedback and requirements from Part 1.

Error Handling: The application now uses double.TryParse instead of double.Parse to gracefully handle invalid input values and provide appropriate feedback to the user.

Consistent Naming Conventions: The code has been updated to follow consistent naming conventions throughout, enhancing code readability. Both the Ingredient and Recipe classes now use Pascal case for their properties.

Validation and Constraints: Validation checks have been added for input values to ensure they meet specific criteria. For example, quantities and calories are validated to be positive numbers, and the food group is checked to be non-empty. These checks help maintain data integrity and prevent unexpected behavior.

Encapsulation: Certain operations have been encapsulated within the RecipeManager class to provide more controlled access to the underlying data. The recipes list is now private, and appropriate methods have been provided to add, retrieve, and modify recipes.

Documentation: XML comments have been added to the code to provide comprehensive documentation for developers. Each method now includes comments describing their purpose, parameters, and return values.
