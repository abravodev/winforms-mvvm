# Winforms MVVM

> *If you can, use WPF. If you can't, keep reading*

This is a sample app built with Windows Forms and using the paradigm of Model-View-ViewModel

It is thought to be used in legacy applications where the Form classes are polluted with different types of code.
The technologies used in this project allow to introduce them little by little (MVVM, dependency injection, data layer...)... without the need of a full rewrite.
Depending on the type of legacy project, the usage of MVVM can be limited because of the fact that there might be current code that breaks its rules.
Even in those cases, having at least the idea of how should be can be helpful enough despite of having to make concessions.
My advice is to use it in new views and, after gaining confidence, starting moving old views little by little.


## Technologies
- **MVVM** - Custom made actually, but inspired by [WFBind](https://github.com/mareklinka/WFBind), [MvvmFx](https://github.com/MvvmFx/MvvmFx), [Prism.WinForms](https://github.com/imasm/Prism.WinForms), [CompositeWPFContrib](http://compositewpfcontrib.codeplex.com/)
- **[SimpleInjector](https://simpleinjector.org/)** - Simple way to have dependency injection in the app and with little trouble to make changes in code structure
- **[Automapper](https://automapper.org/)** - Useful for doing silly mapping between different objects (class for the model, class for the view...)
- **[Dapper](https://stackexchange.github.io/Dapper/)** - If you have a legacy project you may be still using SqlConnection, SqlDataReader, DataRows... You know the hassle, you may not know your savior.