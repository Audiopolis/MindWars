Imports System.ComponentModel
Imports System.Threading

Module AIModule
    Public S As ArrayList
    Public OrigS As ArrayList
    Dim NyVerdi(3) As Integer
    Public NyesteForsøk(3) As Integer
    Public turn As Integer = 0
    Public AIturn As Integer = 0
    Public ReellBW(1) As Integer
    Public SjekkBWArr(1) As Integer
    Dim BWList As ArrayList
    Public FireBeste(3) As Integer
    Public FireBesteScore(3) As Integer
    Public BrukerHarValgtKode As Boolean = True
    Dim testgithub As Integer = 2

    Public Sub Gjett()
        Dim GjettRand As New Random

        Dim SCount As Integer = S.Count
        If Not AIturn = 0 Then
            If SCount < 250 Then
                If SCount > 12 Then
                    frmSpill.Button2.Enabled = False
                    frmSpill.bgworker.RunWorkerAsync()
                Else
                    NyesteForsøk = StringTilArray(OrigS.Item(Minimax))
                    Call UtførForsøk()
                End If
            Else
                Dim nS = GjettRand.Next(0, SCount - 1)
                NyesteForsøk = StringTilArray(S.Item(nS))
                Call UtførForsøk()
            End If
        Else
            Dim gjettindex As Integer = 0
            Do Until gjettindex = antallhull
                Dim n = GjettRand.Next(1, antallfarger)
                NyesteForsøk(gjettindex) = n
                gjettindex += 1
            Loop
            Call UtførForsøk()
        End If

    End Sub

    Public Sub UtførForsøk()
        ReellBW = AIFinnBWDersomLøsningEr(løsning, NyesteForsøk)
        Dim AIFargeIndex As Integer = 0
        Dim AIpeg As PegClass
        Dim forkortelse As Integer = antallforsøk * antallhull - antallhull * (AIturn + 1) + 1

        Do
            AIpeg = testcollection.Item(forkortelse + AIFargeIndex)
            AIpeg.BackColor = fargekoder(NyesteForsøk(AIFargeIndex))
            AIFargeIndex += 1
        Loop Until AIFargeIndex = antallhull

        Dim blackpegs As Integer = ReellBW(0)
        Dim whitepegs As Integer = ReellBW(1)
        Dim BWStep As Integer = 0
        Dim AIBW As BWPegClass

        While blackpegs > 0
            AIBW = BWcollection.Item(forkortelse + BWStep)
            AIBW.BackColor = standardfarger(2)
            blackpegs -= 1
            BWStep += 1
        End While
        While whitepegs > 0
            AIBW = BWcollection.Item(forkortelse + BWStep)
            AIBW.BackColor = standardfarger(3)
            whitepegs -= 1
            BWStep += 1
        End While
        AIturn += 1
        Call EliminerUmulige()
    End Sub

    Public Sub EliminerUmulige()
        Dim countfør As Integer = S.Count
        Dim q As Integer = S.Count - 1
        If Not ReellBW(0) = 4 Then
            Do
                SjekkBWArr = AIFinnBWDersomLøsningEr(StringTilArray(S.Item(q)), NyesteForsøk)
                If Not SjekkBWArr(1) = ReellBW(1) OrElse Not SjekkBWArr(0) = ReellBW(0) Then
                    S.RemoveAt(q)
                End If
                q -= 1
            Loop Until q = -1
            S.TrimToSize()
        Else
            MsgBox("PC vant")
            Call ByttSide()
        End If
    End Sub

    Function RegnUtFjernet(ByVal B As Integer, ByVal W As Integer, ByVal G As Integer)
        Dim antallfjernet As Integer = 0
        Dim q As Integer = 0
        Dim SCount As Integer = S.Count
        Dim StringTilArrayG() As Integer = StringTilArray(G)
        Do Until q = SCount
            SjekkBWArr = AIFinnBWDersomLøsningEr(StringTilArray(S.Item(q)), StringTilArrayG)
            If Not SjekkBWArr(0) = B OrElse Not SjekkBWArr(1) = W Then
                antallfjernet += 1
            End If
            q += 1
        Loop
        Return antallfjernet
    End Function

    Public Function AIFinnBWDersomLøsningEr(ByVal ail() As Integer, ByVal g() As Integer)
        Dim whitepegs As Integer
        Dim blackpegs As Integer
        Dim counted(3) As Integer
        Dim correct(3) As Integer

        Dim checkcorrectindex As Integer = 0
        Do
            If ail(checkcorrectindex) = g(checkcorrectindex) Then
                blackpegs += 1
                counted(checkcorrectindex) = 1
                correct(checkcorrectindex) = 1
            End If
            checkcorrectindex += 1
        Loop Until checkcorrectindex = antallhull

        Dim currentstep As Integer = 0
        Do
            If correct(currentstep) = 0 AndAlso ail.Contains(g(currentstep)) Then
                Dim countedindex As Integer = 0
                Dim foundmatch As Boolean = False
                Do
                    If countedindex = currentstep Then
                        countedindex += 1
                        Continue Do
                    ElseIf ail(countedindex) = g(currentstep) AndAlso counted(countedindex) = 0 Then
                        counted(countedindex) = 1
                        whitepegs += 1
                        foundmatch = True
                    End If
                    countedindex += 1
                Loop Until countedindex = antallhull Or foundmatch = True
            End If
            currentstep += 1
        Loop Until currentstep = antallhull

        Dim AntallBW(1) As Integer
        AntallBW = {blackpegs, whitepegs}
        Return AntallBW
    End Function

    Private Function Minimax()
        Dim BWCount(1) As Integer
        Dim HøyestMinScoreIndex As Integer = "0"
        Dim SScore As Integer = 0
        Dim i As Integer = 0
        Dim OrigSCount As Integer = OrigS.Count
        Dim SCount As Integer = S.Count
        Do Until i = OrigSCount
            Dim q As Integer = 0
            Dim score As Integer = 10000
            Dim OrigSItemArray(antallhull - 1) As Integer
            Dim OrigSItem As Integer = OrigS.Item(i)
            OrigSItemArray = StringTilArray(OrigSItem)
            Do
                BWCount = AIFinnBWDersomLøsningEr(StringTilArray(S.Item(q)), OrigSItemArray)
                Dim tempscore As Integer = (RegnUtFjernet(BWCount(0), BWCount(1), OrigSItem))
                If score > tempscore Then
                    score = tempscore
                End If
                q += 1
            Loop Until q = SCount
            If score > SScore AndAlso score < 10000 Then
                SScore = score
                HøyestMinScoreIndex = i
            End If
            i += 1
        Loop
        Return HøyestMinScoreIndex
    End Function

    Public Sub GenererS(ByVal op As Integer)
        NyVerdi = {1, 1, 1, 1}
        If op = 3 Then
            OrigS = New ArrayList
            OrigS.Clear()
            OrigS = S
        Else
            If op = 1 Then
                S = New ArrayList
                S.Clear()
                S.Add(1111)
            Else
                OrigS = New ArrayList
                OrigS.Clear()
                OrigS.Add(1111)
            End If
            Call BlaGjennom0(op)
            Call BlaGjennom1(op)
            Call BlaGjennom2(op)
            Call BlaGjennom3(op)
        End If
    End Sub

    Public Function StringTilArray(ByVal int As Integer)
        Dim arr(antallhull - 1) As Integer
        Dim str As String = int
        Dim i As Integer = 0
        Dim test As String
        Do
            test = str.Chars(i)
            arr(i) = test
            i += 1
        Loop Until i = antallhull
        Return arr
    End Function

    Public Function ArrayTilString(ByVal array() As Integer)
        Dim tallstring As Integer
        tallstring = array(0) * 1000 + array(1) * 100 + array(2) * 10 + array(3)
        Return tallstring
    End Function

    Private Sub BlaGjennom3(ByVal op As Integer)
        If op = 1 Then
            Do Until NyVerdi(3) = antallfarger
                NyVerdi(3) += 1
                S.Add(ArrayTilString(NyVerdi))
            Loop
        Else
            Do Until NyVerdi(3) = antallfarger
                NyVerdi(3) += 1
                OrigS.Add(ArrayTilString(NyVerdi))
            Loop
        End If

    End Sub

    Private Sub BlaGjennom2(ByVal op As Integer)
        Do Until NyVerdi(2) = antallfarger
            Call BlaGjennom3(op)
            NyVerdi(3) = 1
            NyVerdi(2) += 1
            If op = 1 Then
                S.Add(ArrayTilString(NyVerdi))
            Else
                OrigS.Add(ArrayTilString(NyVerdi))
            End If
        Loop
    End Sub

    Private Sub BlaGjennom1(ByVal op As Integer)
        Do Until NyVerdi(1) = antallfarger
            Call BlaGjennom2(op)
            Call BlaGjennom3(op)
            NyVerdi(3) = 1
            NyVerdi(2) = 1
            NyVerdi(1) += 1
            If op = 1 Then
                S.Add(ArrayTilString(NyVerdi))
            Else
                OrigS.Add(ArrayTilString(NyVerdi))
            End If
        Loop
    End Sub

    Private Sub BlaGjennom0(ByVal op As Integer)
        Do Until NyVerdi(0) = antallfarger
            Call BlaGjennom1(op)
            Call BlaGjennom2(op)
            Call BlaGjennom3(op)
            NyVerdi(3) = 1
            NyVerdi(2) = 1
            NyVerdi(1) = 1
            NyVerdi(0) += 1
            If op = 1 Then
                S.Add(ArrayTilString(NyVerdi))
            Else
                OrigS.Add(ArrayTilString(NyVerdi))
            End If
        Loop
    End Sub



End Module
