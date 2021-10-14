Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base

Namespace ClassLibrary2

    <DefaultClassOptions>
    Public Class PersistentClass2
        Inherits XPObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Private _PersistentPropertyX As String

        Public Property PersistentPropertyX As String
            Get
                Return _PersistentPropertyX
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(PersistentClass2.PersistentPropertyX), _PersistentPropertyX, value)
            End Set
        End Property
    End Class
End Namespace
