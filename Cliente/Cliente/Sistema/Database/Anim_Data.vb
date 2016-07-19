Imports SFML.System
Imports SFML.Graphics

Module Anim_Data
    Public Animation() As AnimData
    Public AnimationBuffer As AnimBuffer
End Module

Public Class AnimData
    Public Nome As String = ""
    Public Layer As Byte
    Public AnimID As Short = 0
    Public FrameCount As Short = 25
    Public FrameX As Short = 5
    Public FrameY As Short = 5
    Public SpeedMS As Short = 40
    Public Colour As Color = Color.White
    Public BlendMode As Byte

    Public Property ColourGDI As System.Drawing.Color
        Get
            Return Drawing.Color.FromArgb(Colour.A, Colour.R, Colour.G, Colour.B)
        End Get
        Set(value As System.Drawing.Color)
            Colour = New Color(value.R, value.G, value.B, value.A)
        End Set
    End Property

    Public ReadOnly Property ID As Integer
        Get
            Return Array.IndexOf(Animation, Me)
        End Get
    End Property
End Class

Public Class AnimBuffer
    Public Item() As clsAnimationBufferBase

    Public Sub New()
        ReDim Item(Options.MAX_ANIMATION_BUFFER + 1)
        For i As Short = 1 To Options.MAX_ANIMATION_BUFFER
            Item(i) = New clsAnimationBufferBase
        Next
    End Sub

    Default Public Property Base(ByVal Index As Integer) As clsAnimationBufferBase
        Get
            Return Item(Index)
        End Get
        Set(value As clsAnimationBufferBase)
            Item(Index) = value
        End Set
    End Property

    Public Sub Add(AnimID As Short, X As Short, Y As Short)
        For i As Short = 1 To Options.MAX_ANIMATION_BUFFER
            If Item(i).AnimationID = 0 Then
                Item(i).AnimationID = AnimID
                Item(i).X = X
                Item(i).Y = Y
                Item(i).Frame = 0
                Item(i).Timer = GetTickCount
                Return
            End If
        Next
    End Sub

    Public Sub Add(AnimID As Short, Position As Vector2f)
        For i As Short = 1 To Options.MAX_ANIMATION_BUFFER
            If Item(i).AnimationID = 0 Then
                Item(i).AnimationID = AnimID
                Item(i).Position = Position
                Item(i).Frame = 0
                Item(i).Timer = GetTickCount
                Return
            End If
        Next
    End Sub

    Public Sub Remove(Index As Integer)
        Item(Index).Frame = 0
        Item(Index).AnimationID = 0
        Item(Index).Timer = 0
        Item(Index).X = 0
        Item(Index).Y = 0
    End Sub

    Public Sub Clear()
        For i As Short = 1 To Options.MAX_ANIMATION_BUFFER
            Remove(i)
        Next
    End Sub
End Class