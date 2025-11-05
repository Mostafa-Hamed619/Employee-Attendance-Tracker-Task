🧾 Employee Attendance Tracker

By: Mostafa Elsayed
Developed for CodeZone LLC

📘 Overview

The Employee Attendance Tracker is an ASP.NET MVC web application designed to manage departments, employees, and their attendance records.
It implements a clean N-Tier architecture that separates concerns across the Presentation, Business, and Data layers.

The system supports:

Department CRUD operations

Employee CRUD operations with auto-generated employee codes

Attendance management (mark, edit, delete)

Dynamic UI using jQuery and calendar integration

Filtering, pagination, and partial views for modern user experience

🧩 Project Architecture
🏗️ N-Tier Structure
Layer	Technology	Responsibility
Presentation Layer	ASP.NET MVC (Views + Controllers)	Handles user interactions and UI rendering. No business logic.
Business Layer	C# Service Classes (e.g. DepartmentService, EmployeeService, AttendanceService)	Contains all validation, business rules, and data-processing logic.
Data Layer	Entity Framework Core (Code-First with InMemory DB)	Manages data persistence, entity configuration, and seeding.
🧠 Key Design Principles

Dependency Injection (DI): Each layer communicates via interfaces for loose coupling.

Entity Framework Core: Used with an In-Memory Database for lightweight testing.

Repository-like Pattern: Implemented implicitly via service layer for encapsulating logic.

Validation: Handled through Data Annotations and service-level checks (e.g., uniqueness, date validation).

⚙️ Setup Instructions
🧱 Prerequisites

.NET SDK 8.0 or later

Visual Studio 2022 or VS Code

Internet browser

▶️ Run Instructions

Clone the repository

git clone https://github.com/mostafaelsayed/EmployeeAttendanceTracker.git


Open the solution in Visual Studio or VS Code.

Build and run the project.

The application uses an In-Memory Database, so no setup is required.

Sample data (departments, employees, attendance) is automatically seeded.

Access via browser

https://localhost:xxxx/

🧑‍💼 Core Features
🏢 Department Management

Add, edit, delete, and list departments.

Validation for unique Name and Code.

Display number of employees in each department.

👨‍💻 Employee Management

Add, edit, delete, and list employees.

Auto-generated Employee Code (unique and non-editable).

Validation for four-word names, each ≥2 letters.

Prevents duplicate email addresses.

Displays current month’s attendance summary.

🗓️ Attendance Management

Mark attendance as Present or Absent per employee/date.

Prevent marking future dates.

Each employee can have only one record per day.

Filter records by department, employee, or date range.

Supports pagination and AJAX-based filtering.

💡 UI / UX Features

jQuery for live updates and filtering.

Calendar widget for selecting dates (future dates disabled).

Partial Views for attendance history and employee details.

Pagination on employee and attendance lists.

Bootstrap 5 styling for a clean, responsive layout.

🧪 Sample Data

The system auto-seeds:

Departments: HRMG, TECH, FINA, etc.

Employees: Predefined with codes and valid emails.

Attendance: Mixed sample of present/absent records.

🧱 Folder Structure
EmployeeAttendanceTracker/
│
├── EmployeeAttendance.Presentation/    # MVC controllers and views
│   ├── Controllers/
│   └── Views/
│
├── EmployeeAttendance.Business/        # Business logic and interfaces
│   ├── Interfaces/
│   └── Implementations/
│
├── EmployeeAttendance.Data/            # Entities, Wrappers, EF Context
│   ├── Entities/
│   ├── Data/
│   └── Wrappers/
│
└── README.md

🧑‍💻 Developer Notes

All validation and business rules are enforced in services (not controllers).

Controllers handle only HTTP request/response and view rendering.

Designed with clean code, modularity, and testability in mind.

🏁 Author

Mostafa Elsayed
Software Developer — .NET | C# | ASP.NET Core | EF Core | MVC
📧 mostafa.hamed@example.com

📅 2025
