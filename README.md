Staff Management Application

A simple Staff Management system using ASP.NET Core Web API (backend with PostgreSQL) and React (frontend).

Backend Setup (ASP.NET Core API)
## 1️⃣ Clone the Repository
git clone https://github.com/Panha12629/Staff_Management_Backend.git
cd Staff_Management.API

## 2️⃣ Configure Database

Update appsettings.json with your PostgreSQL connection string:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=StaffDB;Username=postgres;Password=yourpassword"
}


⚠️ Tip: Replace yourpassword with your actual PostgreSQL password.

## 3️⃣ Apply Migrations & Create Database

Run one of the following commands:

dotnet ef database update


Or, if using Visual Studio Package Manager Console:

Update-Database


This will create the database and apply all migrations.

## 4️⃣ Run the Backend API

Start the backend server:

dotnet run


The API will be available at:

https://localhost:7012


✅ Test an endpoint (example):

https://localhost:7012/api/staff
