using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float givenAmount; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dy = Input.GetAxis("Vertical");
        float dx = Input.GetAxis("Horizontal");

        if(Mathf.Abs(dy)> 0.001f) //if we are moving vertically
        {
           transform.localPosition += new Vector3(0, dy*givenAmount*Time.deltaTime, 0);
            StayInBoundsY();
        }
        if(Mathf.Abs(dx) > 0.001f) //if we are moving horizontally
        {
           transform.localPosition += new Vector3(dx*givenAmount*Time.deltaTime, 0, 0);
            StayInBoundsX();
        }
    }
    //This method clamps the player position to the ends of the screen
    void StayInBoundsX() 
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp01(position.x);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    void StayInBoundsY()
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        position.y = Mathf.Clamp01(position.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }
}
