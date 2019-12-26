N.B: This project is created using ASP.NET MVC following the Repository pattern and entity framework with Linq. And SQL Server is used as a database. For front-end, Jquery and AJAX are used. Separate DbContext is used for user management and project management. 

1.	Save Department:
Department code and name unique checking both in the client and server-side. Code name between 2 to 7 character done by StringLength, MinimumLength attribute. All the fields are required here. 


2.	View All Departments:
Information about all departments shown in a default HTML table and designed by bootstrap 4

3.	Create Course:
Code and name unique checking in the client and server-side. Code minimum length of five characters maintained by StringLength, MinimumLength attribute. The credit range is done by the Range attribute. By default page loads with all department name and id as the value in department dropdown. Also, eight-semester data is loaded from the database. All the fields are required here.

4.	Save Teacher:
Email format is checked by custom REGEX made by me. Email unique is checked on the server-side and client-side. Credit to be taken must not contain a negative value that is done by the Range attribute.

5.	Course Assign to Teacher:
Page loads with all department data and after selecting a department all the teacher and course code loads in the respective dropdown box it's done with Jquery and ajax. After selecting a teacher; the teacher's assigned credit will be shown in credit to be taken field. And remaining credit is calculated with the assigned course's credit and teacher's credit. When a user will select a course for assign; with change event and ajax it will be checked if the course is already been assigned to any teacher to prevent the overlapping problem. To be extra careful it will also be checked on the server-side after the information has been posted. If a teacher remaining credit becomes 0 and the user tries to assign more courses to that teacher a confirmation message will be shown, and if user wises to proceed; the teacher remaining credit will have a negative value. 

6.	View Course Statics:
When a department is selected on the Jquery change event all the courses of that department will be loaded with LINQ semester information and teacher information will be loaded.

7.	Register Student:
Email format for student email will be checked with custom REGEX made by me. For date picker, the JQUERY date picker is used. Registration number fixed format is created on server-side and will be shown in a new details page

8.	Allocate Classrooms:
The page will be loaded with seven days and room information. The overlapping problem for the already allocated classroom on a particular day and between a particular time will be checked with a very strong custom function made by me. If the room already allocated on that particular time error message will be shown. 

9.	View Class Schedule and Room Allocation Information:
After the user selects a department from the dropdown list; all the rooms allocated for the courses of that department will be shown. To mention, for one course; all the allocated room information will be shown in a single row. For showing classroom information LINQ joins and the union is used and data is shown via AJAX call on client-side. 

10.	Enroll in a Course:
On page load, all the student registration numbers will be loaded and student id as the value. After selecting a department all the related information of that student will be loaded. According to the student's department, all the courses on that department will be loaded in the course dropdown box. For date picker, the JQUERY date picker is used.

11.	Save Student Result:
After selecting a student; students' related information will be loaded with an AJAX call. The course will be loaded with all the courses the student has been enrolled in. All the grades will be loaded on page load with grade information. After the user grade a student course, that course will be marked as graded on the database. And if the user reselects the same course and selects a grade an alert will be shown that this course is already graded for this student. And if the user wishes to continue that course grade will be updated with the recent grade. 

12.	View Result:
The student registration number will be loaded on page load, and after the user selects a student that student's information and all the graded nongraded course will be loaded on HTML table. With LINQ join and union, all graded and nongraded courses will be shown with AJAX call and JQUERY change event. After a student result is shown; user can click Make PDF button and via JSPDF the result will be converted to PDF and downloaded automatically. A View page and view model is created for this purpose. 

13.	Unassign All Courses:
After unassign, all courses have been clicked. The user will see a confirmation dialog box and if the user wishes to proceed and clicks yes, the assign courses table in the database which has a column named isAssigned with a bit value of 1, the value of that column will be changed to 0. And no information will be deleted. After unassign, all courses; the teacher remaining credit will be the same as the assigned credit. And the user will be able to reassign teachers and students in the previous assigned course.

14.	Unallocate All Classrooms:
After the user, clicks unallocate classroom a confirmation box will be shown. And if user wishes to proceed all the classrooms will be unallocated from courses. And allocate classroom table in the database which has a column named IsAllocated with the value of 1 bit, will get updated with 0. And can be allocated again.