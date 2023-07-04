# TimecardsLogger Project

This project contains a logger class.  This writes a date-stamped line to a text
file whose path is defined in ProductionAppConstants.  The writing is only done
if the solution is targeting the Debug configuration (that is, the DEBUG
preprocessor variable is set).  So this should have no effect if targeting the
Release configuration.

