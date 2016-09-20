Imports System.Drawing.Drawing2D

Public Class BWPegClass
    Private NameValue As String
    Private CLeft As Integer = 50
    Private CTop As Integer = 50
    Private CWidth As Integer = 10
    Private mstrName As String = ""
    Private ersynlig As Boolean = False
    Public TrengerEndring As Boolean

    Dim g As Graphics = frmSpill.CreateGraphics

    Dim bgcolor As Color = standardfarger(1)
    Dim fcolor As Color = Color.Blue

    Dim rect As New Rectangle
    Dim enstørre As New Rectangle
    Dim enmindre As New Rectangle
    Public GradientRect As New Rectangle

    Dim ElektriskPen As New Pen(fcolor)
    Dim ClearTynnPen As New Pen(Color.Black)
    'Dim ClearPen As New Pen(Color.Black, 2)
    Dim BWPegBrush As New SolidBrush(bgcolor)
    Dim ClearBrush As New SolidBrush(Color.Black)
    Dim ElektriskBWBrush As PathGradientBrush

    Dim testpath As GraphicsPath

    Public Event UtseendeEndret()
    Public Event Created()

    Public Sub New(ByVal left1til4 As Integer, ByVal top1til9 As Integer)
        CLeft = 20 + 14 * left1til4
        CTop = top1til9
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
            BWPegBrush.Color = bgcolor
            ElektriskBWBrush.CenterColor = bgcolor
            RaiseEvent UtseendeEndret()
        End Set
    End Property

    Public Property ForeColor() As Color
        Get
            Return fcolor
        End Get
        Set(ByVal farge As Color)
            fcolor = farge
            ElektriskPen.Color = fcolor
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
            Left = CLeft
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

    Public Sub Skapt() Handles Me.Created
        ersynlig = True
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        rect.X = CLeft - CWidth / 2
        rect.Y = CTop - CWidth / 2
        rect.Width = CWidth
        rect.Height = CWidth
        enstørre.Size = rect.Size
        enmindre.Size = rect.Size
        enstørre.Location = rect.Location
        enmindre.Location = rect.Location
        GradientRect.Size = rect.Size
        GradientRect.Location = rect.Location
        GradientRect.Inflate(-4, -4)
        enstørre.Inflate(1, 1)
        enmindre.Inflate(-1, -1)

        If ElektriskTema = True Then
            testpath = New GraphicsPath
            testpath.AddEllipse(rect)
            ElektriskBWBrush = New PathGradientBrush(testpath)
            ElektriskBWBrush.CenterPoint = New PointF(CLeft, CTop)
            ElektriskBWBrush.CenterColor = fcolor
            ElektriskBWBrush.SurroundColors = {Color.Black}
        End If
        RaiseEvent UtseendeEndret()
    End Sub

    Public Sub Endret() Handles Me.UtseendeEndret
        TrengerEndring = True
        Call DrawACircle()
    End Sub


    Dim R0 As Integer = Color.LightBlue.R
    Dim G0 As Integer = Color.LightBlue.G
    Dim B0 As Integer = Color.LightBlue.B

    Public Sirkelsteg As Integer = 0

    Public Sub DrawACircle()
        If TrengerEndring = True AndAlso ersynlig = True Then
            'DrawEllipse(ClearTynnPen, enmindre)
            g.DrawEllipse(ClearTynnPen, enstørre)
            g.FillEllipse(ClearBrush, rect)
            g.DrawEllipse(ElektriskPen, rect)

            If ElektriskTema = False Then
                g.FillEllipse(BWPegBrush, rect)
            Else
                ElektriskBWBrush.CenterColor = bgcolor
                g.FillEllipse(ElektriskBWBrush, GradientRect)
            End If
            TrengerEndring = False
        End If
    End Sub

    Protected Overrides Sub Finalize()
        g.Dispose()
        MyBase.Finalize()
    End Sub

End Class
