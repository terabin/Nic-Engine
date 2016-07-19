Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Module SFML_System

    ' PATH
    Public PATH_GRAFICO As String = Application.StartupPath & "/Data/Gráfico/"
    Public PATH_GUI As String = PATH_GRAFICO & "GUI/"

    ' Devices
    Public DeviceGame As RenderWindow
    Public DeviceMap As RenderWindow
    Public DeviceNpcSprite As RenderWindow
    Public DeviceItemIcon As RenderWindow
    Public DeviceAnim As RenderWindow

    ' Camera
    Public Camera As IntRect
    Public TileView As IntRect

    ' Font
    Public GameFont As Font

    ' Texturas
    Public texDesign As Integer
    Public texTex As Integer
    Public texCheckBox As Integer
    Public texChar() As Integer
    Public texGUI() As Integer
    Public texAnimation() As Integer
    Public texTile() As Integer
    Public texItem() As Integer


    ' System Textures
    Public GlobalTexture As List(Of clsGlobalTexture) = New List(Of clsGlobalTexture)

    Public Sub InitGraficos()
        Dim Context As ContextSettings = New ContextSettings()
        Context.DepthBits = 24
        Context.StencilBits = 0
        Context.AntialiasingLevel = 0
        Context.MajorVersion = 3
        Context.MinorVersion = 0

        ' Device Main
        DeviceGame = New RenderWindow(frmMain.Handle, Context)
        DeviceMap = New RenderWindow(frmEditor_Map.picTile.Handle)
        DeviceNpcSprite = New RenderWindow(frmEditor_Npc.picSprite.Handle)
        DeviceItemIcon = New RenderWindow(frmEditor_Item.picIcon.Handle)
        DeviceAnim = New RenderWindow(frmEditor_Anim.picAnim.Handle)


        ' Carregar Font
        GameFont = New Font("verdana.ttf")
        InitFontText()

        ' Carregar Texturas
        CacheAllTextures()
    End Sub

    Private Sub CacheAllTextures()
        ' Carregar Texturas Unicas
        texDesign = LoadTexture(PATH_GRAFICO & "design.png")
        texTex = LoadTexture(PATH_GRAFICO & "text.jpg")
        texCheckBox = LoadTexture(PATH_GRAFICO & "checkbox.png")

        ' Sprites
        Dim i As Integer = 0
        ReDim texChar(0)
        While (IO.File.Exists(PATH_GRAFICO & "Personagem/" & i + 1 & ".png"))
            i += 1
            ReDim Preserve texChar(0 To i)
            texChar(i) = LoadTexture(PATH_GRAFICO & "Personagem/" & i & ".png")
        End While

        ' Interfaces
        ReDim texGUI(0)
        i = 0
        While (IO.File.Exists(PATH_GUI & i + 1 & ".png"))
            i += 1
            ReDim Preserve texGUI(0 To i)
            texGUI(i) = LoadTexture(PATH_GUI & i & ".png")
        End While

        ' Animation
        ReDim texAnimation(0)
        i = 0
        While (IO.File.Exists(PATH_GRAFICO & "Animation/" & i + 1 & ".png"))
            i += 1
            ReDim Preserve texAnimation(0 To i)
            texAnimation(i) = LoadTexture(PATH_GRAFICO & "Animation/" & i & ".png")
        End While

        ' Tileset
        ReDim texTile(0)
        i = 0
        While (IO.File.Exists(PATH_GRAFICO & "Tile/" & i + 1 & ".png"))
            i += 1
            ReDim Preserve texTile(0 To i)
            texTile(i) = LoadTexture(PATH_GRAFICO & "Tile/" & i & ".png")
        End While

        ' Items
        ReDim texItem(0)
        i = 0
        While (IO.File.Exists(PATH_GRAFICO & "Item/" & i + 1 & ".png"))
            i += 1
            ReDim Preserve texItem(0 To i)
            texItem(i) = LoadTexture(PATH_GRAFICO & "Item/" & i & ".png")
        End While
    End Sub

    Private Function LoadTexture(ByVal fileName As String) As Integer
        GlobalTexture.Add(New clsGlobalTexture(fileName))
        Return GlobalTexture.Count - 1
    End Function

    Public ReadOnly Property GetTextureSize(ByVal textureNum As Integer) As Vector2f
        Get
            Return GlobalTexture(textureNum).Size
        End Get
    End Property

    Public Sub RenderTexture(ByVal textureNum As Integer, ByVal Pos As IntRect, ByVal Img As IntRect, ByVal Colour As Color, Optional ByVal maskColor As Boolean = False)
        ' Checa se está pronto pra usar
        GlobalTexture(textureNum).Check()

        ' Vertices
        Dim Vertex As VertexArray = New VertexArray()
        Vertex.Append(New Vertex(New Vector2f(Pos.Left, Pos.Top), Colour, New Vector2f(Img.Left, Img.Top)))
        Vertex.Append(New Vertex(New Vector2f(Pos.Left + Pos.Width, Pos.Top), Colour, New Vector2f(Img.Left + Img.Width, Img.Top)))
        Vertex.Append(New Vertex(New Vector2f(Pos.Left, Pos.Top + Pos.Height), Colour, New Vector2f(Img.Left, Img.Top + Img.Height)))
        Vertex.Append(New Vertex(New Vector2f(Pos.Left + Pos.Width, Pos.Top + Pos.Height), Colour, New Vector2f(Img.Left + Img.Width, Img.Top + Img.Height)))
        Vertex.PrimitiveType = SFML.Graphics.PrimitiveType.TrianglesStrip

        ' Desenhar
        Dim render As RenderStates = RenderStates.Default
        render.Texture = GlobalTexture(textureNum).Textura
        If maskColor Then render.BlendMode = BlendMode.Add
        DeviceGame.Draw(Vertex, render)

        ' Tempo para esvaziar a memória
        GlobalTexture(textureNum).Timer = GetTickCount + 5000
    End Sub

    Public Sub RenderTexture(ByVal textureNum As Integer, ByVal Pos As IntRect, ByVal Img As IntRect, Optional ByVal maskColor As Boolean = False)
        ' Checa se está pronto pra usar
        GlobalTexture(textureNum).Check()

        ' Vertices
        Dim Vertex As VertexArray = New VertexArray()
        Vertex.Append(New Vertex(New Vector2f(Pos.Left, Pos.Top), Color.White, New Vector2f(Img.Left, Img.Top)))
        Vertex.Append(New Vertex(New Vector2f(Pos.Left + Pos.Width, Pos.Top), Color.White, New Vector2f(Img.Left + Img.Width, Img.Top)))
        Vertex.Append(New Vertex(New Vector2f(Pos.Left, Pos.Top + Pos.Height), Color.White, New Vector2f(Img.Left, Img.Top + Img.Height)))
        Vertex.Append(New Vertex(New Vector2f(Pos.Left + Pos.Width, Pos.Top + Pos.Height), Color.White, New Vector2f(Img.Left + Img.Width, Img.Top + Img.Height)))
        Vertex.PrimitiveType = SFML.Graphics.PrimitiveType.TrianglesStrip

        ' Desenhar
        Dim render As RenderStates = RenderStates.Default
        render.Texture = GlobalTexture(textureNum).Textura
        If maskColor Then render.BlendMode = BlendMode.Add
        DeviceGame.Draw(Vertex, render)

        ' Tempo para esvaziar a memória
        GlobalTexture(textureNum).Timer = GetTickCount + 5000
    End Sub

    Public Sub RenderBox(ByVal colour As Color, ByVal Retangulo As IntRect)
        ' Top
        RenderTexture(texDesign, New IntRect(Retangulo.Left + 1, Retangulo.Top, Retangulo.Width - 2, 2), New IntRect(1, 0, 15, 2), colour)

        ' Left
        RenderTexture(texDesign, New IntRect(Retangulo.Left, Retangulo.Top + 1, 1, Retangulo.Height - 2), New IntRect(0, 1, 1, 15), colour)
        RenderTexture(texDesign, New IntRect(Retangulo.Left + 1, Retangulo.Top + 2, 1, Retangulo.Height - 4), New IntRect(1, 1, 1, 15), colour)

        ' Bottom
        RenderTexture(texDesign, New IntRect(Retangulo.Left + 1, Retangulo.Top + Retangulo.Height - 2, Retangulo.Width - 2, 2), New IntRect(1, 16, 15, 2), colour)

        ' Right
        RenderTexture(texDesign, New IntRect(Retangulo.Left + Retangulo.Width - 1, Retangulo.Top + 1, 1, Retangulo.Height - 2), New IntRect(17, 1, 1, 15), colour)
        RenderTexture(texDesign, New IntRect(Retangulo.Left + Retangulo.Width - 2, Retangulo.Top + 2, 1, Retangulo.Height - 4), New IntRect(1, 1, 1, 15), colour)

        ' Center
        RenderTexture(texDesign, New IntRect(Retangulo.Left + 2, Retangulo.Top + 2, Retangulo.Width - 4, Retangulo.Height - 4), New IntRect(2, 2, 14, 14), colour)
    End Sub
End Module

Public Class clsGlobalTexture
    ' Dados
    Public Textura As Texture
    Public Width As Integer
    Public Height As Integer
    Public File As String
    Public Timer As Integer

    Public Sub Check()
        If Textura Is Nothing Then
            Textura = New Texture(File)
            Textura.Smooth = True
            Textura.Repeated = False
        End If
    End Sub

    Public ReadOnly Property Size As Vector2f
        Get
            Return New Vector2f(Width, Height)
        End Get
    End Property

    Public Sub CheckClear()
        If Timer > 0 Then
            If GetTickCount > Timer Then
                Textura = Nothing
                Timer = 0
            End If
        End If
    End Sub

    Public Sub New(ByVal fileName As String)
        If Not IO.File.Exists(fileName) Then
            MsgBox("Erro: Textura não existe!")
            Return
        End If
        File = fileName


        Dim imageInfo As Image = New Image(fileName)
        Width = imageInfo.Size.X
        Height = imageInfo.Size.Y
        imageInfo = Nothing
    End Sub
End Class
