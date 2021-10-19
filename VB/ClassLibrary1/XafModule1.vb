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

Namespace ClassLibrary1

    Public Class XafModule1
        Inherits ModuleBase

        Private Shared ReadOnly lockObj As Object = New Object()

        ' Here we will have a single instance, which is initialized only once during the application life cycle.
        Private Shared typeInfoSource1 As XpoTypeInfoSource = Nothing

        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
            application.CreateCustomObjectSpaceProvider += AddressOf application_CreateCustomObjectSpaceProvider
        End Sub

        Private Sub application_CreateCustomObjectSpaceProvider(ByVal sender As Object, ByVal e As CreateCustomObjectSpaceProviderEventArgs)
            Dim application As XafApplication = CType(sender, XafApplication)
            If typeInfoSource1 Is Nothing Then
                SyncLock lockObj
                    If typeInfoSource1 Is Nothing Then
                        typeInfoSource1 = New XpoTypeInfoSource(CType(application.TypesInfo, TypesInfo), GetType(PersistentClass1), GetType(ModuleInfo1))
                    End If

                End SyncLock
            End If

            'XPObjectSpaceProvider objectSpaceProvider1 = new XPObjectSpaceProvider(
            Dim objectSpaceProvider1 As IObjectSpaceProvider = New SecuredObjectSpaceProvider(CType(application.Security, SecurityStrategyComplex), New ConnectionStringDataStoreProvider(ConfigurationManager.ConnectionStrings("ConnectionStringDatabase1").ConnectionString), application.TypesInfo, typeInfoSource1, True)
            objectSpaceProvider1.CheckCompatibilityType = CheckCompatibilityType.ModuleInfo
            e.ObjectSpaceProviders.Add(objectSpaceProvider1)
        End Sub

        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Dim updater As XafModuleUpdater1 = New XafModuleUpdater1(objectSpace, versionFromDB)
            Return New ModuleUpdater() {updater}
        End Function
    End Class

    Public Class XafModuleUpdater1
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            ' Check whether it is a valid ObjectSpace to create objects of a certain type.
            If ObjectSpace.CanInstantiate(GetType(PersistentClass1)) Then
                Dim str As String = "test1"
                Dim theObject As PersistentClass1 = ObjectSpace.FindObject(Of PersistentClass1)(CriteriaOperator.Parse("PersistentProperty1A = ?", str))
                If theObject Is Nothing Then
                    theObject = ObjectSpace.CreateObject(Of PersistentClass1)()
                    theObject.PersistentProperty1A = str
                    theObject.PersistentProperty1B = str
                End If
            End If
        End Sub
    End Class
End Namespace
