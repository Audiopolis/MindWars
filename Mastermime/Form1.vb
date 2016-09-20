Imports System.ComponentModel
Public Class frmSpill
    Dim R0 As Integer = standardfarger(0).R
    Dim G0 As Integer = standardfarger(0).G
    Dim B0 As Integer = standardfarger(0).B
    Dim LysAlpha As Integer = 0

    Private Sub frmSpillMotPC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CausesValidation = False
        FormLastet = True
        Me.Visible = False
        Me.KeyPreview = True
        Call GenererBrett()
        Call MenneskeSpiller()
        Me.Visible = True
    End Sub

    Private Sub peg6_Click(sender As Object, e As EventArgs)
        Call FyllInnFarge(6)
    End Sub
    Private Sub peg5_Click(sender As Object, e As EventArgs)
        Call FyllInnFarge(5)
    End Sub
    Private Sub peg4_Click(sender As Object, e As EventArgs)
        Call FyllInnFarge(4)
    End Sub
    Private Sub peg3_Click(sender As Object, e As EventArgs)
        Call FyllInnFarge(3)
    End Sub
    Private Sub peg2_Click(sender As Object, e As EventArgs)
        Call FyllInnFarge(2)
    End Sub
    Private Sub peg1_Click(sender As Object, e As EventArgs)
        Call FyllInnFarge(1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call Gjett()
    End Sub

    Private Sub bgworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgworker.DoWork
        Dim MinimaxFørsteKvartal As New MinimaxClass
        Dim MinimaxAndreKvartal As New MinimaxClass
        Dim MinimaxTredjeKvartal As New MinimaxClass
        Dim MinimaxFjerdeKvartal As New MinimaxClass
        Dim FKTråd As New System.Threading.Thread(AddressOf MinimaxFørsteKvartal.Minimax)
        Dim AKTråd As New System.Threading.Thread(AddressOf MinimaxAndreKvartal.Minimax)
        Dim TKTråd As New System.Threading.Thread(AddressOf MinimaxTredjeKvartal.Minimax)
        Dim FjKTråd As New System.Threading.Thread(AddressOf MinimaxFjerdeKvartal.Minimax)
        MinimaxFørsteKvartal.Kvartal = 1
        MinimaxAndreKvartal.Kvartal = 2
        MinimaxTredjeKvartal.Kvartal = 3
        MinimaxFjerdeKvartal.Kvartal = 4
        FKTråd.Start()
        AKTråd.Start()
        TKTråd.Start()
        FjKTråd.Start()
        FKTråd.Join()
        AKTråd.Join()
        TKTråd.Join()
        FjKTråd.Join()
        Dim i As Integer = 0
        Dim bestescore As Integer = 0
        Dim besteindex As Integer = 0
        Do Until i <= 4
            If FireBesteScore(i) > bestescore Then
                bestescore = FireBesteScore(i)
                besteindex = FireBeste(i)
            End If
            i += 1
        Loop

        NyesteForsøk = StringTilArray(OrigS.Item(besteindex))


    End Sub

    Private Sub bgworker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgworker.RunWorkerCompleted
        Button2.Enabled = True
        Call UtførForsøk()
    End Sub

    Private Sub ValgPeg1_Click(sender As Object, e As EventArgs)
        Call FyllInnFargeHull(1)
    End Sub

    Private Sub ValgPeg2_Click(sender As Object, e As EventArgs)
        Call FyllInnFargeHull(2)
    End Sub

    Private Sub ValgPeg3_Click(sender As Object, e As EventArgs)
        Call FyllInnFargeHull(3)
    End Sub

    Private Sub ValgPeg4_Click(sender As Object, e As EventArgs)
        Call FyllInnFargeHull(4)
    End Sub

    Private Sub ValgPeg5_Click(sender As Object, e As EventArgs)
        Call FyllInnFargeHull(5)
    End Sub

    Private Sub ValgPeg6_Click(sender As Object, e As EventArgs)
        Call FyllInnFargeHull(6)
    End Sub

    Private Sub ValgPeg7_Click(sender As Object, e As EventArgs)
        Call FyllInnFargeHull(7)
    End Sub

    Private Sub ValgPeg8_Click(sender As Object, e As EventArgs)
        Call FyllInnFargeHull(8)
    End Sub

    Private Sub peg1_DoubleClick(sender As Object, e As EventArgs)
        Call FyllInnFarge(1)
    End Sub

    Private Sub ValgPeg2_DoubleClick(sender As Object, e As EventArgs)
        Call FyllInnFarge(2)
    End Sub

    Private Sub ValgPeg3_DoubleClick(sender As Object, e As EventArgs)
        Call FyllInnFarge(3)
    End Sub

    Private Sub ValgPeg4_DoubleClick(sender As Object, e As EventArgs)
        Call FyllInnFarge(4)
    End Sub

    Private Sub ValgPeg5_DoubleClick(sender As Object, e As EventArgs)
        Call FyllInnFarge(5)
    End Sub

    Private Sub ValgPeg6_DoubleClick(sender As Object, e As EventArgs)
        Call FyllInnFarge(6)
    End Sub

    Private Sub ValgPeg7_DoubleClick(sender As Object, e As EventArgs)
        Call FyllInnFarge(7)
    End Sub

    Private Sub ValgPeg8_DoubleClick(sender As Object, e As EventArgs)
        Call FyllInnFarge(8)
    End Sub






    Public Sub frmSpill_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        'e.Graphics.DrawImage(test, 100, 100)
        If BrukerHarValgtKode = True Then
            For Each PegGrafikk As PegClass In testcollection
                PegGrafikk.DrawACircle()
            Next
            If MenneskeSinTur = True Then
                'If FokusertValgPegObjekt.Sirkulerer = True Then
                'Call FokusertValgPegObjekt.Sirkuler(vinkel)
                'End If
                For Each ValgPegItem As ValgPegClass In ValgPegCollection
                    ValgPegItem.DrawACircle()
                Next
                For Each TilOversValgPeg As ValgPegClass In DeaktiverteValgPegs
                    TilOversValgPeg.DrawACircle()
                Next
            End If
            For Each BWitem As BWPegClass In BWcollection
                BWitem.DrawACircle()
            Next
        Else
            For Each VS As ValgPegClass In VelgSerieCollection
                VS.DrawACircle()
            Next
            For Each SH As PegClass In SerieHullCollection
                SH.DrawACircle()
            Next
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keydata As Keys) As Boolean
        If keydata = Keys.Right Or keydata = Keys.Left Or keydata = Keys.Up Or keydata = Keys.Down Or keydata = Keys.Enter Or keydata = Keys.Space Or Keys.Back Then
            OnKeyDown(New KeyEventArgs(keydata))
            ProcessCmdKey = True
        Else
            ProcessCmdKey = MyBase.ProcessCmdKey(msg, keydata)
        End If
    End Function

    Private Sub frmSpill_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim bHandled As Boolean = False
        Dim OppdaterValgPeg As ValgPegClass
        ' sjekk om du kan bruke antallfarger i fremtiden
        Dim VCount As Integer = ValgPegCollection.Count

        Select Case e.KeyCode
            Case Keys.Right
                If BrukerHarValgtKode = True AndAlso MenneskeSinTur = True Then
                    If testposisjon = False AndAlso Not ValgtPeg = VCount Then
                        If Not ValgtPeg = 4 Then
                            OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                            OppdaterValgPeg.Valgt = False
                            ValgtPeg += 1
                            OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                            OppdaterValgPeg.Valgt = True
                            e.Handled = True
                        End If
                    ElseIf testposisjon = True AndAlso Not ValgtPeg = VCount Then
                        OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                        OppdaterValgPeg.Valgt = False
                        ValgtPeg += 1
                        OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                        OppdaterValgPeg.Valgt = True
                        e.Handled = True
                    End If
                ElseIf BrukerHarValgtKode = False AndAlso Not ValgtSeriePeg = antallfarger Then
                    OppdaterValgPeg = VelgSerieCollection.Item(ValgtSeriePeg)
                    OppdaterValgPeg.Valgt = False
                    ValgtSeriePeg += 1
                    OppdaterValgPeg = VelgSerieCollection.Item(ValgtSeriePeg)
                    OppdaterValgPeg.Valgt = True
                    e.Handled = True
                End If

            Case Keys.Left
                If BrukerHarValgtKode = True AndAlso MenneskeSinTur = True Then
                    If Not ValgtPeg = 1 AndAlso testposisjon = False Then
                        If Not ValgtPeg = 5 Then
                            OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                            OppdaterValgPeg.Valgt = False
                            ValgtPeg -= 1
                            OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                            OppdaterValgPeg.Valgt = True
                            e.Handled = True
                        End If
                    ElseIf Not ValgtPeg = 1 Then
                        OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                        OppdaterValgPeg.Valgt = False
                        ValgtPeg -= 1
                        OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                        OppdaterValgPeg.Valgt = True
                        e.Handled = True
                    End If
                ElseIf BrukerHarValgtKode = False AndAlso Not ValgtSeriePeg = 1 Then
                    OppdaterValgPeg = VelgSerieCollection.Item(ValgtSeriePeg)
                    OppdaterValgPeg.Valgt = False
                    ValgtSeriePeg -= 1
                    OppdaterValgPeg = VelgSerieCollection.Item(ValgtSeriePeg)
                    OppdaterValgPeg.Valgt = True
                    e.Handled = True
                End If
            Case Keys.Up
                If testposisjon = False AndAlso ValgtPeg > 4 AndAlso BrukerHarValgtKode = True AndAlso MenneskeSinTur = True Then
                    OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                    OppdaterValgPeg.Valgt = False
                    ValgtPeg -= 4
                    OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                    OppdaterValgPeg.Valgt = True
                    e.Handled = True
                End If
            Case Keys.Down
                If testposisjon = False AndAlso BrukerHarValgtKode = True AndAlso MenneskeSinTur = True Then
                    If Not ValgtPeg > 4 Then
                        If Not ValgtPeg + 4 > VCount Then
                            OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                            OppdaterValgPeg.Valgt = False
                            ValgtPeg += 4
                            OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                            OppdaterValgPeg.Valgt = True
                        Else
                            OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                            OppdaterValgPeg.Valgt = False
                            ValgtPeg = VCount
                            OppdaterValgPeg = ValgPegCollection.Item(ValgtPeg)
                            OppdaterValgPeg.Valgt = True
                        End If
                        e.Handled = True
                    End If
                End If
            Case Keys.Space
                If BrukerHarValgtKode = True AndAlso MenneskeSinTur = True AndAlso VentMedÅFylleInn = False Then
                    If fargervalgt < antallhull Then
                        Call FyllInnFarge(ValgtPeg)
                        e.Handled = True
                    Else
                        Call Sjekk()
                    End If
                ElseIf BrukerHarValgtKode = False Then
                    Call FyllInnFargeHull(ValgtSeriePeg)
                    e.Handled = True
                End If
            Case Keys.Back
                Call FjernFarge()
                e.Handled = True
        End Select
        If e.Handled = True AndAlso Not ValgtPeg = 0 Then
            FokusertValgPegObjekt = ValgPegCollection.Item(ValgtPeg)
        End If
    End Sub

    Dim BWStep As Integer = 0
    Private Sub BWTimer_Tick(sender As Object, e As EventArgs) Handles BWTimer.Tick
        Dim FyllBW As BWPegClass
        If TimerB > 0 OrElse TimerW > 0 Then
            FyllBW = BWcollection.Item(BWStep + 1 + antallhull * turn)
            If TimerB > 0 Then
                FyllBW.BackColor = standardfarger(2)
                FyllBW.ForeColor = standardfarger(2)
                TimerB -= 1
            Else
                FyllBW.BackColor = standardfarger(3)
                FyllBW.ForeColor = standardfarger(3)
                TimerW -= 1
            End If
            BWStep += 1
        ElseIf BWStep < antallhull Then
            FyllBW = BWcollection.Item(BWStep + 1 + antallhull * turn)
            FyllBW.ForeColor = Color.FromArgb(30, 30, 50)
            FyllBW.BackColor = standardfarger(1)
            BWStep += 1
        ElseIf BWStep = antallhull Then
            BWStep = 0
            BWTimer.Enabled = False
            VentMedÅFylleInn = False
            If vinnbetingelser = True Then
                FokusertPegObjekt.Fokusert = False
                FokusertPegObjekt = testcollection.Item(1)
                FokusertPeg = 1
                MsgBox("Du vant!")
                Call ByttSide()
            ElseIf turn = antallforsøk - 1 Then
                FokusertPegObjekt.Fokusert = False
                FokusertPegObjekt = testcollection.Item(1)
                FokusertPeg = 1
                MsgBox("Du tapte.")
                Call ByttSide()
            Else
                turn += 1
                Call FokusertPegEndret(fargervalgt + antallhull * turn + 1)
                LysTimer.Enabled = True
            End If
        End If
    End Sub

    Private Sub frmSpill_Invalidated(sender As Object, e As InvalidateEventArgs) Handles Me.Invalidated
        If MenneskeSinTur = True Then
            For Each test3 As ValgPegClass In ValgPegCollection
                test3.TrengerEndring = True
            Next
            For Each test4 As ValgPegClass In DeaktiverteValgPegs
                test4.TrengerEndring = True
            Next
        ElseIf BrukerHarValgtKode = False Then
            For Each SH As PegClass In SerieHullCollection
                SH.TrengerEndring = True
            Next
            For Each VS As ValgPegClass In VelgSerieCollection
                VS.TrengerEndring = True
            Next
        End If
        If BrukerHarValgtKode = True Then
            For Each test As PegClass In testcollection
                test.TrengerEndring = True
            Next
            For Each test2 As BWPegClass In BWcollection
                test2.TrengerEndring = True
            Next
        End If
    End Sub

    Private Sub LysTimer_Tick(sender As Object, e As EventArgs) Handles LysTimer.Tick
        FokusertPegObjekt.YtrePen.Color = Color.FromArgb(R0, G0 + LysAlpha, B0 + LysAlpha)
        FokusertPegObjekt.TrengerEndring = True
        Call FokusertPegObjekt.DrawACircle()

        If FokusAlphaØker = True AndAlso B0 + LysAlpha < 250 Then
            LysAlpha += 5
        ElseIf FokusAlphaØker = False AndAlso LysAlpha > 0 Then
            LysAlpha -= 5
        ElseIf FokusAlphaØker = True Then
            FokusAlphaØker = False
        Else
            FokusAlphaØker = True
        End If
    End Sub

    Private Sub GrafikkTimer_Tick(sender As Object, e As EventArgs) Handles GrafikkTimer.Tick
        If FokusertValgPegObjekt.rect.Width > 16 Then
            FokusertValgPegObjekt.TrengerEndring = True
            Call FokusertValgPegObjekt.DrawACircle()
        Else
            FokusertValgPegObjekt.Sirkulerer = True
            Call FokusertValgPegObjekt.Sirkuler(vinkel)
            vinkel += 1
            If vinkel = 360 Then
                vinkel = 0
            End If
        End If
        For Each VP As ValgPegClass In ValgPegCollection
            If VP.rect.Width < VP.Size AndAlso VP.Valgt = False Then
                VP.TrengerEndring = True
                Call VP.DrawACircle()
            End If
        Next


    End Sub

    Private Sub frmSpill_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        InvalidateTimer.Enabled = False
        InvalidateTimer.Enabled = True
    End Sub

    Private Sub InvalidateTimer_Tick(sender As Object, e As EventArgs) Handles InvalidateTimer.Tick
        Me.Refresh()
        InvalidateTimer.Enabled = False
    End Sub
End Class
