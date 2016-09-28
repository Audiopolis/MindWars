Public Class PegClass
    Private NameValue As String
    Private CLeft As Integer = 50
    Private CTop As Integer = 50
    Private CWidth As Integer = 16
    Private mstrName As String = ""
    Private ersynlig As Boolean = False
    Private bildeskapt As Boolean = False
    Private HarFokus As Boolean = False
    Public TrengerEndring As Boolean
    'Public test As Bitmap
    'Dim testg As Graphics
    Dim bgcolor As Color
    Dim fcolor As Color

    Dim rect3 As New Rectangle
    Dim enmindre As New Rectangle

    Dim rect1 As New Rectangle
    Dim clearrect As New Rectangle

    Dim svarttynnpen As New Pen(Color.Black)
    Dim ClearBrush As New SolidBrush(Color.Black)
    Dim PegBrush As New SolidBrush(bgcolor)
    Public YtrePen As New Pen(standardfarger(0))
    Dim gr As Graphics = frmSpill.CreateGraphics
    Dim ClearAlpha As New Pen(Color.Black, 2)
    Dim PanikkPen As New Pen(Color.LightBlue)
    Dim ClearPanikkPen As New Pen(Color.Black, 3)

    Public Event UtseendeEndret()
    Public Event Created()

    Public Sub New(ByVal left1til4 As Integer, ByVal top1til9 As Integer)
        CLeft = 62 + 40 * left1til4
        CTop = 502 - 40 * top1til9
        RaiseEvent Created()
    End Sub

    Public Property Visible() As Boolean
        Get
            Visible = ersynlig
        End Get
        Set(ByVal value As Boolean)
            ersynlig = value
            RaiseEvent UtseendeEndret()
        End Set
    End Property

    Public Property BackColor() As Color
        Get
            BackColor = bgcolor
        End Get
        Set(ByVal farge As Color)
            bgcolor = farge
            PegBrush.Color = bgcolor
            RaiseEvent UtseendeEndret()
        End Set
    End Property

    Public Property Fokusert() As Boolean
        Get
            Return HarFokus
        End Get
        Set(ByVal value As Boolean)
            HarFokus = value
            If HarFokus = False Then
                fcolor = standardfarger(0)
                YtrePen.Color = fcolor
                YtrePen.Width = 1
            ElseIf ElektriskTema = True Then
                YtrePen.Width = 2
            Else
                YtrePen.Width = 1
            End If
            RaiseEvent UtseendeEndret()
        End Set
    End Property

    Public Property ForeColor() As Color
        Get
            ForeColor = fcolor
        End Get
        Set(ByVal farge As Color)
            fcolor = farge
            YtrePen.Color = fcolor
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
            Top = CTop
        End Get
        Set(ByVal value As Integer)
            CTop = value
            RaiseEvent Created()
        End Set
    End Property

    Public Property Name() As String
        Get
            Name = mstrName
        End Get
        Set(ByVal Value As String)
            mstrName = Value
        End Set
    End Property

    Private Sub Skap() Handles Me.Created
        ersynlig = True
        gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        rect3.X = (CLeft - (CWidth / 2) + 2)
        rect3.Y = (CTop - (CWidth / 2) + 2)
        rect3.Width = (CWidth - 4)
        rect3.Height = (CWidth - 4)


        rect1.X = (CLeft - (CWidth / 2) - 4)
        rect1.Y = (CTop - (CWidth / 2) - 4)
        rect1.Height = (CWidth + 8)
        rect1.Width = (CWidth + 8)

        enmindre.X = (CLeft - (CWidth / 2) - 3)
        enmindre.Y = (CTop - (CWidth / 2) - 3)
        enmindre.Width = (CWidth + 6)
        enmindre.Height = (CWidth + 6)


        clearrect.X = (CLeft - (CWidth / 2) - 5)
        clearrect.Y = (CTop - (CWidth / 2) - 5)
        clearrect.Height = (CWidth + 10)
        clearrect.Width = (CWidth + 10)



        RaiseEvent UtseendeEndret()
    End Sub

    Private Sub Endret() Handles Me.UtseendeEndret
        TrengerEndring = True
        Call DrawACircle()
    End Sub

    Public Sub DrawACircle()
        If TrengerEndring = True AndAlso ersynlig = True Then
            If HarFokus = False Then
                gr.FillRectangle(ClearBrush, clearrect)
                gr.DrawEllipse(YtrePen, rect1)
                gr.FillEllipse(PegBrush, rect3)
            ElseIf ElektriskTema = True Then
                If turn < antallforsøk - 1 Then
                    'gr.FillEllipse(ClearBrush, clearrect)
                    gr.DrawEllipse(YtrePen, rect1)
                    gr.DrawEllipse(svarttynnpen, enmindre)
                    gr.DrawEllipse(svarttynnpen, clearrect)
                Else
                    Dim rn As New Random
                    Dim rn2 As New Random
                    Dim rntall = rn.Next(0, 360)
                    Dim rntall2 = rn2.Next(20, 180)
                    gr.FillEllipse(ClearBrush, clearrect)
                    gr.DrawEllipse(YtrePen, rect1)
                    gr.DrawEllipse(svarttynnpen, enmindre)
                    gr.DrawEllipse(svarttynnpen, clearrect)
                    gr.DrawArc(PanikkPen, enmindre, rntall, rntall2)
                End If
            Else
                gr.DrawEllipse(svarttynnpen, enmindre)
                gr.DrawEllipse(svarttynnpen, clearrect)
                gr.DrawEllipse(YtrePen, rect1)
                gr.FillEllipse(PegBrush, rect3)
            End If
            TrengerEndring = False
        End If
    End Sub

    Protected Overrides Sub Finalize()
        gr.Dispose()
        ClearBrush.Dispose()
        ClearAlpha.Dispose()
        YtrePen.Dispose()
        PegBrush.Dispose()
        MyBase.Finalize()
    End Sub
End Class
