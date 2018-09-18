Imports System.IO

Public Class Form2

    Private _form1 As Form1
    Private isEdit As Boolean = False
    Private _btn As Button

    Sub New(form1 As Form1)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _form1 = form1
        Me.Text = "New"
    End Sub

    Sub New(form1 As Form1, btn As Button)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _form1 = form1
        Me.Text = "Edit"
        _btn = btn
        TextBoxPrompt.Text = _btn.Text
        TextBoxCopy.Text = _btn.Tag
        isEdit = True
    End Sub

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        Dim record() As String = {TextBoxPrompt.Text, TextBoxCopy.Text}
        If isEdit Then
            Dim objWriter As New StreamWriter(_form1.filePath, False)
            For i As Integer = 0 To _form1.numberOfButtons - 1
                If String.Compare(_btn.Text, _form1.records(i)(0)) <> 0 And String.Compare(_btn.Tag, _form1.records(i)(1)) <> 0 Then
                    objWriter.WriteLine(_form1.records(i)(0) & "|" & _form1.records(i)(1))
                Else
                    objWriter.WriteLine(TextBoxPrompt.Text & "|" & TextBoxCopy.Text)
                End If
            Next
            objWriter.Close()
            _form1.notifyEdit(_btn, record)
        Else
            _form1.notifyAdd(record)
        End If
        Me.Dispose()
    End Sub

    Private Sub ButtonReset_Click(sender As Object, e As EventArgs) Handles ButtonReset.Click
        TextBoxCopy.Text = ""
        TextBoxPrompt.Text = ""
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Dispose()
    End Sub

    Private Sub TextBoxPrompt_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPrompt.TextChanged
        TextBoxCopy.Text = TextBoxPrompt.Text
    End Sub
End Class