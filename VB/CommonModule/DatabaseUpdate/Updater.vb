Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports CommonModule.BusinessObjects

Namespace CommonModule.DatabaseUpdate

    ' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            ' Check whether it is a valid ObjectSpace to create objects of a certain type.
            If ObjectSpace.CanInstantiate(GetType(ApplicationUser)) Then
                CreateSecurityObjects()
            End If

            ObjectSpace.CommitChanges() 'This line persists created object(s).
        End Sub

        Private Sub CreateSecurityObjects()
            Dim sampleUser As ApplicationUser = ObjectSpace.FirstOrDefault(Of ApplicationUser)(Function(u) u.UserName Is "User")
            If sampleUser Is Nothing Then
                sampleUser = ObjectSpace.CreateObject(Of ApplicationUser)()
                sampleUser.UserName = "User"
                ' Set a password if the standard authentication type is used
                sampleUser.SetPassword("")
                ' The UserLoginInfo object requires a user object Id (Oid).
                ' Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                ObjectSpace.CommitChanges() 'This line persists created object(s).
                CType(sampleUser, ISecurityUserWithLoginInfo).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser))
            End If

            Dim defaultRole As PermissionPolicyRole = CreateDefaultRole()
            sampleUser.Roles.Add(defaultRole)
            Dim userAdmin As ApplicationUser = ObjectSpace.FirstOrDefault(Of ApplicationUser)(Function(u) u.UserName Is "Admin")
            If userAdmin Is Nothing Then
                userAdmin = ObjectSpace.CreateObject(Of ApplicationUser)()
                userAdmin.UserName = "Admin"
                ' Set a password if the standard authentication type is used
                userAdmin.SetPassword("")
                ' The UserLoginInfo object requires a user object Id (Oid).
                ' Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                ObjectSpace.CommitChanges() 'This line persists created object(s).
                CType(userAdmin, ISecurityUserWithLoginInfo).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin))
            End If

            ' If a role with the Administrators name doesn't exist in the database, create this role
            Dim adminRole As PermissionPolicyRole = ObjectSpace.FirstOrDefault(Of PermissionPolicyRole)(Function(r) r.Name Is "Administrators")
            If adminRole Is Nothing Then
                adminRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
                adminRole.Name = "Administrators"
            End If

            adminRole.IsAdministrative = True
            userAdmin.Roles.Add(adminRole)
        End Sub

        Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
            MyBase.UpdateDatabaseBeforeUpdateSchema()
        'if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        '    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        '}
        End Sub

        Private Function CreateDefaultRole() As PermissionPolicyRole
            Dim defaultRole As PermissionPolicyRole = ObjectSpace.FirstOrDefault(Of PermissionPolicyRole)(Function(role) role.Name Is "Default")
            If defaultRole Is Nothing Then
                defaultRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
                defaultRole.Name = "Default"
                defaultRole.AddObjectPermissionFromLambda(Of ApplicationUser)(SecurityOperations.Read, Function(cm) cm.Oid = CType(CurrentUserIdOperator.CurrentUserId(), Guid), SecurityPermissionState.Allow)
                defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow)
                defaultRole.AddMemberPermissionFromLambda(Of ApplicationUser)(SecurityOperations.Write, "ChangePasswordOnFirstLogon", Function(cm) cm.Oid = CType(CurrentUserIdOperator.CurrentUserId(), Guid), SecurityPermissionState.Allow)
                defaultRole.AddMemberPermissionFromLambda(Of ApplicationUser)(SecurityOperations.Write, "StoredPassword", Function(cm) cm.Oid = CType(CurrentUserIdOperator.CurrentUserId(), Guid), SecurityPermissionState.Allow)
                defaultRole.AddTypePermissionsRecursively(Of PermissionPolicyRole)(SecurityOperations.Read, SecurityPermissionState.Deny)
                defaultRole.AddTypePermissionsRecursively(Of ModelDifference)(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow)
                defaultRole.AddTypePermissionsRecursively(Of ModelDifferenceAspect)(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow)
                defaultRole.AddTypePermissionsRecursively(Of ModelDifference)(SecurityOperations.Create, SecurityPermissionState.Allow)
                defaultRole.AddTypePermissionsRecursively(Of ModelDifferenceAspect)(SecurityOperations.Create, SecurityPermissionState.Allow)
                defaultRole.AddTypePermission(ObjectSpace.TypesInfo.FindTypeInfo("ClassLibrary1.PersistentClass1").Type, "Create;Read;Write", SecurityPermissionState.Allow)
                defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/PersistentClass1_ListView", SecurityPermissionState.Allow)
                defaultRole.AddTypePermission(ObjectSpace.TypesInfo.FindTypeInfo("ClassLibrary2.PersistentClass2").Type, SecurityOperations.Read, SecurityPermissionState.Allow)
                defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/PersistentClass2_ListView", SecurityPermissionState.Allow)
            End If

            Return defaultRole
        End Function
    End Class
End Namespace
