Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports CommonModule.BusinessObjects
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.DC.Xpo
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.Adapters
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Xpo

Namespace CommonModule

    Public Class [Module]
        Inherits ModuleBase

        Public Sub New()
            Me.AdditionalExportedTypes.Add(GetType(DevExpress.Persistent.BaseImpl.ModelDifference))
            Me.AdditionalExportedTypes.Add(GetType(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect))
            Me.AdditionalExportedTypes.Add(GetType(ApplicationUser))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.SystemModule.SystemModule))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Security.SecurityModule))
        End Sub

        Private Shared ReadOnly lockObj As Object = New Object()

        ' Here we will have a single instance, which is initialized only once during the application life cycle.
        Private Shared typeInfoSource As XpoTypeInfoSource = Nothing

        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
            application.CreateCustomObjectSpaceProvider += AddressOf application_CreateCustomObjectSpaceProvider
            TryCast(application.Security, SecurityStrategy)?.RegisterXPOAdapterProviders(New SecurityPermissionsProviderDefault(application))
            application.ObjectSpaceCreated += AddressOf Application_ObjectSpaceCreated
        End Sub

        Private Sub Application_ObjectSpaceCreated(ByVal sender As Object, ByVal e As ObjectSpaceCreatedEventArgs)
            TryCast(e.ObjectSpace, CompositeObjectSpace)?.PopulateAdditionalObjectSpaces(CType(sender, XafApplication))
        End Sub

        Private Sub application_CreateCustomObjectSpaceProvider(ByVal sender As Object, ByVal e As CreateCustomObjectSpaceProviderEventArgs)
            Dim application As XafApplication = CType(sender, XafApplication)
            If typeInfoSource Is Nothing Then
                SyncLock lockObj
                    If typeInfoSource Is Nothing Then
                        typeInfoSource = New XpoTypeInfoSource(CType(application.TypesInfo, TypesInfo), GetType(DevExpress.Persistent.BaseImpl.ModelDifference), GetType(DevExpress.Persistent.BaseImpl.ModelDifferenceAspect), GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyUser), GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole), GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyTypePermissionObject), GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyObjectPermissionsObject), GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyMemberPermissionsObject), GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyNavigationPermissionObject), GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyActionPermissionObject), GetType(ApplicationUser))
                    End If

                End SyncLock
            End If

            'IObjectSpaceProvider objectSpaceProvider = new XPObjectSpaceProvider(
            Dim objectSpaceProvider As IObjectSpaceProvider = New SecuredObjectSpaceProvider(CType(application.Security, SecurityStrategyComplex), New ConnectionStringDataStoreProvider(ConfigurationManager.ConnectionStrings("ConnectionStringDatabaseCommon").ConnectionString), application.TypesInfo, typeInfoSource, True)
            objectSpaceProvider.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema
            e.ObjectSpaceProviders.Add(objectSpaceProvider)
            e.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(application.TypesInfo, Nothing))
        End Sub

        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Dim updater As ModuleUpdater = New DatabaseUpdate.Updater(objectSpace, versionFromDB)
            Return New ModuleUpdater() {updater}
        End Function
    End Class
End Namespace
