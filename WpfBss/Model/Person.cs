using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace WpfBss.Model
{
    public class Person : ViewModelBase
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                    _Id = value;
                RaisePropertyChanged("Id");
            }
        }
        private string _FIO;
        public string FIO
        {
            get { return _FIO; }
            set
            {
                if (_FIO != value)
                    _FIO = value;
                RaisePropertyChanged("FIO");
            }
        }
        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set
            {
                if (_Phone != value)
                    _Phone = value;
                RaisePropertyChanged("Phone");
            }
        }
        private DateTime? _BirthDay;
        public DateTime? BirthDay
        {
            get { return _BirthDay; }
            set
            {
                if (_BirthDay != value)
                    _BirthDay = value;
                RaisePropertyChanged("BirthDay");
            }
        }
    }
}
