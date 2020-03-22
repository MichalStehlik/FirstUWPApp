using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TemplateSelector
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public void Teacher_Click(Object sender, RoutedEventArgs e)
        {
            string shortname = (sender as Button).Tag.ToString();
            launchURI(new Uri("https://web.pslib.cz/pro-studenty/rozvrh/teacher:"+shortname));
        }

        public void Class_Click(Object sender, RoutedEventArgs e)
        {
            string classname = (sender as Button).Tag.ToString();
            launchURI(new Uri("https://web.pslib.cz/pro-studenty/rozvrh/class:" + classname));
        }

        public async void Email_Click(Object sender, RoutedEventArgs e)
        {
            string email = (sender as Button).Tag.ToString();
            var dialog = new MessageDialog("Are you sure you want to send mail to " +email+ "?", "Send Email");
            var confirmCommand = new UICommand("Yes");
            var cancelCommand = new UICommand("No");
            dialog.Commands.Add(confirmCommand);
            dialog.Commands.Add(cancelCommand);
            if (await dialog.ShowAsync() == confirmCommand)
            {
                launchURI(new Uri("mailto:" + email));
            }
        }

        private async void launchURI(Uri path)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(path);

            if (success)
            {
                // URI launched
            }
            else
            {
                // URI launch failed
            }
        }
    }
}
