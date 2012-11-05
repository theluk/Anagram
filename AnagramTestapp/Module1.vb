Imports System.IO

Module Module1

    Private dict As New Dictionary(Of String, String())

    Sub Main()
        Dim file As New StringReader(My.Resources.wl)
        Dim list = New List(Of String)

        Do While file.Peek >= 0
            list.Add(file.ReadLine.Trim())
        Loop

        dict = (From i In list
                        Let sorted = String.Join("", i.ToCharArray.ToList().OrderBy(Function(s) s.ToString).ToArray())
                        Group By sorted Into Group Select New With {.key = sorted, .group = (From inner In Group Select inner.i).ToArray}
                        ).ToDictionary(Of String, String())(Function(i) i.key, Function(i) i.group)

        Do
            Console.WriteLine("Type in a word and press ENTER or type 'q' to exit this application:")
            Dim input = Console.ReadLine
            If input = "q" Then Exit Do
            Dim result = anagram(input)
            If result Is Nothing Then
                Console.WriteLine("No anagram found for {0}", input)
            Else
                Console.WriteLine(String.Join(", ", result))
            End If

        Loop

    End Sub

    Private Function anagram(rawInput As String) As String()

        Dim input = String.Join("", rawInput.ToCharArray.ToList.OrderBy(Function(i) i.ToString).ToArray)
        If dict.ContainsKey(input) Then Return dict(input) Else Return Nothing

    End Function

End Module
