using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilageController : MonoBehaviour
{
    
    private float distance_driven = 0.0f; // kms
    private float milage = 10.0f; //km per liter
    private float tank_capacity = 10.0f; // liters
    private float tank_gague = 0.0f;  // liters
    private bool empty_tank = false;


    public MilageController()
    {
        Initialize();
    }

    public void Initialize()
    {
        distance_driven = 0.0f; // car should drive only 1km
        tank_gague = 1f; // refueling to 0.1L
    }

    public bool UpdateMilage(float _distance_travelled_tick)
    {
        distance_driven += _distance_travelled_tick;
        float new_tank = distance_driven / milage;
        tank_gague -= new_tank;
        if(tank_gague <= 0.0f)
        {
            empty_tank = true; 
        }
        
        Debug.Log("tank gauge: "+tank_gague);
        return empty_tank;

    }

    public void Refuel()
    {
        tank_gague = tank_capacity;
    }
    /*
            Milage Controller
    
        Game Start Fuel Level
        Car Distance Travelled
        Calculate Milage
        Car is coasting.. End State Incoming

     */


}
