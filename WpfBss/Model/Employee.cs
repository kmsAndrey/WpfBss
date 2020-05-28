using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace WpfBss.Model
{
    public static class EmployeeModelExtensions
    {
        public static Employee ConvertFrom(this Employee source)
        {
            return new Employee
            {
                Id = source.Id,
                INN = source.INN,
                Person = source.Person
            };
        }
    }
    public class Employee : ViewModelBase
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
        private Person _Person;
        public Person Person
        {
            get { return _Person; }
            set
            {
                if (_Person != value)
                    _Person = value;
                RaisePropertyChanged("Person");
            }
        }
        private string _INN;
        public string INN
        {
            get { return _INN; }
            set
            {
                if (_INN != value)
                    _INN = value;
                RaisePropertyChanged("INN");
            }
        }
    }
}
