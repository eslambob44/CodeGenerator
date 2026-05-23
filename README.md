# :pager: Code generator
Code Generator is an application designed to help backend developers who use C# and ADO.NET and Sql server. It generates basic CRUD operations for any given table from a database.

## :technologist:Technologies
- Using SOLID principles to enable scalability and maintainability
- Using WinForms to build the desktop application interface

## :rocket:Features
- Reading the entire table schema from a given database, allowing the user to easily choose which tables to generate code for
- Generating the data access and business layers for the selected CRUD operations
- Generating the data access code using stored procedures and ADO.NET

## :arrows_counterclockwise: The Process
This project was built after I took the SOLID Principles course. I wanted to build an application that could easily scale to any future requirement, such as adding compatibility for new languages (my application currently only supports generating C# code, but I can easily add other languages in the future without modifying the existing code).

I built it using SOLID principles to ensure every class has a single responsibility and to keep the classes loosely coupled using Dependency Injection (DI).

## :books: What I Learned
It was an interesting project. I learned:
- How to generate code using fixed templates
- How to think about and apply SOLID principles to my project, such as utilizing Dependency Injection (DI) in the essential parts of the application to keep it loosely coupled, allowing me to add or remove components as easily as Lego pieces.

## :bulb:Future Improvements
This project has high potential for growth:
- I can add support for other database providers (e.g., MySQL, PostgreSQL).
- I can add support for other data access frameworks, such as Entity Framework Core (EF Core).
- I can add support for other target languages, such as Python or PHP.

## :vertical_traffic_light:Running the project
It is simple:  
When you download the project, just go to `Presentation Layer -> Presentation Layer.sln` and run it.
