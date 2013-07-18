using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfApplication1.Entities;
using WpfApplication1.Statics;

namespace WpfApplication1.Bus
{
    /// <summary>
    /// Zhidan.xaml 的交互逻辑
    /// </summary>
    public partial class Zhidan : UserControl
    {

        public Zhidan()
        {
            InitializeComponent();
            Obj = new TMain() { ApplyNo = "045102301", CountryNo = "106", ZxNo = "104",CompleteYn = true,DrawbackDt = DateTime.Parse("2013-06-03")};
            this.DataContext = this;
        }

        public ObservableCollection<TBaseCode> Countries
        {
            get{return  TBaseCode.Countries;}
        }

        public ObservableCollection<TBaseCode> Zxs
        {
            get { return TBaseCode.zxs; }
        }

        public TMain Obj { get; set; }
    }
}
