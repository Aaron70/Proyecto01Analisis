<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.btn_Repartir = New System.Windows.Forms.Button()
        Me.btn_atras = New System.Windows.Forms.Button()
        Me.panel_contenedor.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel_contenedor
        '
        Me.panel_contenedor.Controls.Add(Me.btn_atras)
        Me.panel_contenedor.Controls.Add(Me.btn_Repartir)
        Me.panel_contenedor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel_contenedor.Location = New System.Drawing.Point(0, 0)
        Me.panel_contenedor.Name = "panel_contenedor"
        Me.panel_contenedor.Size = New System.Drawing.Size(1336, 701)
        Me.panel_contenedor.TabIndex = 0
        '
        'btn_Repartir
        '
        Me.btn_Repartir.Location = New System.Drawing.Point(1241, 604)
        Me.btn_Repartir.Name = "btn_Repartir"
        Me.btn_Repartir.Size = New System.Drawing.Size(83, 85)
        Me.btn_Repartir.TabIndex = 0
        Me.btn_Repartir.Text = "Repartir"
        Me.btn_Repartir.UseVisualStyleBackColor = True
        '
        'btn_atras
        '
        Me.btn_atras.Location = New System.Drawing.Point(1164, 647)
        Me.btn_atras.Name = "btn_atras"
        Me.btn_atras.Size = New System.Drawing.Size(71, 42)
        Me.btn_atras.TabIndex = 1
        Me.btn_atras.Text = "Atras"
        Me.btn_atras.UseVisualStyleBackColor = True
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
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panel_contenedor As Panel
    Friend WithEvents btn_Repartir As Button
    Friend WithEvents btn_atras As Button
End Class
