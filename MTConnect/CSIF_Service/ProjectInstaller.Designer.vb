<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SPI_Csif = New System.ServiceProcess.ServiceProcessInstaller()
        Me.SI_Csif = New System.ServiceProcess.ServiceInstaller()
        '
        'SPI_Csif
        '
        Me.SPI_Csif.Account = System.ServiceProcess.ServiceAccount.NetworkService
        Me.SPI_Csif.Password = Nothing
        Me.SPI_Csif.Username = Nothing
        '
        'SI_Csif
        '
        Me.SI_Csif.DelayedAutoStart = True
        Me.SI_Csif.Description = "CSIFlexServerService"
        Me.SI_Csif.DisplayName = "CSIFlexServerService"
        Me.SI_Csif.ServiceName = "CSIFlexServerService"
        Me.SI_Csif.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        Me.SI_Csif.DelayedAutoStart = True
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.SPI_Csif, Me.SI_Csif})

    End Sub
    Friend WithEvents SPI_Csif As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents SI_Csif As System.ServiceProcess.ServiceInstaller

End Class
