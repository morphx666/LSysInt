<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.TextBoxCode = New System.Windows.Forms.TextBox()
        Me.ComboBoxLSysCodeDefs = New System.Windows.Forms.ComboBox()
        Me.LSysRendererCtrl = New LSysInt.LSysRenderer()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.Controls.Add(Me.ComboBoxLSysCodeDefs)
        Me.SplitContainerMain.Panel1.Controls.Add(Me.TextBoxCode)
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.LSysRendererCtrl)
        Me.SplitContainerMain.Size = New System.Drawing.Size(951, 567)
        Me.SplitContainerMain.SplitterDistance = 303
        Me.SplitContainerMain.TabIndex = 1
        '
        'TextBoxCode
        '
        Me.TextBoxCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.TextBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxCode.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCode.ForeColor = System.Drawing.Color.GreenYellow
        Me.TextBoxCode.Location = New System.Drawing.Point(0, 23)
        Me.TextBoxCode.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBoxCode.Multiline = True
        Me.TextBoxCode.Name = "TextBoxCode"
        Me.TextBoxCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxCode.Size = New System.Drawing.Size(303, 544)
        Me.TextBoxCode.TabIndex = 0
        '
        'ComboBoxLSysCodeDefs
        '
        Me.ComboBoxLSysCodeDefs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxLSysCodeDefs.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.ComboBoxLSysCodeDefs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxLSysCodeDefs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBoxLSysCodeDefs.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxLSysCodeDefs.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.ComboBoxLSysCodeDefs.FormattingEnabled = True
        Me.ComboBoxLSysCodeDefs.Location = New System.Drawing.Point(1, 0)
        Me.ComboBoxLSysCodeDefs.Margin = New System.Windows.Forms.Padding(0)
        Me.ComboBoxLSysCodeDefs.Name = "ComboBoxLSysCodeDefs"
        Me.ComboBoxLSysCodeDefs.Size = New System.Drawing.Size(302, 23)
        Me.ComboBoxLSysCodeDefs.TabIndex = 1
        '
        'LSysRendererCtrl
        '
        Me.LSysRendererCtrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LSysRendererCtrl.LDef = Nothing
        Me.LSysRendererCtrl.Location = New System.Drawing.Point(0, 0)
        Me.LSysRendererCtrl.Name = "LSysRendererCtrl"
        Me.LSysRendererCtrl.Size = New System.Drawing.Size(644, 567)
        Me.LSysRendererCtrl.TabIndex = 0
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(951, 567)
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LSysInt Tester"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel1.PerformLayout()
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LSysRendererCtrl As LSysInt.LSysRenderer
    Friend WithEvents SplitContainerMain As SplitContainer
    Friend WithEvents TextBoxCode As TextBox
    Friend WithEvents ComboBoxLSysCodeDefs As ComboBox
End Class
