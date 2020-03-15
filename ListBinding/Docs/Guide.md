# Seznamy a šablony dat

Toto cvičení představuje několik nových vlastností MVVM v UWP aplikacích. Celá aplikace má představovat seznam studentů u kterých chceme evidovat, jestli již byli vyzkoušeni.
Aplikace je psaná pro UWP, je možné, že práce se soubory se ve WPF bude lišit.

Předpokládáme, že už ovládáte:
* Pro**bindování** vlastnosti z ViewModelu do View a naopak
* Mechanismus **Command**ů a to i těch s parametrem
* Základní **vlastnosti**, které mají komponenty v Page
* **Stylování** prvků
* **Convertery**

Nové vlastnosti jsou:
* **[Nabindování seznamu](#bindovani-seznamu)** objektů mezi ViewModel a View tak, aby se přidání a odebrání prvku projevilo ve View
* Postup, jakým způsobem udělat aby se v seznamu [projevovaly také **editace prvků**](#bindování-prvku-seznamu)
* **[DataTemplate](#datatemplate)** jako nastavení layoutu prvků seznamu
* [Dialogy](#dialogy)
* Práce se **[soubory](#soubory)** v UWP
* **[Serializování](#serializace)** objektů do JSON formátu

## Bindování seznamu
Už víme, že pokud se mají změny hodnot mezi View a ViewModelem vzájemně promítat, musí ViewModel (nebo jiný sledovaný objekt) implementovat rozhraní [INotifyPropertyChanged](https://docs.microsoft.com/cs-cz/dotnet/api/system.componentmodel.inotifypropertychanged?view=netcore-3.1).
Logickým krokem by tedy bylo dát mezi vlastnosti List<>. Je ale potřeba si uvědomit, že pak je ten List předáván a sledován referencí a celý mechanismus by pak viděl jen vytvoření, zrušení nebo nahrazení seznamu. Neprojevovalo by se například přidání prvku, protože by nedošlo ke změně adresy Listu a List samotný tuto informaci neumí signalizovat (neimplementuje INotifyPropertyChanged).

Proto je nutné použít jinou kolekci, která umí signalizovat změnu svého stavu přes svoji událost *CollectionChanged*. Takovou kolekcí je [ObservableCollection<T>](https://docs.microsoft.com/cs-cz/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netcore-3.1). Obecně by se daly vytvořit i jiné kolekcce, stačí, aby implementovaly INotifyCollectionChanged, INotifyPropertyChanged.

Ve ViewModelu tedy máme:
```
public ObservableCollection<Student> _students = new ObservableCollection<Student>();
```

Ve View se pak na něj z například ListBoxu nebo ListView připojíme takto:
```
<ListBox ItemsSource="{Binding Students}" SelectedIndex="{Binding SelectedStudentIndex, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" SelectedItem="{Binding SelectedStudent, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
```
Přes vlastnost *SelectedItem* můžeme získat aktuálně vybraný prvek a přes *SelectedIndex* jeho index. Obojí lze použít pro manipulaci s kolekcí (mazání a editace prvků).

## Bindování prvku seznamu
Observable Collection má v sobě ovšem jiné objekty. Jediná věc, kterou o svém obsahu ví je to, kolik prvků má. Nevidí dovnitř objektů a tedy nemůže sledovat změnu jejich obsahu.
Pokud tedy dojde ke změně uvnitř objektu, ve View se neprojeví. Aby bylo možné tyto změny sledovat musí objekt v kolekci také implementovat INotifyPropertyChanged. Proto je objekt [Student](../Model/Student.cs) napsán na první pohled složitě.

## DataTemplate
V UWP i WPF je možné vytvořit šablonu pro to, jak mají vypadat nabindovaná data. V našem případě je to ItemTemplate v ListBoxu. Díky tomu jsou prvky seznamu živé a je možné je editovat přímo v seznamu. ItemTemplate se skládá z nějakého kontejneru a jeho nabindovaných částí.

```
<ListBox.ItemTemplate>
    <DataTemplate>
        <Grid HorizontalAlignment="Stretch">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="30"/>
                <ColumnDefinition Width="*" MinWidth="150"/>
                <ColumnDefinition Width="*" MinWidth="150"/>
                <ColumnDefinition Width="30"/>
            </Grid.ColumnDefinitions>
            <SymbolIcon Symbol="{Binding Gender, Converter={StaticResource GenderToSymbolConverter}}" VerticalAlignment="Center"/>
            <CheckBox IsChecked="{Binding Path=Examined, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" Margin="5" VerticalAlignment="Center" Grid.Column="3"/>
            <TextBox Grid.Column="1" Text="{Binding Path=Firstname, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" Margin="5" FontSize="14"/>
            <TextBox Grid.Column="2" Text="{Binding Path=Lastname, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" Margin="5" FontSize="14"/>
        </Grid>
    </DataTemplate>
</ListBox.ItemTemplate>
```
DataTemplate je často vhodné vložit do Page.Resources, jak je ukázáno v dokumentaci.

* [Item Containers and DataTemplates](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/item-containers-templates)

## Dialogy
Pokud chceme pracovat se soubory, měli bychom dát uživateli na vybranou jeho umístění. K tomu slouží systémové dialogy [FileOpenPicker](https://docs.microsoft.com/en-us/uwp/api/windows.storage.pickers.fileopenpicker) a [FileSavePicker](https://docs.microsoft.com/en-us/uwp/api/windows.storage.pickers.filesavepicker). Logicky tato dialogová okna budou spouštěna uživatelem z View a měla by se v samotném View také nacházet. Žádná taková vizuální komponenta ovšem není a budeme je tedy muset vytvářet. Globální data a samotné uložení do souboru se nachází ve ViewModelu a tak námi vytvořené dialogy bude nutné nějak propojit na ViewModel přes DataContext okna.

Protože je nutné dialogová okna vytvořit, budeme to dělat po spuštění aplikace v tzv. *code behind*, to je soubor [MainPage.xaml.cs](../MainPage.xaml.cs). Zde je v konstruktoru příkaz, který vezme XAML a vytvoří z něj skutečné komponenty.
```
this.InitializeComponent();
```
Pod ním si už tedy můžeme vyzvednout a uložit DataContext. Ten si přetypujeme na konkrétní třídu MainView.
```
_vm = (MainViewModel)this.DataContext; 
```
Samotné dialogy budeme otevírat v reakci na událost kliknutí. To se dělá přes vlastnosti komponent pod ikonkou "blesku". Událost je "Click" a její obsluhu lze vytvořit pouhým poklepáním na pole ve vlastnostech komponent. Obsluha nahrávání dat pak vypadá takto:
```
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
```
První tři řádky nastavují vzhled dialogu. Na čtvrtém je dialog otevřen. Sedmý řádek předává proměnnou reprezentující soubor (StorageFile) do ViewModelu. Konečně osmý až jedenáctý řádek spouštějí Command ve ViewModelu.

Podobně je pak nastaven i dialog pro ukládání souboru.

## Soubory

Pro práci se soubory se používá proměnná typu `StorageFile` ve ViewModelu. Návod na [práci se soubory](https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files) poskytuje MSDN. 

Víceméně pro malé soubory stačí jen 
```
await Windows.Storage.FileIO.WriteTextAsync(File, "Ahoj, světe");
string serializedSourceText = await Windows.Storage.FileIO.ReadTextAsync(File);
```
pro větší je pak rozumné použít přístup před Stream.

## Serializace
I když bychom mohli vytvořit nějaký vlastní formát pro ukládání souboru (v předchozím případě pracujeme s textovým souborem) už existuje hotový formát. Tím může být buď [XML](http://programujte.com/clanek/2007030501-xml-pro-zacatecniky-1-cast/) nebo [JSON](https://www.json.org/json-cz.html). Procesu převedení objektu na texovou reprezentaci se říká *serializace*, opačný postup je *deserializace*. 

K serializaci dat použijeme balík *[Utf8Json](https://github.com/neuecc/Utf8Json)* nainstalovaný přes NuGet.
```
Install-Package Utf8Json
```
Kód pro serializaci a deserializaci dat vypadá takto:
```
string serializedData = JsonSerializer.ToJsonString(Students);
Students = JsonSerializer.Deserialize<ObservableCollection<Student>>(serializedSourceText);
```

Nezapomeňme, že souborové operace a samotné serializace se nemusí podařit - soubor nemusí přístupný, formát dat nemusí být srozumitelný pro serializér. Proto by tyto části kódu měly být v chráněném bloku výjimky.

Otevírat dialogové okno MessageDialog z ViewModelu je samozřejmě špatně, měl by být otevírán ve View. To ovšem není jednoduché a tak si to pro tentokrát odpustíme.
