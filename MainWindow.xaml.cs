﻿using System;
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

        // 6.5 Create a button method called “AddNewItem” that will add a new service item to a Queue<> based 
        // on the priority. Use TextBoxes for the Client Name, Drone Model, Service Problem and Service Cost.
        // Use a numeric control for the Service Tag. The new service item will be added to the appropriate
        // Queue based on the Priority radio button.
        public void AddNewItem()
        {

        }
        // 6.17 Create a custom method that will clear all the textboxes after each service item has been added
        public void Clearboxes()
        {
            ClientNameTextbox.Clear();
            DroneModelTextbox.Clear();
            DroneIssueTextbox.Clear();
            ServiceTagTextbox.Clear();
            RepairCostTextbox.Clear();
            RegularRadio.IsChecked = false;
            ExpressRadio.IsChecked = false;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Clearboxes();
        }

        // 6.6 Before a new service item is added to the Express Queue the service cost must be increased by 15%.



    }
}
