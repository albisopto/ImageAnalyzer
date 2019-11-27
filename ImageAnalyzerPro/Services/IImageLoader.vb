Public Interface IImageLoader

    Property HasImages As Boolean
    Property ImageCount As Integer
    Sub LoadImages(uri As Uri)
    Function GetImage(index As Integer) As ChipImage

End Interface
