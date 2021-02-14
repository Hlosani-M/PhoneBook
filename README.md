# PhoneBook

### Built following the Repository Pattern 

A simple phone book application built on ASP.NET CORE 5.0.0 Razor Pages.

The application communicates to the database via Entity Framework Core 5.0.3.
A User can Add, Update and Delete an Entry. Phone duplicates are not allowed in the system.
A User has the ability to search for entries.

The application also includes an API to demonstrate the knowledge in that area.
The API is not used in the application but is available for CRUD operations via specified endpoints.


It may be necessary to Update the database when the project is cloned. Use the following command :

```dotnet ef database update -s ../PhoneBook/PhoneBook.csproj```

The project will definitely use a couple of unit tests, (to be added).

### Application Screen images are included in the project files, e.g PhoneBook2.PNG
