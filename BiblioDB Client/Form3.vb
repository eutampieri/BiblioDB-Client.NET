Public Class Form3

    Private Sub Form3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.IP
        If My.Settings.ImgPath = "" Then
            TextBox4.Text = GlobalVariables.logo
        Else
            TextBox4.Text = My.Settings.ImgPath
        End If
        TextBox2.Text = My.Settings.Port
        TextBox3.Text = My.Settings.BiblioString
    End Sub

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        OpenFileDialog1.ShowDialog()
        TextBox4.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If Not TextBox4.Text = GlobalVariables.logo Then
            My.Settings.ImgPath = TextBox4.Text
        End If
        My.Settings.IP = TextBox1.Text
        My.Settings.Port = TextBox2.Text
        My.Settings.BiblioString = TextBox3.Text
        Me.Close()
    End Sub
End Class