# EmployeeManagement
This is Demo project for employee management for practice 

### The tools which are used for this project ###
* Asp.Net Core 5
* Entity Framework Core
* vuejs 3
* vuex

# ApiProject

## REST APIs
In this project REST APIs is emplemented with Swagger documentation [See Controller](https://github.com/Bilal-surawala/EmployeeManagement/blob/master/ApiProject/Controllers/EmployeesController.cs)

## AutoMapper 
Used AutoMappre for map domain entity with view model [See Class](https://github.com/Bilal-surawala/EmployeeManagement/blob/master/ApiProject/AutoMapper/MapProfiles.cs)

## Azure Blob storage
Azure Blob storage used to store Images, Images are stored in azure but facing issue while access it [See](https://github.com/Bilal-surawala/EmployeeManagement/blob/master/ApiProject/Services/BlobsService.cs)

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

# DAL
Database access layer for API which is loosely coupled with API project

## Entity framework core
used entity framework core in project with Fluent API to configure domain classes [See EntityTypeConfiguration](https://github.com/Bilal-surawala/EmployeeManagement/blob/master/DAL/Mapping/EmployeeMap.cs)
used common service interface for CRUD of specific entity

# Client-app
create frontend project with Vue3 and vuex
