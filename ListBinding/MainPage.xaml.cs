using ListBinding.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ListBinding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Windows.Storage.Pickers.FileOpenPicker _loadPicker = new Windows.Storage.Pickers.FileOpenPicker();
        private Windows.Storage.Pickers.FileSavePicker _savePicker = new Windows.Storage.Pickers.FileSavePicker();
        private MainViewModel _vm;
        public MainPage()
        {
            // co se má udělat před vytvořením formuláře
            this.InitializeComponent(); // vytvoření formuláře
            // co se má udělat po vytvoření formuláře
            _vm = (MainViewModel)this.DataContext; // jak se dostat k DataContextu 
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {          
            _loadPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            _loadPicker.FileTypeFilter.Add(".students");
            _loadPicker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFile file = await _loadPicker.PickSingleFileAsync();
            if (file != null)
            {
                _vm.File = file; // uložení do DataContextu
                if (_vm.Load.CanExecute(null))
                {
                    _vm.Load.Execute(null); // zavolání commandu 
                };
            }
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            _savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            _savePicker.SuggestedSaveFile = _vm.File;
            _savePicker.FileTypeChoices.Clear();
            _savePicker.FileTypeChoices.Add("Students Data File", new List<string> { ".students" });
            _savePicker.SuggestedFileName = "Studenti";
            _savePicker.DefaultFileExtension = ".students";
            Windows.Storage.StorageFile file = await _savePicker.PickSaveFileAsync();
            if (file != null)
            {
                _vm.File = file; // uložení do DataContextu
                if (_vm.Save.CanExecute(null))
                {
                    _vm.Save.Execute(null);
                };
            }
        }
    }
}
