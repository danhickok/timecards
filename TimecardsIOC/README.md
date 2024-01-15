# TimecardsIOC Project

This project contains the Inversion-of-Control class that allows components of a multi-layer application to work together.

This project contains:

* The Factory class, which is:
  * Used by the "owner" projects (the Timecards project for production, and the TimecardsTesting project for testing) to _register_ concrete classes with their respective interface classes
  * Used by the core and other layers to _resolve_ an interface to a concrete class.

Additionally, the Factory class is responsible for maintaining singleton instances.  Singletons are determined during the registration, and the Factory is responsible for the disposal of them when the application finishes execution.
