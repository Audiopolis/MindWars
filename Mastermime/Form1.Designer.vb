<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSpill
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.bgworker = New System.ComponentModel.BackgroundWorker()
        Me.BWTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LysTimer = New System.Windows.Forms.Timer(Me.components)
        Me.GrafikkTimer = New System.Windows.Forms.Timer(Me.components)
        Me.InvalidateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.VerifiserValgTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(39, 28)
        Me.Button2.TabIndex = 187
        Me.Button2.Text = "Gjett"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'bgworker
        '
        Me.bgworker.WorkerReportsProgress = True
        Me.bgworker.WorkerSupportsCancellation = True
        '
        'BWTimer
        '
        Me.BWTimer.Interval = 250
        '
        'LysTimer
        '
        Me.LysTimer.Interval = 40
        '
        'GrafikkTimer
        '
        Me.GrafikkTimer.Enabled = True
        Me.GrafikkTimer.Interval = 90
        '
        'InvalidateTimer
        '
        Me.InvalidateTimer.Interval = 200
        '
        'VerifiserValgTimer
        '
        Me.VerifiserValgTimer.Interval = 50
        '
        'frmSpill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(225, 493)
        Me.Controls.Add(Me.Button2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmSpill"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Spill mot PC"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button2 As Button
    Public WithEvents bgworker As System.ComponentModel.BackgroundWorker
    Public WithEvents BWTimer As Timer
    Public WithEvents LysTimer As Timer
    Friend WithEvents GrafikkTimer As Timer
    Friend WithEvents InvalidateTimer As Timer
    Friend WithEvents VerifiserValgTimer As Timer
End Class
