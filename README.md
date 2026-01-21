1. Clone the Repository
git clone https://github.com/Panha12629/Staff_Management_Backend.git
cd Staff_Management.API

2. Configure Database

Update appsettings.json with your PostgreSQL connection string:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=StaffDB;Username=postgres;Password=yourpassword"
}


⚠️ Tip: Replace yourpassword with your actual PostgreSQL password.

3. Apply Migrations & Create Database

Run one of the following commands to apply migrations and create the database:

dotnet ef database update


or, if using Package Manager Console in Visual Studio:

Update-Database

4. Run the API

Start the backend server with:

dotnet run


The API will be available at:

https://localhost:5282