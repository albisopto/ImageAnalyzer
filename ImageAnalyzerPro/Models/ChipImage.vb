Public Class ChipImage

    Public Sub New()

    End Sub

    Public Sub New(chipNumber As String, imagePath As String)
        Me.ChipNumber = chipNumber
        Me.ImagePath = imagePath
    End Sub

    Public Property ChipNumber As String
    Public Property ImagePath As String

End Class
