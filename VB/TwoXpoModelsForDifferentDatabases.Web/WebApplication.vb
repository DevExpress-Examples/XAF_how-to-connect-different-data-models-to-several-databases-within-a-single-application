Imports System
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Web

'using DevExpress.ExpressApp.Security;
Namespace TwoXpoModelsForDifferentDatabases.Web

    ' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/DevExpressExpressAppWebWebApplicationMembersTopicAll
    Public Partial Class TwoXpoModelsForDifferentDatabasesAspNetApplication
        Inherits WebApplication

        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule

        Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule

        Private xafModule11 As ClassLibrary1.XafModule1

        Private xafModule21 As ClassLibrary2.XafModule2

        Private commonModule As CommonModule.Module

        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule

        Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex

        Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub TwoXpoModelsForDifferentDatabasesAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)
#If EASYTEST
            e.Updater.Update();
            e.Handled = true;
#Else
            If System.Diagnostics.Debugger.IsAttached Then
                e.Updater.Update()
                e.Handled = True
            Else
                Dim message As String = "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & Microsoft.VisualBasic.Constants.vbCrLf & "This error occurred  because the automatic database update was disabled when the application was started without debugging." & Microsoft.VisualBasic.Constants.vbCrLf & "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " & "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & "or manually create a database using the 'DBUpdater' tool." & Microsoft.VisualBasic.Constants.vbCrLf & "Anyway, refer to the following help topics for more detailed information:" & Microsoft.VisualBasic.Constants.vbCrLf & "'Update Application and Database Versions' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm" & Microsoft.VisualBasic.Constants.vbCrLf & "'Database Security References' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument3237.htm" & Microsoft.VisualBasic.Constants.vbCrLf & "If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/"
                If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
                    message += Microsoft.VisualBasic.Constants.vbCrLf & Microsoft.VisualBasic.Constants.vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
                End If

                Throw New InvalidOperationException(message)
            End If
#End If
        End Sub

        Private Sub InitializeComponent()
            module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
            xafModule11 = New ClassLibrary1.XafModule1()
            xafModule21 = New ClassLibrary2.XafModule2()
            commonModule = New CommonModule.Module()
            securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
            securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
            authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' securityStrategyComplex1
            ' 
            securityStrategyComplex1.Authentication = authenticationStandard1
            securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
            ' ApplicationUser descends from PermissionPolicyUser and supports OAuth authentication. For more information, refer to the following help topic: https://docs.devexpress.com/eXpressAppFramework/402197
            ' If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
            securityStrategyComplex1.UserType = GetType(CommonModule.BusinessObjects.ApplicationUser)
            ' 
            ' securityModule1
            ' 
            securityModule1.UserType = GetType(CommonModule.BusinessObjects.ApplicationUser)
            ' 
            ' authenticationStandard1
            ' 
            authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
            ' ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
            ' Comment out the following line if using PermissionPolicyUser or a custom user type.
            authenticationStandard1.UserLoginInfoType = GetType(CommonModule.BusinessObjects.ApplicationUserLoginInfo)
            ' 
            ' TwoXpoModelsForDifferentDatabasesAspNetApplication
            ' 
            Me.ApplicationName = "TwoXpoModelsForDifferentDatabases"
            Me.Modules.Add(module1)
            Me.Modules.Add(module2)
            Me.Modules.Add(xafModule11)
            Me.Modules.Add(xafModule21)
            Me.Modules.Add(commonModule)
            Me.Modules.Add(securityModule1)
            Me.Security = securityStrategyComplex1
             ''' Cannot convert AssignmentExpressionSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
'''             this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.TwoXpoModelsForDifferentDatabasesAspNetApplication_DatabaseVersionMismatch)
'''  CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        End Sub
    End Class
End Namespace
