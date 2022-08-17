
# Release Retention API

## Welcome to DevOps Deploy

Welcome to DevOps Deploy: the popular tool that software teams use to deploy their applications. 

DevOps Deploy enables repeatable, reliable **deployments** by managing the **release** of software **projects** through **environments**. DevOps Deploy uses these concepts (more on them [later](#devops-deploy-concepts)) to help teams gain confidence in their ability to deploy the software they make.

## Success!

What DevOps Deploy is selling has struck a chord with software teams. They have been enjoying using DevOps Deploy so much that they find themselves deploying very frequently. 

But the plot thickens! Some of DevOps Deploys long-time customers are starting to notice that the thousands upon thousands of releases that they have created are causing some problems:

- The deployments of releases store logs and artifacts on disk, so having many releases is consuming huge amounts of storage.
- Having many releases can make it difficult to find the releases that the teams care about.
- DevOps Deploy slows down when it needs to process a huge number of releases.

## The Pitch

The team at DevOps Deploy has looked into the problems reported by their long-time customers and it turns out customers really only care about releases that have recently been deployed. To free up resources, releases that have not had any recent activity could be deleted without being missed. 

So DevOps Deploy has decided to delete old releases, and is calling this cool new feature Release Retention.

## The API

The API implements the Release Retention rule which determines which releases to keep. Given a set of **projects**, **environments**, **releases** and **deployments**: determine the releases that should be kept.

The Release Retention rule:
> [**Projects**](#project) can have zero or more [**releases**](#release), which can be released to an [**environment**](#environment) by creating a [**deployment**](#deployment). A **release** can have zero or more **deployments** for a **project** and **environment**.  
> 
> For each **project**/**environment** combination, keep `n` **releases** that have most recently been deployed, where `n` is the number of releases to keep. 
> note: A **release** is considered to have "been deployed" if the release _has one or more_ **deployments**.

The Release Retention rule must meet the following requirements:
- Be reusable and testable
- Have tests, as you would for production code
- Not have a UI, CLI, or database
- Take the number of releases to keep as a parameter
- Return the releases that should be kept
- Log why a release should be kept

The supplied JSON files contain sets of projects, environments, releases and deployments that represent what shape the input data might be in.

### Sample Test Cases

To help clarify the behaviour of the Release Retention, here are some sample test cases and their expected results (these are **not** based on the supplied sample data). They could be used as a guide to understanding the problem domain and be necessary for a successful implementation:

#### Test Case: 1 Release, Keep 1


##### Test Data
| Project-1 | Environment-1 |
| ------------- | ------------- |
| `Release-1` (Version: `1.0.0`, Created: `2000-01-01T08:00:00`)  | `Deployment-1` (DeployedAt: `2000-01-01T10:00:00`) |

##### Expected Result

- `Release-1` kept because it was the most recently deployed to `Environment-1`

#### Test Case: 2 Releases deployed to the same environment, Keep 1

##### Test Data
| Project-1 | Environment-1 |
| ------------- | ------------- |
| `Release-1` (Version: `1.0.0`, Created: `2000-01-01T08:00:00`)  | `Deployment-2` (DeployedAt: `2000-01-01T11:00:00`) |
| `Release-2` (Version: `1.0.1`, Created: `2000-01-01T09:00:00`)  | `Deployment-1` (DeployedAt: `2000-01-01T10:00:00`) |

##### Expected Result

- `Release-1` kept because it was the most recently deployed to `Environment-1`

#### Test Case: 2 Releases deployed to different environments, Keep 1

##### Test Data
| Project-1 | Environment-1 | Environment-2 |
| ------------- | ------------- | ------------- |
| `Release-1` (Version: `1.0.0`, Created: `2000-01-01T08:00:00`)  | | `Deployment-2` (DeployedAt: `2000-01-02T11:00:00`) |
| `Release-2` (Version: `1.0.1`, Created: `2000-01-01T09:00:00`)  | `Deployment-1` (DeployedAt: `2000-01-01T10:00:00`) | |

##### Expected Result

- `Release-1` kept because it was the most recently deployed to `Environment-2`
- `Release-2` kept because it was the most recently deployed to `Environment-1`

## DevOps Deploy Concepts

#### Project

A project represents the thing that is getting deployed and contains all of the information about how it is deployed. A project usually maps to the software that the team is deploying: an application, a website, a database, or a service. In this exercise, there are two projects: Random Quotes and Pet Shop.

#### Release

A release is a snapshot in time of a project, which is a fancy way to say a version. The first snapshot of a project may be version 1.0.0 (which would be release 1.0.0). Some bugs are fixed, or new features added, and the next version is ready to ship! This would become a new release with version 1.0.1.

#### Environment

An environment is a collection of the things that get deployed to. It would usually be composed of the physical and virtual machines where the project will run. In this exercise there is a Production environment, where the projects will be customer-facing, and a Staging environment where changes are tested on their way to the Production environment.

#### Deployment

A deployment is the way a release of a project ends up in an environment. If we created release 1.0.0 of the Pet Shop project, we could deploy it to the Staging environment. We'll check the deployment and make sure everything is ok, and then create a new deployment of the same release to the Production environment. Every time a release of a project is sent to an environment, a new deployment is created.

## Architecture
### Logical architecture
![Architecture var 1](https://github.com/MrRustenLine/ReleaseRetention/blob/master/ReleaseRetentionAPILogical%20Architecture.png)

### Physical architecture
![Architecture var 1](https://github.com/MrRustenLine/ReleaseRetention/blob/b1473b5444689b5dbfa032f1cce284b73a2edccf/Release%20Retention%20API%20physical%20architecture%20-%20Clean%20Architecture.png)

The physical architecture implements the Clean Architecture pattern whose "...overriding rule that makes this architecture work is The Dependency Rule. This rule says that source code dependencies can only point inwards. Nothing in an inner circle can know anything at all about something in an outer circle." To implement it, another design pattern IoC/DI proves quite handy. For example, ReleaseController injects all dependencies into BusinessManager.

