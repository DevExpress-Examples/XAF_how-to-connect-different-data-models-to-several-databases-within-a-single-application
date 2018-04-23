Imports System
Imports System.Linq
Imports DevExpress.Xpo
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base

Namespace ClassLibrary2
	<DefaultClassOptions>
	Public Class PersistentClass2
		Inherits XPObject

		Public Sub New(ByVal s As Session)
			MyBase.New(s)
		End Sub
		Public Property PersistentProperty2() As String
	End Class
End Namespace
