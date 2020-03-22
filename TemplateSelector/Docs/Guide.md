# TemplateSelector, ControlTemplate

Toto cvičení představuje několik nových vlastností MVVM v UWP aplikacích. Celá aplikace má představovat seznam lidí, 
kterým bychom mohli posílat mail a u kterých nás zajímá jejich školní rozvrh.
Aplikace je psaná pro UWP.

Předpokládáme, že už ovládáte:
* Pro**bindování** vlastnosti z ViewModelu do View a naopak
* Mechanismus **Command**ů a to i těch s parametrem
* Základní **vlastnosti**, které mají komponenty v Page
* **Stylování** prvků
* **Convertery**
* **Bindování seznamu**
* **DataTemplate**

Nové vlastnosti jsou:
* **[TemplateSelector](#templateselector)** možnost vybírat si mezi několika šablonami
* Postup, jak [vložit do šablony tlačítka svázaná s daty](#tlačítka-v-šablonách)
* **[ControlTemplate](#controltemplate)** kompletní přepsání šablony komponenty
* [Spouštění odkazů na internet](#odkazy-na-internet)
* **[Potvrzovací dialogy](#potvrzovací-dialogy)** v UWP

## Template Selector
V tomto případě máme v modelu tři druhy dat - Person, Student a Teacher, kde Student a Teacher jsou odvození od Person. V bindovaném seznamu Persons je samozřejmě ``private ObservableCollection<Person> _persons = new ObservableCollection<Person>();``. Tentokrát ovšem se všechny zajímavé věci budou odehrávat v [Page](../MainPage.xaml), ViewModel slouží jen jako zdroj dat.

Neměl by být problém data nabindovat, umíme vytvořit i hezkou šablonu. My bychom ale potřebovali, aby Student a Teacher měly úplně jinou funkcionalitu a tedy jinou šablonu. Je tedy nutné vytvořit pro každý typ obsahu samostatnou šablonu, nevkládat do ListBoxu přímo odkaz na šablonu, ale odkaz na něco, co bude rozhodovat o tom, kterou šablonu použít.

Máme tedy šablony:
```
<DataTemplate x:Key="PersonTemplate">...</DataTemplate>
<DataTemplate x:Key="StudentTemplate">...</DataTemplate>
<DataTemplate x:Key="TeacherTemplate">...</DataTemplate>
```
Uvnitř každé z nich pomocí kontajnerů (Grid, StackPanel, ...) rozmístíme prvky šablony. V Resouces vytvoříme odkaz na třídu TemplateSelectoru
```
<view:PersonTemplateSelector x:Key="PersonTemplateSelector" TeacherTemplate="{StaticResource TeacherTemplate}" StudentTemplate="{StaticResource StudentTemplate}" PersonTemplate="{StaticResource PersonTemplate}" />
```
V samotném ListBoxu se na TemplateSelector odkážeme takto
```
<ListBox HorizontalContentAlignment="Stretch" ItemsSource="{Binding Persons}" SelectedItem="{Binding SelectedPerson}" SelectedIndex="{Binding SelectedPersonIndex}" ItemTemplateSelector="{StaticResource PersonTemplateSelector}"/>
```
Kód samotného selectoru je v [samostatném souboru](../ViewHelpers/PersonTemplateSelector.cs). 

## Tlačítka v šablonách
Zajímavé je, že komponenty v šablonách tedy například TextBox, TextBlock, Button a podobně mají automaticky svůj DataContext nastavený na objekt, kterému dělají šablonu. V šabloně tak lze přímo psát například:
```
<TextBlock Text="{Binding Firstname}"
```
A je možné se takto odkazovat na Commandy.

## ControlTemplate
Kromě stylování komponent je možné také kompletně převzít kontrolu nad vykreslováním komponent pomocí [ControlTemplate](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.controltemplate), příklad ukazuje také jak se postarat o stav komponenty, například, když je kurzor nad tlačítkem.
```
<ControlTemplate TargetType="Button" x:Key="MailButton">
    <Border x:Name="MailButtonBorder" CornerRadius="5" BorderThickness="1" Background="Gold" BorderBrush="Black" Padding="3">
        <ContentPresenter x:Name="MyContent" Margin="2" HorizontalAlignment="Center" VerticalAlignment="Center" />
    </Border>
</ControlTemplate>
```
V příkladu nahrazujeme běžný vzhled tlačítka pomocí vykreslení okraje se zakulacenými rohy, zlatým pozadím. Obsah prvku se vloží na místo ``<ContentPresenter>``.

## Odkazy na internet
Naším cílem po stisku tlačítka je otevřít internetový odkaz. K této akci nepotřebujeme žádné data z ViewModelu, data už máme nabindovaná. Kód pro otevření odkazu najdeme v codebehind [Mainpage.xaml.cs](../Mainpage.xaml.cs).
```
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
```
Zajímavá je vazba mezi komponentou reprezentovanou přes Sender a obsluhou události. K tomu využijeme vlastnost Tag s typem object.
```
public void Teacher_Click(Object sender, RoutedEventArgs e)
{
    string shortname = (sender as Button).Tag.ToString();
    launchURI(new Uri("https://web.pslib.cz/pro-studenty/rozvrh/teacher:"+shortname));
}
```
A v komponentě samotné:
```
<Button Template="{StaticResource MailButton}" Tag="{Binding Email}" Click="Email_Click">Email</Button>
```
## Potvrzovací dialogy
Poslední novou vlastností je vytvoření [potvrzovacího dialogu](https://docs.microsoft.com/en-us/uwp/api/windows.ui.popups.messagedialog), který vytvoříme v codebehind:
```
var dialog = new MessageDialog("Are you sure you want to send mail to " +email+ "?", "Send Email");
var confirmCommand = new UICommand("Yes"); // vytvoř tlačítko Yes
var cancelCommand = new UICommand("No"); // vytvoř tlačítko No
dialog.Commands.Add(confirmCommand); // přidej tlačítko
dialog.Commands.Add(cancelCommand); // přidej tlačítko
if (await dialog.ShowAsync() == confirmCommand) // co se má stát, když zmáčkneme Yes
{
// nějaký kód
}
```
