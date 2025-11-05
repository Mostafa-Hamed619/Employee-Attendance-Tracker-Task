<h1 align="center"><strong>Employee Attendance Tracker</strong></h1>

<p align="center">
  A <strong>technical assessment project</strong> for <strong>CodeZone LLC</strong> — developed using 
  <strong>ASP.NET Core MVC</strong> with a clean <strong>N-Tier Architecture</strong> and 
  <strong>Entity Framework Core (In-Memory Database)</strong>.
</p>

<hr>

<h2><strong>🧑‍💻 Overview</strong></h2>

<p>
The <strong>Employee Attendance Tracker</strong> is a web-based system that allows organizations to 
manage <strong>employees, departments, and daily attendance records</strong> efficiently.  
The system ensures each employee can have only one attendance record per day, prevents future-date entries, 
and provides a smooth and interactive user experience using <strong>jQuery</strong> and <strong>AJAX</strong>.
</p>

<p>
This project was developed by <strong>Mostafa Elsayed</strong> as a demonstration of 
enterprise-grade architecture design, clean coding practices, and modern web development principles.
</p>

<hr>

<h2><strong>🏗️ Project Architecture</strong></h2>

<p>
The project follows a structured <strong>N-tier architecture</strong> that separates responsibilities across 
Presentation, Business, and Data layers. This ensures scalability, testability, and maintainability.
</p>

<h3><strong>1️⃣ Presentation Layer — <em>(EmployeeAttendanceTracker.Presentation)</em></strong></h3>
<ul>
  <li>Implements the <strong>ASP.NET Core MVC</strong> pattern for the user interface.</li>
  <li>Contains all <strong>Controllers</strong>, <strong>Views</strong>, and <strong>ViewModels</strong>.</li>
  <li>Controllers are kept “thin” — handling only user requests and delegating logic to the Business Layer.</li>
</ul>

<h3><strong>2️⃣ Business Layer — <em>(EmployeeAttendanceTracker.Business)</em></strong></h3>
<ul>
  <li>Encapsulates all <strong>business logic</strong>, validation, and core operations.</li>
  <li>Implements services such as <strong>DepartmentService</strong>, <strong>EmployeeService</strong>, and <strong>AttendanceService</strong>.</li>
  <li>Independent of UI or data source, making it easy to test and extend.</li>
</ul>

<h3><strong>3️⃣ Data Layer — <em>(EmployeeAttendanceTracker.Data)</em></strong></h3>
<ul>
  <li>Handles data persistence using <strong>Entity Framework Core</strong>.</li>
  <li>Includes <strong>DbContext</strong>, <strong>Entity Models</strong>, and <strong>Repository Implementations</strong>.</li>
  <li>Follows the <strong>Repository Pattern</strong> for abstracting database operations and promoting flexibility.</li>
</ul>

<p>
The entire application uses <strong>Dependency Injection (DI)</strong> for clean and decoupled communication between layers.
</p>

<hr>

<h2><strong>⚙️ Setup & Installation</strong></h2>

<p>This project is preconfigured with an <strong>in-memory database</strong>, so no external setup is required.</p>

<h3><strong>Option 1 — Using Visual Studio</strong></h3>
<ol>
  <li><strong>Clone the Repository:</strong><br>
      <code>git clone &lt;your-github-repository-url&gt;</code></li>
  <li><strong>Open the Solution:</strong><br>
      Open <code>EmployeeAttendanceTracker.sln</code> in <strong>Visual Studio 2022</strong> or later.</li>
  <li><strong>Run the Application:</strong><br>
      Set <code>EmployeeAttendanceTracker.Presentation</code> as the startup project and press <strong>F5</strong>.</li>
</ol>

<h3><strong>Option 2 — Using .NET CLI</strong></h3>
<ol>
  <li><strong>Clone the Repository:</strong><br>
      <code>git clone &lt;your-github-repository-url&gt;</code></li>
  <li><strong>Navigate to the Solution Folder:</strong><br>
      <code>cd EmployeeAttendanceTracker</code></li>
  <li><strong>Run the Application:</strong><br>
      <code>dotnet run --project EmployeeAttendanceTracker.Presentation --launch-profile https</code></li>
</ol>

<p>
The app will launch automatically and be accessible at:<br>
👉 <a href="https://localhost:7142" target="_blank"><strong>https://localhost:7142</strong></a>
</p>

<p>
The <strong>in-memory database</strong> is self-seeding, so demo data will be created automatically upon startup.
</p>

<hr>

<h2><strong>🚀 Features Implemented</strong></h2>

<h3><strong>1. Department Management</strong></h3>
<ul>
  <li>Full <strong>CRUD (Create, Read, Update, Delete)</strong> operations.</li>
  <li>Displays <strong>number of employees</strong> in each department.</li>
  <li>Validates <strong>unique department names</strong> and <strong>codes</strong> through service-layer logic.</li>
</ul>

<h3><strong>2. Employee Management</strong></h3>
<ul>
  <li>Full <strong>CRUD</strong> operations for employees.</li>
  <li>Displays monthly attendance statistics (Present, Absent, Attendance %).</li>
  <li>Validates employee names (must have <strong>four words</strong>, each ≥ 2 characters).</li>
  <li>Ensures <strong>unique emails</strong> using both business-layer validation and EF Core constraints.</li>
</ul>

<h3><strong>3. Attendance Management</strong></h3>
<ul>
  <li><strong>Mark Attendance Dynamically:</strong> Mark employees as <strong>Present</strong> or <strong>Absent</strong> for a given day.</li>
  <li>Automatically prevents marking attendance for <strong>future dates</strong>.</li>
  <li>Provides <strong>live updates</strong> via <strong>jQuery + AJAX</strong> without full-page reload.</li>
  <li><strong>Filterable Attendance Reports:</strong> Filter by Department, Employee, or Date Range.</li>
</ul>

<h3><strong>4. Bonus Features</strong></h3>
<ul>
  <li><strong>Partial Views:</strong> Used for reusable UI components like <code>AttendanceTable.cshtml</code>.</li>
  <li><strong>Pagination:</strong> Implemented for large Employee and Attendance lists using a <code>PageResult&lt;T&gt;</code> wrapper.</li>
  <li><strong>Interactive UI:</strong> jQuery-driven filters and live search enhance the user experience.</li>
</ul>

<hr>

<h2><strong>🧰 Technologies Used</strong></h2>
<ul>
  <li>ASP.NET Core MVC 8.0</li>
  <li>Entity Framework Core (In-Memory)</li>
  <li>C# 12</li>
  <li>jQuery & AJAX</li>
  <li>Bootstrap 5</li>
  <li>Dependency Injection</li>
</ul>

<hr>

<h2><strong>👨‍💻 Author</strong></h2>
<p><strong>Mostafa Elsayed</strong></p>
<ul>
  <li>Email: <a href="mailto:hamedmostafa726@gmail.com">hamedmostafa726@gmail.com</a></li>
  <li>GitHub: <a href="https://github.com/Mostafa-Hamed619" target="_blank">github.com/Mostafa-Hamed619</a></li>
</ul>

<p align="center">⭐ If you like this project, consider giving it a star on GitHub!</p>
