Imports System.IO
Imports System.IO.Compression
Module Memoria
    Public Function Compress(ByVal b() As Byte) As Byte()
        Dim ms As New System.IO.MemoryStream()
        Dim gzipstream As New Compression.GZipStream(ms, CompressionMode.Compress)
        gzipstream.Write(b, 0, b.Length)
        gzipstream.Flush()
        gzipstream.Close()
        Dim ret() As Byte = ms.ToArray()
        gzipstream.Close()
        gzipstream.Dispose()
        ms.Close()
        ms.Dispose()
        Return ret
    End Function

    Public Function Decompress(ByVal bytes() As Byte) As Byte()
        Dim ms As New MemoryStream(bytes)
        Dim gz As New GZipStream(ms, CompressionMode.Decompress)
        Dim bt(3) As Byte
        ms.Position = ms.Length - 4
        ms.Read(bt, 0, 4)
        ms.Position = 0
        Dim size As Integer = BitConverter.ToInt32(bt, 0)
        Dim buffer(size + 100) As Byte
        Dim offset As Integer = 0
        Dim total As Integer = 0
        While (True)
            Dim j As Integer = gz.Read(buffer, offset, 100)
            If j = 0 Then Exit While
            offset += j
            total += j
        End While
        gz.Close()
        gz.Dispose()
        ms.Close()
        ms.Dispose()
        Dim ra(total - 1) As Byte
        Array.ConstrainedCopy(buffer, 0, ra, 0, total)
        Return ra
    End Function

    Public Function GetObjectIntPtr(ByVal obj As Object) As IntPtr
        'Dim ms As New MemoryStream
        'Dim s As New StreamWriter(ms)
        's.Write(obj)
        'Return (ms.ToArray)
        'Return obj.ToString
        Dim gc As System.Runtime.InteropServices.GCHandle
        gc = System.Runtime.InteropServices.GCHandle.Alloc(obj)
        Return gc
    End Function

    Public Function GetIntPtrtoObject(ByVal b As IntPtr, ByVal tipo As System.Type) As Object
        'Dim ms As New MemoryStream
        'Dim bf As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        'ms.Write(b, 0, b.Length)
        'ms.Seek(0, SeekOrigin.Begin)
        'Return bf.Deserialize(ms)
        ' Return Convert.ChangeType(b, type)
        Dim gc As System.Runtime.InteropServices.GCHandle
        gc = b
        GetIntPtrtoObject = gc.Target

    End Function
End Module
