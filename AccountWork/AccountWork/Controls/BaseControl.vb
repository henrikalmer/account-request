Imports System.Globalization
Imports System.Windows.Markup

Public Class BaseControl
    Inherits UserControl

    Public Sub New()
        MyBase.New()
        ' Set control language to the application language
        Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)
    End Sub
End Class
