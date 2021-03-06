﻿using ListBinding.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utf8Json;
using Windows.Storage;
using Windows.UI.Popups;

namespace ListBinding.ViewModels
{
    // https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files

    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Student> _students = new ObservableCollection<Student>();
        private int _selectedStudentIndex;
        private Student _selectedStudent;
        private StorageFile _file;

        public RelayCommand Add { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Load { get; set; }
        public RelayCommand Save { get; set; }

        public MainViewModel()
        {
            Students.Add(new Student { Firstname = "Adam", Lastname="Antoš", Average=2.05, Gender=Gender.Male, Examined=false});
            Students.Add(new Student { Firstname = "Boris", Lastname = "Bohatý", Average = 4.4, Gender = Gender.Male, Examined = true });
            Students.Add(new Student { Firstname = "Ctirad", Lastname = "Culík", Average = 1.4, Gender = Gender.Male, Examined = false });
            Students.Add(new Student { Firstname = "Daniela", Lastname = "Dvořáková", Average = 3.0, Gender = Gender.Female, Examined = false });
            Students.Add(new Student { Firstname = "Eva", Lastname = "Ebenová", Average = 3.0, Gender = Gender.Female, Examined = false });
            Students.Add(new Student { Firstname = "Filip", Lastname = "Fiala", Average = 2.5, Gender = Gender.Other, Examined = true });
            Add = new RelayCommand(
                () => { Students.Add(new Student { Firstname = "Nový", Lastname = "Student" }); },
                () => true
            );
            Remove = new RelayCommand(
                () => { Students.Remove(SelectedStudent); },
                () => { return SelectedStudent != null; }
            );
            Load = new RelayCommand(
                async () => {
                    try
                    {
                        string serializedSourceText = await Windows.Storage.FileIO.ReadTextAsync(File);
                        Students = JsonSerializer.Deserialize<ObservableCollection<Student>>(serializedSourceText);
                    }
                    catch (Exception ex)
                    {
                        var messageDialog = new MessageDialog("Error: " + ex.Message); // vyhození dialogu odsud není ideální, ale funguje
                        await messageDialog.ShowAsync();
                    }                    
                },
                () => true
            );
            Save = new RelayCommand(
                async () => {
                    try 
                    {
                        string serializedData = JsonSerializer.ToJsonString(Students);
                        await Windows.Storage.FileIO.WriteTextAsync(File, serializedData);
                        /* pokusně přes stream
                        var stream = await File.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                        using (var outputStream = stream.GetOutputStreamAt(0))
                        {
                            using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                            {
                                dataWriter.WriteString(JsonSerializer.ToJsonString(_students));
                                await dataWriter.StoreAsync();
                                await outputStream.FlushAsync();
                            }                      
                        }
                        stream.Dispose();
                    */
                    }
                    catch (Exception ex)
                    {
                        var messageDialog = new MessageDialog("Error: " + ex.Message);
                        await messageDialog.ShowAsync();
                    }
                },
                () => true
            );
        }

        public List<Gender> Genders 
        { 
            get {
                return Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            } 
        }
        public ObservableCollection<Student> Students { get { return _students; } set { _students = value; NotifyPropertyChanged(); } }

        public StorageFile File { get { return _file; } set { _file = value; NotifyPropertyChanged(); } }
        public int SelectedStudentIndex { get { return _selectedStudentIndex + 1; } set { _selectedStudentIndex = value; NotifyPropertyChanged(); Remove.RaiseCanExecuteChanged(); } }
        public Student SelectedStudent { get { return _selectedStudent; } set { _selectedStudent = value; NotifyPropertyChanged(); } }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
