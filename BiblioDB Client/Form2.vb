Imports System.IO
Imports System.Net
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text

Public Class Form2
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim strToHash As String
        strToHash = TextBox2.Text
        Dim sha1Obj As New SHA512CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)
        bytesToHash = sha1Obj.ComputeHash(bytesToHash)
        Dim strResult As String = ""
        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next
        Dim request As WebRequest = _
        WebRequest.Create("http://192.168.1.6:5000/auth/" + TextBox1.Text + "/" + strResult.ToString)
        Dim response As WebResponse = request.GetResponse()
        Dim dataStream As Stream = response.GetResponseStream()
        Dim areader As New StreamReader(dataStream)
        Dim titolo As String = areader.ReadToEnd()
        If titolo = 1 Then
            MsgBox("Si è entrati nel sistema", MsgBoxStyle.Information)
            GlobalVariables.user = TextBox1.Text
            GlobalVariables.password = strResult.ToString
            GlobalVariables.logged = True
            Form1.LoginToolStripMenuItem.Text = "Logout"
            Form1.TabControl1.TabPages(2).Enabled = True
            Form1.TabControl1.TabPages(3).Enabled = True
            Me.Close()
        ElseIf titolo = 2 Then
            MsgBox("Terminale non autorizzato!", MsgBoxStyle.Critical)
        ElseIf titolo = 3 Then
            MsgBox("Password errata!", MsgBoxStyle.Exclamation)
        End If
        areader.Close()
        response.Close()
    End Sub
End Class
