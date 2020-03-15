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
* Postup, jakým způsobem udělat aby se v seznamu projevovaly také **editace prvků**
* **DataTemplate** jako nastavení layoutu prvků seznamu
* Práce se **soubory** v UWP
* **Serializování** objektů do JSON formátu

## Bindování seznamu
Už víme, že pokud se mají změny hodnot mezi View a ViewModelem vzájemně promítat, musí ViewModel (nebo jiný sledovaný objekt) implementovat rozhraní [INotyfyPropertyChanged](https://docs.microsoft.com/cs-cz/dotnet/api/system.componentmodel.inotifypropertychanged?view=netcore-3.1).
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