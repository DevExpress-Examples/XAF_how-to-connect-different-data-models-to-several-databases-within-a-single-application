Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.DC.Xpo
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Xpo

Namespace ClassLibrary2

    Public Class XafModule2
        Inherits ModuleBase

        Private Shared ReadOnly lockObj As Object = New Object()

        ' Here we will have a single instance, which is initialized only once during the application life cycle.
        Private Shared typeInfoSource2 As XpoTypeInfoSource = Nothing

        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
            application.CreateCustomObjectSpaceProvider += AddressOf application_CreateCustomObjectSpaceProvider
        End Sub

        Private Sub application_CreateCustomObjectSpaceProvider(ByVal sender As Object, ByVal e As CreateCustomObjectSpaceProviderEventArgs)
            Dim application As XafApplication = CType(sender, XafApplication)
            If typeInfoSource2 Is Nothing Then
                SyncLock lockObj
                    If typeInfoSource2 Is Nothing Then
                        typeInfoSource2 = New XpoTypeInfoSource(CType(application.TypesInfo, TypesInfo), GetType(PersistentClass2))
                    End If

                End SyncLock
            End If

            'XPObjectSpaceProvider objectSpaceProvider2 = new XPObjectSpaceProvider(
            Dim objectSpaceProvider2 As IObjectSpaceProvider = New SecuredObjectSpaceProvider(CType(application.Security, SecurityStrategyComplex), New ConnectionStringDataStoreProvider(ConfigurationManager.ConnectionStrings("ConnectionStringDatabase2").ConnectionString), application.TypesInfo, typeInfoSource2, True)
            objectSpaceProvider2.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema
            e.ObjectSpaceProviders.Add(objectSpaceProvider2)
        End Sub

        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Dim updater As XafModuleUpdater2 = New XafModuleUpdater2(objectSpace, versionFromDB)
            Return New ModuleUpdater() {updater}
        End Function
    End Class

    Public Class XafModuleUpdater2
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            ' Check whether it is a valid ObjectSpace to create objects of a certain type.
            If ObjectSpace.CanInstantiate(GetType(PersistentClass2)) Then
                Dim str As String = "test2"
                Dim theObject As PersistentClass2 = ObjectSpace.FindObject(Of PersistentClass2)(CriteriaOperator.Parse("PersistentPropertyX = ?", str))
                If theObject Is Nothing Then
                    theObject = ObjectSpace.CreateObject(Of PersistentClass2)()
                    theObject.PersistentPropertyX = str
                End If
            End If
        End Sub
    End Class
End Namespace
