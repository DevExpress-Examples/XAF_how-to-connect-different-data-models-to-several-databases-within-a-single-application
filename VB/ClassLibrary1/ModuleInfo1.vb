Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.DC.Xpo
Imports DevExpress.ExpressApp.Updating

Namespace ClassLibrary1

    <MemberDesignTimeVisibility(False)>
    Public Class ModuleInfo1
        Inherits XPBaseObject
        Implements IModuleInfo

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Key(True)>
        Public Property ID As Integer

        Public Property Version As String Implements IModuleInfo.Version

        Public Property Name As String Implements IModuleInfo.Name

        Public Property AssemblyFileName As String Implements IModuleInfo.AssemblyFileName

        Public Property IsMain As Boolean Implements IModuleInfo.IsMain

        Public Overrides Function ToString() As String
            Return If(Not String.IsNullOrEmpty(Name), Name, MyBase.ToString())
        End Function
    End Class
End Namespace
