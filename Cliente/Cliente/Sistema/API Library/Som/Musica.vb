Imports SFML
Imports SFML.Audio

Public Class Musica
    Private Shared Componente As Music
    Private Shared buffer As String = ""
    Public Shared PATH As String = Application.StartupPath & "/Data/Musica/"

    Public Shared Sub Play(ByVal fileName As String)
        If Not Options.Music Then Return
        If IO.File.Exists(PATH & fileName) And Not buffer = fileName Then
            Componente = New Music(PATH & fileName)
            Componente.Loop = True
            Componente.Play()
            buffer = fileName
        End If
    End Sub

    Public Shared Sub [Stop]()
        If Not Componente Is Nothing Then
            Componente.Stop()
            Componente = Nothing
            buffer = ""
        End If
    End Sub
End Class
