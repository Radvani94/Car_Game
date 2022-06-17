using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    void Start()
    {
        Initialize();
    }

    public void GetInput()
    {
        m_horizontal_input = Input.GetAxis("Horizontal");
        m_vertical_input = Input.GetAxis("Vertical");

        if(m_vertical_input>0.0f)
        {
            engine_idle = false;
        }
        else
        {
            engine_idle = true;
        }
    }

    public void Steer()
    {
        m_steerAngle = maxSteerAngle * m_horizontal_input;
        frontRW.steerAngle = m_steerAngle;
        frontLW.steerAngle = m_steerAngle;

    }

    public void Accelerate()
    {
        if (!engine_on)
        {
            frontRW.motorTorque = 0;
            frontLW.motorTorque = 0;
            return;
        }
        frontRW.motorTorque = motorForce * m_vertical_input;
        frontLW.motorTorque = motorForce * m_vertical_input;

    }

    public void UpdateWheelPoses()
    {
        UpdateWheelPose(frontRW, frontRT);
        UpdateWheelPose(frontLW, frontLT);
        UpdateWheelPose(rearLW, rearLT);
        UpdateWheelPose(rearRW, rearRT);

    }

    public void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    public void updateFuelConsumption()
    {
        if (!engine_on) return;
        if (engine_idle) return;
        
        float t = (frontRW.motorTorque / 50) * fuelConsumptionAtTick; //1000 = 100 meters = 0.1km
        //totalFuelConsumption += t / 10000;
        //Debug.Log(totalFuelConsumption);
        
        if(milageController.UpdateMilage(t / 10000)) // sending meters
        {
            KillEngine();
        }
        
    }

    private void Update()
    {
        GetInput();
        Steer();
        Accelerate();
        updateFuelConsumption();
        UpdateWheelPoses();
    }

    void KillEngine()
    {
        engine_on = false;
        Debug.Log("Engine Killed");
    }

    private void Initialize()
    {
        fuelConsumptionAtTick = 0.1f;
        totalFuelConsumption = 0.0f;
        engine_on = true;
        milageController = GetComponent<MilageController>();
        if(milageController == null)
        {
            milageController = new MilageController();
            Debug.Log("New Milage Controller Created");
        }
    }

    private float m_horizontal_input;
    private float m_vertical_input;

    private float m_steerAngle;
    private bool engine_on;

    public WheelCollider frontRW, frontLW;
    public WheelCollider rearRW, rearLW;
    public Transform frontRT, frontLT;
    public Transform rearRT, rearLT;

    public float maxSteerAngle = 30;

    public float motorForce = 50;

    private bool engine_idle = true;
    private float fuelConsumptionAtTick; //liters
    private float totalFuelConsumption;

    private MilageController milageController;
}
