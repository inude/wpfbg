using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WpfApplication1.Entities
{
    public class TMain : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string applyNo;
        public string ApplyNo
        {
            get { return applyNo; }
            set
            {
                this.applyNo = value;
                RaisePropertyChanged("ApplyNo");
            }
        }

        private string countryNo;
        public string CountryNo
        {
            get { return countryNo; }
            set
            {
                this.countryNo = value;
                RaisePropertyChanged("CountryNo");
            }
        }

        private string zxNo;
        public string ZxNo
        {
            get { return zxNo; }
            set
            {
                this.zxNo = value;
                RaisePropertyChanged("ZxNo");
            }
        }

        private bool completeYn;
        public bool CompleteYn
        {
            get { return completeYn; }
            set
            {
                this.completeYn = value;
                RaisePropertyChanged("CompleteYn");
            }
        }

        private DateTime drawbackDt;
        public DateTime DrawbackDt
        {
            get { return drawbackDt; }
            set
            {
                this.drawbackDt = value;
                RaisePropertyChanged("DrawbackDt");
            }
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }
    }


    public class City
    {
        public string Name { get; set; }
    }

    public class Province
    {
        public string Name { get; set; }
        public List<City> CityList { get; set; }
    }

    public class Country
    {
        public string Name { get; set; }
        public List<Province> ProvinceList { get; set; }
    }

    public class Calculate
    {
        public string Add(string arg0, string arg1)
        {
            double a = 0,b = 0;
            if (double.TryParse(arg0, out a) && double.TryParse(arg1, out b))
            {
                return (a + b).ToString();
            }
            return "输入错误";
        }
    }
}