Imports System.IO
Imports System.Net
Public Class GlobalVariables
    Public Shared user As String = ""
    Public Shared password As Integer = ""
End Class
Public Class Form1
    Dim user As String
    Private Sub LoginToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ProvaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ProvaToolStripMenuItem.Click
    End Sub

    Private Sub LoginToolStripMenuItem_Click_1(sender As System.Object, e As System.EventArgs) Handles LoginToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New()

        ' Chiamata richiesta dalla finestra di progettazione.
        InitializeComponent()
        TabControl1.TabPages(2).Enabled = False
        TabControl1.TabPages(3).Enabled = False
        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().
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
End Class
