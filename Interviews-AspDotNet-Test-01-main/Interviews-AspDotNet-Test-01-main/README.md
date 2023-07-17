## Instructions:

You have been asked to implement a set of services to assist with a school's rostering application.   Given a domain with Classroom, Teacher and Student objects complete as much of the implementation as possible within the allotted time.  

*The system should be able to print the roster with a teacher and a minimum of 3 students.*

For convenience, a template of an ASP.NET Core in .NET 5 project has been provided. The project contains a Swagger portal for easy testing and a generic Classroom class with helper methods. The controller routes, Teacher and Student objects are up to you to implement.

The excecise is designed to test your understanding of ASP.NET controllers, HTTP & some advanced coding practices. It is understandable given the short time period if a multi-tiered solution is not implemented.

## Requirements:

The system will track the roster for a single classroom.

The system shall be capable of adding a teacher to the classroom.

The system shall be capable of adding multiple students to the classroom.

The system shall be capable of returning the names of the teacher and each student in the classroom after they have been added.

The system shall not return a roster if no teacher has been assigned.

The system shall not return a roster if less than 3 students have been assigned.

The system should return the roster in JSON format.

The system should handle and log any exceptions.

The system should return appropriate HTTP response codes to the client.

