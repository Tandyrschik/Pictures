# Picture

This project is an ASP.NET Core web application designed for managing pictures and user accounts. It provides functionalities to store pictures in a database and allows users to create their accounts.
(To view the latest changes to the project, go to the development branch.)

## Features

- **Picture Upload**: Users can upload pictures to the application, and the pictures will be stored in the local database.
- **Picture Management**: Users can view and delete the pictures they have uploaded.
- **User Account Creation**: New users can create an account with the application.
- **User Authentication and Authorization**: Secure user authentication and authorization features to control access to the application's functionalities.

## Technologies Used

- **ASP.NET Core**: The application is built using ASP.NET Core, a cross-platform, high-performance framework for web applications.
- **Entity Framework**: For interacting with the database, Entity Framework is used to provide a convenient and object-oriented data access layer.
- **SQL Server**: The database is powered by SQL Server, which stores the users account information and the pictures.
- **CSS & HTML**: The site has animation when you hover the mouse over the picture and a pop-up window with a full-size picture when you click on the picture.
- **xUnit**: The project contains unit tests for controllers, services and repositories.
- **Moq**: This framework is used to test аll complex classes.

## Launch guide
- **Install .NET 7 packages**(SDK 7.0.405, ASP.NET Core Runtime 7.0.15, .NET Runtime 7.0.15) from: https://dotnet.microsoft.com/en-us/download/dotnet/7.0 
- **Download Visual Studio 2022 Community**: https://visualstudio.microsoft.com/ru/vs/community/
- **Open Installer and click on checkmark with ASP.NET package and install program**:
![alt text](https://github.com/Tandyrschik/Tandyrschik/blob/main/1.png?raw=true)
- **Download project.zip from develop branch**: https://github.com/Tandyrschik/Pictures/tree/develop
- **Extract .zip to your repos directory**, open folder "Pictures-develop", open file Pictures.sln to start visual studio project.
- **In visual studio click (1) to open nested list, select IIS Express (2)**.
![alt text](https://github.com/Tandyrschik/Tandyrschik/blob/main/2.png?raw=true)
- **Click to this button to start program**.
![alt text](https://github.com/Tandyrschik/Tandyrschik/blob/main/3.png?raw=true)


## Contributing

Contributions are welcome! If you have any ideas or suggestions for new features, feel free to open an issue or a pull request on the GitHub repository.
