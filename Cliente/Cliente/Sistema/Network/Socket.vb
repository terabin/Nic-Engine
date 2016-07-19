Imports System.Net.Sockets
Imports System.IO
Module Socket
    Private PlayerBuffer As ByteBuffer = New ByteBuffer
    Public PlayerSocket As TcpClient
    Private myStream As NetworkStream
    Private myReader As StreamReader
    Private myWriter As StreamWriter
    Private asyncBuff As Byte()

    Public Sub Connect()
        If Not PlayerSocket Is Nothing Then
            If PlayerSocket.Connected Then Exit Sub
            PlayerSocket.Close()
            PlayerSocket = Nothing
        End If
        PlayerSocket = New TcpClient()
        PlayerSocket.ReceiveBufferSize = 4096
        PlayerSocket.SendBufferSize = 4096
        PlayerSocket.NoDelay = False
        ReDim asyncBuff(8192)
        PlayerSocket.BeginConnect(Options.IP, Options.Port, New AsyncCallback(AddressOf connectCallback), PlayerSocket)
        'SckConnecting = True
    End Sub

    Sub connectCallback(ByVal asyncConnect As IAsyncResult)
        Try
            PlayerSocket.EndConnect(asyncConnect)
            If PlayerSocket.Connected Then

                PlayerSocket.NoDelay = True
                myStream = PlayerSocket.GetStream()
                myStream.BeginRead(asyncBuff, 0, 8192, AddressOf OnReceive, Nothing)
            End If
        Catch
        End Try
    End Sub

    Sub OnReceive(ByVal ar As IAsyncResult)
        Try
            Dim byteAmt As Integer
            If Not myStream Is Nothing Then byteAmt = myStream.EndRead(ar)
            Dim myBytes() As Byte
            ReDim myBytes(byteAmt - 1)
            Buffer.BlockCopy(asyncBuff, 0, myBytes, 0, byteAmt)
            If byteAmt = 0 Then Exit Sub
            HandleData(myBytes)
            myStream.BeginRead(asyncBuff, 0, 8192, AddressOf OnReceive, Nothing)
        Catch
            'Debug.Print("Erro Packet")
            'GameDestroy()
        End Try
    End Sub

    Public Sub HandleData(ByVal data() As Byte)
        Dim Buffer() As Byte
        Buffer = data.Clone
        Dim pLength As Long
        ' Dim tmpdata() As Byte
        'Dim i As Long
        If PlayerBuffer Is Nothing Then PlayerBuffer = New ByteBuffer
        PlayerBuffer.WriteBytes(Buffer)

        If PlayerBuffer.Length >= 8 Then pLength = PlayerBuffer.ReadLong(False)
        Do While pLength > 0 And pLength <= PlayerBuffer.Length - 8
            If pLength <= PlayerBuffer.Length - 8 Then
                PlayerBuffer.ReadLong()
                HandleDataPackets(PlayerBuffer.ReadBytes(pLength))
            End If

            pLength = 0
            If PlayerBuffer.Length >= 8 Then pLength = PlayerBuffer.ReadLong(False)
        Loop
        PlayerBuffer.Clear()
        DoEvents()



        ' If PlayerBuffer Is Nothing Then PlayerBuffer = New ByteBuffer
        ' PlayerBuffer.WriteBytes(Buffer)

        ' If PlayerBuffer.Count = 0 Then
        '     PlayerBuffer.Clear()
        '     Exit Sub
        ' End If

        ' If PlayerBuffer.Length >= 4 Then
        '  pLength = PlayerBuffer.ReadLong(False)

        '     If pLength <= 0 Then
        '         PlayerBuffer.Clear()
        '         Exit Sub
        '     End If
        ' End If

        'Do While pLength > 0 And pLength <= PlayerBuffer.Length - 8

        ' If pLength <= PlayerBuffer.Length - 8 Then
        '     PlayerBuffer.ReadLong()
        '     data = PlayerBuffer.ReadBytes(pLength)
        '     HandleDataPackets(data)
        ' End If

        ' pLength = 0

        '           If PlayerBuffer.Length >= 8 Then
        '                pLength = PlayerBuffer.ReadLong(False)

        '        If pLength < 0 Then
        '            Exit Sub
        '        End If
        '    Else
        '        If PlayerBuffer.GetReadPos >= PlayerBuffer.Length Then
        '            PlayerBuffer.Clear()
        '            Exit Sub
        '        Else
        '            If Buffer(PlayerBuffer.GetReadPos) > 0 Then
        '                i = PlayerBuffer.GetReadPos
        '                ReDim tmpdata(PlayerBuffer.Length - 1)
        '                Array.Copy(Buffer, PlayerBuffer.GetReadPos, tmpdata, 0, PlayerBuffer.Length - 1)
        '                PlayerBuffer.Clear()
        '                PlayerBuffer.WriteBytes(tmpdata)
        '                Exit Sub
        '            End If
        '        End If
        '    End If
        'Loop

        'If pLength <= 1 Then PlayerBuffer.Clear()
    End Sub

    Public Sub SendData(ByVal bytes() As Byte)
        Try
            Dim buffer As ByteBuffer
            buffer = New ByteBuffer


            ' Enviar
            buffer = New ByteBuffer
            buffer.WriteLong(UBound(bytes) - LBound(bytes) + 1)

            buffer.WriteBytes(bytes)
            'Send data in the socket stream to the server
            myStream.WriteTimeout = 10
            myStream.Write(buffer.ToArray, 0, buffer.ToArray.Length)

            buffer = Nothing
        Catch
        End Try
    End Sub

    Public ReadOnly Property IsConnected As Boolean
        Get
            Return PlayerSocket.Connected
        End Get
    End Property

    Public ReadOnly Property IsPlaying(ByVal Index As Integer) As Boolean
        Get
            Return Not Player(Index).Nome.Length.Equals(0)
        End Get
    End Property

End Module
