Module modPlayer
    Public Conta() As clsConta
    Public Player() As clsPlayer
    Public TempPlayer() As clsTempPlayer

    ''' <summary>
    ''' Quantia de <c>Pontos</c> que o jogador vai ganhar por <c>level</c>!
    ''' </summary>
    Public Const PLAYER_POINT_LEVEL As Short = 3

    Public Sub JoinGame(ByVal Index As Integer)
        TempPlayer(Index).InGame = True
        isUpdateListView = True

        ' Enviar Database
        SendAnimations(Index)
        SendNpcs(Index)
        SendItems(Index)

        ' Enviar Dados
        SendInventoy(Index)

        ' Coloca o Jogador no Mapa
        PlayerWarp(Index, Player(Index).Map, Player(Index).X, Player(Index).Y)

        ' Começa o jogo hue
        SendInGame(Index)
    End Sub

    Public Sub PlayerWarp(ByVal Index As Integer, ByVal MapID As Short, ByVal X As Short, ByVal Y As Short)

        ' Atualiza dados do atual mapa para all
        Player(Index).Map = MapID
        Player(Index).X = X
        Player(Index).Y = Y
        SendPlayerXY(Index)

        ' Check Map
        SendCheckMap(Index)
    End Sub

    Public Sub LeftGame(ByVal Index As Integer)
        If TempPlayer(Index).InGame Then
            TempPlayer(Index).InGame = False
            Player(Index).Save()
            Player(Index).Clear()
            isUpdateListView = True
        End If
    End Sub

    Public Sub ProcessPlayerAttack(ByVal Index As Integer)
        Dim X As Short = Player(Index).X
        Dim Y As Short = Player(Index).Y
        Dim Damage As Integer = 0

        Select Case Player(Index).Dir
            Case Dirs.UP : Y -= 1
            Case Dirs.DOWN : Y += 1
            Case Dirs.LEFT : X -= 1
            Case Dirs.RIGHT : X += 1
        End Select

        ' NPC
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            If MapNpc(Player(Index).Map)(i).Num > 0 Then
                If Not MapNpc(Player(Index).Map)(i).Dead And MapNpc(Player(Index).Map)(i).HP > 0 Then
                    If MapNpc(Player(Index).Map)(i).X = X And MapNpc(Player(Index).Map)(i).Y = Y Then
                        Damage = Player(Index).Damage - MapNpc(Player(Index).Map)(i).Dados.CON
                        If Damage > 0 Then
                            Combat.PlayerToNpc(Index, i, Damage)
                            GoTo endPoint
                        End If
                    End If
                End If
            End If
        Next


endPoint:
        ' Enviar frame de ataque
        SendAttack(Index)
    End Sub

    Public ReadOnly Property IsPlaying(ByVal Index As Integer) As Boolean
        Get
            Return TempPlayer(Index).InGame
        End Get
    End Property

    Public Property Inventory(ByVal Index As Integer, ByVal ID As Short) As clsInv
        Get
            Return Player(Index).Inv(ID)
        End Get
        Set(ByVal value As clsInv)
            Player(Index).Inv(ID) = value
        End Set
    End Property

End Module


#Region "Estrutura"
Public Class clsConta
    Public Nome As String
    Public Senha As String
    Public CharID(5) As String
    Public Vip As Byte
    Public VipDate As Date

    Public Sub New()
        Clear()
    End Sub

    Public Sub Load()
        Dim fileName As String = Application.StartupPath & "\Data\Conta\" & Nome & ".bin"
        Dim s As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using r As New IO.BinaryReader(s)
            Nome = r.ReadString
            Senha = r.ReadString

            For i As Short = 0 To 4
                CharID(i) = r.ReadString
            Next
            Vip = r.ReadByte
            VipDate = Date.Parse(r.ReadString)
            r.Close()
        End Using

    End Sub

    Public Sub Save()
        Dim fileName As String = Application.StartupPath & "\Data\Conta\" & Nome & ".bin"
        Dim s As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using w As New IO.BinaryWriter(s)
            w.Write(Nome)
            w.Write(Senha)

            For i As Short = 0 To 4
                w.Write(CharID(i))
            Next
            w.Write(Vip)
            w.Write(VipDate.ToShortTimeString)
            w.Close()
        End Using
    End Sub

    Public Sub Clear()
        Nome = ""
        Senha = ""
        Vip = 0
        VipDate = Date.Parse("2000-01-01")
        For i As Short = 0 To 4
            CharID(i) = ""
        Next
    End Sub
End Class

Public Class clsPlayer

#Region "Dados"
    Public Nome As String
    Public Classe As Short
    Public Sprite As Short
    Public Level As Short
    Public Map As Short
    Public X As Short
    Public Y As Short
    Public Dir As Byte
    Private sSTR As Integer
    Private sCON As Integer
    Private sHP As Integer
    Private sMP As Integer
    Public Points As Integer
    Public EXP As Long
    Public Access As Byte
    Public PK As Byte
    Public PKCount As Short
    Public PVPCount As Short
    Public Inv() As clsInv
#End Region

#Region "Property"
    Public Property STR(Optional ByVal RAW As Boolean = False) As Integer
        Get
            If RAW Then
                Return sSTR
            Else
                Return sSTR
            End If
        End Get
        Set(ByVal value As Integer)
            sSTR = value
        End Set
    End Property

    Public Property CON(Optional ByVal RAW As Boolean = False) As Integer
        Get
            If RAW Then
                Return sCON
            Else
                Return sCON
            End If
        End Get
        Set(ByVal value As Integer)
            sCON = value
        End Set
    End Property

    Public Property HP As Integer
        Get
            Return sHP
        End Get
        Set(ByVal value As Integer)
            sHP = value
        End Set
    End Property

    Public Property MP As Integer
        Get
            Return sMP
        End Get
        Set(ByVal value As Integer)
            sMP = value
        End Set
    End Property

    Public ReadOnly Property GetInvCount As Short
        Get
            Return Inv.Length - 1
        End Get
    End Property

    Public ReadOnly Property GetInvMax As Short
        Get
            Return 30
        End Get
    End Property

    ''' <summary>
    ''' Dano Melee do Jogador
    ''' </summary>
    Public ReadOnly Property Damage As Integer
        Get
            Return (STR * 0.7) + Rand(STR * 0.05 * -1, STR * 0.05)
        End Get
    End Property

    ''' <summary>
    ''' Valor máximo da Vida do jogador
    ''' </summary>
    Public ReadOnly Property MaxHP As Integer
        Get
            Return 100 + ((50 * (Level - 1)) * (Level \ 5))
        End Get
    End Property

    ''' <summary>
    ''' Valor máximo da Mana do jogador
    ''' </summary>
    Public ReadOnly Property MaxMP As Integer
        Get
            Return 60 + ((30 * (Level - 1)) * (Level \ 5))
        End Get
    End Property

    ''' <summary>
    ''' Retorna o calculo de EXP para o próximo level do jogador
    ''' </summary>
    Public ReadOnly Property NextLevel As Long
        Get
            Return 50 + ((15 * (Level - 1)) * (Level \ 5))
        End Get
    End Property

    ''' <summary>
    ''' Índice do Jogador
    ''' </summary>
    Public ReadOnly Property MyIndex As Integer
        Get
            Return Array.IndexOf(Player, Me)
        End Get
    End Property

    ''' <summary>
    ''' Calculo de Defesa do Jogador
    ''' </summary>
    Public ReadOnly Property Defesa As Integer
        Get
            Dim calc As Integer = CON * 0.6 + Rand(CON * 0.05 * -1, CON * 0.05)
            If calc < 0 Then calc = 0
            Return calc
        End Get
    End Property
#End Region

#Region "Method"
    Public Sub New()
        Clear()
    End Sub

    Public Sub Clear()
        Nome = ""
        Classe = 1
        Sprite = 1
        Level = 1
        Map = 1
        X = 0
        Y = 0
        Dir = 0
        sSTR = 0
        sCON = 0
        sHP = 100
        sMP = 50
        Points = 0
        EXP = 0
        Access = 0
        PK = 0
        PKCount = 0
        PVPCount = 0

        ReDim Inv(Options.MAX_INV)
        For i As Short = 0 To Options.MAX_INV - 1
            Inv(i) = New clsInv
        Next
    End Sub

    Public Sub Save()
        Dim fileName As String = Application.StartupPath & "\Data\Personagem\" & Nome & ".bin"
        Dim s As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using w As New IO.BinaryWriter(s)
            w.Write(Nome)
            w.Write(Classe)
            w.Write(Sprite)
            w.Write(Level)
            w.Write(Map)
            w.Write(X)
            w.Write(Y)
            w.Write(Dir)
            w.Write(STR(True))
            w.Write(CON(True))
            w.Write(HP)
            w.Write(MP)
            w.Write(Points)
            w.Write(EXP)
            w.Write(Access)
            w.Write(PK)
            w.Write(PKCount)
            w.Write(PVPCount)

            For i As Short = 0 To Options.MAX_INV - 1
                w.Write(Inv(i).Num)
                w.Write(Inv(i).Value)
                For o As Short = 0 To Options.MAX_INV_GEMA - 1
                    w.Write(Inv(i).GemaSlot(o))
                Next
            Next
        End Using
    End Sub

    Public Sub Load(Optional ByVal isNome As Boolean = False)
        Dim fileName As String = Application.StartupPath & "\Data\Personagem\" & Nome & ".bin"
        If Not IO.File.Exists(fileName) Then Return
        Dim s As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using r As New IO.BinaryReader(s)
            Nome = r.ReadString
            Classe = r.ReadInt16
            Sprite = r.ReadInt16
            Level = r.ReadInt16
            Map = r.ReadInt16
            X = r.ReadInt16
            Y = r.ReadInt16
            Dir = r.ReadByte
            STR = r.ReadInt32
            CON = r.ReadInt32
            HP = r.ReadInt32
            MP = r.ReadInt32
            Points = r.ReadInt32
            EXP = r.ReadInt64
            Access = r.ReadByte
            PK = r.ReadByte
            PKCount = r.ReadInt16
            PVPCount = r.ReadInt16

            For i As Short = 0 To Options.MAX_INV - 1
                Inv(i).Num = r.ReadInt16
                Inv(i).Value = r.ReadInt32

                For o As Short = 0 To Options.MAX_INV_GEMA - 1
                    Inv(i).GemaSlot(o) = r.ReadInt32
                Next
            Next
            r.Close()
        End Using

    End Sub

    ''' <summary>
    ''' Adiciona e calcula a EXP para o level do jogador
    ''' </summary>
    Public Sub AddExp(ByVal value As Long)
        Dim countLevel As Short = 0
        EXP += value

        While EXP >= NextLevel
            Dim result As Long = EXP - NextLevel
            countLevel += 1

            ' Retorna o Calculo da EXP
            EXP = result
        End While

        If countLevel > 0 Then
            SendMsgAnim(MyIndex, "Level UP", X * 32, (Y * 32) - 16, Color.White.ToArgb)
            Points += PLAYER_POINT_LEVEL * countLevel
            SendPlayerData(MyIndex)
        End If
    End Sub

    ''' <summary>
    ''' Função de quando o Jogador morre
    ''' </summary>
    Public Sub Dead()
        HP = MaxHP
        PlayerWarp(MyIndex, Classe_Data.Classe(Classe).Mapa, Classe_Data.Classe(Classe).X, Classe_Data.Classe(Classe).Y)
    End Sub
#Region "Inventory"
    Public Function AddInvItem(ByVal ItemID As Integer) As clsPlayer
        If CBool(Item(ItemID).isStack) Then
            For i As Short = 0 To Options.MAX_INV - 1
                If Inv(i).Num = ItemID Then
                    With Inv(i)
                        .Value += 1
                    End With
                    SendInvSlotUpdate(Array.IndexOf(Player, Me), i)
                    Return Me
                End If
            Next

            ' Caso não tenha o item
            For i As Short = 0 To Options.MAX_INV - 1
                If Inv(i).Num = 0 Then
                    With Inv(i)
                        .Num = ItemID
                        .Value = 1
                        For o As Short = 0 To Options.MAX_INV_GEMA - 1
                            .GemaSlot(o) = 0
                        Next
                    End With
                    SendInvSlotUpdate(Array.IndexOf(Player, Me), i)
                    Return Me
                End If
            Next
        Else
            For i As Short = 0 To Options.MAX_INV - 1
                If Inv(i).Num = 0 Then
                    With Inv(i)
                        .Num = ItemID
                        .Value = 1
                        For o As Short = 0 To Options.MAX_INV_GEMA - 1
                            .GemaSlot(o) = 0
                        Next
                    End With
                    SendInvSlotUpdate(Array.IndexOf(Player, Me), i)
                    Return Me
                End If
            Next
        End If
        Return Me
    End Function

    Public Function AddInvItem(ByVal ItemID As Integer, ByVal Value As Integer) As clsPlayer
        If CBool(Item(ItemID).isStack) Then
            For i As Short = 0 To Options.MAX_INV - 1
                If Inv(i).Num = ItemID Then
                    With Inv(i)
                        .Value += Value
                    End With
                    SendInvSlotUpdate(Array.IndexOf(Player, Me), i)
                    Return Me
                End If
            Next

            ' Caso não tenha o item
            For i As Short = 0 To Options.MAX_INV - 1
                If Inv(i).Num = 0 Then
                    With Inv(i)
                        .Num = ItemID
                        .Value = Value
                        For o As Short = 0 To Options.MAX_INV_GEMA - 1
                            .GemaSlot(o) = 0
                        Next
                    End With
                    SendInvSlotUpdate(Array.IndexOf(Player, Me), i)
                    Return Me
                End If
            Next
        Else
            For i As Short = 0 To Options.MAX_INV - 1
                If Inv(i).Num = 0 Then
                    With Inv(i)
                        .Num = ItemID
                        .Value = Value
                        For o As Short = 0 To Options.MAX_INV_GEMA - 1
                            .GemaSlot(o) = 0
                        Next
                    End With
                    SendInvSlotUpdate(Array.IndexOf(Player, Me), i)
                    Return Me
                End If
            Next
        End If
        Return Me
    End Function

    Public Function RemoveInvSlot(ByVal invID As Short) As clsPlayer
        Inv(invID).Value -= 1
        If Inv(invID).Value <= 0 Then
            Inv(invID).Num = 0
            For o As Short = 0 To Options.MAX_INV_GEMA - 1
                Inv(invID).GemaSlot(o) = 0
            Next
        End If
        SendInvSlotUpdate(Array.IndexOf(Player, Me), invID)
        Return Me
    End Function

    Public Function RemoveInvSlot(ByVal invID As Short, ByVal Value As Short) As clsPlayer
        Inv(invID).Value -= Value
        If Inv(invID).Value <= 0 Then
            Inv(invID).Num = 0
            For o As Short = 0 To Options.MAX_INV_GEMA - 1
                Inv(invID).GemaSlot(o) = 0
            Next
        End If
        SendInvSlotUpdate(Array.IndexOf(Player, Me), invID)
        Return Me
    End Function
#End Region
#End Region
End Class

Public Class clsTempPlayer
    ' Network
    Public Buffer As ByteBuffer
    Public DataBytes As Integer
    Public DataPackets As Integer
    Public DataTimer As Integer

    ' Game
    Public InGame As Boolean
End Class

Public Class clsInv
    Public Num As Short
    Public Value As Integer
    Public GemaSlot() As Integer

#Region "Property"
    Public ReadOnly Property Dados As ItemData
        Get
            If Num > 0 Then
                Return Item(Num)
            End If
            Return Nothing
        End Get
    End Property
#End Region

#Region "Method"
    Public Sub New()
        Num = 0
        Value = 0
        ReDim GemaSlot(Options.MAX_INV_GEMA)

        For i As Short = 0 To Options.MAX_INV_GEMA - 1
            GemaSlot(i) = 0
        Next
    End Sub
#End Region
End Class
#End Region