using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public void GetInput()
    {
        m_horizontal_input = Input.GetAxis("Horizontal");
        m_vertical_input = Input.GetAxis("Vertical");

    }

    public void Steer()
    {
        m_steerAngle = maxSteerAngle * m_horizontal_input * 0.5f;
        frontRW.steerAngle = m_steerAngle;
        frontLW.steerAngle = m_steerAngle;

    }

    public void Accelerate()
    {

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

    private void Update()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
    private float m_horizontal_input;
    private float m_vertical_input;

    private float m_steerAngle;

    public WheelCollider frontRW, frontLW;
    public WheelCollider rearRW, rearLW;
    public Transform frontRT, frontLT;
    public Transform rearRT, rearLT;

    public float maxSteerAngle = 30;

    public float motorForce = 50;
    public float brakeForce = 50;

}
