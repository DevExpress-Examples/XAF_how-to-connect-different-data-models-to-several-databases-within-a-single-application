Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.DC.Xpo
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
			AddHandler application.CreateCustomObjectSpaceProvider, AddressOf application_CreateCustomObjectSpaceProvider
		End Sub
		Private Sub application_CreateCustomObjectSpaceProvider(ByVal sender As Object, ByVal e As CreateCustomObjectSpaceProviderEventArgs)
			Dim application_Renamed As XafApplication = DirectCast(sender, XafApplication)
			If typeInfoSource1 Is Nothing Then
				SyncLock lockObj
					If typeInfoSource1 Is Nothing Then
						typeInfoSource1 = New XpoTypeInfoSource(DirectCast(application_Renamed.TypesInfo, TypesInfo), GetType(PersistentClass1), GetType(ModuleInfo1))
					End If
				End SyncLock
			End If
			Dim objectSpaceProvider1 As XPObjectSpaceProvider = New XPObjectSpaceProvider(New ConnectionStringDataStoreProvider(ConfigurationManager.ConnectionStrings("ConnectionStringDatabase1").ConnectionString), application_Renamed.TypesInfo, typeInfoSource1, True)
			e.ObjectSpaceProviders.Add(objectSpaceProvider1)
			e.IsObjectSpaceProviderOwner = False
		End Sub
		Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
			Dim updater As New XafModuleUpdater1(objectSpace, versionFromDB)
			Return New ModuleUpdater() { updater }
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
			If ObjectSpace.Database.Contains("1") Then
			'if (ObjectSpace.CanInstantiate(typeof(PersistentClass1))) {
				Dim str As String = "test1"
				Dim theObject As PersistentClass1 = ObjectSpace.FindObject(Of PersistentClass1)(CriteriaOperator.Parse("PersistentProperty1 = ?", str))
				If theObject Is Nothing Then
					theObject = ObjectSpace.CreateObject(Of PersistentClass1)()
					theObject.PersistentProperty1 = str
				End If
			End If
		End Sub
	End Class
End Namespace