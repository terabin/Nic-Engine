Module Anim_Data
    Public Animation() As AnimData

    Public Sub LoadAnimations()
        For i As Short = 1 To Options.MAX_ANIMATION
            Animation(i).Load()
        Next
    End Sub
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

    Public ReadOnly Property ID As Integer
        Get
            Return Array.IndexOf(Animation, Me)
        End Get
    End Property

    Public Sub Save()
        Dim fileName As String = Application.StartupPath & "\Data\Anim\" & ID & ".bin"
        Dim s As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using w As New IO.BinaryWriter(s)
            w.Write(Nome)
            w.Write(Layer)
            w.Write(AnimID)
            w.Write(FrameCount)
            w.Write(FrameX)
            w.Write(FrameY)
            w.Write(SpeedMS)
            w.Write(Colour.ToArgb)
            w.Write(BlendMode)
            w.Close()
        End Using
        s.Close()
    End Sub

    Public Sub Load()
        Dim fileName As String = Application.StartupPath & "\Data\Anim\" & ID & ".bin"
        If Not IO.File.Exists(fileName) Then Save()

        Dim s As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)
        Using r As New IO.BinaryReader(s)
            Nome = r.ReadString
            Layer = r.ReadByte
            AnimID = r.ReadInt16
            FrameCount = r.ReadInt16
            FrameX = r.ReadInt16
            FrameY = r.ReadInt16
            SpeedMS = r.ReadInt16
            Colour = Color.FromArgb(r.ReadInt32)
            BlendMode = r.ReadByte
            r.Close()
        End Using
        s.Close()
    End Sub

End Class
