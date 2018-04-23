Namespace TwoXpoModelsForDifferentDatabases.Module
	Partial Public Class TwoXpoModelsForDifferentDatabasesModule
		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			' 
			' TwoXpoModelsForDifferentDatabasesModule
			' 
			Me.AdditionalExportedTypes.Add(GetType(DevExpress.Xpo.XPObject))
			Me.AdditionalExportedTypes.Add(GetType(DevExpress.Xpo.XPCustomObject))
			Me.AdditionalExportedTypes.Add(GetType(DevExpress.Xpo.XPBaseObject))
			Me.AdditionalExportedTypes.Add(GetType(DevExpress.Xpo.PersistentBase))
			Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.SystemModule.SystemModule))
			Me.RequiredModuleTypes.Add(GetType(ClassLibrary1.XafModule1))
			Me.RequiredModuleTypes.Add(GetType(ClassLibrary2.XafModule2))

		End Sub

		#End Region
	End Class
End Namespace
