Imports System.IO
Imports System.Net
Imports System.Net.Sockets

Public Class Client
    Public index As Long
    Public IP As String
    Public Socket As TcpClient
    Public myStream As NetworkStream
    Private readBuff As Byte()

    Public Sub Start()
        Socket.SendBufferSize = 4096
        Socket.ReceiveBufferSize = 4096
        myStream = Socket.GetStream()
        'ReDim readBuff(Socket.ReceiveBufferSize - 1)
        ReDim readBuff(8192)
        'myStream.ReadTimeout = 10
        'myStream.WriteTimeout = 10
        myStream.BeginRead(readBuff, 0, 8192, AddressOf OnReceiveData, Nothing)
    End Sub

    Private Sub OnReceiveData(ByVal ar As IAsyncResult)
        ' 
        Dim readbytes As Integer
        Try
            readbytes = myStream.EndRead(ar)
            If (readbytes <= 0) Then
                CloseSocket(index) 'Disconnect
                Exit Sub
            End If
        Catch
            'CloseSocket(index)
            'Return
        End Try
        Dim newBytes As Byte()
        ReDim newBytes(readbytes - 1)
        Buffer.BlockCopy(readBuff, 0, newBytes, 0, readbytes)
        HandleData(index, newBytes)
        Try
            'myStream.BeginRead(readBuff, 0, Socket.ReceiveBufferSize, AddressOf OnReceiveData, Nothing)
            myStream.BeginRead(readBuff, 0, 8192, AddressOf OnReceiveData, Nothing)
        Catch
            'Debug.Print("Erro")
            '   CloseSocket(index)
        End Try
    End Sub
End Class

