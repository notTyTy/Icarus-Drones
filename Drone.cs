using System;

namespace Icarus_Drones
{

    // 6.1 Create a seperate class file called Drone.cs
    // Use seperate setter and getter methods
    public class Drone
    {
        // 6.1 Ensure attributes are private
        private string _clientName;
        private int _serviceTag;
        private string _serviceProblem;
        private double _cost;
        private string _model;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Drone() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        // 6.1 Ensure accessor methods are public
        public string GetClientName() // Getter
        {
            return _clientName;
        }
        public int GetServiceTag()
        {
            return _serviceTag;
        }
        public string GetServiceProblem()
        {
            return _serviceProblem;
        }
        public double GetCost()
        {
            return _cost;
        }
        public string GetModel()
        {
            return _model;
        }
        public void SetClientName(string clientName)
        {
            _clientName = clientName;
        }
        public void SetServiceTag(int serviceTag)
        {
            _serviceTag = serviceTag;
        }
        public void SetServiceProblem(string serviceProblem)
        {
            _serviceProblem = serviceProblem;
        }
        public void SetCost(double cost)
        {
            _cost = cost;
        }
        public void SetModel(string model)
        {
            _model = model;
        }
    }
}