# Blog_Asp
ASP.Net Blog API project
<br/>
Onion architecture
<br/>
Followed REST convention for making this API
<br/>
## Instructions for installing localy
1. Download project
2. Extract project files
3. Import database script.sql in SSMS ( i didnt' use faker so you have to do this step)
4. In EfDataAccess/BlogContext.cs set name of your database from ssms in this line of code 
<br/>
<b>optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=name_of_wout_database_from_ssms;Integrated Security=True"); </b>
5. Open project using visual studio
6. Build project
7. Use postman for testing my API

## Login credentials
-Used JWT Bearer token-
<br/>
* Reistrated user
  * username: nevenka
  * password: sifra3
* Moderator
  * username: sinisa
  * password: sifra2
* Admin
  * username: nikola
  * password: sifra1
### If you don't have time to follow instructions and build project localy, here's my api documentation
![Swagger](https://github.com/NJevric/Blog_Asp/blob/main/screencapture-localhost-5001-swagger-index-html-2021-10-21-20_01_40.png)
