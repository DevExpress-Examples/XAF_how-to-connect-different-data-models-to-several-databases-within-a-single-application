Imports System
Imports System.Linq
Imports DevExpress.Xpo
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base

Namespace ClassLibrary1
	<DefaultClassOptions>
	Public Class PersistentClass1
		Inherits XPObject

		Public Sub New(ByVal s As Session)
			MyBase.New(s)
		End Sub
		Public Property PersistentProperty1() As String
	End Class
End Namespace
