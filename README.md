# Introduction 
FUForum is a open source project for everyone. Every member can create new knowledge base record (KB) and share to community. For each KB, user can vote it and comment to below KB.

# TechStack
1. ASP.NET Core 8.0
2. Angular 16.0.2	
3. IdentityServer4
4. SQL Server 2019
# How to run this project
1.	Clone this source code from Repository
2.  Build solution to restore all Nuget Packages
3.  Set startup project is FUForum.BackendServer
4.  Run Update-Database to generate database
5.  Set startup project to multiple projects include: FUForum.BackendServer and FUForum.WebPortal

# References
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-8.0&tabs=windows)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/)