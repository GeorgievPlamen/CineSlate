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

### [Visit live website](https://orange-glacier-08896bc03.6.azurestaticapps.net/) - Takes a second to load initially (free tier cold start on azure).

## What CineSlate supports

- **Movie discovery** - users can browse TMDB-backed movie lists, search by title, filter by genre and release year, and open detail pages with metadata and community ratings. See the [movies page](./Client/src/modules/Movies/index.tsx), [movie details UI](./Client/src/modules/Movies/Details/index.tsx), [movie API handlers](./Server/src/Api/Features/Movies/MoviesEndpoint.cs), and [TMDB client](./Server/src/Infrastructure/MovieClient/TMDBClient.cs).
- **Reviews and discussion** - authenticated users can create and update reviews, like reviews, and comment on them; public pages expose latest reviews, movie reviews, and review details. See the [review endpoints](./Server/src/Api/Features/Reviews/ReviewsEndpoint.cs), [review details UI](./Client/src/modules/Review/ReviewDetails.tsx), and [review application features](./Server/src/Application/Reviews).
- **Personal watchlists** - users can save films they want to watch and mark them as watched later. See the [watchlist UI](./Client/src/modules/Watchlist/index.tsx), [watchlist aggregate](./Server/src/Domain/Watchlist/WatchlistAggregate.cs), and [watchlist endpoints](./Server/src/Api/Features/Watchlist/WatchlistEndpint.cs).
- **User accounts and profiles** - the app supports registration, login, refresh-token based sessions, profile editing, and browsing recent community members. See the [user endpoints](./Server/src/Api/Features/Users/UsersEndpoint.cs), [auth bootstrap flow](./Client/src/common/AuthBootstrap.tsx), [HTTP auth handling](./Client/src/api/index.ts), and [critics page](./Client/src/modules/Critics/index.tsx).
- **Realtime notifications** - liking a review raises a domain event, stores a notification, and pushes it to the review author over SignalR. See [LikedReviewEvent](./Server/src/Domain/Movies/Reviews/Events/LikedReviewEvent.cs), [notification creation](./Server/src/Application/Notifications/EventHandlers/CreateNotificationOnReviewLikedHandler.cs), [SignalR publishing](./Server/src/Api/Features/Notifications/EventHandlers/PushRealtimeNotificationOnReviewLikedHandler.cs), and the [client realtime provider](./Client/src/common/Realtime/RealtimeProvider.tsx).

## Technical and design decisions

- **Clean Architecture with DDD-style boundaries** - the server is split into `Domain`, `Application`, `Infrastructure`, and `Api` projects so business rules, use cases, persistence, and transport concerns stay separate. Aggregates and value objects live in the domain layer, for example [MovieAggregate](./Server/src/Domain/Movies/MovieAggregate.cs), [WatchlistAggregate](./Server/src/Domain/Watchlist/WatchlistAggregate.cs), and shared [entity/value-object base types](./Server/src/Domain/Common/Models).
- **CQRS-style request handling with MediatR** - endpoints stay thin and delegate work to commands/queries such as [CreateReviewCommandHandler](./Server/src/Application/Reviews/Create/CreateReviewCommandHandler.cs). Cross-cutting concerns are applied through MediatR pipeline behaviors for [validation](./Server/src/Application/Common/PipelineBehaviours/ValidationBehaviour.cs) and logging, registered in [ApplicationServices](./Server/src/Application/ServicesRegistration.cs).
- **Result pattern for expected failures** - application handlers return `Result<T>` instead of throwing for normal business failures, and the API maps those results to HTTP responses in one place. See [Result.cs](./Server/src/Application/Common/Result.cs) and [Response.cs](./Server/src/Api/Common/Response.cs).
- **Repository abstractions with EF Core implementations** - application code depends on interfaces such as [IMovieRepository](./Server/src/Application/Movies/Interfaces/IMovieRepository.cs), while PostgreSQL/EF Core implementations live in infrastructure, for example [MovieRepository](./Server/src/Infrastructure/Repositories/MovieRepository.cs). This keeps persistence details outside the use-case layer and allows the domain model to remain richer than the database models.
- **Domain events for side effects** - review likes emit a domain event and separate handlers react by persisting notifications and pushing realtime updates, instead of embedding those side effects in the review workflow itself. The flow is visible across [LikedReviewEvent](./Server/src/Domain/Movies/Reviews/Events/LikedReviewEvent.cs), the [application handler](./Server/src/Application/Notifications/EventHandlers/CreateNotificationOnReviewLikedHandler.cs), and the [API handler](./Server/src/Api/Features/Notifications/EventHandlers/PushRealtimeNotificationOnReviewLikedHandler.cs).
- **Modern React client architecture** - the frontend is organized by feature modules and uses TanStack Router for routes, TanStack Query for server state, Zustand for local user state, Axios interceptors for token refresh, and SignalR for realtime updates. The app composition is visible in [main.tsx](./Client/src/main.tsx), with examples in the [movies module](./Client/src/modules/Movies/index.tsx), [API client](./Client/src/api/index.ts), and [realtime client area](./Client/src/common/Realtime).
- **Testing at multiple layers** - the server includes domain, application, infrastructure, and API tests rather than relying on endpoint tests alone. Representative examples include [RegisterCommandHandlerTests](./Server/tests/ApplicationTests/Users/Register/RegisterCommandHandlerTests.cs), [ReviewsEndpointTests](./Server/tests/ApiTests/Features/Reviews/ReviewsEndpointTests.cs), and [TMDBClientTests](./Server/tests/InfrastructureTests/MoviesClient/TMDBClientTests.cs).

## Setup and build

Create a .env file in root folder containing:

    PSQL_PSW=secretpassword
    PSQL_DB=cineslate
    PGADMIN_MAIL=admin@email.com
    PGADMIN_PW=secretpassword
    TMDB_KEY={your TMDB key}

You will need an api key from TMDB -> https://www.themoviedb.org/settings/api

Add the key to appsettings file under ApiKeys: TMDBKey.

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
