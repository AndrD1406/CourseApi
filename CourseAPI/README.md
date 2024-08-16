.NET Core Web API Project with Swagger, Entity Framework & MySQL

Objective:
Create a .NET Core Web API project that manages three entities: Student, Teacher, and Course. This project should include Swagger for API documentation, Entity Framework with Pomelo MySQL for data storage, and unit tests using Moq for testing the application. Additionally, the project should incorporate the Repository Pattern for data access and use a code-first approach for database migrations.

Task Description:

1. Entity Design:
   - Define three entities: Student, Teacher, and Course with relevant attributes. Establish the necessary relationships between these entities, such as a Student enrolling in multiple Courses, a Course having a Teacher, etc.

2. Repository Pattern:
   - Implement the Repository Pattern for data access. Create separate repositories for each entity to encapsulate the data access logic and abstract the underlying data source.

3. API Endpoints:
   - Implement CRUD (Create, Read, Update, Delete) operations for each entity using RESTful API endpoints. These endpoints should interact with the repositories to access and manipulate the data. These endpoints should include, but are not limited to:
    - Students: Get all students, Get student by ID, Create a new student, Update a student, Delete a student.
    - Teachers: Get all teachers, Get teacher by ID, Create a new teacher, Update a teacher, Delete a teacher.
    - Courses: Get all courses, Get course by ID, Create a new course, Update a course, Delete a course.

4. Swagger Documentation:
   - Integrate Swagger to automatically generate API documentation. Document the available endpoints, their input parameters, and expected responses.

5. Data Storage:
   - Utilize Entity Framework Core with a code-first approach for database migrations to connect to a MySQL database. Configure the data context, models, and migrations for the three entities (Student, Teacher, Course). Ensure proper database relationships and foreign key constraints.

6. Testing:
   - Write unit tests for the API using Moq for mocking dependencies. Test the API endpoints for different scenarios, including positive and negative test cases. Ensure that tests cover data validation, error handling, and expected behavior.

7. Authentication and Authorization (Optional):
   - Implement authentication and authorization mechanisms if required. You can use JWT, OAuth, or any other suitable method.

8. Exception Handling:
   - Implement consistent error handling and return meaningful error responses in case of failures.

9. Validation:
   - Implement input validation and data validation to ensure data integrity.

10. Logging (Optional):
    - Implement logging to track API activity and errors for monitoring and debugging.
