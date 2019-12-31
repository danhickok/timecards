# TimecardsUI Project

This project contains all the Windows forms used by the application.

`MainForm` is the first form loaded by the Timecard project.  All other forms
are opened as dialogs to minimize interaction between forms, keeping the code
as simple as possible.

Currently only one form - `ImportForm` - makes use of threading, so that the
long-running process of the data import does not leave the form unresponsive.
