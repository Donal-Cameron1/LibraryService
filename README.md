
# Library System Sheffield

--- 
### Project Initialisation
--- 

Before running the solution it is needed to setup the server. 

This is done by clicking 'Windows + r'. 

In the upcoming field you have to put `%windir%\Microsoft.NET\Framework\v4.0.30319`

A folder should open and there you have to look for the file called `aspnet_regsql.exe`

Run it, click through the upcoming Wizard. Enter following for Server and Database (database name won't come up in the drop down)

**Server:** (LocalDb)\MSSQLLocalDB

**Database:** aspnetdb

After completing the Setup of the server you can run the application.

See also: https://stackoverflow.com/questions/2165908/could-not-find-stored-procedure-dbo-aspnet-checkschemaversion

If a database with the name 'aspnetdb' already existed before it has to be dropped first and start the procedure again.

---
### Creating an Admin
___

Before creating an admin you have to register as a normal user. This user will be the admin later. 

As soon as you have done that call the URL http://localhost:65407/InitAdmin/Index

Here enter your log in details of the earlier created user. If the initilisation of the admin was successfull you have to log in as the newly created admin again. 

A admin can then change the roles of any user. 
