﻿Imports System.Data.OleDb

Public Class Form1
    Sub Kosongkan()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox1.Focus()
    End Sub

    Sub TampilGrid()
        DA = New OleDbDataAdapter("select * from [sheet1$]", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
        DataGridView1.ReadOnly = True
        ADP = New OleDbDataAdapter("select * from [sheet2$]", CONN)
        TS = New DataSet
        ADP.Fill(TS)
        DataGridView2.DataSource = TS.Tables(0)
        DataGridView2.ReadOnly = True
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call TampilGrid()
        Call Kosongkan()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 9
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            CMD = New OleDbCommand("Select * from [sheet1$] where val(Kode)='" & TextBox1.Text & "'", CONN)
            UB = New OleDbCommand("Select * from [sheet2$] where val(Kode)='" & TextBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            BC = UB.ExecuteReader
            RD.Read()
            BC.Read()
            If Not RD.HasRows Then
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox2.Focus()
                TextBox3.Focus()
                TextBox4.Focus()
            Else
                TextBox2.Text = RD.Item("NIS")
                TextBox3.Text = RD.Item("NIS")
                TextBox4.Text = RD.Item("NIS")
                TextBox2.Focus()
                TextBox3.Focus()
                TextBox4.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        TextBox2.MaxLength = 30
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        TextBox3.MaxLength = 30
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        TextBox4.MaxLength = 30
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Data belum lengkap")
            Exit Sub
        Else
            Call Koneksi()
            CMD = New OleDbCommand("Select * from [sheet1$] where val(Kode)='" & TextBox1.Text & "'" & TextBox2.Text & "'" & TextBox3.Text & "'", CONN)
            UB = New OleDbCommand("Select * from [sheet2$] where val(Kode)='" & TextBox1.Text & "'" & TextBox2.Text & "'" & TextBox4.Text & "'", CONN)
            RD = CMD.ExecuteReader()
            BC = UB.ExecuteReader()
            RD.Read()
            BC.Read()
            If Not RD.HasRows Then
                Dim simpan As String = "insert into [sheet1$] values ('" & TextBox1.Text & "'" & TextBox2.Text & "'" & TextBox3.Text & "')"
                Dim save As String = "insert into [sheet2$] values ('" & TextBox1.Text & "'" & TextBox2.Text & "'" & TextBox4.Text & "')"
                CMD = New OleDbCommand(simpan, CONN)
                UB = New OleDbCommand(save, CONN)
                CMD.ExecuteNonQuery()
                UB.ExecuteNonQuery()
            Else
                Dim edit As String = "update [sheet1$] set NIS='" & TextBox2.Text & "' where val(Kode)='" & TextBox1.Text & "'"
                Dim ganti As String = "update [sheet1$] set NIS='" & TextBox2.Text & "' where val(Kode)='" & TextBox1.Text & "'"
                CMD = New OleDbCommand(edit, CONN)
                UB = New OleDbCommand(ganti, CONN)
                CMD.ExecuteNonQuery()
                UB.ExecuteNonQuery()
            End If

            Call TampilGrid()
            Call Kosongkan()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("val(Kode) Barang masih kosong, silakan diisi dulu")
            TextBox1.Focus()
            Exit Sub
        Else
            If MessageBox.Show("Yakin akan dihapus..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call TampilGrid()
                Dim hapus As String = "delete * from [sheet1$] where val(Kode)='" & TextBox1.Text & "'"
                Dim del As String = "delete * from [sheet2$] where val(Kode)='" & TextBox1.Text & "'"
                CMD = New OleDbCommand(hapus, CONN)
                UB = New OleDbCommand(del, CONN)
                CMD.ExecuteNonQuery()
                UB.ExecuteNonQuery()
                Call Kosongkan()
            Else
                Call Kosongkan()
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call Kosongkan()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class
