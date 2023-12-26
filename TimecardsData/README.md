# TimecardsData Project

This project contains the logic needed to access the timecards database.  At
this time, this project is designed to work with SQLite databases; but it can be
modified to handle any data source, as long as it meets the requirements of the
interfaces defined in the core project.

This project follows the Code-First scheme for Entity Framework.  Database
schema is automatically created the first time the database is accessed.

This project contains:

* Entity Framework model classes.
* An EF context class.
* A repository for common database operations.
* A database connection builder, which is responsible for resolving the
`%APPDATA%` path in the connection strings.
* Extension methods that define EF-to-core property mapping.

----

I did some experimenting with a tutorial on using .NET and Sqlite.  This is the link to
the tutorial:

https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli

Here's what I learned:

* It's actually pretty easy to set up the context object.

```
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BloggingContext : DbContext
{
    public string DbPath { get; }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    public BloggingContext()
    {
        DbPath = @"c:\temp\EFGetStarted.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
```

* You can use attributes to more precisely define required, key, and foreign key relationships.

```
[Table("Blogs")]
public class Blog
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Url { get; set; } = "";

    public List<Post> Posts { get; } = [];
}

[Table("BlogPosts")]
public class Post
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    [ForeignKey("Blog")]
    public int BlogId { get; set; }

    public Blog? Blog { get; set; }
}
```

* You must use dotnet tools to create the migration code:

```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```

* Once the database migrations are set up, you can make sure the database gets created
on first use by using the `EnsureCreated()` method.

```
using var db = new BloggingContext();

// this statement requires the database to already exist
Console.WriteLine($"Database path: {db.DbPath}");

Console.WriteLine("Ensuring database is created");
db.Database.EnsureCreated();
```