Module Socket
    Public Clients() As Client
    Public ServerSocket As TcpListener

    Public Sub InitNetwork()
        ServerSocket = New TcpListener(IPAddress.Any, Options.Port)
        ServerSocket.Start()
        ServerSocket.BeginAcceptTcpClient(AddressOf OnClientConnect, Nothing)
    End Sub

    Private Sub OnClientConnect(ByVal ar As IAsyncResult)
        Try
            Dim client As TcpClient = ServerSocket.EndAcceptTcpClient(ar)
            client.NoDelay = False
            ServerSocket.BeginAcceptTcpClient(AddressOf OnClientConnect, Nothing)
            For i = 1 To Options.MAX_PLAYERS
                If Clients(i).index = 0 Then
                    Clients(i).Socket = client
                    Clients(i).index = i
                    Clients(i).IP = DirectCast(client.Client.RemoteEndPoint, IPEndPoint).Address.ToString
                    Clients(i).Start()
                    'TextAdd("Connection received from " & Clients(i).IP)
                    Exit For
                End If
            Next
        Catch
            Debug.Print("Erro")
        End Try

    End Sub

    Public Sub SendDataTo(ByVal Index As Long, ByRef Data() As Byte)
        If Not IsConnected(Index) Then Exit Sub
        Try
            Dim buffer As ByteBuffer
            buffer = New ByteBuffer
            buffer.WriteLong((UBound(Data) - LBound(Data)) + 1)
            buffer.WriteBytes(Data)
            Clients(Index).myStream.WriteTimeout = 10
            Clients(Index).myStream.Write(buffer.ToArray, 0, buffer.ToArray.Length)
            buffer = Nothing
        Catch
        End Try
    End Sub

    Public Sub SendDataToMap(ByVal MapID As Integer, ByRef Data() As Byte)
        For i As Short = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) Then
                If Player(i).Map = MapID Then
                    SendDataTo(i, Data)
                    ' DoEvents()
                End If
            End If
        Next
    End Sub

    Public Sub SendDataToMapBut(ByVal Index As Integer, ByRef Data() As Byte)
        For i As Short = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) And i <> Index Then
                If Player(i).Map = Player(Index).Map Then
                    SendDataTo(i, Data)
                End If
            End If
        Next
    End Sub

    Public Sub CloseSocket(ByVal Index As Long)
        If Index > 0 Then
            LeftGame(Index)
            If Not Clients(Index).Socket Is Nothing Then Clients(Index).Socket.Close()
            Conta(Index).Clear()
            Player(Index).Clear()
        End If
    End Sub

    Public Function IsConnected(ByVal Index As Integer) As Boolean
        If Not Clients(Index) Is Nothing Then
            Return Clients(Index).Socket.Connected
        Else
            Return False
        End If
    End Function

    Public Sub HandleData(ByVal index As Long, ByVal data() As Byte)
        Dim Buffer() As Byte
        Buffer = data.Clone
        Dim pLength As Long
        If index <= 0 Then Return

        If TempPlayer(index).Buffer Is Nothing Then TempPlayer(index).Buffer = New ByteBuffer

        'If GetPlayerAccess(index) <= 0 Then

        ' Check for data flooding
        'If TempPlayer(index).DataBytes > 1000 Then
        ' Exit Sub
        ' End If

        ' Check for packet flooding
        ' If TempPlayer(index).DataPackets > 25 Then
        ' Exit Sub
        ' End If
        ' End If

        ' Check if elapsed time has passed
        '  TempPlayer(index).DataBytes = TempPlayer(index).DataBytes + Buffer.Length
        ' If GetTickCount() >= TempPlayer(index).DataTimer Then
        ' TempPlayer(index).DataTimer = GetTickCount() + 1000
        ' TempPlayer(index).DataBytes = 0
        ' TempPlayer(index).DataPackets = 0
        ' End If


        'TempPlayer(index).Buffer.WriteBytes(Buffer)


        'If TempPlayer(index).Buffer.Count = 0 Then
        ' TempPlayer(index).Buffer.Clear()
        ' Exit Sub
        ' End If

        ' If TempPlayer(index).Buffer.Length >= 4 Then
        ' pLength = TempPlayer(index).Buffer.ReadLong(False)
        '
        ' If pLength <= 0 Then
        ' TempPlayer(index).Buffer.Clear()
        ' Exit Sub
        ' End If
        ' End If

        ' Do While pLength > 0 And pLength <= TempPlayer(index).Buffer.Length - 8

        'If pLength <= TempPlayer(index).Buffer.Length - 8 Then
        ' TempPlayer(index).Buffer.ReadLong()
        'data = TempPlayer(index).Buffer.ReadBytes(pLength)
        ' HandleDataPackets(index, data)
        ' End If

        ' pLength = 0

        ' If TempPlayer(index).Buffer.Length >= 4 Then
        'pLength = TempPlayer(index).Buffer.ReadLong(False)

        ' If pLength < 0 Then
        'TempPlayer(index).Buffer.Clear()
        ' Exit Sub
        '  End If
        '  End If

        ' Loop

        'TempPlayer(index).Buffer.Clear()

        'seperates the data just in case 2 packets or more came in at one time, handles the data and makes sure
        'no one is packet flooding and stuff

        'TempPlayer(index).DataBytes = TempPlayer(index).DataBytes + Buffer.Length
        'If GetTickCount >= TempPlayer(index).DataTimer Then
        ' TempPlayer(index).DataTimer = GetTickCount + 1000
        ' TempPlayer(index).DataBytes = 0
        ' TempPlayer(index).DataPackets = 0
        ' End If

        ' Get the data from the socket now
        'frmServer.Socket(index).GetData(Buffer(), vbUnicode, DataLength)
        'TempPlayer(index).Buffer.WriteBytes(Buffer)

        'If TempPlayer(index).Buffer.Length >= 8 Then
        ' pLength = TempPlayer(index).Buffer.ReadLong(False)

        ' If pLength < 0 Then
        ' Exit Sub
        ' End If
        ' End If

        ' Do While pLength > 0 And pLength <= TempPlayer(index).Buffer.Length - 8
        ' If pLength <= TempPlayer(index).Buffer.Length - 8 Then
        ' TempPlayer(index).DataPackets = TempPlayer(index).DataPackets + 1
        ' TempPlayer(index).Buffer.ReadLong()
        ' HandleDataPackets(index, TempPlayer(index).Buffer.ReadBytes(pLength))
        'End If

        'pLength = 0
        'If TempPlayer(index).Buffer.Length >= 8 Then
        ' pLength = TempPlayer(index).Buffer.ReadLong(False)
        '
        '       If pLength < 0 Then
        ' Exit Sub
        ' End If
        'End If
        'Loop

        'TempPlayer(index).Buffer.Clear()


        TempPlayer(index).Buffer.WriteBytes(Buffer)

        If TempPlayer(index).Buffer.Length >= 8 Then pLength = TempPlayer(index).Buffer.ReadLong(False)
        Do While pLength > 0 And pLength <= TempPlayer(index).Buffer.Length - 8
            If pLength <= TempPlayer(index).Buffer.Length - 8 Then
                TempPlayer(index).Buffer.ReadLong()
                HandleDataPackets(index, TempPlayer(index).Buffer.ReadBytes(pLength))
            End If

            pLength = 0
            If TempPlayer(index).Buffer.Length >= 8 Then pLength = TempPlayer(index).Buffer.ReadLong(False)
        Loop
        TempPlayer(index).Buffer.Clear()
        DoEvents()
    End Sub
End Module
