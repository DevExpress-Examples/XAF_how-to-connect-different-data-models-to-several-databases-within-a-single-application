Namespace TwoXpoModelsForDifferentDatabases.Win

    Partial Class TwoXpoModelsForDifferentDatabasesWindowsFormsApplication

        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (Me.components IsNot Nothing) Then
                Me.components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"
        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
            Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
            Me.xafModule11 = New ClassLibrary1.XafModule1()
            Me.xafModule21 = New ClassLibrary2.XafModule2()
            Me.commonModule = New CommonModule.[Module]()
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
            Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
            Me.securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
            Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
            CType((Me), System.ComponentModel.ISupportInitialize).BeginInit()
            ' 
            ' securityStrategyComplex1
            ' 
            Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
            Me.securityStrategyComplex1.RoleType = GetType(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole)
            ' ApplicationUser descends from PermissionPolicyUser and supports OAuth authentication. For more information, refer to the following help topic: https://docs.devexpress.com/eXpressAppFramework/402197
            ' If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
            Me.securityStrategyComplex1.UserType = GetType(CommonModule.BusinessObjects.ApplicationUser)
            ' 
            ' securityModule1
            ' 
            Me.securityModule1.UserType = GetType(CommonModule.BusinessObjects.ApplicationUser)
            ' 
            ' authenticationStandard1
            ' 
            Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
            ' ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
            ' Comment out the following line if using PermissionPolicyUser or a custom user type.
            Me.authenticationStandard1.UserLoginInfoType = GetType(CommonModule.BusinessObjects.ApplicationUserLoginInfo)
            ' 
            ' TwoXpoModelsForDifferentDatabasesWindowsFormsApplication
            ' 
            Me.ApplicationName = "TwoXpoModelsForDifferentDatabases"
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.xafModule11)
            Me.Modules.Add(Me.xafModule21)
            Me.Modules.Add(Me.commonModule)
            Me.Modules.Add(Me.securityModule1)
            Me.Security = Me.securityStrategyComplex1
             ''' Cannot convert AssignmentExpressionSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
'''             this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.TwoXpoModelsForDifferentDatabasesWindowsFormsApplication_DatabaseVersionMismatch)
'''   ''' Cannot convert AssignmentExpressionSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
'''             this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.TwoXpoModelsForDifferentDatabasesWindowsFormsApplication_CustomizeLanguagesList)
'''  CType((Me), System.ComponentModel.ISupportInitialize).EndInit()
        End Sub

#End Region
        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule

        Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule

        Private xafModule11 As ClassLibrary1.XafModule1

        Private xafModule21 As ClassLibrary2.XafModule2

        Private commonModule As CommonModule.[Module]

        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule

        Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex

        Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
    End Class
End Namespace
