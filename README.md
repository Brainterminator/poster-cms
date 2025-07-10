# Web Dev
by Leo Sulzbacher

## How to setup
Start by cloning the repository:

```bash
git clone https://github.com/Brainterminator/poster-cms
```

Then you need to setup a local [PostgreSQL](https://www.postgresql.org/download/) Server on your machine.

In the _appsettings.json_ file you can find an example of how to add a Connection String for your database connection. It is recommended to create a _appsettings.Development.json_ in your project for development. That file will be ignored by version control, therefore no secrets will be accidentally shared.

```json
"ConnectionStrings": {
    "PostgresConnection": "Host=localhost;Port=5432;Database=<DATABASE>;Username=<USER>;Password=<PASSWORD>"
}
```

You have to install [Entity Framework](https://learn.microsoft.com/de-de/ef/):

```bash
dotnet tool install --global dotnet-ef
```

You can now migrate your database. Run the following command in the root of this repo:

```bash
dotnet ef database update
```

To make sure Cookies and JavaScript is working install the Dotnet Developer certificates:

```bash
dotnet dev-certs https --trust
```

## Running the Developer Server

If you proceeded all prior steps you should be able to run the server using:

```bash
dotnet run
```