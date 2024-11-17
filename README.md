<h1 align="center">
  CineSlate
</h1>

<div align="center">
<img height="50" src="https://user-images.githubusercontent.com/25181517/121405384-444d7300-c95d-11eb-959f-913020d3bf90.png">
<img height="50" src="https://user-images.githubusercontent.com/25181517/121405754-b4f48f80-c95d-11eb-8893-fc325bde617f.png">
<img height="50" src="https://user-images.githubusercontent.com/25181517/192107858-fe19f043-c502-4009-8c47-476fc89718ad.png">
<img height="50" src="https://user-images.githubusercontent.com/25181517/183890598-19a0ac2d-e88a-4005-a8df-1ee36782fde1.png">
<img height="50" src="https://user-images.githubusercontent.com/25181517/183897015-94a058a6-b86e-4e42-a37f-bf92061753e5.png">
<div></div>
<img height="50" src="https://user-images.githubusercontent.com/25181517/202896760-337261ed-ee92-4979-84c4-d4b829c7355d.png">
<img height="50" src="https://user-images.githubusercontent.com/25181517/187896150-cc1dcb12-d490-445c-8e4d-1275cd2388d6.png">
<img height="50" src="https://github.com/marwin1991/profile-technology-icons/assets/62091613/b40892ef-efb8-4b0e-a6b5-d1cfc2f3fc35">
 <img height="50" src="https://user-images.githubusercontent.com/25181517/117208740-bfb78400-adf5-11eb-97bb-09072b6bedfc.png">
 <img height="50" src="https://user-images.githubusercontent.com/25181517/117207330-263ba280-adf4-11eb-9b97-0ac5b40bc3be.png">
</div>
<hr>

<p align="center">
  This is a fullstack web application for movie reviews. Goal is to practice new technical skills and software design concepts.
</p>

## Setup and build

Create a .env file in root folder containing:

    PSQL_PSW=secretpassword
    PSQL_DB=cineslate
    PGADMIN_MAIL=admin@email.com
    PGADMIN_PW=secretpassword

You will need an api key from TMDB -> https://www.themoviedb.org/settings/api

Add the key to appSettings under ApiSettings: TMDBKey.

When you're ready, start your application by running:
`docker compose up --build -w`

CD into Client - npm i -> npm run dev

React client will be available at http://localhost:3030.

Server will be available at http://localhost:8080.

Requests can be tried at http://localhost:8080/scalar/v1.

PG Admin will be available at http://localhost:5050/browser.

Login with email and password from env file.

Add new server from quick menu or right click Servers -> Register -> Server

In section "General"

- add a name (Cineslate)

### In section "Connection"

- Host name/address = postgres
- username = postgres
- password = from env (default = secretpassword)

### Add sln (optional)

    cd Server
    dotnet new sln
    dotnet sln add **/**/*.csproj

### Add volumes (optional)

If you want data to be persisted between containers, uncomment the volume's inside the docker compose file.

### Add migrations

cd Server/src

dotnet ef migrations add {name} -s ./Api -p ./Infrastructure -o ./Database/Migrations

## References

- [Docker's .NET guide](https://docs.docker.com/language/dotnet/)
- The [dotnet-docker](https://github.com/dotnet/dotnet-docker/tree/main/samples)
  repository has many relevant samples and docs.
