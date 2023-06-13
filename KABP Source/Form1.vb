Public Class Form1

    'Created By Rodrigo Destri Perozin
    'Date: 23/03/2022
    'Description: Program kills a port specified.

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim FILE_NAME As String = "c:/cacheKABP.txt"
        Dim TextLine As String = ""
        Dim LineLenght As Integer = 0
        Dim PID As String = ""
        Dim regExpPort As String = "[ ]{1,2}\d{3,5}$"
        Dim verify As New System.Text.RegularExpressions.Regex(regExpPort, System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace)
        Dim command As String = "netstat -aon | find """ + NumericUpDown1.Value.ToString() + """ > c:/cacheKABP.txt"

        Util.RunCMD(command, True)
        Threading.Thread.Sleep(2500)
        If System.IO.File.Exists(FILE_NAME) = True Then

            Dim objReader As New System.IO.StreamReader(FILE_NAME)

            Do While objReader.Peek() <> -1

                TextLine = objReader.ReadLine()

                LineLenght = TextLine.Length()

                If LineLenght <> 0 And TextLine.StartsWith("INFORMA€åES:").Equals(False) Then

                    PID = verify.Match(TextLine).Value.Remove(0, 0).Replace(" ", "")
                    Util.RunCMD("taskkill /F /FI ""PID eq " + PID + """", True)

                Else

                    MessageBox.Show("Can't find a task running at port " + NumericUpDown1.Value.ToString())
                    Exit Do

                End If

            Loop
            MessageBox.Show("Task finished. Port is unlocked!")

        Else

            MessageBox.Show("File Does Not Exist")

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MessageBox.Show("By rodrigo.perozin@neomind.com.br" & vbCrLf & "Version: 0.0.1")
    End Sub
End Class
