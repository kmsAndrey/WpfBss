using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using WpfBss.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using DevExpress.Mvvm.UI;
using System.Windows;
using WpfBss.Logic;

namespace WpfBss.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public DelegateCommand SaveActionCommand { get; set; }
        public DelegateCommand ClearActionCommand { get; set; }
        public DelegateCommand SerializeActionCommand { get; set; }
        public DelegateCommand DeSerializeActionCommand { get; set; }
        public DelegateCommand LoadDBActionCommand { get; set; }
        public DelegateCommand NewUserActionCommand { get; set; }
        public DelegateCommand InitCommand { get; set; }
        public DelegateCommand DeleteActionCommand { get; set; }

        private bool _TypeUser;
        public bool TypeUser
        {
            get { return _TypeUser; }
            set
            {
                if (_TypeUser != value)
                    _TypeUser = value;
                RaisePropertyChanged("TypeUser");
            }
        }

        private Client _SelectedClient;
        public Client SelectedClient
        {
            get { return _SelectedClient; }
            set
            {
                if (_SelectedClient != value)
                {
                    _SelectedClient = value;
                    UpdateClient();
                }
                RaisePropertyChanged("SelectedClient");
            }
        }

        private Employee _SelectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set
            {
                if (_SelectedEmployee != value)
                {
                    _SelectedEmployee = value;
                    UpdateEmployee();
                }
                RaisePropertyChanged("SelectedEmployee");
            }
        }

        private Client _ObjClient = new Client();
        public Client ObjClient
        {
            get { return _ObjClient; }
            set
            {
                if (_ObjClient != value)
                    _ObjClient = value;
                RaisePropertyChanged("ObjClient");

            }
        }

        private Employee _ObjEmployee = new Employee();
        public Employee ObjEmployee
        {
            get { return _ObjEmployee; }
            set
            {
                if (_ObjEmployee != value)
                    _ObjEmployee = value;
                RaisePropertyChanged("ObjEmployee");
            }
        }

        private Person _ObjPerson = new Person();

        public Person ObjPerson
        {
            get { return _ObjPerson; }
            set
            {
                if (_ObjPerson != value)
                    _ObjPerson = value;
                RaisePropertyChanged("ObjPerson");
            }
        }

        public ObservableCollection<Client> _Clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get { return _Clients; }
            set
            {
                if (_Clients != value)
                {
                    _Clients = value;
                    RaisePropertyChanged("Clients");
                }
            }
        }
        public ObservableCollection<Employee> _Employees = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> Employees
        {
            get { return _Employees; }
            set
            {
                if (_Employees != value)
                {
                    _Employees = value;
                    RaisePropertyChanged("Employees");
                }
            }
        }

        public MainViewModel()
        {
            SaveActionCommand = new DelegateCommand(SaveAction);
            ClearActionCommand = new DelegateCommand(ClearAction);
            SerializeActionCommand = new DelegateCommand(SerializeAction);
            DeSerializeActionCommand = new DelegateCommand(DeSerializeAction);
            LoadDBActionCommand = new DelegateCommand(LoadDBAction);
            NewUserActionCommand = new DelegateCommand(NewUserAction);
            DeleteActionCommand = new DelegateCommand(DeleteAction);
            InitCommand = new DelegateCommand(Init);
        }

        public void Init()
        {
            Clients = new ObservableCollection<Client>();
            Employees = new ObservableCollection<Employee>();
            ObjPerson = new Person();
            ObjClient = new Client();
            ObjEmployee = new Employee();
        }

        public void DeleteAction()
        {
            if (TypeUser == false)
            {
                using (var con = new UserContext())
                {
                    var client = con.Clients.Find(SelectedClient.Id);
                    if (client != null)
                    {
                        con.Clients.Remove(client);
                        con.SaveChanges();
                        Download();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
            }
            else
            {
                using (var con = new UserContext())
                {
                    var employee = con.Employees.Find(SelectedEmployee.Id);
                    if (employee != null)
                    {
                        con.Employees.Remove(employee);
                        con.SaveChanges();
                        Download();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
            }
        }

        public void NewUserAction()
        {
            ObjPerson = new Person();
            ObjClient = new Client();
            ObjEmployee = new Employee();
            SelectedClient = null;
            SelectedEmployee = null;
        }

        public void LoadDBAction()
        {
            Download();
        }
        public void DeSerializeAction()
        {
            ObservableCollection<Employee> Emp = new ObservableCollection<Employee>();
            ObservableCollection<Client> Cli = new ObservableCollection<Client>();
            XmlData.DeSerializeSerializationData(ref Emp, ref Cli, ObjClient, ObjEmployee);
            Employees = Emp;
            Clients = Cli;
        }

        public void SerializeAction()
        {
            ObservableCollection<Employee> Emp = Employees;
            ObservableCollection<Client> Cli = Clients;
            XmlData.SerializationData(ref Emp, ref Cli, ObjClient, ObjEmployee);
        }

        public void Download()
        {
            Clients.Clear();
            Employees.Clear();
            using (var con = new UserContext())
            {
                var i = con.Employees.Select(x => new
                {
                    Id = x.Id,
                    INN = x.INN,
                    PersonId = x.Person.Id,
                    PersonFIO = x.Person.FIO,
                    PersonBirthDay = x.Person.BirthDay,
                    PersonPhone = x.Person.Phone
                }).AsEnumerable().Select(y => new Employee
                {
                    Id = y.Id,
                    INN = y.INN,
                    Person = new Person
                    {
                        Id = y.PersonId,
                        FIO = y.PersonFIO,
                        BirthDay = y.PersonBirthDay,
                        Phone = y.PersonPhone
                    }
                }).ToList();
                i.ForEach(x => Employees.Add(x));
            }
            using (var con = new UserContext())
            {
                var i = con.Clients.Select(x => new
                {
                    Id = x.Id,
                    MedicalCard = x.MedicalCard,
                    PersonId = x.Person.Id,
                    PersonFIO = x.Person.FIO,
                    PersonBirthDay = x.Person.BirthDay,
                    PersonPhone = x.Person.Phone
                }).AsEnumerable().Select(y => new Client
                {
                    Id = y.Id,
                    MedicalCard = y.MedicalCard,
                    Person = new Person
                    {
                        Id = y.PersonId,
                        FIO = y.PersonFIO,
                        BirthDay = y.PersonBirthDay,
                        Phone = y.PersonPhone
                    }
                }).ToList();
                i.ForEach(x => Clients.Add(x));
            }
        }

        public void SaveAction()
        {
            if (TypeUser == false)
            {

                Client client = _ObjClient;
                client.Person = _ObjPerson;
                Client clientUpdate;
                if (String.IsNullOrWhiteSpace(client.Person.Phone) || String.IsNullOrWhiteSpace(client.MedicalCard) || String.IsNullOrWhiteSpace(client.Person.FIO) || ((bool)!client?.Person?.BirthDay.HasValue))
                {
                    MessageBox.Show("Заполните все доступные поля!");
                    return;
                }
                using (var con = new UserContext())
                {
                    if (client?.Id == 0)
                    {
                        clientUpdate = con.Clients.Add(client);
                        con.SaveChanges();
                        Clients.Add(clientUpdate);
                    }
                    else
                    {
                        var i = con.Clients.Find(client.Id);
                        i.MedicalCard = client.MedicalCard;
                        i.Person = con.Persons.Find(client.Person.Id);
                        i.Person.FIO = client.Person.FIO;
                        i.Person.Phone = client.Person.Phone;
                        i.Person.BirthDay = client.Person.BirthDay;
                        con.SaveChanges();
                        Download();
                    }
                }
            }
            else
            {
                Employee employee = _ObjEmployee;
                employee.Person = _ObjPerson;
                Employee employeeUpdate;
                if (String.IsNullOrWhiteSpace(employee.Person.Phone) || String.IsNullOrWhiteSpace(employee.INN) || String.IsNullOrWhiteSpace(employee.Person.FIO) || ((bool)!employee?.Person?.BirthDay.HasValue))
                {
                    MessageBox.Show("Заполните все доступные поля!");
                    return;
                }
                using (var con = new UserContext())
                {
                    if (employee?.Id == 0)
                    {
                        employeeUpdate = con.Employees.Add(employee);
                        con.SaveChanges();
                        Employees.Add(employeeUpdate);
                    }
                    else
                    {
                        var i = con.Employees.Find(employee.Id);
                        i.INN = employee.INN;
                        i.Person = con.Persons.Find(employee.Person.Id);
                        i.Person.FIO = employee.Person.FIO;
                        i.Person.Phone = employee.Person.Phone;
                        i.Person.BirthDay = employee.Person.BirthDay;
                        con.SaveChanges();
                        Download();
                    }
                }
            }
        }

        public void ClearAction()
        {
            Init();
        }

        public void UpdateClient()
        {
            if (SelectedClient != null)
            {
                ObjClient = SelectedClient.ConvertFrom();
                ObjPerson = ObjClient.Person;
                ObjEmployee = new Employee();
                SelectedEmployee = null;
                TypeUser = false;
            }
        }

        public void UpdateEmployee()
        {
            if (SelectedEmployee != null)
            {
                ObjEmployee = SelectedEmployee.ConvertFrom();
                ObjPerson = ObjEmployee.Person;
                ObjClient = new Client();
                SelectedClient = null;
                TypeUser = true;
            }
        }
    }
}
