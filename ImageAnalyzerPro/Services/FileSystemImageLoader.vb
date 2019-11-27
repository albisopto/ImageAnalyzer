Imports System.ComponentModel.Composition
Imports System.IO
Imports ImageAnalyzerPro

<Export(GetType(IImageLoader))>
Public Class FileSystemImageLoader
    Implements IImageLoader

    Public Sub New()
        ChipImages = New List(Of ChipImage)
        HasImages = False
        ImageCount = 0
    End Sub

    Private Property ChipImages As List(Of ChipImage)

    Public Property HasImages As Boolean Implements IImageLoader.hasImages
    Public Property ImageCount As Integer Implements IImageLoader.ImageCount


    Public Sub LoadImages(uri As Uri) Implements IImageLoader.LoadImages
        Dim _initialFile As String = String.Empty
        Dim _initialFileInfo As FileInfo
        Dim _directory As DirectoryInfo
        Dim files As List(Of FileInfo) = New List(Of FileInfo)

        If uri.IsFile Then
            _initialFile = uri.LocalPath
        End If

        _initialFileInfo = New FileInfo(_initialFile)
        _directory = _initialFileInfo.Directory
        files = _directory.GetFiles.ToList

        'Convert to ChipImages (unsorted)
        ChipImages.Clear()
        For Each f In files
            'Extract chipnumber
            Dim chipNumber As String = f.Name.Split(".")(0)
            Dim chipImage As New ChipImage(chipNumber, f.FullName)
            ChipImages.Add(chipImage)
        Next
        'To do: Sort ChipImage list
        HasImages = True
        ImageCount = ChipImages.Count
    End Sub

    Public Function GetImage(index As Integer) As ChipImage Implements IImageLoader.GetImage
        Return ChipImages(index)
    End Function

End Class
