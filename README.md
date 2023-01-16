# Winforms MVVM

> *If you can, use WPF. If you can't, keep reading*

[![CI](https://github.com/abravodev/winforms-mvvm/actions/workflows/ci.yml/badge.svg)](https://github.com/abravodev/winforms-mvvm/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

This is a sample app built with Windows Forms and using the architectural pattern of [Model-View-ViewModel (MVVM)](https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern).

It is thought to be used in legacy applications where the Form classes are polluted with different types of code.
The technologies used in this project allow to introduce them little by little (MVVM, dependency injection, data layer...)... without the need of a full rewrite.

Depending on the type of legacy project, the usage of MVVM can be limited because of the fact that there might be current code that breaks its rules. Even in those cases, having at least the idea of how should be can be helpful enough despite of having to make concessions.

My advice is to use it in new views and, after gaining confidence, starting moving old views little by little.

The ultimate objective is to reduce logic in views so much that, theoretically, they could be represented with XML (like XAML in WPF). If there's no real business logic in the view and there's only glue logic, you can build a comprehensive test suite full of unit tests for the rest of the code, and rely on end-to-end tests to check that the glue logic from views work.
Keep in mind that end-to-end tests comes with some requirements:
- They take more time to run
- They will need a Desktop session: you are interacting with windows)
- They cannot easily run in parallel: you are interacting with windows, so they would be stealing the focus to each other

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

## Alternatives
There are some alternatives to MVVM, let's review it:
- **Do nothing**. That's the default option. It is true that you don't need a framework to build your app, but you will end up with hard-to-reason code or with building your own framework inadvertently.
- **MVC**. Model View Controller. Examples:
    - [shane-lab/winforms-mvc](https://github.com/shane-lab/winforms-mvc)
- **MVP**. Model View Presenter. Examples:
    - [mrts/winforms-mvp](https://github.com/mrts/winforms-mvp)
    - [Los Techies - Model View Presenter Styles](https://lostechies.com/derekgreer/2008/11/23/model-view-presenter-styles/)

# Sample app
## Setup
To launch the app or pass the integration tests, you will need to recreate the database, using the `UserManager.DataAccess.Migrations` project. It is a console app that will run the pending migrations. It can accept an argument with the first command that you want to execute.

#### Run through Visual Studio
You can setup that the default action when you run the console app is to `MigrateToLatest`.

This is set through `Project > Properties > Debug > Command Line Arguments`

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

# Toolbox features
## Bindings
You can use bindings for data properties:
```csharp
this.BindTo(ViewModel)

    // Bind the 'Text' property of 'tb_firstName' to the value of 'FirstName' in the ViewModel
    .For(this.tb_firstName, _ => _.Text, _ => _.FirstName)
    .For(this.tb_firstName, _ => _.FirstName) // A more concise version

    // Bind the 'ForeColor' property of lbl_status to the value of 'DatabaseConnection.ConnectionStatus' in the ViewModel
    // In this case, we're using a custom converter (StatusToColorConverter) to map from an enum to a color
    .For(this.lbl_status, _ => _.ForeColor)
        .WithConverter(_ => _.DatabaseConnection.ConnectionStatus, new StatusToColorConverter())

    // It also works with DataGridViews mapped to an in-memory list
    .For(this.dgv_userlist, _ => _.Users)

    // Bind the tooltip text on 'lbl_databaseConnectionString' to the value of DatabaseConnection.Server in the ViewModel
    .WithTooltipOn(this.lbl_databaseConnectionString, _ => _.DatabaseConnection.Server, dependsOn: _ => _.DatabaseConnection)
```
You can use bindings for behaviours:
```csharp
this.BindTo(ViewModel)

    // Make the button 'bt_save' enabled based on a boolean property in the viewmodel
    .For(this.bt_save, _ => _.Enabled, _ => _.CanCreateUser, dependsOn: _ => _.CreateUserInfo)

    // When someone clicks on 'btn_users', the command 'NavigateToUsersView' in the ViewModel will execute
    .Click(this.bt_save, _ => _.CreateUserCommand)
    .Click(this.bt_save, () => Console.WriteLine("Button pressed")); // It can be an anonymous action

    // Include contextual menus in DataGridViews or any other control
    .WithContextMenu(this.dgv_userlist,
        // We set the menu option text, the action on clicked and the icon (optional)
        MenuOption.Create(General.Delete, ViewModel.DeleteUserCommand, IconChar.Times))
```
Add validation to your form:
```csharp
// ep_createUser is the ErrorProvider of the form
this.WithValidation(ep_createUser)

    // Make the email 'TextBox' required
    .On(this.tb_email).FieldRequired();
```
## Other good stuff:
- `IMessageDialog` so that you don't depend on the `MessageBox`
- `SnackbarControl` to notify the user after you make an action. And a `ISnackbarMessage` for using it in your VMs.
- `IViewNavigator` to navigate between views