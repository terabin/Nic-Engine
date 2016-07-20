Module General
    Public CharData As clsCharData

    Public Sub Main()
        ' Sortear Semente
        Randomize()

        ' Carregar Opções
        Options.Load()

        ' Preparar
        PrepararDados()

        ' Iniciando Gráficos
        InitGraficos()

        ' Inicia Som e Musicas
        Som.Init()

        ' Iniciar Tela
        Tela_General.Init()

        ' Rodar
        CharData = New clsCharData
        frmMain.Text = Options.GAMENAME
        frmMain.Show()
        GameLoop()
    End Sub

    Private Sub PrepararDados()
        ' Player
        ReDim Player(Options.MAX_PLAYERS + 1)
        For i As Short = 0 To Options.MAX_PLAYERS
            Player(i) = New clsPlayer()
        Next

        ' Npc
        ReDim Npc(Options.MAX_NPC + 1)
        For i As Short = 1 To Options.MAX_NPC
            Npc(i) = New NpcData
        Next

        ' Item
        ReDim Item(Options.MAX_ITEM + 1)
        For i As Short = 1 To Options.MAX_ITEM
            Item(i) = New ItemData
        Next

        ' Spawn
        Spawn = New cSpawn

        ' Drops
        Drop = New DropData

        ' Mensagem Animada
        MsgAnim = New MsgAnimBuffer

        ' Animação
        AnimationBuffer = New AnimBuffer
        ReDim Animation(Options.MAX_ANIMATION + 1)
        For i As Short = 1 To Options.MAX_ANIMATION
            Animation(i) = New AnimData
        Next

        ' Skills
        Skill.Add(Nothing)
        For i As Short = 1 To Options.MAX_SKILL
            Skill.Add(New SkillData)
        Next

    End Sub

    Public ReadOnly Property GetKeyName(ByVal idKey As Integer) As String
        Get
            Select Case idKey
                Case 187 : Return "="
                Case 189 : Return "-"
                Case Else
                    Return Convert.ToChar(idKey).ToString
            End Select
        End Get
    End Property

    Public ReadOnly Property GetTickCount As Long
        Get
            Return System.Environment.TickCount
        End Get
    End Property

    Public Sub DoEvents()
        Application.DoEvents()
    End Sub

    Public Sub Sleep(ByVal miliseconds As Long)
        Threading.Thread.Sleep(miliseconds)
    End Sub

    Public ReadOnly Property Colisao(ByVal point As SFML.System.Vector2f, ByVal rec As SFML.Graphics.IntRect) As Boolean
        Get
            If point.X >= rec.Left And point.X <= rec.Left + rec.Width Then
                If point.Y >= rec.Top And point.Y <= rec.Top + rec.Height Then
                    Return True
                End If
            End If
            Return False
        End Get
    End Property
End Module
