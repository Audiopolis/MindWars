Module SystemModule
    Public rn As New Random
    Public antallforsøk As Integer = 9
    Public antallfarger As Integer = 6
    Public antallhull As Integer = 4
    Public ValgtPeg As Integer = 1
    Public ValgtSeriePeg As Integer
    Public MenneskeSinTur As Boolean = True
    Public VentMedÅFylleInn As Boolean = False

    Public Sub ByttSide()
        If MenneskeSinTur = True Then
            frmSpill.GrafikkTimer.Enabled = False
            MenneskeSinTur = False
            Call AISpiller()
            Call RegenererBrett()
        Else
            MenneskeSinTur = True
            Call MenneskeSpiller()
            Call RegenererBrett()
            frmSpill.GrafikkTimer.Enabled = True
        End If
    End Sub

End Module
