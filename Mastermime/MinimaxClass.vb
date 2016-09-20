Class MinimaxClass
    Public Kvartal As Integer = 0

    Public Sub Minimax()

        Do Until S.Count > 0 AndAlso OrigS.Count > 0 AndAlso Not Kvartal = 0
            System.Threading.Thread.Sleep(10)
        Loop

        Dim BWCount(1) As Integer
        Dim HøyestMinScoreIndex As Integer = "0"
        Dim SScore As Integer = 0
        Dim i As Integer
        Dim isiste As Integer
        Dim fjerdedel As Integer
        fjerdedel = OrigS.Count / 4
        Dim remainder As Integer = OrigS.Count Mod 4
        Dim SCount As Integer = S.Count
        Dim OrigSCount As Integer = OrigS.Count
        Dim BWList As New ArrayList

        If Not remainder = 0 Then
            If remainder = 1 Then
                fjerdedel -= 0.5
            ElseIf remainder = 2 Then
                fjerdedel -= 0.75
            Else
                fjerdedel = Math.Round(fjerdedel)
            End If
        End If

        If Kvartal = 1 Then
            isiste = fjerdedel
            i = 0
        ElseIf Kvartal = 2 Then
            isiste = fjerdedel * 2
            i = fjerdedel - 1
        ElseIf Kvartal = 3 Then
            isiste = fjerdedel * 3
            i = fjerdedel * 2 - 1
        ElseIf Kvartal = 4 Then
            isiste = OrigS.Count
            i = fjerdedel * 3 - 1
        Else
            MsgBox("Feil: Kvartal = " & Kvartal)
        End If

        Do Until i = isiste
            Dim q As Integer = 0
            Dim score As Integer = 10000
            Dim OrigSItemiArray() As Integer
            Dim OrigSItemi As Integer = OrigS.Item(i)
            OrigSItemiArray = StringTilArray(OrigSItemi)
            Do
                BWCount = AIFinnBWDersomLøsningEr(StringTilArray(S.Item(q)), OrigSItemiArray)
                Dim BWint As Integer = BWCount(0) * 10 + BWCount(1)
                If Not BWList.Contains(BWint) Then
                    Dim tempscore As Integer = (RegnUtFjernet(BWCount(0), BWCount(1), OrigSItemi))
                    If score > tempscore Then
                        score = tempscore
                    End If
                    BWList.Add(BWint)
                End If
                q += 1
            Loop Until q = SCount
            BWList.Clear()
            q = 0
            If score > SScore AndAlso score < 10000 Then
                SScore = score
                HøyestMinScoreIndex = i
            End If
            i += 1
        Loop
        FireBeste(Kvartal - 1) = HøyestMinScoreIndex
        FireBesteScore(Kvartal - 1) = SScore
    End Sub
End Class
