# ThAmCo-Reviews

| BetterCodeHub                                                                                                                  | CodeFactor                                                                                                                                                          | Azure                                                                                                                                                                                                         |
| ------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [![BCH compliance](https://bettercodehub.com/edge/badge/Don-t-Fail/ThAmCo-Reviews?branch=develop)](https://bettercodehub.com/) | [![CodeFactor](https://www.codefactor.io/repository/github/don-t-fail/thamco-reviews/badge)](https://www.codefactor.io/repository/github/don-t-fail/thamco-reviews) | [![Build Status](https://dev.azure.com/dsc1998/ThAmCo/_apis/build/status/Don-t-Fail.ThAmCo-Reviews?branchName=develop)](https://dev.azure.com/dsc1998/ThAmCo/_build/latest?definitionId=1&branchName=develop) |

# Overview
This web service handles the Reviews of Products within the ThAmCo project.

## Branch Management

The main branch is Master, and should only be committed to/pushed to when the service has been confirmed working (this includes passing tests and human testing). All pushes to Master will be pushed into production.
Develop is where features may be added (only by means of feature branches), tests should all pass when being pushed to this branch, however some things may not work fully.
Feature branches are assumed unstable, and are actively being worked on. CI does not build these branches.

# Implementation

## Tools and Frameworks
* Visual Studio 2019 (with ReSharper) for development, debugging done in IIS Express.
* Visual Studio Code, used for editing .MD README files.
* C#, ASP.NET Core version 2.2.
* Azure DevOps Pipelines, used for CI and building docker images.
* CodeFactor, used to help ensure code quality on pull requests.
* BetterCodeHub, same as above.
* Docker, Docker-Compose
* Stylecop
* Windows 10 (Development Environment)
* Linux Ubuntu (Selfhosted Docker)
* GitHub, GitHub Desktop
* Postman

### Security Topics
Due to no central Login/Accounts for the project, please view the Accounts controller in `Controllers/AccountController.cs` as this is where authentication takes place within this service. Controller actions are authorised appropriately and can be seen throughout `Reviews.Controllers.ReviewController.cs`.

### Testing
* Visual Studio Test Tools & ReSharper Test Explorer, to run tests and check coverage.
* Moq is used to test HttpClients, this can be seen in `Reviews.Tests.Data.PurchaseService Tests lines 69-119` where they are created, and used in the same file, `lines 155-158`, for example.
* Test doubles are also used extensively, and can be seen most clearly in `Reviews.Tests.Controllers.ReviewsControllerTests`, with test data at the top of the file, in its own class, for use within `Reviews.Data.Reviews.FakeReviewRepository`.
* Postman was used for testin the API endpoints in this project, and also to help with the creation of DTOs from other services.

### Continuous Integration (CI)
I chose to use Azure DevOps Pipelines to perform tests and continuous integration for this project, triggered on commits and pull requests to the develop and master branch, as I deemed this to be most useful for my purposes. The configuration file for this (`azure-pipelines.yml`) can be found in the root of this project. 

Azure also builds and pushes a docker image from the `Dockerfile` of this project, to help with dpeloyment later.

### Deployment
A `Dockerfile` is included within the repository to allow the service to be deployed using Docker, and works using the artefact produced from the Azure DevOps Pipeline.

In order to run this service as a Docker Container, you can either pull the image down from [Docker Hub](https://hub.docker.com/repository/docker/dcurrey/thamco-reviews), or build it yourself using the Dockerfile.

Once the other services were completed, the plan was to use docker-compose to facilitate the deployment of all the services as needed as this would be the most efficient way to deploy them, along with their databases and any additional setup required.
