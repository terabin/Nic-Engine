Public Class Combat
    ''' <summary>
    ''' Usado para atacar um npc
    ''' </summary>
    Public Shared Sub PlayerToNpc(ByVal Index As Integer, ByVal spawnID As Short, ByVal Damage As Integer)
        Dim dados As SpawnData.SpawnItemData = MapNpc(Player(Index).Map)(spawnID)
        Dim mapID As Integer = Player(Index).Map

        If Damage >= dados.HP Then
            Dim Exp As Long = dados.Dados.EXP
            If Exp > 0 Then Player(Index).AddExp(Exp)
            ' Drop?
            For Each d As NpcData.DropData In dados.Dados.Drop
                If d.Num > 0 Then
                    If Rand(1, d.Chance) = 1 Then
                        Dim value As Integer = d.Value
                        If value > 1 Then value = Rand(value * 0.85, value)
                        SendDropItem(mapID, Drop(mapID).Add(d.Num, value, dados.X, dados.Y))
                    End If
                End If
            Next

            ' Ele morreu caralho
            dados.HP = 0
            dados.timerRespawn = GetTickCount + (dados.Dados.SpawnTime * 1000)
            dados.Dead = True
            SendNpcSpawn(Index, spawnID)
        Else
            dados.HP -= Damage
            dados.Alvo = Index
            SendNpcVital(mapID, spawnID)
        End If

        SendMsgAnim(Index, "-" & Damage, dados.X * 32, (dados.Y * 32) - 30, Color.White.ToArgb)
    End Sub

    ''' <summary>
    ''' Usado quando o Npc ataca o Jogador
    ''' </summary>
    Public Shared Sub NpcToPlayer(ByVal mapID As Integer, ByVal spawnID As Short, ByVal Damage As Integer)
        Dim alvo As clsPlayer = Player(MapNpc(mapID)(spawnID).Alvo)

        If Damage >= alvo.HP Then
            Dim calcEXP As Long = alvo.EXP * 0.05 ' Perder 5% da exp
            'sendmsg
            alvo.EXP -= calcEXP
            alvo.Dead() ' Morrer hue
        Else
            alvo.HP -= Damage
            SendPlayerVitalHP(alvo.MyIndex)
        End If
        SendMsgAnim(alvo.MyIndex, "-" & Damage, alvo.X * 32 + 18, alvo.Y * 32 - 16, Color.Red.ToArgb)
    End Sub
End Class
