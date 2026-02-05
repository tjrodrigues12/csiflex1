
'source:http://stackoverflow.com/questions/9956486/distributed-probability-random-number-generator
Public Class RNGesus

    'End Class


    'Public Class LoadedDie
    ' Initializes a new loaded die.  Probs
    ' is an array of numbers indicating the relative
    ' probability of each choice relative to all the
    ' others.  For example, if probs is [3,4,2], then
    ' the chances are 3/9, 4/9, and 2/9, since the probabilities
    ' add up to 9.
    Public Sub New(probs As Integer)
        Me.prob = New List(Of Long)()
        Me.[alias] = New List(Of Integer)()
        Me.total = 0
        Me.n = probs
        Me.even = True
    End Sub

    Private random As New Random()

    Private prob As List(Of Long)
    Private [alias] As List(Of Integer)
    Private total As Long
    Private n As Integer
    Private even As Boolean

    Public Sub New(probs As IEnumerable(Of Integer))
        ' Raise an error if nil
        If probs Is Nothing Then
            Throw New ArgumentNullException("probs")
        End If
        Me.prob = New List(Of Long)()
        Me.[alias] = New List(Of Integer)()
        Me.total = 0
        Me.even = False
        Dim small = New List(Of Integer)()
        Dim large = New List(Of Integer)()
        Dim tmpprobs = New List(Of Long)()
        For Each p In probs
            tmpprobs.Add(p)
        Next
        Me.n = tmpprobs.Count
        ' Get the max and min choice and calculate total
        Dim mx As Long = -1, mn As Long = -1
        For Each p In tmpprobs
            If p < 0 Then
                Throw New ArgumentException("probs contains a negative probability.")
            End If
            mx = If((mx < 0 OrElse p > mx), p, mx)
            mn = If((mn < 0 OrElse p < mn), p, mn)
            Me.total += p
        Next
        ' We use a shortcut if all probabilities are equal
        If mx = mn Then
            Me.even = True
            Return
        End If
        ' Clone the probabilities and scale them by
        ' the number of probabilities
        For i = 0 To tmpprobs.Count - 1
            tmpprobs(i) *= Me.n
            Me.[alias].Add(0)
            Me.prob.Add(0)
        Next
        ' Use Michael Vose's alias method
        For i = 0 To tmpprobs.Count - 1
            If tmpprobs(i) < Me.total Then
                small.Add(i)
            Else
                ' Smaller than probability sum
                large.Add(i)
                ' Probability sum or greater
            End If
        Next
        ' Calculate probabilities and aliases
        While small.Count > 0 AndAlso large.Count > 0
            Dim l = small(small.Count - 1)
            small.RemoveAt(small.Count - 1)
            Dim g = large(large.Count - 1)
            large.RemoveAt(large.Count - 1)
            Me.prob(l) = tmpprobs(l)
            Me.[alias](l) = g
            Dim newprob = (tmpprobs(g) + tmpprobs(l)) - Me.total
            tmpprobs(g) = newprob
            If newprob < Me.total Then
                small.Add(g)
            Else
                large.Add(g)
            End If
        End While
        For Each g In large
            Me.prob(g) = Me.total
        Next
        For Each l In small
            Me.prob(l) = Me.total
        Next
    End Sub

    ' Returns the number of choices.
    Public ReadOnly Property Count() As Integer
        Get
            Return Me.n
        End Get
    End Property
    ' Chooses a choice at random, ranging from 0 to the number of choices
    ' minus 1.
    Public Function NextValue() As Integer
        Dim i = random.[Next](Me.n)
        Return If((Me.even OrElse random.[Next](CInt(Me.total)) < Me.prob(i)), i, Me.[alias](i))
    End Function
End Class
