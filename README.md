# NMR-Billing
A simple program that takes log files from different scientific devices and generates billing reports.

Limitations were imposed during the creation of the program. It was known that the client would be responsible for future maintainance and modifications starting when the client approves the final release version. The program must be written in a way that an unexperienced novice programmer can perform maintenance or updates on their own. The program must be fully commented at the time of release to help guide any future programmers. The program must have error handling, but it must not be complicated in nature, to allow a novice to understand and replicate it if needed. The client insists that a form be dedicated to displaying the various types of information that has been parsed and calculated in the event troubleshooting is needed in the future.

Features that still need implementing (May not be complete):

1. Bind all elements on the forms to allow expanding/contracting the window.
2. Add a "Generate all reports" button.
3. Add a ListBox on the main form to display the generated reports for the user's viewing convenience.
4. Finish reporting implementation for Groups by Department and Users by Group.
5. Add more sophisticated error handling for report generation, format control, and log parsing.
6. Move all test ListBoxes to a separate form to allow the client to view them if they encounter a problem during use.
7. Finalize the UI. The current UI is mainly for ease of testing, not practical use.
8. Potentially add support for .rtf or .pdf report generation depending on client's needs.
9. Find a method to store and recall the fiscal year to date calculation that will support the adding and removing of reports.
10. Allow the user to delete unwanted reports from within the GUI.
11. Create a psudo "install" feature. Generate the appropriate folders to store various data such as master lists, log files, and 
     generated reports.
