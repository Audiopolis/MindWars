Module GrafikkModule
    Public fargekoder() As Color = New Color() {Color.Black, Color.Yellow, Color.Orange, Color.Green, Color.Red, Color.Pink, Color.Blue, Color.DarkRed, Color.Cyan, Color.Gray, Color.Purple}
    Public standardfarger() As Color = New Color() {Color.DeepPink, Color.Black, Color.FromArgb(255, 100, 30), Color.LightBlue, Color.Black, Color.LightCyan}
    '0: Hull ikke fylt, 1: BW-hull ikke fyllt, 2: BW-hull fyllt, riktig posisjon, 3: BW-hull fyllt, feil posisjon, 4: vindu bakgrunn
    'Public Hull(3) As PictureBox
    Dim HullFyltInn As Integer = 0
    Public vinkel As Single = 0
    Public testcollection As New Collection
    Public BWcollection As New Collection
    Public ValgPegCollection As New Collection
    Public VelgSerieCollection As New Collection
    Public SerieHullCollection As New Collection
    Public DeaktiverteValgPegs As New Collection
    Public FokusAlphaØker As Boolean = True
    Public ElektriskTema As Boolean = True
    Public FokusertValgPegObjekt As ValgPegClass
    Public FokusertPegObjekt As PegClass

    Public BlinkRad As Boolean = False
    Public FormLastet As Boolean = False
    Public Skaleringskonstant As Integer
    Public testposisjon As Boolean = False
    Public FokusertPeg As Integer

    Public Sub GenererValg()
        Dim testvalg As ValgPegClass
        Dim i = 0
        Do Until i = antallfarger
            If i >= 4 AndAlso testposisjon = False Then
                testvalg = New ValgPegClass(i - 4, 2)
            Else
                testvalg = New ValgPegClass(i, 1)
            End If
            ValgPegCollection.Add(testvalg)
            testvalg = ValgPegCollection.Item(i + 1)
            testvalg.BackColor = fargekoder(i + 1)
            i += 1
        Loop
        Dim tilovers As Integer = 8 - antallfarger
        Dim k = 0
        Do Until k = tilovers
            If i >= 4 AndAlso testposisjon = False Then
                testvalg = New ValgPegClass(i - 4, 2)
            Else
                testvalg = New ValgPegClass(i, 1)
            End If
            i += 1
            DeaktiverteValgPegs.Add(testvalg)
            testvalg = DeaktiverteValgPegs.Item(k + 1)
            testvalg.Aktiv = False
            k += 1
        Loop
        ValgtPeg = 1
        FokusertValgPegObjekt = ValgPegCollection.Item(1)
        FokusertValgPegObjekt.Valgt = True
    End Sub

    Public Sub GenererBrett()
        Call GenererValg()
        Dim w = 0
        Dim x = 0
        Do Until w = antallforsøk
            Dim y = 0
            Do Until y = antallhull
                Dim GenererBWPegs As BWPegClass
                If antallhull = 4 Then
                    If y < 2 Then
                        GenererBWPegs = New BWPegClass(y, 376 - 40 * w)
                    Else
                        GenererBWPegs = New BWPegClass(y - 2, 390 - 40 * w)
                    End If
                Else
                    GenererBWPegs = New BWPegClass(y, 320 + 30 * w)
                End If
                BWcollection.Add(GenererBWPegs)
                Dim GenererPegs As New PegClass(y, 3.2 + w)
                testcollection.Add(GenererPegs)
                y += 1
                x += 1
            Loop
            w += 1
        Loop
    End Sub

    Public Sub RegenererBrett()
        Dim i = 0
        Dim RegenererBW As BWPegClass
        Dim RegenererPeg As PegClass
        Do Until i = antallhull * antallforsøk
            RegenererBW = BWcollection.Item(i + 1)
            RegenererBW.BackColor = standardfarger(1)
            RegenererBW.ForeColor = Color.Blue
            RegenererPeg = testcollection.Item(i + 1)
            RegenererPeg.BackColor = Color.Black
            RegenererPeg.ForeColor = standardfarger(0)
            i += 1
        Loop
        For Each BWGrafikk As BWPegClass In BWcollection
            BWGrafikk.Visible = True
        Next
        For Each PegGrafikk As PegClass In testcollection
            PegGrafikk.Visible = True
        Next
        For Each VPG As ValgPegClass In ValgPegCollection
            VPG.Visible = True
        Next
        For Each Deaktivert As ValgPegClass In DeaktiverteValgPegs
            Deaktivert.Visible = True
        Next
    End Sub

    Public Sub MenneskeSpiller()
        ValgtPeg = 1
        FokusertValgPegObjekt = ValgPegCollection.Item(1)
        Call GenererLøsning()
        vinnbetingelser = False
        AIturn = 0
        turn = 0
        Call FokusertPegEndret(1)
        FokusertValgPegObjekt.Valgt = False
        FokusertValgPegObjekt = ValgPegCollection.Item(1)
        FokusertValgPegObjekt.Size = 24
        FokusertValgPegObjekt.Valgt = True
        frmSpill.LysTimer.Enabled = True
        frmSpill.GrafikkTimer.Enabled = True
    End Sub

    Public Sub AISpiller()
        FokusertValgPegObjekt = ValgPegCollection.Item(ValgtPeg)
        FokusertValgPegObjekt.Valgt = False
        BrukerHarValgtKode = False
        Dim g As Graphics = frmSpill.CreateGraphics
        g.Clear(Color.Black)
        ValgtPeg = 0
        AIturn = 0
        turn = 0
        Call GenererS(1)
        Call GenererS(3)
        ValgtSeriePeg = 1
        Dim i As Integer = 0
        SerieHullCollection.Clear()
        VelgSerieCollection.Clear()
        Dim hullavstand As Integer = 50
        Dim fargeavstand As Integer = ((antallhull - 1) / (antallfarger - 1)) * hullavstand
        Do Until i = antallhull
            Dim SerieHull As New PegClass(-1, -1)
            SerieHull.Left = (frmSpill.ClientRectangle.Width / 2) + hullavstand * i - 0.5 * hullavstand * (antallhull - 1)
            SerieHull.Top = (frmSpill.ClientRectangle.Height / 2) - 80
            SerieHull.BackColor = Color.Purple
            SerieHullCollection.Add(SerieHull)
            i += 1
        Loop
        Dim heh As PegClass = SerieHullCollection.Item(SerieHullCollection.Count)
        Dim heh2 As PegClass = SerieHullCollection.Item(1)

        Dim q As Integer = 0
        Do Until q = antallfarger
            Dim VelgSeriePeg As New ValgPegClass(-1, -1)
            VelgSeriePeg.SerieValgStil = True
            VelgSeriePeg.Left = (frmSpill.ClientRectangle.Width / 2) + fargeavstand * q - 0.5 * fargeavstand * (antallfarger - 1)
            VelgSeriePeg.Top = (frmSpill.ClientRectangle.Height / 2)
            VelgSeriePeg.BackColor = fargekoder(q + 1)
            VelgSeriePeg.Size -= 4
            VelgSerieCollection.Add(VelgSeriePeg)
            If q = 0 Then
                VelgSeriePeg = VelgSerieCollection.Item(1)
                VelgSeriePeg.Valgt = True
            End If
            q += 1
        Loop
        HullFyltInn = 0
        frmSpill.Invalidate()
    End Sub

    Public Sub FjernFarge()
        If fargervalgt > 0 Then
            Dim test As PegClass = testcollection.Item(FokusertPeg)
            test.Fokusert = False
            FokusertPeg -= 1
            fargervalgt -= 1
            user(fargervalgt) = 0
            FokusertPegObjekt = testcollection.Item(FokusertPeg)
            FokusertPegObjekt.BackColor = Color.Black
            FokusertPegObjekt.Fokusert = True
            frmSpill.LysTimer.Enabled = True
        End If
    End Sub

    Public Sub FyllInnFarge(ByVal FargeIndex As Integer)
        user(fargervalgt) = FargeIndex
        Dim test As PegClass = testcollection.Item(fargervalgt + antallhull * turn + 1)
        test.BackColor = fargekoder(FargeIndex)
        test.Fokusert = False
        fargervalgt += 1
        If fargervalgt < antallhull Then
            FokusertPeg = fargervalgt + antallhull * turn + 1
            frmSpill.LysTimer.Enabled = True
            Call FokusertPegEndret(fargervalgt + antallhull * turn + 1)
        ElseIf fargervalgt = antallhull Then
            FokusertPeg = antallhull * (turn + 1)
            test = testcollection.Item(FokusertPeg)
            frmSpill.LysTimer.Enabled = False
            test.Fokusert = False
            BlinkRad = True
            Call FokusertPegEndret(fargervalgt + antallhull * turn + 1)
            Dim i = 1
        End If
    End Sub

    Public Sub FokusertPegEndret(ByVal pegindex As Integer)
        If Not pegindex > testcollection.Count Then
            FokusertPeg = pegindex
            FokusertPegObjekt = testcollection.Item(pegindex)
            FokusertPegObjekt.Fokusert = True
        Else
            frmSpill.LysTimer.Enabled = False
            FokusertPegObjekt = testcollection.Item(pegindex - 1)
            FokusertPegObjekt.Fokusert = True
            FokusertPeg = 1
        End If
    End Sub

    Public Sub FyllInnFargeHull(ByVal FargeIndex As Integer)
        løsning(HullFyltInn) = FargeIndex
        Dim FyllHullPeg As PegClass = SerieHullCollection.Item(HullFyltInn + 1)
        FyllHullPeg.BackColor = fargekoder(FargeIndex)
        HullFyltInn += 1
        If HullFyltInn = antallhull Then
            SerieHullCollection.Clear()
            VelgSerieCollection.Clear()
            BrukerHarValgtKode = True
            frmSpill.Invalidate()
        End If
    End Sub

End Module
