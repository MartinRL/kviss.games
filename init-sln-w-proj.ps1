# Skapa en ny lösning med namnet "kviss.games"
dotnet new sln -n kviss.games

# Skapa ett Blazor Server-projekt med namnet "kviss.games"
dotnet new blazorserver -n kviss.games

# Lägg till Blazor-projektet i lösningen
dotnet sln add .\kviss.games\kviss.games.csproj

# Skapa ett xUnit-testprojekt med namnet "kviss.games.Tests"
dotnet new xunit -n kviss.games.Tests

# Lägg till testprojektet i lösningen
dotnet sln add .\kviss.games.Tests\kviss.games.Tests.csproj

# Lägg till en referens från testprojektet till Blazor-projektet
dotnet add .\kviss.games.Tests\kviss.games.Tests.csproj reference .\kviss.games\kviss.games.csproj

# Skapa mappen för spelets funktionella kärna (Spel) i Blazor-projektet
New-Item -ItemType Directory -Force -Path .\kviss.games\Spel

# Skapa filen Spel.cs i mappen Spel
New-Item -ItemType File -Force -Path .\kviss.games\Spel\Spel.cs

# Skapa mappen för enhetstester för spelet i testprojektet
New-Item -ItemType Directory -Force -Path .\kviss.games.Tests\Speltest

# Skapa filen Speltest.cs i mappen Speltest
New-Item -ItemType File -Force -Path .\kviss.games.Tests\Speltest\Speltest.cs
