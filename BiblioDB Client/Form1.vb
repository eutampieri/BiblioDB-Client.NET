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
        WebRequest.Create("http://" + My.Settings.IP + ":" + My.Settings.Port + "/lista")
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
        If My.Settings.ImgPath = "" Then
            GlobalVariables.logo = GlobalVariables.logo
        Else
            GlobalVariables.logo = My.Settings.ImgPath
        End If
    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Label2.Text = ComboBox1.Text
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = "Titolo" Then
            Dim request As WebRequest = _
            WebRequest.Create("http://" + My.Settings.IP + ":" + My.Settings.Port + "/isbninfo/isbn/" + TextBox1.Text)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim areader As New StreamReader(dataStream)
            Dim isbn As String = areader.ReadToEnd()
            areader.Close()
            response.Close()
            request = _
            WebRequest.Create("http://" + My.Settings.IP + ":" + My.Settings.Port + "/isbninfo/scheda/" + TextBox1.Text)
            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            TextBox2.Text = responseFromServer.Replace("Autore", Environment.NewLine + "Autore").Replace("Posizione", Environment.NewLine + "Posizione").Replace("ISBN", Environment.NewLine + "ISBN").Replace("Stato", Environment.NewLine + "Stato")
            reader.Close()
            response.Close()
            Dim url As String
            url = "http://" + My.Settings.IP + ":" + My.Settings.Port + "/gbooks/" + isbn + "/copertina"
            Dim tClient As WebClient = New WebClient
            Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))
            PictureBox1.Image = tImage
        Else
            Dim request As WebRequest = _
            WebRequest.Create("http://" + My.Settings.IP + ":" + My.Settings.Port + "/isbninfo/titolo/" + TextBox1.Text)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim areader As New StreamReader(dataStream)
            Dim titolo As String = areader.ReadToEnd()
            areader.Close()
            response.Close()
            request = _
            WebRequest.Create("http://" + My.Settings.IP + ":" + My.Settings.Port + "/isbninfo/scheda/" + titolo)
            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            TextBox2.Text = responseFromServer.Replace("Autore", Environment.NewLine + "Autore").Replace("Posizione", Environment.NewLine + "Posizione").Replace("ISBN", Environment.NewLine + "ISBN").Replace("Stato", Environment.NewLine + "Stato")
            reader.Close()
            response.Close()
            Dim url As String
            url = "http://" + My.Settings.IP + ":" + My.Settings.Port + "/gbooks/" + TextBox1.Text + "/copertina"
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
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawImage(Image.FromFile(GlobalVariables.logo), 100, 50, Image.FromFile(GlobalVariables.logo).Width, Image.FromFile(GlobalVariables.logo).Height)
        e.Graphics.DrawString(My.Settings.BiblioString, Font, Brushes.Black, 100, Image.FromFile(GlobalVariables.logo).Height + 60)
        e.Graphics.DrawLine(Pens.Gray, 100, Image.FromFile(GlobalVariables.logo).Height + 80, 300, Image.FromFile(GlobalVariables.logo).Height + 80)
        e.Graphics.DrawString(RichTextBox2.Text, Font, Brushes.Black, 100, Image.FromFile(GlobalVariables.logo).Height + 85)
        '
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
            url = "http://" + My.Settings.IP + ":" + My.Settings.Port + "/presta/" + GlobalVariables.user + "/" + GlobalVariables.password + "/" + TextBox3.Text + "/" + TextBox4.Text + "/" + Str(statoP)
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
            WebRequest.Create("http://" + My.Settings.IP + ":" + My.Settings.Port + "/isbninfo/isbn/" + TextBox3.Text)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim areader As New StreamReader(dataStream)
            Dim isbn As String = areader.ReadToEnd().Replace(" ", "")
            areader.Close()
            response.Close()
            url = "http://" + My.Settings.IP + ":" + My.Settings.Port + "/presta/" + GlobalVariables.user + "/" + GlobalVariables.password + "/" + isbn + "/" + TextBox4.Text + "/" + Str(statoP)
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

    Private Sub OpzioniToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpzioniToolStripMenuItem.Click
        Form3.Show()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim request As WebRequest = _
            WebRequest.Create("http://" + My.Settings.IP + ":" + My.Settings.Port + "/add/" + GlobalVariables.user + "/" + GlobalVariables.password + "/" + TextBox5.Text + "/" + TextBox7.Text + "/" + TextBox6.Text + "/" + TextBox8.Text)
        Dim response As WebResponse = request.GetResponse()
        Dim dataStream As Stream = response.GetResponseStream()
        Dim areader As New StreamReader(dataStream)
        Dim responseFromServer As String = areader.ReadToEnd()
        areader.Close()
        response.Close()
        RichTextBox3.Text = responseFromServer
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim url As String
        If Not TextBox7.Text = "" Then
            Dim request As WebRequest = _
            WebRequest.Create("http://" + My.Settings.IP + ":" + My.Settings.Port + "/gbooks/" + TextBox7.Text + "/autore")
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim ar As New StreamReader(dataStream)
            Dim autore As String = ar.ReadToEnd().Replace(" ", "")
            ar.Close()
            response.Close()
            TextBox6.Text = autore
            url = "http://" + My.Settings.IP + ":" + My.Settings.Port + "/gbooks/" + TextBox7.Text + "/titolo"
            request = _
            WebRequest.Create(url)
            response = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim at As New StreamReader(dataStream)
            Dim responseFromServer As String = at.ReadToEnd()
            TextBox5.Text = responseFromServer
            at.Close()
            response.Close()
        Else
            MsgBox("Inserisci prima l'ISBN!")
        End If
    End Sub

    Private Sub DonaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DonaToolStripMenuItem.Click
        Form4.Show()
    End Sub
End Class
Public Class GlobalVariables
    Public Shared appPath As String = System.IO.Path.GetDirectoryName( _
    System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
    Public Shared user As String = ""
    Public Shared password As String = ""
    Public Shared logged As Boolean = False
    Public Shared logo As String = appPath.Replace("file:\", "") + "\bibliodb.png"
End Class