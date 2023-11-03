﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;


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
        readonly List<Drone> FinishedList = new();
        // 6.3 Create a global Queue<T> of type Drone called “RegularService”.
        readonly Queue<Drone> RegularService = new();
        // 6.4 Create a global Queue<T> of type Drone called “ExpressService”.
        readonly Queue<Drone> ExpressService = new();

        // 6.5 Create a button method called “AddNewItem” that will add a new service item to a Queue<> based 
        // on the priority. Use TextBoxes for the Client Name, Drone Model, Service Problem and Service Cost.
        // Use a numeric control for the Service Tag. The new service item will be added to the appropriate
        // Queue based on the Priority radio button.
        public void AddNewItem()
        {
            if (int.TryParse(ServiceTagTextbox.Text, out int serviceTag) // Service tag needs to be updated to an updown 
                && double.TryParse(RepairCostTextbox.Text, out double cost))
            {
                var priority = GetServicePriority();
                var setInt = Enqueue(serviceTag, cost);
                var queueChosen = priority switch
                {
                    "Regular" => RegularService,
                    "Express" => ExpressService,
                    _ => throw new NotImplementedException()
                };
                queueChosen.Enqueue(setInt);
            }
            else
            {
                MessageBox.Show("Please input cost correctly! (2.dp)", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            DisplayQueue();
            Clearboxes();
        }
        private void DisplayQueue()
        {
            ExpressListview.Items.Clear();
            RegularListview.Items.Clear();
            foreach (Drone item in ExpressService)
            {
                ExpressListview.Items.Add(new
                {
                    GetClientName = item.GetClientName(),
                    GetServiceTag = item.GetServiceTag()
                });
            }
            foreach (Drone item in RegularService)
            {
                RegularListview.Items.Add(new
                {
                    GetClientName = item.GetClientName(),
                    GetServiceTag = item.GetServiceTag()
                });
            }
        }
        private Drone Enqueue(int serviceTag, double cost)
        {
            Drone drone = new();

            drone.SetClientName(ClientNameTextbox.Text);
            drone.SetServiceTag(serviceTag);
            drone.SetServiceProblem(DroneIssueTextbox.Text);
            drone.SetCost(cost);
            drone.SetModel(DroneModelTextbox.Text);
            return drone;
        }
        // 6.17 Create a custom method that will clear all the textboxes after each service item has been added
        public void Clearboxes()
        {
            ClientNameTextbox.Clear();
            DroneModelTextbox.Clear();
            DroneIssueTextbox.Clear();
            ServiceTagTextbox.Clear(); // Needs to be a Numeric Control 
            RepairCostTextbox.Clear();
            RegularRadio.IsChecked = false;
            ExpressRadio.IsChecked = false;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddNewItem();
        }
        // 6.7 Which returns the value of the priority radio group.
        // This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        private string GetServicePriority()
        {
            if (RegularRadio.IsChecked == true | RegularListview.SelectedIndex > -1)
            {
                return "Regular";
            }
            else if (ExpressRadio.IsChecked == true | ExpressListview.SelectedIndex > -1)
            {
                return "Express";
            }
            else return "";
        }

        // 6.6 Before a new service item is added to the Express Queue the service cost must be increased by 15%.
        private void ExpressCost()
        {
            if (GetServicePriority() == "Express")
            {
                double cost = Convert.ToInt32(RepairCostTextbox.Text) * 1.15;
                RepairCostTextbox.Text = cost.ToString();
                // Need to add regext logic for the textbox
            }
        }
        public void DisplayTextboxes()
        {
            ClientNameTextbox.Text = RegularListview.SelectedValue.ToString();
        }

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var priority = GetServicePriority();
            var queueChosen = priority switch
            {
                "Regular" => RegularService,
                "Express" => ExpressService,
                _ => throw new NotImplementedException()
            };
            int index = default;
            if((GetServicePriority() == "Regular" && RegularListview.SelectedIndex > -1 ))
            {
                ExpressListview.UnselectAll();

                index = RegularListview.SelectedIndex;
            }
            else if (GetServicePriority() == "Express" && ExpressListview.SelectedIndex > -1)
            {
                RegularListview.UnselectAll();

                index = ExpressListview.SelectedIndex;
            }
            ClientNameTextbox.Text = queueChosen.ElementAt(index).GetClientName();
            DroneModelTextbox.Text = queueChosen.ElementAt(index).GetModel();
            DroneIssueTextbox.Text = queueChosen.ElementAt(index).GetServiceProblem();
            ServiceTagTextbox.Text = queueChosen.ElementAt(index).GetServiceTag().ToString();
            RepairCostTextbox.Text = queueChosen.ElementAt(index).GetCost().ToString();
            
        }

    }
}