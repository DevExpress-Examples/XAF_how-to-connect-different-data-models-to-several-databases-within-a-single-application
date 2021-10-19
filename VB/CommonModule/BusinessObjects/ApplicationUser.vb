Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.Xpo

Namespace CommonModule.BusinessObjects

    <MapInheritance(MapInheritanceType.ParentTable)>
    <System.ComponentModel.DisplayName("User"), ComponentModel.DefaultPropertyAttribute(NameOf(PermissionPolicyUser.UserName))>
    Public Class ApplicationUser
        Inherits PermissionPolicyUser
        Implements IObjectSpaceLink, ISecurityUserWithLoginInfo

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Browsable(False)>
        <Aggregated, Association("User-LoginInfo")>
        Public ReadOnly Property LoginInfo As XPCollection(Of ApplicationUserLoginInfo)
            Get
                Return GetCollection(Of ApplicationUserLoginInfo)(NameOf(ApplicationUser.LoginInfo))
            End Get
        End Property

        Private ReadOnly Property UserLogins As IEnumerable(Of ISecurityUserLoginInfo) Implements ISecurityUserWithLoginInfo.UserLogins
            Get
                Return LoginInfo.OfType(Of ISecurityUserLoginInfo)()
            End Get
        End Property

        Private Property ObjectSpace As IObjectSpace Implements IObjectSpaceLink.ObjectSpace

        Private Function CreateUserLoginInfo(ByVal loginProviderName As String, ByVal providerUserKey As String) As ISecurityUserLoginInfo Implements ISecurityUserWithLoginInfo.CreateUserLoginInfo
            Dim result As ApplicationUserLoginInfo = CType(Me, IObjectSpaceLink).ObjectSpace.CreateObject(Of ApplicationUserLoginInfo)()
            result.LoginProviderName = loginProviderName
            result.ProviderUserKey = providerUserKey
            result.MyUser = Me
            Return result
        End Function
    End Class
End Namespace
