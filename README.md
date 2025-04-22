# Identity Scaffolding Demo
This ASP.NET 8.0 MVC web site was created in Visual Studio 2022.
The following steps were taken to create a base web site.

1. Created a new project with identity included (Authentication with Individual Accounts).
2. Added a model named AppUser that inherits from IdentityUser.
3. Added scaffolding for some of the identity Pages.
4. Added seed data for an admin user.
5. Changed the database to MySQL.

Here's a link to a screen-cast showing the whole process:  
[Video recording on Google Drive](https://drive.google.com/file/d/1Cb-Bli0FkrnRkIpaXX5dLWnf7giKxRIv/view?usp=sharing)

After the screen-cast, more changes were made to improve security:
1. Admin seed credentials were removed from the SeeAdmin class and put in appsettings.json.
2. All appsettings files were removed from git tracking and replaced with appsettins.Example files which do not contain database credentials.

