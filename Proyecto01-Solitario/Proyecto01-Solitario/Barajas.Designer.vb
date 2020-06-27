<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Barajas
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.NumeroBarajas = New System.Windows.Forms.NumericUpDown()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.RBunaFamilia = New System.Windows.Forms.RadioButton()
        Me.RBdosFamilias = New System.Windows.Forms.RadioButton()
        Me.RBcuatroFamilias = New System.Windows.Forms.RadioButton()
        CType(Me.NumeroBarajas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumeroBarajas
        '
        Me.NumeroBarajas.Location = New System.Drawing.Point(283, 183)
        Me.NumeroBarajas.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumeroBarajas.Name = "NumeroBarajas"
        Me.NumeroBarajas.Size = New System.Drawing.Size(101, 20)
        Me.NumeroBarajas.TabIndex = 0
        Me.NumeroBarajas.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Proyecto01_Solitario.My.Resources.Resources.BotonCrear
        Me.PictureBox2.Location = New System.Drawing.Point(324, 268)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(131, 74)
        Me.PictureBox2.TabIndex = 7
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Proyecto01_Solitario.My.Resources.Resources.FondoBarajas
        Me.PictureBox1.Location = New System.Drawing.Point(0, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(800, 451)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'RBunaFamilia
        '
        Me.RBunaFamilia.AutoSize = True
        Me.RBunaFamilia.Checked = True
        Me.RBunaFamilia.Location = New System.Drawing.Point(339, 137)
        Me.RBunaFamilia.Name = "RBunaFamilia"
        Me.RBunaFamilia.Size = New System.Drawing.Size(14, 13)
        Me.RBunaFamilia.TabIndex = 8
        Me.RBunaFamilia.TabStop = True
        Me.RBunaFamilia.UseVisualStyleBackColor = True
        '
        'RBdosFamilias
        '
        Me.RBdosFamilias.AutoSize = True
        Me.RBdosFamilias.Location = New System.Drawing.Point(452, 137)
        Me.RBdosFamilias.Name = "RBdosFamilias"
        Me.RBdosFamilias.Size = New System.Drawing.Size(14, 13)
        Me.RBdosFamilias.TabIndex = 9
        Me.RBdosFamilias.UseVisualStyleBackColor = True
        '
        'RBcuatroFamilias
        '
        Me.RBcuatroFamilias.AutoSize = True
        Me.RBcuatroFamilias.Location = New System.Drawing.Point(564, 137)
        Me.RBcuatroFamilias.Name = "RBcuatroFamilias"
        Me.RBcuatroFamilias.Size = New System.Drawing.Size(14, 13)
        Me.RBcuatroFamilias.TabIndex = 10
        Me.RBcuatroFamilias.UseVisualStyleBackColor = True
        '
        'Barajas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(800, 453)
        Me.Controls.Add(Me.RBcuatroFamilias)
        Me.Controls.Add(Me.RBdosFamilias)
        Me.Controls.Add(Me.RBunaFamilia)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.NumeroBarajas)
        Me.Controls.Add(Me.PictureBox1)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(816, 492)
        Me.MinimumSize = New System.Drawing.Size(816, 492)
        Me.Name = "Barajas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Barajas"
        CType(Me.NumeroBarajas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents NumeroBarajas As NumericUpDown
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents RBunaFamilia As RadioButton
    Friend WithEvents RBdosFamilias As RadioButton
    Friend WithEvents RBcuatroFamilias As RadioButton
End Class
