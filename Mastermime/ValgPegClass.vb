Imports System.Drawing.Drawing2D

Public Class ValgPegClass
    Private NameValue As String

    Private CLeft As Integer
    Private CTop As Integer
    Private CWidth As Integer
    Private mstrName As String = ""
    Private ersynlig As Boolean = False
    Private ervalgt As Boolean
    Private eraktiv As Boolean = True
    Public TrengerEndring As Boolean
    Public Sirkulerer As Boolean
    Public SerieValgStil As Boolean = False


    Dim g As Graphics = frmSpill.CreateGraphics

    Private bgcolor As Color = Color.FromArgb(20, 20, 20)
    Private fcolor As Color = Color.Black

    Public rect As New Rectangle
    Dim omrissrect As New Rectangle
    Dim enstørre As New Rectangle
    Dim clearrect As New Rectangle
    Dim enmindreomrissrect As New Rectangle
    Dim enstørreomrissrect As New Rectangle
    Dim trekantrect As New Rectangle

    Dim DeaktivertBrush As New Drawing2D.HatchBrush(Drawing2D.HatchStyle.BackwardDiagonal, Color.Black, Color.FromArgb(20, 20, 20))
    Dim PegBrush As New SolidBrush(bgcolor)
    Dim ClearBrush As New SolidBrush(Color.Black)
    Dim OmrissPen As New Pen(fcolor)
    Dim ClearPen As New Pen(Color.Black)
    Dim VinkelPen As New Pen(Color.White)
    Dim ClearVinkelPen As New Pen(Color.Black)

    Public Event UtseendeEndret()
    Public Event Created()
    Public Event ValgEndret()

    Public Sub New(ByVal left1til4 As Integer, ByVal top1til2 As Integer)
        If testposisjon = False Then
            CLeft = 62 + 40 * left1til4
            CTop = 382 + 40 * top1til2
            OmrissPen.Width = 5
            CWidth = 24
        Else
            CLeft = 50 + 24 * left1til4
            CTop = 370 + 40
            CWidth = 18
            OmrissPen.Width = 4
        End If
        ervalgt = False
        eraktiv = True
        RaiseEvent Created()
    End Sub

    Public Property Aktiv() As Boolean
        Get
            Return eraktiv
        End Get
        Set(value As Boolean)
            eraktiv = value
            If eraktiv = False Then
                PegBrush.Color = Color.FromArgb(20, 20, 20)
            Else
                PegBrush.Color = bgcolor
            End If
            RaiseEvent UtseendeEndret()
        End Set
    End Property

    Public Property Valgt() As Boolean
        Get
            Return ervalgt
        End Get
        Set(value As Boolean)
            ervalgt = value
            If value = False Then
                If SerieValgStil = False Then
                    Call SlettSirkel()
                Else
                    Call SlettTrekant()
                End If
            End If
            RaiseEvent ValgEndret()
        End Set
    End Property

    Public Property Visible() As Boolean
        Get
            Return ersynlig
        End Get
        Set(ByVal value As Boolean)
            ersynlig = value
            RaiseEvent UtseendeEndret()
        End Set
    End Property

    Public Property BackColor() As Color
        Get
            Return bgcolor
        End Get
        Set(ByVal farge As Color)
            bgcolor = farge
            PegBrush.Color = farge
            Dim R As Integer = farge.R
            Dim G As Integer = farge.G
            Dim B As Integer = farge.B

            For i = 0 To 10
                If R * 1.1 < 255 Then
                    R = R * 1.1
                End If
                If G * 1.1 < 255 Then
                    G = G * 1.1
                End If
                If B * 1.1 < 255 Then
                    B = B * 1.1
                End If
                If B * 1.1 < 255 Then
                    B = B * 1.1
                End If
                If R * 1.1 > 255 AndAlso G * 1.1 > 255 AndAlso B * 1.1 > 255 Then
                    Exit For
                End If
            Next i

            VinkelPen.Color = Color.FromArgb(R, G, B)
            RaiseEvent UtseendeEndret()
        End Set
    End Property

    Public Property ForeColor() As Color
        Get
            Return fcolor
        End Get
        Set(ByVal farge As Color)
            fcolor = farge
            OmrissPen.Color = fcolor
            RaiseEvent UtseendeEndret()
        End Set
    End Property

    Public Property Size() As Integer
        Get
            Return CWidth
        End Get
        Set(ByVal value As Integer)
            CWidth = value
            RaiseEvent Created()
        End Set
    End Property

    Public Property Left() As Integer
        Get
            Return CLeft
        End Get
        Set(ByVal value As Integer)
            CLeft = value
            RaiseEvent Created()
        End Set
    End Property

    Public Property Top() As Integer
        Get
            Return CTop
        End Get
        Set(ByVal value As Integer)
            CTop = value
            RaiseEvent Created()
        End Set
    End Property

    Public Property Name() As String
        Get
            Return mstrName
        End Get
        Set(ByVal Value As String)
            mstrName = Value
        End Set
    End Property

    Private Sub Velg() Handles Me.ValgEndret
        If ElektriskTema = True Then
            If SerieValgStil = False Then
                If ervalgt = True Then
                    ClearPen.Width = 1
                Else
                    ClearPen.Width = 2
                    fcolor = Color.Black
                    OmrissPen.Color = fcolor
                    OmrissPen.Width = 7
                End If
                RaiseEvent UtseendeEndret()
            Else
                If ervalgt = True Then
                    Call TegnTrekant()
                Else
                    Call SlettTrekant()
                End If
            End If
        Else
            If ervalgt = True Then
                fcolor = bgcolor
                OmrissPen.Color = fcolor
                OmrissPen.Width = 5
            Else
                fcolor = Color.Black
                OmrissPen.Color = fcolor
                OmrissPen.Width = 7
            End If
            RaiseEvent UtseendeEndret()
        End If

    End Sub

    Public Sub Endret() Handles Me.UtseendeEndret
        TrengerEndring = True
        Call DrawACircle()
    End Sub

    Public Sub Skapt() Handles Me.Created
        ersynlig = True
        TykkClearPen.SetLineCap(LineCap.Flat, LineCap.Flat, DashCap.Flat)
        VinkelPen.SetLineCap(LineCap.Flat, LineCap.Flat, DashCap.Flat)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        rect.X = CLeft - CWidth / 2
        rect.Y = CTop - CWidth / 2
        rect.Width = CWidth
        rect.Height = CWidth

        trekantrect.Size = rect.Size
        trekantrect.Location = rect.Location
        trekantrect.Y += 30
        trekantrect.Inflate(-2, -4)

        omrissrect.Width = CWidth
        omrissrect.Height = CWidth
        omrissrect.Location = rect.Location
        omrissrect.Inflate(4, 4)

        enstørreomrissrect.Size = omrissrect.Size
        enstørreomrissrect.Location = omrissrect.Location
        enstørreomrissrect.Inflate(1, 1)

        enmindreomrissrect.Size = omrissrect.Size
        enmindreomrissrect.Location = omrissrect.Location
        enmindreomrissrect.Inflate(-1, -1)

        enstørre.Size = rect.Size
        enstørre.Location = rect.Location
        enstørre.Inflate(1, 1)


        clearrect.X = CLeft - 5 - CWidth / 2
        clearrect.Y = CTop - 5 - CWidth / 2
        clearrect.Height = CWidth + 10
        clearrect.Width = CWidth + 10

        RaiseEvent UtseendeEndret()
    End Sub
    Dim TykkClearPen As New Pen(Color.Black, 3)

    Public Sub SlettSirkel()
        TykkClearPen.Width = 5
        g.DrawEllipse(TykkClearPen, omrissrect)
        TykkClearPen.Width = 3
    End Sub

    Public Sub Sirkuler(ByVal Vinkel As Single)
        g.DrawEllipse(ClearVinkelPen, enstørreomrissrect)
        g.DrawEllipse(ClearVinkelPen, enmindreomrissrect)
        g.DrawArc(TykkClearPen, omrissrect, 0 + Vinkel, 30)
        g.DrawArc(VinkelPen, omrissrect, 30 + Vinkel, 70)
        g.DrawArc(TykkClearPen, omrissrect, 120 + Vinkel, 30)
        g.DrawArc(VinkelPen, omrissrect, 150 + Vinkel, 70)
        g.DrawArc(TykkClearPen, omrissrect, 240 + Vinkel, 30)
        g.DrawArc(VinkelPen, omrissrect, 270 + Vinkel, 70)
    End Sub
    Dim TrekantPunkter(3) As PointF
    Public Sub TegnTrekant()
        TrekantPunkter(0) = New PointF(trekantrect.Left, trekantrect.Bottom)
        TrekantPunkter(1) = New PointF(trekantrect.Left + trekantrect.Width / 2, trekantrect.Top)
        TrekantPunkter(2) = New PointF(trekantrect.Right, trekantrect.Bottom)
        TrekantPunkter(3) = New PointF(trekantrect.Left, trekantrect.Bottom)
        g.DrawLines(VinkelPen, TrekantPunkter)
    End Sub
    Public Sub SlettTrekant()
        TrekantPunkter(0) = New PointF(trekantrect.Left, trekantrect.Bottom)
        TrekantPunkter(1) = New PointF(trekantrect.Left + trekantrect.Width / 2, trekantrect.Top)
        TrekantPunkter(2) = New PointF(trekantrect.Right, trekantrect.Bottom)
        TrekantPunkter(3) = New PointF(trekantrect.Left, trekantrect.Bottom)
        g.FillRectangle(ClearBrush, trekantrect)
        g.DrawRectangle(TykkClearPen, trekantrect)
    End Sub

    Public Sub DrawACircle()
        If TrengerEndring = True Then
            ' AndAlso ersynlig = True Then
            If ElektriskTema = True AndAlso SerieValgStil = False Then
                If ervalgt = True Then
                    If rect.Width > 16 Then
                        ClearPen.Width = 4
                        rect.Inflate(-2, -2)
                        enstørre.Inflate(-2, -2)
                        g.DrawEllipse(ClearPen, enstørre)
                        g.FillEllipse(PegBrush, rect)
                    Else
                        ClearPen.Width = 1
                        g.DrawEllipse(ClearPen, enstørre)
                        g.FillEllipse(PegBrush, rect)
                        If rect.Width < 16 Then
                            MsgBox("Mindre")
                            rect.Inflate(1, 1)
                            enstørre.Inflate(1, 1)
                        End If
                    End If
                Else
                    If rect.Width < 24 Then
                        rect.Inflate(1, 1)
                        enstørre.Inflate(1, 1)
                        g.FillEllipse(PegBrush, rect)
                    Else
                        ClearPen.Width = 1
                        g.DrawEllipse(ClearPen, enstørre)
                        g.FillEllipse(PegBrush, rect)
                    End If
                    'g.DrawEllipse(ClearPen, tostørre)
                End If
            Else
                ClearPen.Width = 1
                g.DrawEllipse(ClearPen, enstørre)
                g.FillEllipse(PegBrush, rect)
            End If
            If eraktiv = False Then
                g.FillRectangle(ClearBrush, clearrect)
                g.FillEllipse(DeaktivertBrush, rect)
            End If
        End If
        TrengerEndring = False
    End Sub

    Protected Overrides Sub Finalize()
        g.Dispose()
        MyBase.Finalize()
    End Sub

End Class
