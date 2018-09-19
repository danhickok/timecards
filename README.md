# Timecards

This is a small Windows app for tracking your time.

This application serves not only as a useful application, but it also as an
example of a properly designed multi-layer application in that it has a core
layer with no dependencies, and has two outside layers: one for data storage and
retrieval and one for the user interface.

This is currently a work in progress.  At this time, the core and data layers
are finished, and included is a set of unit tests that validate these layers.  Remaining
are the integration tests and the UI layer.

See the "Design Notes" document for notes on the overall design and a running
commentary on what was discovered during development.


