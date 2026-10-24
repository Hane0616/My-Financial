# MyFinance — Personal Finance Tracker

A personal finance management web application built with C# and ASP.NET Core MVC. MyFinance helps users record income and expenses, manage transaction records, and review monthly financial summaries.

## Features

- **Transaction Management** — Create, view, edit, and delete income and expense records.
- **Monthly Financial Dashboard** — View monthly income, expenses, and balance.
- **Expense Category Summary** — Group monthly expenses by category and calculate totals.
- **Data Persistence** — Store transaction records in a SQLite database using Entity Framework Core.
- **Data Validation** — Validate transaction inputs with model validation attributes.

## Tech Stack

- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- Razor Views
- Bootstrap
- Git & GitHub

## Project Structure

```text
MyFinancial/
├── Controllers/
├── Data/
├── Migrations/
├── Models/
├── Views/
├── wwwroot/
├── Program.cs
└── MyFinancial.csproj
```

## Getting Started

### Prerequisites

- Visual Studio with ASP.NET and web development support
- A compatible .NET SDK

### Run Locally

1. Clone this repository.
2. Open `MyFinancial.slnx` in Visual Studio.
3. Restore the NuGet packages.
4. Apply database migrations using the Package Manager Console:

   ```powershell
   Update-Database
   ```

5. Run the application from Visual Studio.

## Current Status

The core transaction CRUD operations and monthly financial dashboard have been implemented.

## Future Improvements

- Search and filter transactions by date, type, and category.
- Add monthly budget management.
- Improve dashboard visualizations.
- Add automated tests and improve error handling.