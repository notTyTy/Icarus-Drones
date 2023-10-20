using System;
using System.Collections.Generic;
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

namespace Icarus_Drones
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // 6.2 Create a global List<T> of type Drone called “FinishedList”.
        List<Drone> FinishedList = new();
        // 6.3 Create a global Queue<T> of type Drone called “RegularService”.
        Queue<Drone> RegularService = new();
        // 6.4 Create a global Queue<T> of type Drone called “ExpressService”.
        Queue<Drone> ExpressService = new();

    }
}
