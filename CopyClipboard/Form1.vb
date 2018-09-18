Imports System.IO

Public Class Form1
    Public filePath As String = ".\Text.txt"
    Public records As New ArrayList
    Private buttons As New ArrayList
    Public numberOfButtons As Integer = 0
    Private funcBtnnumberOfButtons = -1
    Private btnAdd As New Button

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'newButton
        Dim objReader As New StreamReader(filePath)
        Dim sLine As String = ""
        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                AddNewButton(sLine.Split("|"))
            End If
        Loop Until sLine Is Nothing
        objReader.Close()

        'addButton
        Dim locAdd As Point
        locAdd.X = 6
        locAdd.Y = 6 + numberOfButtons * 33
        btnAdd.Location = locAdd
        btnAdd.Height = 27
        btnAdd.Width = 260
        btnAdd.Text = "Add"
        AddHandler btnAdd.Click, AddressOf AddButton_Click
        Me.Controls.Add(btnAdd)
        btnAdd.Select()
    End Sub

    Private Sub AddNewButton(ByVal record() As String)
        Dim btn As New Button
        Dim loc As Point
        loc.X = 6
        loc.Y = 6 + numberOfButtons * 33
        btn.Location = loc
        btn.Height = 27
        btn.Width = 260
        btn.Text = record(0)
        btn.Tag = record(1)
        records.Add(record)
        'Console.WriteLine(record(0) & " " & record(1))
        AddHandler btn.Click, AddressOf NewButton_Click
        btn.ContextMenuStrip = ContextMenuStrip1
        Me.Controls.Add(btn)
        buttons.Add(btn)
        numberOfButtons += 1
    End Sub

    Private Sub NewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Clipboard.Clear()
        Clipboard.SetText(btn.Tag)
        Me.Dispose()
    End Sub
    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fm2 As New Form2(Me)
        fm2.Show()
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Clipboard.Clear()
        Dim btn As Button
        btn = sender
        Clipboard.SetText(btn.Tag)
        Me.Dispose()
    End Sub

    Public Sub notifyAdd(ByVal record() As String)
        Dim objWriter As New StreamWriter(filePath, True)
        objWriter.WriteLine(record(0) & "|" & record(1))
        objWriter.Close()
        AddNewButton(record)
        Dim locAdd As Point
        locAdd.X = 6
        locAdd.Y = 6 + numberOfButtons * 33
        btnAdd.Location = locAdd
    End Sub

    Public Sub notifyEdit(ByVal btn As Button, ByVal record() As String)
        btn.Text = record(0)
        btn.Tag = record(1)
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim tsmi As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(tsmi.Owner, ContextMenuStrip)
        Dim btn As Button = CType(cms.SourceControl, Button)
        Dim fm2 As New Form2(Me, btn)
        fm2.Show()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Dim tsmi As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(tsmi.Owner, ContextMenuStrip)
        Dim btn As Button = CType(cms.SourceControl, Button)
        Dim result = MsgBox("Are you sure want to delete?", MsgBoxStyle.OkCancel, "Confirm?")
        If result = MsgBoxResult.Ok Then
            Dim found As Boolean = False
            Dim objWriter As New StreamWriter(filePath, False)
            For i As Integer = 0 To numberOfButtons - 1
                If found Then
                    Dim btnTemp As Button = buttons(i)
                    Dim loc As Point = btnTemp.Location
                    loc.X = 6
                    loc.Y -= 33
                    btnTemp.Location = loc
                End If
                If String.Compare(btn.Text, records(i)(0)) <> 0 And String.Compare(btn.Tag, records(i)(1)) <> 0 Then
                    objWriter.WriteLine(records(i)(0) & "|" & records(i)(1))
                Else
                    found = True
                End If
            Next
            numberOfButtons -= 1
            Me.Controls.Remove(btn)
            Dim locAdd As Point
            locAdd.X = 6
            locAdd.Y = 6 + numberOfButtons * 33
            btnAdd.Location = locAdd
            objWriter.Close()
        End If
    End Sub
End Class