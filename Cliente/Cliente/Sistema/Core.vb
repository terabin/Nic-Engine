Module Core
    Public GameRun As Boolean
    Public GameFPS As Integer
    Public MyIndex As Integer

    Public Sub GameLoop()
        Dim tick As Integer
        Dim tmrFPS As Integer, FPS As Integer
        Dim tmrRender As Integer
        Dim tmrClearTexture As Integer

        GameRun = True

        While GameRun
            tick = GetTickCount

            If GetTickCount > tmrClearTexture Then
                For Each i As clsGlobalTexture In GlobalTexture
                    i.CheckClear()
                Next
                tmrClearTexture = GetTickCount + 1000
            End If

            If GetTickCount > TextBox.AnimationTimer Then
                TextBox.Animation = Not TextBox.Animation
                TextBox.AnimationTimer = GetTickCount + 250
            End If

            ' Core Audio
            Som.Core()

            ' Core Telas
            Tela_General.Core()

            ' Desenhar Telas
            If GetTickCount > tmrRender Then
                Tela_General.Draw()
                If frmEditor_Map.Visible Then RenderEditor_MapTile()
                tmrRender = GetTickCount + 2
            End If

            DoEvents()
            While GetTickCount < tick + 10
                Sleep(1)
            End While

            If GetTickCount > tmrFPS Then
                GameFPS = FPS
                FPS = 0
                tmrFPS = GetTickCount + 1000
            Else
                FPS += 1
            End If
        End While

        GameDestroy()
    End Sub

    Public Sub GameDestroy()
        Som.StopAll()
        Musica.Stop()
        Application.Exit()
    End Sub

    Public Sub CheckKeys()
        If pressKeyUp Then PlayerMove(Dirs.UP)
        If pressKeyDown Then PlayerMove(Dirs.DOWN)
        If pressKeyLeft Then PlayerMove(Dirs.LEFT)
        If pressKeyRight Then PlayerMove(Dirs.RIGHT)
    End Sub

    Public Function MapX(ByVal X As Short) As Short
        Return X - (TileView.Left * 32) - Camera.Left
        'Return X - Camera.Left
    End Function

    Public Function MapY(ByVal Y As Short) As Short
        Return Y - (TileView.Top * 32) - Camera.Top
        'Return Y - Camera.Top
    End Function

    Public Function isTryingMap(ByVal X As Short, ByVal Y As Short) As Boolean
        If X < 0 Or X > Map.MaxX Then Return False
        If Y < 0 Or Y > Map.MaxY Then Return False
        Return True
    End Function

    

End Module
