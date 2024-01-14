# Seeker's iShop

Welcome to my pet project, iShop. A simple ecommerce site built using AngularJS, ASP.NET MVC and SQL server. This project showcases many advanced technologies like Restful API, authentication and authorization using JWT Token, and many more.

## How to Run

Find the 'databaseGeneration.sql' file in the root directory of ServerApp folder. Run the script in SQL for database creation. Make sure the connectionString is 
accurate in 'appsettings.json' in the server project.

Once serverApp is running, make sure the listening port matches the set URL in ClientApp->src->proxy.conf.json. If not, change the proxy port to match.

Finally, run 'ng serve' in cmd in the clientApp directory, and goto localhost:4200 in your browser. That's it! Feel free to leave a comment or get in touch via email at 'fahimseeker@gmail.com' for any confusions or feedback.

