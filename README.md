# Winforms MVVM

> *If you can, use WPF. If you can't, keep reading*

This is a sample app built with Windows Forms and using the architectural pattern of [Model-View-ViewModel (MVVM)](https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern).

It is thought to be used in legacy applications where the Form classes are polluted with different types of code.
The technologies used in this project allow to introduce them little by little (MVVM, dependency injection, data layer...)... without the need of a full rewrite.

Depending on the type of legacy project, the usage of MVVM can be limited because of the fact that there might be current code that breaks its rules. Even in those cases, having at least the idea of how should be can be helpful enough despite of having to make concessions.

My advice is to use it in new views and, after gaining confidence, starting moving old views little by little.

## Technologies
- **MVVM** - Custom made actually, but inspired by other tools [see Inspiration section](#Inspiration).

- **[SimpleInjector](https://simpleinjector.org/)** - Simple way to have dependency injection in the app and with little trouble to make changes in code structure
- **[Automapper](https://automapper.org/)** - Useful for doing silly mapping between different objects (class for the model, class for the view...)
- **[Dapper](https://stackexchange.github.io/Dapper/)** - If you have a legacy project you may be still using SqlConnection, SqlDataReader, DataRows... You know the hassle, you may not know your savior.
- **[Easy.MessageHub](https://github.com/NimaAra/Easy.MessageHub)** - To communicate between view models via events/messages.

## Rest of toolbox
- **[FluentMigrator](https://fluentmigrator.github.io/)** - Version your database migrations. As said in [Paul Stovell's blog](https://paulstovell.com/database-deployment/), « if your database isn't in source control, you don't deserve one. Go use Excel ».
- **[SmartEnum](https://github.com/ardalis/SmartEnum)** - Type-safe enums to include behaviour and avoid switch-cases
- **[Serilog](https://serilog.net/)** - For logging
- **[FlaUI](https://github.com/FlaUI/FlaUI)** - Successor of [TestStack/White](https://github.com/TestStack/White), it is used to automate UI tests
- **[AdvancedDataGridView](https://github.com/davidegironi/advanceddatagridview)** - It adds Excel-like filtering and sorting to the tables
- There might be more... check the nuget packages.

## Inspiration
While building this, I've had to search for a lot of things in Google, so there are many inspirations (in no special order):

[WFBind](https://github.com/mareklinka/WFBind) - [MvvmFx](https://github.com/MvvmFx/MvvmFx) - [Prism.WinForms](https://github.com/imasm/Prism.WinForms) - [CompositeWPFContrib](http://compositewpfcontrib.codeplex.com/) - [DevExpress MVVM Framework](https://docs.devexpress.com/WPF/15112/mvvm-framework)

# Sample app
## Setup
To launch the app or pass the integration tests, you will need to recreate the database, using the `UserManager.DataAccess.Migrations` project. It is a console app that will run the pending migrations.

## Features
### Users
- List users
- Create user
### Roles
- List roles
### Language
- Change language of the app
	- *This is not complete because I haven't found a proper way to keep a minimum number of resources yet*
- Save it into user preferences