# EmployeeLeaveProgramAssessment

## Local set up
1. Load the project with visual studio that supports .NET 4.6 or above framework
2. Add database connection string and smtp in web.config with any SQL database
3. Navigate to root/Migrations/Configuration.cs and modify var employees = new List<Employee> to your need or list. (Recommended: Please use email that's accessible so notification can be sent to the email and be seen)
4. Go to Manage Nuget > Package Console and run 'update database'
5. Run the app with IIS

## Guide on how to use
1. There are three existing users which are
    i. Abdul Ghani (EmployeeId: 1321)
    ii. Ang Foo (EmployeeId:1322)
    iii. Anumugham Muthu (EmployeeId:1323)

2. When applying for leave, use one of the existing employee id information for applicant and manager as per seeded to apply for leave.
3. An email will be sent to the manager email adddress for approval. (Note: Please use an actual mail address that can be accessed to get the link)
4. Once the link is clicked, manager can either approve or reject