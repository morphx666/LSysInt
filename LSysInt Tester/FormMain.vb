Imports System.Threading

Public Class FormMain
    Private updateCodeTimer As Timer
    Private lsys As LSysInt.LSysInt

    Private Class LSysCodeDef
        Public Property Name As String
        Public Property Code As String

        Public Sub New(name As String, code As String)
            Me.Name = name
            Me.Code = code
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBoxLSysCodeDefs.Items.Add(New LSysCodeDef("VonKochSimple",
"VonKochSimple {
    level: 5
    axiom: -(90) f(0.5) +(90) F(1)
    rule: F(x) = F(x/3) +(60) F(x/3) -(120)
                 F(x/3) +(60) F(x/3)
}"))

        ComboBoxLSysCodeDefs.Items.Add(New LSysCodeDef("Fern",
"Fern {
    level: 6
    axiom: f(0.5) -(90) f(1) -(180) X(0.3)
    rule: X(x) =
                 F(x/2) -(25) [ [ X(x/2) ] +(25) X(x/2) ]
                 +(25) F(x/2) [ +(25) F(x/2) X(x/2) ] -(25) X(x/2)
}"))

        ComboBoxLSysCodeDefs.Items.Add(New LSysCodeDef("SpinIt",
"SpinIt {
    level: 60
    axiom: -(90) f(0.5) +(90) f(0.5) X(0.01)
    rule: X(n) = Square(n) +(2) X(n+0.01)
    rule: Square(n) = [ +(45) f(n*Sqrt(2)/2)
                      -(135) F(n)
                       -(90) F(n)
                       -(90) F(n)
                       -(90) F(n) ]
}"))

        ComboBoxLSysCodeDefs.Items.Add(New LSysCodeDef("Dragon",
"Dragon {
    level: 11
    axiom: -(45) f(0.47) * F(0.6) X(0.6)
    rule: X(n) = X(n/Sqrt(2)) +(90) Y(n/Sqrt(2)) F(n/Sqrt(2)) +(90)
    rule: Y(n) = -(90) F(n/Sqrt(2)) X(n/Sqrt(2)) -(90) Y(n/Sqrt(2))
    rule: F(n) = F(n/Sqrt(2))
    rule: *    = * -(45)
}"))

        ComboBoxLSysCodeDefs.Items.Add(New LSysCodeDef("VonKochColor",
"VonKochColor {
    level: 5
    axiom: -(90) f(0.28) +(90) f(0.05) F(0.9,0,0.9)
    rule: F(x,d,k) =
                    F(x)
                    C(d)         F(x/3, d,         k/4)    +(60)
                    C(d + k/4)   F(x/3, d + k/4,   k/4)   -(120)
                    C(d + k/2)   F(x/3, d + k/2,   k/4)    +(60)
                    C(d + 3/4*k) F(x/3, d + 3/4*k, k/4)
    rule: C(d) = %(0,d,0.1,1)
}"))

        updateCodeTimer = New Timer(New TimerCallback(Sub()
                                                          lsys = New LSysInt.LSysInt(TextBoxCode.Text)
                                                          If lsys.LDefs.Any() Then
                                                              LSysRendererCtrl.LDef = lsys.LDefs(0)
                                                          Else
                                                              LSysRendererCtrl.LDef = Nothing
                                                          End If
                                                      End Sub),
                                                Nothing,
                                                Timeout.Infinite,
                                                Timeout.Infinite)

        AddHandler TextBoxCode.TextChanged, Sub() updateCodeTimer.Change(500, Timeout.Infinite)
        AddHandler ComboBoxLSysCodeDefs.SelectedIndexChanged, Sub()
                                                                  TextBoxCode.Text = CType(ComboBoxLSysCodeDefs.SelectedItem, LSysCodeDef).Code
                                                                  TextBoxCode.SelectionStart = 0
                                                                  TextBoxCode.Focus()
                                                              End Sub
        ComboBoxLSysCodeDefs.SelectedIndex = 0
    End Sub
End Class
