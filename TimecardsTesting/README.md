# TimecardsTesting Project

This project hosts all the unit and integration tests for the application. It uses the `NUnit` package to run the tests.

There are global setup and teardown routines in the "...Init" class of each folder. These routines make sure that the tests always start with a new, empty database for testing.
