# TimecardsData Project

This project contains the logic needed to access the timecards database.  At this time, this project is designed to work with SQLite databases; but it can be modified to handle any data source, as long as it meets the requirements of the interfaces defined in the core project.

This project follows the Code-First scheme for Entity Framework.  Database schema is automatically created the first time the database is accessed.

This project contains:

* Entity Framework model classes.
* An EF database context class.
* A repository for common database operations.
* A database connection string builder, which is responsible for resolving the `%APPDATA%` path in the connection strings.
* Extension methods that define EF-to-core property mapping.

