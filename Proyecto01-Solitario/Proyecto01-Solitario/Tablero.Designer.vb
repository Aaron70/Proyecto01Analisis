﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tablero
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
        Me.panel_contenedor = New System.Windows.Forms.Panel()
        Me.btnSiguiente = New System.Windows.Forms.Button()
        Me.btnJugar = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.lbNumeroTablero = New System.Windows.Forms.Label()
        Me.nudNumeroTablero = New System.Windows.Forms.NumericUpDown()
        Me.btn_atras = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Puntaje = New System.Windows.Forms.Label()
        Me.panel_contenedor.SuspendLayout()
        CType(Me.nudNumeroTablero, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panel_contenedor
        '
        Me.panel_contenedor.AllowDrop = True
        Me.panel_contenedor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panel_contenedor.BackColor = System.Drawing.SystemColors.ControlDark
        Me.panel_contenedor.Controls.Add(Me.Puntaje)
        Me.panel_contenedor.Controls.Add(Me.Label2)
        Me.panel_contenedor.Controls.Add(Me.Label1)
        Me.panel_contenedor.Controls.Add(Me.btnSiguiente)
        Me.panel_contenedor.Controls.Add(Me.btnJugar)
        Me.panel_contenedor.Controls.Add(Me.btnAnterior)
        Me.panel_contenedor.Controls.Add(Me.lbNumeroTablero)
        Me.panel_contenedor.Controls.Add(Me.nudNumeroTablero)
        Me.panel_contenedor.Controls.Add(Me.btn_atras)
        Me.panel_contenedor.Location = New System.Drawing.Point(0, 0)
        Me.panel_contenedor.Name = "panel_contenedor"
        Me.panel_contenedor.Size = New System.Drawing.Size(1336, 701)
        Me.panel_contenedor.TabIndex = 0
        '
        'btnSiguiente
        '
        Me.btnSiguiente.Location = New System.Drawing.Point(98, 556)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(75, 23)
        Me.btnSiguiente.TabIndex = 6
        Me.btnSiguiente.Text = "Siguiente"
        Me.btnSiguiente.UseVisualStyleBackColor = True
        '
        'btnJugar
        '
        Me.btnJugar.Location = New System.Drawing.Point(57, 585)
        Me.btnJugar.Name = "btnJugar"
        Me.btnJugar.Size = New System.Drawing.Size(75, 23)
        Me.btnJugar.TabIndex = 5
        Me.btnJugar.Text = "Jugar"
        Me.btnJugar.UseVisualStyleBackColor = True
        '
        'btnAnterior
        '
        Me.btnAnterior.Location = New System.Drawing.Point(17, 556)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.Size = New System.Drawing.Size(75, 23)
        Me.btnAnterior.TabIndex = 4
        Me.btnAnterior.Text = "Anterior"
        Me.btnAnterior.UseVisualStyleBackColor = True
        '
        'lbNumeroTablero
        '
        Me.lbNumeroTablero.AutoSize = True
        Me.lbNumeroTablero.Location = New System.Drawing.Point(34, 495)
        Me.lbNumeroTablero.Name = "lbNumeroTablero"
        Me.lbNumeroTablero.Size = New System.Drawing.Size(129, 17)
        Me.lbNumeroTablero.TabIndex = 3
        Me.lbNumeroTablero.Text = "Numero del tablero"
        '
        'nudNumeroTablero
        '
        Me.nudNumeroTablero.Location = New System.Drawing.Point(17, 528)
        Me.nudNumeroTablero.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudNumeroTablero.Name = "nudNumeroTablero"
        Me.nudNumeroTablero.Size = New System.Drawing.Size(156, 22)
        Me.nudNumeroTablero.TabIndex = 2
        Me.nudNumeroTablero.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'btn_atras
        '
        Me.btn_atras.Location = New System.Drawing.Point(187, 12)
        Me.btn_atras.Name = "btn_atras"
        Me.btn_atras.Size = New System.Drawing.Size(71, 42)
        Me.btn_atras.TabIndex = 1
        Me.btn_atras.Text = "Atras"
        Me.btn_atras.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(185, 591)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 17)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Undefined"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(254, 591)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 17)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Undefined"
        '
        'Puntaje
        '
        Me.Puntaje.AutoSize = True
        Me.Puntaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Puntaje.Location = New System.Drawing.Point(284, 13)
        Me.Puntaje.Name = "Puntaje"
        Me.Puntaje.Size = New System.Drawing.Size(108, 24)
        Me.Puntaje.TabIndex = 9
        Me.Puntaje.Text = "Puntos: 500"
        '
        'Tablero
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1336, 701)
        Me.Controls.Add(Me.panel_contenedor)
        Me.Name = "Tablero"
        Me.Text = "Tablero"
        Me.panel_contenedor.ResumeLayout(False)
        Me.panel_contenedor.PerformLayout()
        CType(Me.nudNumeroTablero, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panel_contenedor As Panel
    Friend WithEvents btn_atras As Button
    Friend WithEvents lbNumeroTablero As Label
    Friend WithEvents nudNumeroTablero As NumericUpDown
    Friend WithEvents btnSiguiente As Button
    Friend WithEvents btnJugar As Button
    Friend WithEvents btnAnterior As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Puntaje As Label
End Class
