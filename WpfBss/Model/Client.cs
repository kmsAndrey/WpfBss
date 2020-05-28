using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace WpfBss.Model
{
    public static class ClientModelExtensions
    {
        public static Client ConvertFrom(this Client source)
        {
            return new Client
            {
                 Id=source.Id,
                 MedicalCard =source.MedicalCard,
                 Person=source.Person
            };
        }
    }

    public class Client : ViewModelBase
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
                if(_Person!=value)
                {
                    _Person = value;
                    RaisePropertyChanged("Person");
                }
            }
        }
        private string _MedicalCard;
        public string MedicalCard
        {
            get { return _MedicalCard; }
            set
            {
                if (_MedicalCard != value)
                    _MedicalCard = value;
                RaisePropertyChanged("MedicalCard");
            }
        }
    }
}
