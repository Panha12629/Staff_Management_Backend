Backend

Clone the repo:

git clone https://github.com/Panha12629/Staff_Management_Backend.git
cd Staff_Management.API


Update appsettings.json with your PostgreSQL connection string:

"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=StaffDB;Username=postgres;Password=yourpassword"
}


Apply migrations and create the database:

dotnet ef database update


Run the API:

dotnet run


Backend will run at https://localhost:5282