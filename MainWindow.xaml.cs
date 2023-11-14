using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

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
            if (int.TryParse(ServiceTagTextbox.Text, out int serviceTag) // Service tag needs to be updated to an updown 
                && double.TryParse(RepairCostTextbox.Text, out double cost))
            {
                int priority = ServicePriority();
                var setInt = Enqueue(serviceTag, cost);
                var queueChosen = priority switch
                {
                    1 => RegularService,
                    2 => ExpressService,
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
        private Drone Enqueue(int serviceTag, double cost)
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
            ServiceTagTextbox.Clear(); // Needs to be a Numeric Control 
            RepairCostTextbox.Clear();
            RegularRadio.IsChecked = false;
            ExpressRadio.IsChecked = false;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddNewItem();

        }
        // 6.6 Before a new service item is added to the Express Queue the service cost must be increased by 15%.
        private void ExpressCost()
        {
            if (ServicePriority() == 2)
            {
                double cost = Convert.ToInt32(RepairCostTextbox.Text) * 1.15;
                RepairCostTextbox.Text = cost.ToString();
                // Need to add regex logic for the textbox
            }
        }
        private enum SelectCheck
        {
            None,
            Regular,
            Express
        }
        // 6.7 Which returns the value of the priority radio group.
        // This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
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
                ServiceTagTextbox.Text = queueChosen.ElementAt(index).GetServiceTag().ToString();
                RepairCostTextbox.Text = queueChosen.ElementAt(index).GetCost().ToString();
                radioSelection.IsChecked = true;
            }
        }
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
        #region LostFocus
        private void ExpressListview_LostFocus(object sender, RoutedEventArgs e)
        {
            ExpressListview.UnselectAll();
        }
        private void RegularListview_LostFocus(object sender, RoutedEventArgs e)
        {
            RegularListview.UnselectAll();
        }
        #endregion
    }
}
