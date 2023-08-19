# TimecardsTesting Project

This project hosts all the unit and integration tests for the application. It
uses the `Microsoft.VisualStudio.TestTools.UnitTesting` to run the tests.

There are global setup and teardown routines in classes in the Base folder.
These routines make sure that the tests always start with a new, empty database
for testing.
