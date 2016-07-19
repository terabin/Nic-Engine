Module Core
    Public ServerRun As Boolean
    Public isUpdateListView As Boolean
    Public Sub ServerLoop()
        Dim tmrSocket As Integer
        Dim tmrIA As Integer

        While ServerRun
            If GetTickCount > tmrSocket Then
                For i = 1 To Options.MAX_PLAYERS
                    If Not Clients(i).Socket Is Nothing Then
                        If Not Clients(i).Socket.Connected Then
                            Call CloseSocket(i)
                        End If
                    End If
                Next
                tmrSocket = GetTickCount + 500
            End If

            ' IA
            If GetTickCount > tmrIA Then
                ProcessSpawnIA()
                tmrIA = GetTickCount + 350
            End If

            ' UpdateList
            UpdateListView()

            DoEvents()
            Sleep(1)
        End While

        ServerDestroy()
    End Sub

    Public Sub ServerDestroy()
        Application.Exit()
    End Sub

    Public Sub UpdateListView()
        If isUpdateListView Then
            For i As Integer = 1 To Options.MAX_PLAYERS
                If Not Clients(i).Socket Is Nothing Then
                    frmServer.lstView.Items(i - 1).SubItems(1).Text = Clients(i).IP
                    frmServer.lstView.Items(i - 1).SubItems(2).Text = Conta(i).Nome
                    frmServer.lstView.Items(i - 1).SubItems(3).Text = Player(i).Nome
                Else
                    frmServer.lstView.Items(i - 1).SubItems(1).Text = ""
                    frmServer.lstView.Items(i - 1).SubItems(2).Text = ""
                    frmServer.lstView.Items(i - 1).SubItems(3).Text = ""
                End If
            Next
            isUpdateListView = False
        End If
    End Sub
End Module
