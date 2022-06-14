using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDo.DbRepository;
using ToDo.Models;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<ToDoModel> _toDoData;
        public MainWindow()
        {
            InitializeComponent();
            _toDoData = new BindingList<ToDoModel>();
            var dbOperations = new DbOperations("data.json");
            try
            {
                _toDoData = dbOperations.Load();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.Close();
            }

            
            _toDoData.ListChanged += ToDoDataOnListChanged;

        }

        private void ToDoDataOnListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded||e.ListChangedType == ListChangedType.ItemChanged||e.ListChangedType == ListChangedType.ItemDeleted)
            {
                try
                {
                    var dbOperations = new DbOperations("data.json");
                    if (sender != null) dbOperations.Save(sender);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgToDoList.ItemsSource = _toDoData;
           
        }
    }
}
