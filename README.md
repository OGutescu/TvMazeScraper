# Welcome to my Tv Maze Scraper application!

This application exposes an Api with all the tv shows in the TVMaze database and their cast.
The TVMaze database provides a public REST API that I used to query for data.
http://www.tvmaze.com/api

This application consist of 2 parts: 
- a service that scraps the data from the TvMaze Api and saves it in a local DB
- a REST API that retrieves that data

This is the api endpoint: <host>/api/tvshows
   
Parameters: 
- page - optional, default is 1 - is the page number
- pageSize - optional, default is 25

Returns:
  - 200 ok - list of tv shows with their cast
  - 400 BadRequest - if the parameters are not valid
  - 503 ServiceUnavailable - the db is not created
  - 500 InternalServerError - something went wrong with the call

Some examples of usage with parameters:
 - route: /api/tvshows
 - route: /api/tvshows/2
 - route: /api/tvshows/2/30
     
The result is a json object like this:
 [{
     "id": 1,
     "name": "Under the Dome",
     "cast": [{
           "id": 7,
           "name": "Mackenzie Lintz",
           "birthday": "1996-11-22"
       },
       {
           "id": 5,
           "name": "Colin Ford",
           "birthday": "1996-09-12"
       }
  }]

Happy usage!


