Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Windows.Forms

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace HowToCustomizeTemplate.Win
    Friend NotInheritable Class Program
        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        Private Sub New()
        End Sub
        <STAThread> _
        Shared Sub Main()
#If EASYTEST Then
            DevExpress.ExpressApp.EasyTest.WinAdapter.RemotingRegistration.Register(4100)
#End If

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
            Dim winApplication As New HowToCustomizeTemplateWindowsFormsApplication()
            AddHandler winApplication.CreateCustomTemplate, AddressOf application_CreateCustomTemplate

            If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            End If
#If EASYTEST Then
            If ConfigurationManager.ConnectionStrings("EasyTestConnectionString") IsNot Nothing Then
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("EasyTestConnectionString").ConnectionString
            End If
#End If

            Try
                winApplication.Setup()
                winApplication.Start()
            Catch e As Exception
                winApplication.HandleException(e)
            End Try
        End Sub
        Private Shared Sub application_CreateCustomTemplate(ByVal sender As Object, ByVal e As CreateCustomTemplateEventArgs)
            If e.Context.Name = TemplateContext.ApplicationWindow.Name Then
                e.Template = New DevExpress.ExpressApp.Win.CustomTemplates.MainForm()
            End If
        End Sub
    End Class
End Namespace