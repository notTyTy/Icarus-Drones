﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Icarus_Drones
{
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
        // queue based on the Priority radio button.
        public void AddNewItem()
        {
            {
                double.TryParse(RepairCostTextbox.Text, out double cost);
                int priority = ServicePriority();

                if (priority != 0)
                {
                    (Queue<Drone> queueChosen, double serviceCost) = priority switch
                    {
                        1 => (RegularService, cost),
                        // 6.6 Before a new service item is added to the Express Queue the service cost must be increased by 15%.
                        2 => (ExpressService, (cost * 1.15)),
                        _ => throw new NotImplementedException()
                    };

                    string finalCost = serviceCost.ToString("F2");
                    Drone setInt = Enqueue(finalCost);
                    queueChosen.Enqueue(setInt);

                    DisplayQueue();
                    Clearboxes();
                }
            }
        }

        // 6.8, 6.9 Create a custom method that will display all the elements in the Regular and Express service queue
        // The display must use a listview and with the appropriate column headers
        private void DisplayQueue()
        {
            ExpressListview.Items.Clear();
            RegularListview.Items.Clear();
            CompletedListbox.Items.Clear();
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
            foreach (Drone item in FinishedList)
            {
                CompletedListbox.Items.Add(new
                {
                    GetClientName = item.GetClientName(),
                    GetCost = item.GetCost()
                });
            }
        }
        private Drone Enqueue(string cost)
        {
            Drone drone = new();

            drone.SetClientName(ClientNameTextbox.Text);
            drone.SetServiceTag(ServiceTag.Value);
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
            RepairCostTextbox.Clear();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddNewItem();
        }

        // TODO Update the ServiceTag Updown to the next incremement


        // 6.7 Which returns the value of the priority radio group.
        // This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        #region ServicePriority(), GetIndex()
        private enum SelectCheck
        {
            None,
            Regular,
            Express
        }
        private int ServicePriority()
        {
            if (RegularRadio.IsChecked == true)
            {
                return (int)SelectCheck.Regular;
            }
            else if (ExpressRadio.IsChecked == true)
            {
                return (int)SelectCheck.Express;
            }
            return (int)SelectCheck.None;
        }
        private int GetIndex()
        {
            if (RegularListview.SelectedItem != null)
            {
                return (int)SelectCheck.Regular;
            }
            else if (ExpressListview.SelectedItem != null)
            {
                return (int)SelectCheck.Express;
            }
            return (int)SelectCheck.None;
        }
        #endregion

        // 6.12, 6.13 Create a mouse click method for the regular and express service ListView that will display the Client Name
        // and service problem in the related textboxes
        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int priority = GetIndex();
            if (priority != 0)
            {
                (Queue<Drone> queueChosen, RadioButton radioSelection, ListView listviewChosen) = priority switch
                {
                    1 => (RegularService, RegularRadio, RegularListview),
                    2 => (ExpressService, ExpressRadio, ExpressListview),
                    _ => throw new NotImplementedException()
                };
                int index = listviewChosen.SelectedIndex;
                ClientNameTextbox.Text = queueChosen.ElementAt(index).GetClientName();
                DroneModelTextbox.Text = queueChosen.ElementAt(index).GetModel();
                DroneIssueTextbox.Text = queueChosen.ElementAt(index).GetServiceProblem();
                ServiceTag.Value = queueChosen.ElementAt(index).GetServiceTag();
                RepairCostTextbox.Text = queueChosen.ElementAt(index).GetCost().ToString();
                radioSelection.IsChecked = true;
            }
        }

        // 6.14, 6.15 Create a button click method that will remove a service item from the regular/express ListView and dequeue
        // The regular/express service from the express service Queue<Drone> data structure. The dequeued item must be added
        // to the List<Drone> and displayed in the ListBox for finished service items
        private void ServicedBtn_Click(object sender, RoutedEventArgs e)
        {
            int priority = ServicePriority();
            if (priority != 0)
            {
                (Queue<Drone> queueChosen, ListView listviewChosen) = priority switch
                {
                    1 => (RegularService, RegularListview),
                    2 => (ExpressService, ExpressListview),
                    _ => throw new NotImplementedException()
                };
                if (listviewChosen.SelectedItems != null)
                {
                    FinishedList.Add(queueChosen.Dequeue());
                }
                Clearboxes();
                DisplayQueue();
                return;
            }
            MessageBox.Show("Please select an item before adding it to the completed order", "Invalid selection", MessageBoxButton.OK);
        }

        // 6.16 Create a double mouse click method that will delete a service item from the finished listbox 
        // and remove the same item from the List<Drone>
        #region Remove order
        private void RemoveOrder()
        {
            int index = CompletedListbox.SelectedIndex;
            if (index > -1)
            {
                FinishedList.RemoveAt(index);
                DisplayQueue();
                return;
            }
            MessageBox.Show("Please select an item before trying to remove it", "Completed order not selected!", MessageBoxButton.OK);
        }
        private void CompletedListbox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RemoveOrder();
        }
        private void CollectedBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveOrder();
        }
        #endregion
        #region LostFocus
        private void ExpressListview_LostFocus(object sender, RoutedEventArgs e)
        {
            ExpressListview.UnselectAll();
            Clearboxes();
        }
        private void RegularListview_LostFocus(object sender, RoutedEventArgs e)
        {
            RegularListview.UnselectAll();
            Clearboxes();
        }
        #endregion
        #region Regex, Space and Paste handling
        private void RepairCostTextbox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string newText = RepairCostTextbox.Text + e.Text;

            Regex regex = new Regex(@"^(?=\d{1,4}(\.\d{0,2})?$)\d{1,4}(\.\d{0,2})?$");
            e.Handled = !regex.IsMatch(newText);
        }
        private void RepairCostTextbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == ((Key.LeftCtrl | Key.RightCtrl) & Key.V)
                || e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}