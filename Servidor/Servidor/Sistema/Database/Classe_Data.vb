Module Classe_Data
    Public Classe() As clsClasse
    Public Enum Classes
        None
        Guerreiro
        Mago
        Assassino
        Alquimista

        ' Quantia
        Count
    End Enum

    Public Sub LoadClasses()
        ReDim Classe(0 To Classes.Count - 1)
        For i As Short = 0 To Classes.Count - 1
            Classe(i) = New clsClasse
        Next

        With Classe(Classes.Guerreiro)
            .Nome = "Guerreiro"
            .Mapa = 1
            .X = 10
            .Y = 10
            .STR = 10
            .CON = 5
            .Sprite = 1
        End With

        With Classe(Classes.Mago)
            .Nome = "Mago"
            .Mapa = 1
            .X = 10
            .Y = 10
            .STR = 10
            .CON = 5
            .Sprite = 1
        End With

        With Classe(Classes.Assassino)
            .Nome = "Assassino"
            .Mapa = 1
            .X = 10
            .Y = 10
            .STR = 10
            .CON = 5
            .Sprite = 1
        End With

        With Classe(Classes.Alquimista)
            .Nome = "Alquimista"
            .Mapa = 1
            .X = 10
            .Y = 10
            .STR = 10
            .CON = 5
            .Sprite = 1
        End With
    End Sub
End Module


Public Class clsClasse
    Public Nome As String = ""
    Public Sprite As Short
    Public Mapa As Short
    Public X As Short
    Public Y As Short
    Public STR As Integer
    Public CON As Integer
End Class