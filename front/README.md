Project Overview

This project is a Todo Management System built using:

ASP.NET Core 8 Minimal API as the backend

CQRS Pattern with MediatR for clean and maintainable business logic

Pure HTML + JavaScript for the frontend

Bootstrap for styling

SweetAlert for  alerts

 Project Structure

Backend:

ASP.NET Core Minimal API project

Implements CQRS using MediatR for separation of concerns

Contains Commands, Handlers, and Models

Connected to a SQL Server database

Frontend:

A single static HTML file (index.html)

Built using pure HTML, JavaScript, and Bootstrap

Uses SweetAlert for enhanced alert messages

⚖️ Technologies Used

Layer

Technologies

Backend

.NET 8, Minimal API, MediatR, CQRS, EF Core

Frontend

HTML, JavaScript, Bootstrap, SweetAlert

Database

SQL Server

✅ Steps to Run the Project

▶️ 1. Backend Setup

Requirements:

.NET 8 SDK

SQL Server

Visual Studio or VS Code

Setup Instructions:

Open the backend project in your IDE.

Update the Connection String in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=TodoDb;Trusted_Connection=True;"
}

Apply migrations and update the database:

dotnet ef database update

Run the API:

dotnet run

The API will run at:

https://localhost:7074

▶️ 2. Frontend Setup

Open the index.html file in your editor.

Use Live Server to serve the file.

The frontend will automatically interact with the backend API at https://localhost:7074.

You can now:

Add, update, or delete tasks

Mark tasks as complete

Filter tasks by status