<<<<<<< HEAD
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
=======
Navigate to frontend folder:

cd staff_management_frontend

Install dependencies:

npm install

Start the React app:

npm start

Frontend will run at http://localhost:3000

API Endpoints
Method Endpoint Description
GET /api/staff Get all staff / search staff
GET /api/staff/{id} Get a single staff by ID
POST /api/staff Create a new staff
PUT /api/staff/{id} Update an existing staff
DELETE /api/staff/{id} Delete a staff
How to Use

Open your browser at http://localhost:3000

Use Add Staff form to create staff records

Edit or delete staff directly from the table

Search staff by ID, gender, or birthday range

Export the staff list to Excel or PDF
>>>>>>> b0a4028e64b6f95c52d838973399b92b1b351d87
