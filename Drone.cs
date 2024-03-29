﻿using System;
using System.Globalization;

namespace Icarus_Drones
{

    // 6.1 Create a seperate class file called Drone.cs
    // Use seperate setter and getter methods
    public class Drone
    {
        public Drone() { }

        #region Attributes
        private string _clientName;
        private int _serviceTag;
        private string _serviceProblem;
        private double _cost;
        private string _model;
        #endregion
        // 6.1 Ensure accessor methods are public
        #region Getters
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
        #endregion
        #region Setters
        public void SetClientName(string clientName)
        {
            // 6.1 Ensure that Client name is saved as titlecase
            // Creates a TextInfo based on the "en-US" culture.
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            _clientName = myTI.ToTitleCase(clientName);
        }
        public void SetServiceTag(int serviceTag) => _serviceTag = serviceTag;
        public void SetServiceProblem(string serviceProblem) => _serviceProblem = serviceProblem;
        public void SetCost(double cost) => _cost = cost;
        public void SetModel(string model) => _model = model;
        #endregion
    }
}