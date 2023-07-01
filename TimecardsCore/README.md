# TimcardsCore Project

This project contains the domain classes that define the application at its
core, plus business logic for manipulating objects created from these classes.

The domain classes also serve as data transfer objects:  they are independent of
their Entity Framework counterparts, so they can hold information more in line
with the application itself.

This project contains:

* A custom EventArgs class, used in the bulk data operation to raise an event
while the import process is executing.  This is handled by the UI layer to
show the progress of the import to the user.
* Custom Exception classes, to make exception handling logic clear.
* An extension method for providing user-friendly descriptions of enum values.
* An extension method for making it easier to add attributes to an XML node.
* Interfaces for allowing the core to access objects from the outside layers.
* Business logic classes that separate logic from the data they manipulate.
* Model (or domain) classes.
