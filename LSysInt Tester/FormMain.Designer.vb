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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxCode = New System.Windows.Forms.TextBox()
        Me.ComboBoxLSysCodeDefs = New System.Windows.Forms.ComboBox()
        Me.LSysRendererCtrl = New LSysInt.LSysRenderer()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.SplitContainerMain.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.LSysRendererCtrl)
        Me.SplitContainerMain.Size = New System.Drawing.Size(1365, 716)
        Me.SplitContainerMain.SplitterDistance = 455
        Me.SplitContainerMain.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxCode, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ComboBoxLSysCodeDefs, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(455, 716)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TextBoxCode
        '
        Me.TextBoxCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.TextBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxCode.Font = New System.Drawing.Font("Consolas", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCode.ForeColor = System.Drawing.Color.GreenYellow
        Me.TextBoxCode.Location = New System.Drawing.Point(3, 32)
        Me.TextBoxCode.Multiline = True
        Me.TextBoxCode.Name = "TextBoxCode"
        Me.TextBoxCode.Size = New System.Drawing.Size(449, 681)
        Me.TextBoxCode.TabIndex = 0
        '
        'ComboBoxLSysCodeDefs
        '
        Me.ComboBoxLSysCodeDefs.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.ComboBoxLSysCodeDefs.Dock = System.Windows.Forms.DockStyle.Top
        Me.ComboBoxLSysCodeDefs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxLSysCodeDefs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBoxLSysCodeDefs.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxLSysCodeDefs.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.ComboBoxLSysCodeDefs.FormattingEnabled = True
        Me.ComboBoxLSysCodeDefs.Location = New System.Drawing.Point(3, 3)
        Me.ComboBoxLSysCodeDefs.Name = "ComboBoxLSysCodeDefs"
        Me.ComboBoxLSysCodeDefs.Size = New System.Drawing.Size(449, 23)
        Me.ComboBoxLSysCodeDefs.TabIndex = 1
        '
        'LSysRendererCtrl
        '
        Me.LSysRendererCtrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LSysRendererCtrl.LDef = Nothing
        Me.LSysRendererCtrl.Location = New System.Drawing.Point(0, 0)
        Me.LSysRendererCtrl.Name = "LSysRendererCtrl"
        Me.LSysRendererCtrl.Size = New System.Drawing.Size(906, 716)
        Me.LSysRendererCtrl.TabIndex = 0
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(1365, 716)
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LSysInt Tester"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LSysRendererCtrl As LSysInt.LSysRenderer
    Friend WithEvents SplitContainerMain As SplitContainer
    Friend WithEvents TextBoxCode As TextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ComboBoxLSysCodeDefs As ComboBox
End Class
