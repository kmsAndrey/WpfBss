using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBss.ViewModel;
using WpfBss.Model;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;

namespace WpfBss.Logic
{
    public static class XmlData
    {

        public static void SerializationData(ref ObservableCollection<Employee> Employees, ref ObservableCollection<Client> Clients, Client ObjClient, Employee ObjEmployee)
        {
            try
            {
                XmlSerializer formatterEmployee = new XmlSerializer(typeof(ObservableCollection<Employee>));
                using (FileStream fs = new FileStream($"{ObjEmployee.GetType().Name}.xml", FileMode.OpenOrCreate))
                {
                    formatterEmployee.Serialize(fs, Employees);
                }
                XmlSerializer formatterClient = new XmlSerializer(typeof(ObservableCollection<Client>));
                using (FileStream fs = new FileStream($"{ObjClient.GetType().Name}.xml", FileMode.OpenOrCreate))
                {
                    formatterClient.Serialize(fs, Clients);
                }
                MessageBox.Show($"Созданы xml файлы {ObjClient.GetType().Name} и {ObjEmployee.GetType().Name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void DeSerializeSerializationData(ref ObservableCollection<Employee> Employees, ref ObservableCollection<Client> Clients, Client ObjClient, Employee ObjEmployee)
        {
            IOpenFileDialogService OpenFileDialogService = new OpenFileDialogService
            {
                Filter = "Text Files (.xml)|*.xml|All Files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Test Dialog",
                Multiselect = true,
            };

            var DialogResult = OpenFileDialogService.ShowDialog();
            if (!DialogResult)
            {
            }
            else
            {
                try
                {
                    XmlSerializer formatterClient = new XmlSerializer(typeof(ObservableCollection<Client>));
                    XmlSerializer formatterEmployee = new XmlSerializer(typeof(ObservableCollection<Employee>));
                    foreach (IFileInfo file in OpenFileDialogService.Files)
                    {
                        if (file.Name.Contains(ObjClient.GetType().Name))
                        {
                            using (FileStream fs = new FileStream(file.Name, FileMode.OpenOrCreate))
                            {
                                Clients = (ObservableCollection<Client>)formatterClient.Deserialize(fs);
                            }
                        }
                        if (file.Name.Contains(ObjEmployee.GetType().Name))
                        {
                            using (FileStream fs = new FileStream(file.Name, FileMode.OpenOrCreate))
                            {
                                Employees = (ObservableCollection<Employee>)formatterEmployee.Deserialize(fs);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
