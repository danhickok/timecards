# Timecards Project

This project is the startup project for the application.  In addition to
launching the application itself, it is also responsible for registering key
classes in the IOC container, and for displaying any untrapped exceptions.

Aside from this startup project, the application contains the following projects:

* **TimecardsCore** - this contains the domain classes and business logic.
* **TimecardsData** - this contains data access logic (including a repository, EF classes, and mappings between the domain and EF classes).
* **TimecardsIOC** - this contains the inversion-of-control (IOC) class.
* **TimecardsLogger** - this contains a class for writing logging messages.
* **TimecardsTesting** - this contains the unit and integration tests for the application.
* **TimecardsUI** - this contains the user interface (winforms) for the application.

This application follows the "clean architecture" principles of separating the core of the application from other "layers" around it.  This has sometimes been referred to as "Onion" architecture or "hexagonal" architecture.

All dependencies are _toward_ the core:  the core knows nothing of the outside world, save for interfaces. The IOC class supplies the core with objects from the outside layers who match the interfaces it knows about.

The result is that the components are loosely coupled, minimizing the impact that changes have in any one component.
