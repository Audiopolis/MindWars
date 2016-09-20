Module SpillerModule
    Public løsning(antallhull - 1) As Integer
    Public fargervalgt As Integer = 0
    Public user(antallhull - 1) As Integer
    Public TimerB As Integer = 0
    Public TimerW As Integer = 0
    Public vinnbetingelser As Boolean = False
    Public HoldRekke As Boolean = False

    Public Sub GenererLøsning()
        Dim løsningsindex As Integer = 0
        Do Until løsningsindex = antallhull
            Dim n = rn.Next(1, antallfarger)
            frmSpill.Text = frmSpill.Text & n
            løsning(løsningsindex) = n
            løsningsindex += 1
        Loop
    End Sub

    Public Sub Sjekk()
        frmSpill.LysTimer.Enabled = False
        VentMedÅFylleInn = True
        Dim whitepegs As Integer = 0
        Dim blackpegs As Integer = 0
        Dim counted(antallhull - 1) As Integer
        Dim correct(antallhull - 1) As Integer
        Dim checkcorrectindex As Integer = 0
        Do Until checkcorrectindex = antallhull
            If løsning(checkcorrectindex) = user(checkcorrectindex) Then
                blackpegs += 1
                counted(checkcorrectindex) = 1
                correct(checkcorrectindex) = 1
            Else
                counted(checkcorrectindex) = 0
                correct(checkcorrectindex) = 0
            End If
            checkcorrectindex += 1
        Loop

        Dim currentstep As Integer = 0
        Do Until currentstep = antallhull
            If correct(currentstep) = 0 And løsning.Contains(user(currentstep)) Then
                Dim countedindex As Integer = 0
                Dim foundmatch As Boolean = False
                Do Until countedindex = antallhull Or foundmatch = True
                    If countedindex = currentstep Then
                        countedindex += 1
                        Continue Do
                    End If
                    If løsning(countedindex) = user(currentstep) And counted(countedindex) = 0 Then
                        counted(countedindex) = 1
                        whitepegs += 1
                        foundmatch = True
                    End If
                    countedindex += 1
                Loop
            End If
            user(currentstep) = 0
            currentstep += 1
        Loop

        If blackpegs = antallhull Then
            vinnbetingelser = True
        End If

        TimerB = blackpegs
        TimerW = whitepegs

        fargervalgt = 0

        frmSpill.BWTimer.Enabled = True
        'Call FokusertPegEndret(fargervalgt + antallhull * (turn + 1) + 1)
    End Sub

End Module
