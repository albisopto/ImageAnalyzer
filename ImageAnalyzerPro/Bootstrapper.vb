Imports System.ComponentModel.Composition.Hosting
Imports Prism.Mef

Public Class Bootstrapper
    Inherits MefBootstrapper

    Protected Overrides Sub ConfigureAggregateCatalog()
        MyBase.ConfigureAggregateCatalog()
        ' Add this assembly to the catalog.
        Me.AggregateCatalog.Catalogs.Add(New AssemblyCatalog(GetType(Bootstrapper).Assembly))
    End Sub

    Protected Overrides Sub ConfigureContainer()
        MyBase.ConfigureContainer()
    End Sub

    Protected Overrides Function CreateShell() As DependencyObject
        Dim shell As MainView = Me.Container.GetExportedValue(Of MainView)()
        Return shell
    End Function

    Protected Overrides Sub InitializeShell()
        MyBase.InitializeShell()
        Application.Current.MainWindow = Shell
        Application.Current.MainWindow.Show()
    End Sub

End Class
