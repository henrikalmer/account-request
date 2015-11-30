Public Class LuhnCheck
    Private Property Number As String

    Public Sub New(num As String)
        If (num.Length <> 12 Or Not IsNumeric(num)) Then
            Throw New ArgumentException("Argument must be exactly 12 digits.")
        End If
        Number = num.Substring(2)
    End Sub

    ' Verifies the checksum of a personal ID number or organization number.
    ' Returns True For valid numbers And False for invalid numbers.
    Public Function VerifyChecksum() As Boolean
        Return Number.Select(Function(c, i) (AscW(c) - 48) << ((Number.Length - i - 1) And 1)).Sum(Function(n) If(n > 9, n - 9, n)) Mod 10 = 0
    End Function

    ' Verifies the control digit of a personal ID number or organization number.
    ' Returns True For valid numbers And False for invalid numbers.
    Public Function VerifyControlDigit() As Boolean
        Dim num As String = Number.Substring(0, 9)
        Dim controlDigit = (10 - (num.Select(Function(c, i) (AscW(c) - 48) << ((num.Length - i) And 1)).Sum(Function(n) If(n > 9, n - 9, n)) Mod 10)) Mod 10
        Return controlDigit = Integer.Parse(Number.Substring(9, 1))
    End Function
End Class
