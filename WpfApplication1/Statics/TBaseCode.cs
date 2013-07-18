using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfApplication1.Statics
{
    public class TBaseCode
    {

        public string Ucode { get; set; }
        public string Namec { get; set; }

        public static readonly ObservableCollection<TBaseCode> Countries = new ObservableCollection<TBaseCode>
            {
                new TBaseCode() {Ucode = "101", Namec = "101 美国"},
                new TBaseCode() {Ucode = "102", Namec = "102 德国"},
                new TBaseCode() {Ucode = "103", Namec = "103 法国"},
                new TBaseCode() {Ucode = "104", Namec = "104 英国"},
                new TBaseCode() {Ucode = "105", Namec = "105 日本"},
                new TBaseCode() {Ucode = "106", Namec = "106 中国"},
                new TBaseCode() {Ucode = "107", Namec = "107 印度"},
                new TBaseCode() {Ucode = "108", Namec = "108 香港"},
                new TBaseCode() {Ucode = "109", Namec = "109 台湾"}
            };

        public static readonly ObservableCollection<TBaseCode> zxs = new ObservableCollection<TBaseCode>
            {
                new TBaseCode() {Ucode = "101", Namec = "101 有纸"},
                new TBaseCode() {Ucode = "102", Namec = "102 中心"},
                new TBaseCode() {Ucode = "103", Namec = "103 自存"},
                new TBaseCode() {Ucode = "104", Namec = "104 现场"},
                new TBaseCode() {Ucode = "105", Namec = "105 无纸"}
            };
    }
}
