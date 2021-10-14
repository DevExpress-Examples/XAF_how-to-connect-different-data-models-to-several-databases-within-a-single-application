Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base

Namespace ClassLibrary1

    <DefaultClassOptions>
    Public Class PersistentClass1
        Inherits XPObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Private _PersistentProperty1A As String

        Public Property PersistentProperty1A As String
            Get
                Return _PersistentProperty1A
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(PersistentClass1.PersistentProperty1A), _PersistentProperty1A, value)
            End Set
        End Property

        Private _PersistentProperty1B As String

        Public Property PersistentProperty1B As String
            Get
                Return _PersistentProperty1B
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(PersistentClass1.PersistentProperty1B), _PersistentProperty1B, value)
            End Set
        End Property
    End Class
End Namespace
