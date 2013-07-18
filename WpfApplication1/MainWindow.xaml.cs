using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Com.Dao.ExtendControls;
using WpfApplication1.Entities;
using WpfApplication1.Statics;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public TMain Obj { get; set; }

        public ObservableCollection<TBaseCode> Countries {get {return Statics.TBaseCode.Countries;}} 

        public MainWindow()
        {
            InitializeComponent();

            Obj = new TMain();
            Obj.CountryNo = "105";
            this.DataContext = Obj;
//            CountryTxtBox.SetBinding(ExTextAuto.TextProperty, new Binding("CountryNo") { Source = tMain });

//            CountryTxtBox.DataProvider = Countries;
//            CountryTxtBox.Text = "102";
//            tMain = new TMain();
//            Binding binding = new Binding("CountryNo");
//            binding.Source = tMain;
//            CountryTxtBox.SetBinding(ExTextAuto.TextProperty, binding);
//          //  binding.Path = new PropertyPath("ApplyNo");
//            BindingOperations.SetBinding(this.helloText, TextBox.TextProperty, binding);
            helloBt.Click += (sender, args) => Obj.CountryNo = "108";
//            helloText.SetBinding(TextBox.TextProperty, new Binding("ApplyNo") { Source = tMain = new TMain() });
//
//            City city1 = new City() { Name = "深圳" };
//            City city2 = new City() { Name = "广州" };
//            City city3 = new City() { Name = "合肥" };
//            Province p1 = new Province(){Name = "广东",CityList = new List<City>{ city1,city2}};
//            Province p2 = new Province() { Name = "安徽", CityList = new List<City> { city3 } };
//
//
//            List<Country> countryList = new List<Country>()
//                {
//                    new Country(){Name = "中国",ProvinceList = new List<Province>{p1,p2}},
//                    new Country(){Name = "中国",ProvinceList = new List<Province>{p1,p2}}
//                    
//                };
//           // MessageBox.Show(countryList[0].ProvinceList[0].CityList[1].Name);
//            country.SetBinding(TextBox.TextProperty, new Binding("/Name") {Source = countryList});
//            province.SetBinding(TextBox.TextProperty, new Binding("/ProvinceList/Name") { Source = countryList });
//            city.SetBinding(TextBox.TextProperty, new Binding("/ProvinceList/CityList/Name") { Source = countryList });
//
//            this.setBinging();
        }

        private void setBinging()
        {
            ObjectDataProvider odp = new ObjectDataProvider();
            odp.ObjectInstance = new Calculate();
            odp.MethodName = "Add";
            odp.MethodParameters.Add("0");
            odp.MethodParameters.Add("0");

            Binding binding0 = new Binding("MethodParameters[0]");
            binding0.Source = odp;
            binding0.BindsDirectlyToSource = true;
            binding0.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding binding1 = new Binding("MethodParameters[1]");
            binding1.Source = odp;
            binding1.BindsDirectlyToSource = true;
            binding1.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding binding2 = new Binding(".");
            binding2.Source = odp;

            this.a0.SetBinding(TextBox.TextProperty, binding0);
            this.a1.SetBinding(TextBox.TextProperty, binding1);
            this.r0.SetBinding(TextBox.TextProperty, binding2);
        }

        private void bt_OnClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello WPF");

            this.Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            //txtAnswer.Text =   txtQuestion.Text;
            this.Cursor = null;
        }

        private void CountryTxtAuto_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
//            var auto = sender as ExTextAuto;
//            core.TBaseCode code = auto.SelectedListBoxItem as core.TBaseCode;
//            if(code != null)
//                MessageBox.Show(code.Ucode);
        }

        private void CountryTxtLookup_OnSearch(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var auto = sender as ExTextLookup;
            auto.DataItems = Countries;
            this.Cursor = null;
        }
    }
}
