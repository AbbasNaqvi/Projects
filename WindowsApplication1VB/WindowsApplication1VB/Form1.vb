Public Class Form1

    Private Property i As Integer = 50

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim MyArray(50) As Integer


        For i As Integer = 0 To MyArray.Length - 1

            MyArray(i) = i

        Next
        Array.Reverse(MyArray)
        Array.Reverse(MyArray)
        For Each x As Integer In MyArray
            RichTextBox1.Text += x.ToString() + vbNewLine

        Next



    End Sub
End Class
