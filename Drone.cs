﻿using System;

// 6.1 Create a seperate class file called Drone.cs
// Use seperate setter and getter methods
public class Drone
{
    // 6.1 Ensure attributes are private
    private string _clientName;
    private int _serviceTag;
    private string _serviceProblem;
    private int _cost;
    private string _model;

    public Drone() { }

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
    public int GetCost()
    {
        return _cost;
    }
    public string GetModel()
    {
        return _model;
    }
    public void SetClientName(string clientName)
    {
        clientName = _clientName;
    }
    public void SetServiceTag(int serviceTag)
    {
        serviceTag = _serviceTag;
    }
    public void SetServiceProblem(string serviceProblem)
    {
        serviceProblem = _serviceProblem;
    }
    public void SetCost(int cost)
    {
        cost = _cost;
    }
    public void SetModel(string model)
    {
        model = _model;
    }
}
