Imports System.IO
Imports System.Net
Imports System.Drawing.Printing

Public Class Form1
    Private Sub LoginToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LoginToolStripMenuItem.Click
        If GlobalVariables.logged = False Then
            Form2.Show()
        Else
            LoginToolStripMenuItem.Text = "Login"
            GlobalVariables.user = ""
            GlobalVariables.password = ""
            GlobalVariables.logged = False
            TabControl1.TabPages(2).Enabled = False
            TabControl1.TabPages(3).Enabled = False
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Text = "Titolo"
        Dim request As WebRequest = _
        WebRequest.Create("http://192.168.1.6:5000/lista")
        Dim response As WebResponse = request.GetResponse()
        Dim dataStream As Stream = response.GetResponseStream()
        Dim areader As New StreamReader(dataStream)
        Dim lista As String = areader.ReadToEnd()
        RichTextBox1.Text = lista
        areader.Close()
        response.Close()
        TabControl1.TabPages(2).Enabled = False
        TabControl1.TabPages(3).Enabled = False
        ComboBox1.Text = "Titolo"
        ComboBox2.Text = "ISBN"
        ComboBox3.Text = "Presta"
    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Label2.Text = ComboBox1.Text
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = "Titolo" Then
            Dim request As WebRequest = _
            WebRequest.Create("http://192.168.1.6:5000/isbninfo/isbn/" + TextBox1.Text)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim areader As New StreamReader(dataStream)
            Dim isbn As String = areader.ReadToEnd()
            areader.Close()
            response.Close()
            request = _
            WebRequest.Create("http://192.168.1.6:5000/isbninfo/scheda/" + TextBox1.Text)
            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            TextBox2.Text = responseFromServer.Replace("Autore", Environment.NewLine + "Autore").Replace("Posizione", Environment.NewLine + "Posizione").Replace("ISBN", Environment.NewLine + "ISBN").Replace("Stato", Environment.NewLine + "Stato")
            reader.Close()
            response.Close()
            Dim url As String
            url = "http://192.168.1.6:5000/gbooks/" + isbn + "/copertina"
            Dim tClient As WebClient = New WebClient
            Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
            PictureBox1.Image = tImage
        Else
            Dim request As WebRequest = _
            WebRequest.Create("http://192.168.1.6:5000/isbninfo/titolo/" + TextBox1.Text)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim areader As New StreamReader(dataStream)
            Dim titolo As String = areader.ReadToEnd()
            areader.Close()
            response.Close()
            request = _
            WebRequest.Create("http://192.168.1.6:5000/isbninfo/scheda/" + titolo)
            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            TextBox2.Text = responseFromServer.Replace("Autore", Environment.NewLine + "Autore").Replace("Posizione", Environment.NewLine + "Posizione").Replace("ISBN", Environment.NewLine + "ISBN").Replace("Stato", Environment.NewLine + "Stato")
            reader.Close()
            response.Close()
            Dim url As String
            url = "http://192.168.1.6:5000/gbooks/" + TextBox1.Text + "/copertina"
            Dim tClient As WebClient = New WebClient
            Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
            PictureBox1.Image = tImage
        End If
    End Sub

    Private Sub InfoSuToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InfoSuToolStripMenuItem.Click
        MsgBox("BiblioDB Client.NET" + vbNewLine + "By Eugenio Tampieri", MsgBoxStyle.Information)
    End Sub

    Private Sub Label4_Click(sender As System.Object, e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Label3.Text = ComboBox2.Text
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim margins As New Margins(40, 40, 40, 40)
        PrintDocument1.DefaultPageSettings.Margins = margins
        e.Graphics.DrawString("", Font, Brushes.Black, 100, 200)
        e.Graphics.DrawString(RichTextBox2.Text, Font, Brushes.Black, 150, 250)
        'e.Graphics.DrawImage(Image.FromFile("C:\Creek.jpg"), 20, 50, 40, 60)
        'e.Graphics.DrawLine(Pens.Red, 100, 150, 300, 400)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim statoP As Integer
        Dim url As String
        If ComboBox3.Text = "Presta" Then
            statoP = "0"
        Else
            statoP = "1"
        End If
        If ComboBox2.Text = "ISBN" Then
            url = "http://192.168.1.6:5000/presta/" + GlobalVariables.user + "/" + GlobalVariables.password + "/" + TextBox3.Text + "/" + TextBox4.Text + "/" + Str(statoP)
            Dim request As WebRequest = _
            WebRequest.Create(url)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim areader As New StreamReader(dataStream)
            Dim titolo As String = areader.ReadToEnd()
            RichTextBox2.Text = titolo
            areader.Close()
            response.Close()
        ElseIf ComboBox2.Text = "Titolo" Then
            Dim request As WebRequest = _
            WebRequest.Create("http://192.168.1.6:5000/isbninfo/isbn/" + TextBox3.Text)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim areader As New StreamReader(dataStream)
            Dim isbn As String = areader.ReadToEnd().Replace(" ", "")
            areader.Close()
            response.Close()
            url = "http://192.168.1.6:5000/presta/" + GlobalVariables.user + "/" + GlobalVariables.password + "/" + isbn + "/" + TextBox4.Text + "/" + Str(statoP)
            request = _
            WebRequest.Create(url)
            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            RichTextBox2.Text = responseFromServer
            reader.Close()
            response.Close()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Button2.Text = ComboBox3.Text
    End Sub
End Class
Public Class GlobalVariables
    Public Shared user As String = ""
    Public Shared password As String = ""
    Public Shared logged As Boolean = False
    Public Shared ip As String
    Public Shared port As String
End Class