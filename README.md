# Users REST API
This repository contains my implementation of a simple REST API for working with the users.
## Domain Description
Each user is defined by the following attributes: unique login, password, group and status. The group attribute is an enumeration and can be "User" or "Admin". The status attribute is also an enumeration and can take the value "Active" or "Blocked".

At any given time, there can be no more than one active administrator in the system (user with the "Admin" group). In addition, there should not be two or more active users with the same login (users with the "Active" group).

The process of creating a new user should take 5 seconds. During this time it should not be possible to add a new user with the same login.

When a user is deleted, his status in the system should change to "Blocked".

## API Description
Swagger UI documentation is implemented for this API. Also the table below shows the implemented HTTP request methods and their descriptions.

| Request Method  | Request Parameters | Request Body | Response Body | Description |
| --------------- | ---------- | ----- |-------- | ------ | 
| POST            | -          | { "login": "string",<br>"password": "string",<br>"userGroupCode": "Enum" } | { "userId": "int",<br> "login": "string",<br> "groupCode": "Enum",<br> "stateCode": "Enum" }| Adds a new user and returns information about him, if possible (status code 201)<br>When trying to create a second administrator or a user with the same login,<br> it returns an error (status code 409)<br>When trying to create a new user while another one with the same login is being<br> created, it returns an error (status code 422)|
| GET             | -          |  - | [<br>{ "login": "string",<br>"groupCode": "Enum",<br>"stateCode": "Enum"}<br>] | Returns information about all users (status code 200)<br>stateCode of returned users is always "Active" |
| GET             | int64 id   | - | <br>{ "login": "string",<br>"groupCode": "Enum",<br>"stateCode": "Enum"}<br> | Returns information about the user with the passed id if possible (status code 200)<br>stateCode of returned user is always "Active"<br>If a user with this id does not exist, returns an error (status code 404) | 
| DELETE          | int64 id   | - | - | Sets the status of the user with the given id to "Blocked" if possible (status code 204)<br>If a user with this id does not exist, returns an error (status code 404)|

## Used Technologies
A PostgreSQL database was chosen. To start the Docker container with the database, enter the ```docker compose up``` command in the terminal.

* Entity Framework Core is used to work with the database.
* The BCrypt.Net-Next library is used to hash user passwords.
* AutoMapper is used For mapping between models of different layers of the application

Unit tests were written for the service (Domain Layer) and repository (Data Access Layer). The following tools were used to write tests:
* xUnit - chosen testing framework.
* Moq - libary for mocking repository in service class unit tests.
* FluentAssertions - library for assertions.
* Microsoft.EntityFrameworkCore.InMemory - library for using inmemory database in repository class unit test.
