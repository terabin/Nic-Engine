Imports SFML
Imports SFML.Audio

Public Class Som
    Public Shared PATH As String = Application.StartupPath & "/Data/Som/"
    Private Shared Componente(50) As Sound

    Public Shared Sub Init()
        For i As Short = 0 To Componente.Count - 1
            Componente(i) = New Sound()
        Next
    End Sub

    Public Shared Sub Play(ByVal fileName As String)
        If Not Options.Sound Then Return
        If Not IO.File.Exists(PATH & fileName) Then Return
        ' On Error Resume Next
        For i As Short = 0 To Componente.Count - 1
            If Componente(i) Is Nothing Then Return
            If Componente(i).Status = SoundStatus.Stopped Then
                Dim buffer As SoundBuffer = New SoundBuffer(PATH & fileName)
                Componente(i).SoundBuffer = buffer
                Componente(i).Play()
                Return

            End If
        Next
    End Sub


    Public Shared Sub StopAll()
        On Error Resume Next
        For i As Short = 0 To Componente.Count - 1
            If Componente(i) Is Nothing Then Return
            If Not Componente(i).SoundBuffer Is Nothing Then
                Componente(i).Stop()
                Componente(i).SoundBuffer = Nothing
            End If
        Next
    End Sub

    Public Shared Sub Core()
        On Error Resume Next
        For i As Short = 0 To Componente.Count - 1
            If Componente(i) Is Nothing Then Return
            If Not Componente(i).SoundBuffer Is Nothing Then
                If Componente(i).Status = SoundStatus.Stopped Then
                    '   Componente(i).SoundBuffer = Nothing
                End If
            End If
        Next
    End Sub
End Class
