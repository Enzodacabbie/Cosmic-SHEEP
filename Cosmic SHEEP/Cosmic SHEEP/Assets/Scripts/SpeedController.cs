using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public float baseSpeed;
    public float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = this.GetComponentInChildren<VehicleController>().maxSpeed;
        minSpeed = this.GetComponentInChildren<VehicleController>().minSpeed;
        baseSpeed = this.GetComponentInChildren<VehicleController>().baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Boost");


    }
}
