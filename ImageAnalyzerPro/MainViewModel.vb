Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports Microsoft.Win32
Imports Prism.Commands

Public Class MainViewModel
    Implements INotifyPropertyChanged

    Private index As Integer

    Public Sub New()

        OpenCommand = New DelegateCommand(AddressOf HandleOpenCommand)
        BackCommand = New DelegateCommand(AddressOf HandleBackCommand, AddressOf canExecuteBackCommand)
        ForwardCommand = New DelegateCommand(AddressOf HandleForwardCommand, AddressOf canExecuteForwardCommand)
        ChipImages = New ObservableCollection(Of ChipImage)

        _currentChipNumber = "No folder selected"
    End Sub


#Region "Viewmodel Properties"

    Private _currentChipImage As ChipImage
    Public Property CurrentChipImage As ChipImage
        Get
            Return _currentChipImage
        End Get
        Set(value As ChipImage)
            _currentChipImage = value
            NotifyPropertyChanged("CurrentChipImage")
        End Set
    End Property

    Private _currentChipNumber As String
    Public ReadOnly Property CurrentChipNumber As String
        Get
            If ChipImages.Count = 0 Then
                Return "No image folder selected ....."
            Else
                Return "Current chip: " + _currentChipImage.ChipNumber
            End If
        End Get
    End Property


    Private Property ChipImages As ObservableCollection(Of ChipImage)

    Public Property OpenCommand As DelegateCommand
    Public Property BackCommand As DelegateCommand
    Public Property ForwardCommand As DelegateCommand
    Public Property SetFailureCommand As DelegateCommand
    Public Property SaveListCommand As DelegateCommand

#End Region

#Region "Command Handlers"

    Private Sub HandleOpenCommand()

        Dim _initialFileInfo As FileInfo
        Dim _directory As DirectoryInfo
        Dim files As List(Of FileInfo) = New List(Of FileInfo)

        'Open file dialog
        Dim _openFileDialog As OpenFileDialog = New OpenFileDialog
        _openFileDialog.InitialDirectory = "C:\tmp"
        _openFileDialog.Filter = "Image Files (*.jpg)|*.jpg; *.png"
        If _openFileDialog.ShowDialog = True Then

            'Get Files
            Dim _initialFile As String = _openFileDialog.FileName
            _initialFileInfo = New FileInfo(_initialFile)
            _directory = _initialFileInfo.Directory
            files = _directory.GetFiles.ToList

            'Convert to ChipImages (unsorted)  To do: Sort files
            For Each f In files
                'Extract chipnumber
                Dim chipNumber As String = f.Name.Split(".")(0)
                Dim chipImage As New ChipImage(chipNumber, f.FullName)
                ChipImages.Add(chipImage)
            Next
        End If

        'Display first chip (unsorted)
        If ChipImages.Count > 0 Then
            index = 0
            CurrentChipImage = ChipImages(index)
            ForwardCommand.RaiseCanExecuteChanged()
            BackCommand.RaiseCanExecuteChanged()
            NotifyPropertyChanged("CurrentChipImage")
            NotifyPropertyChanged("CurrentChipNumber")
        End If

    End Sub

    Private Function canExecuteBackCommand() As Boolean
        If ChipImages.Count = 0 Or index < 1 Then
            Return False
        End If
        Return True
    End Function

    Private Sub HandleBackCommand()
        If ChipImages.Count > 0 And index > 0 Then
            index = index - 1
            CurrentChipImage = ChipImages(index)
            ForwardCommand.RaiseCanExecuteChanged()
            BackCommand.RaiseCanExecuteChanged()
            NotifyPropertyChanged("CurrentChipImage")
            NotifyPropertyChanged("CurrentChipNumber")
        End If
    End Sub

    Private Function canExecuteForwardCommand() As Boolean
        If ChipImages.Count = 0 Or index >= ChipImages.Count - 1 Then
            Return False
        End If
        Return True
    End Function

    Private Sub HandleForwardCommand()
        If ChipImages.Count > 0 And index < ChipImages.Count - 1 Then
            index = index + 1
            CurrentChipImage = ChipImages(index)
            ForwardCommand.RaiseCanExecuteChanged()
            BackCommand.RaiseCanExecuteChanged()
            NotifyPropertyChanged("CurrentChipImage")
            NotifyPropertyChanged("CurrentChipNumber")
        End If
    End Sub

#End Region

#Region "INotifyPropertyChanged Members"

    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Friend Sub NotifyPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

#End Region

End Class
