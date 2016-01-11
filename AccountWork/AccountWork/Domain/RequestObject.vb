Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json

Namespace Domain
    <DataContract>
    Public Class RequestObject
        <DataMember>
        Public Property Authority As String = "Ekobrottsmyndigheten"
        <DataMember>
        Public Property EbNumber As String
        <DataMember>
        Public Property Prosecutor As String
        <DataMember>
        Public Property RequestId As Integer
        <DataMember(Name:="SecrecyDate")>
        Private Property _SecrecyDate As String
        Public Property SecrecyDate As Date
            Get
                Return Date.Parse(_SecrecyDate)
            End Get
            Set(value As Date)
                _SecrecyDate = value.ToString("O")
            End Set
        End Property
        <DataMember>
        Public Property Contact As String
        <DataMember>
        Public Property TypeId As Integer
        <DataMember>
        Public Property TypeString As String
        <DataMember>
        Public Property IdNumber As String
        <DataMember>
        Public Property AccountNumber As String
        <DataMember(Name:="PeriodStartDate")>
        Private Property _PeriodStartDate As String
        <DataMember(Name:="PeriodEndDate")>
        Private Property _PeriodEndDate As String
        Public Property PeriodStartDate As Date
            Get
                Return Date.Parse(_PeriodStartDate)
            End Get
            Set(value As Date)
                _PeriodStartDate = value.ToString("O")
            End Set
        End Property
        Public Property PeriodEndDate As Date
            Get
                Return Date.Parse(_PeriodEndDate)
            End Get
            Set(value As Date)
                _PeriodEndDate = value.ToString("O")
            End Set
        End Property
        <DataMember>
        Public Property IncludeStatements As String

        Public Sub New()
        End Sub

        Public Sub New(value As String, type As String)
            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentException("value")
            End If
            If UCase(type) = "XML" Then
                FromXml(value)
            ElseIf UCase(type) = "JSON"
                FromJson(value)
            Else
                Throw New ArgumentException("type")
            End If
        End Sub

        Public Sub FromXml(value As String)
            Dim serializer As New XmlSerializer(GetType(RequestObject))
            Using textReader As StringReader = New StringReader(value)
                Using xmlReader As XmlReader = XmlReader.Create(textReader)
                    Dim obj = DirectCast(serializer.Deserialize(xmlReader), RequestObject)
                    FromObject(obj)
                End Using
            End Using
        End Sub

        Public Sub FromJson(value As String)
            Dim serializer As New DataContractJsonSerializer(GetType(RequestObject))
            Dim byteArray = Encoding.UTF8.GetBytes(value)
            Using stream As MemoryStream = New MemoryStream(byteArray)
                Dim obj = DirectCast(serializer.ReadObject(stream), RequestObject)
                FromObject(obj)
            End Using
        End Sub

        Public Sub FromObject(obj As RequestObject)
            EbNumber = obj.EbNumber
            Prosecutor = obj.Prosecutor
            SecrecyDate = obj.SecrecyDate
            RequestId = obj.RequestId
            Contact = obj.Contact
            TypeId = obj.TypeId
            TypeString = obj.TypeString
            IdNumber = obj.IdNumber
            AccountNumber = obj.AccountNumber
            PeriodStartDate = obj.PeriodStartDate
            PeriodEndDate = obj.PeriodEndDate
            IncludeStatements = obj.IncludeStatements
        End Sub

        Public Function ToXml() As String
            Dim serializer As New XmlSerializer(GetType(RequestObject))
            Dim settings As New XmlWriterSettings()
            settings.Encoding = New UnicodeEncoding(False, False) ' no BOM In a .NET String
            settings.Indent = False
            settings.OmitXmlDeclaration = False
            Using writer As StringWriter = New StringWriter()
                Using xmlWriter As XmlWriter = XmlWriter.Create(writer, settings)
                    serializer.Serialize(xmlWriter, Me)
                    Return writer.ToString()
                End Using
            End Using
        End Function

        Public Function ToJson() As String
            Dim serializer As New DataContractJsonSerializer(GetType(RequestObject))
            Using stream As MemoryStream = New MemoryStream()
                serializer.WriteObject(stream, Me)
                stream.Seek(0, 0)
                Using sr As StreamReader = New StreamReader(stream)
                    Return sr.ReadToEnd()
                End Using
            End Using
        End Function
    End Class
End Namespace
