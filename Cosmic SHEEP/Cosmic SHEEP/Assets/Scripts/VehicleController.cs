using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float givenAmount;
    public float health;
    public float maxHealth;
    public float nextDash;
    public float dashCooldown;
    public float dashDistance;

    // Start is called before the first frame update
    void Start()
    {
        nextDash = 0;
        dashCooldown = 3;
        dashDistance = 5;
    }

    // Update is called once per frame
    void Update()
    {
        float dy = Input.GetAxis("Vertical");
        float dx = Input.GetAxis("Horizontal");
        float dashx = Input.GetAxis("Dashing");

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
        if(Mathf.Abs(dashx) > 0.001f && Time.time > nextDash) //if there is dash input and cooldown is done
        {
            Debug.Log("Dashing");
            if(dashx > 0.0f) //dash to the right
            {
                print(dashx);
                //transform.rotation = Quaternion.Slerp()
                transform.localPosition += new Vector3(dashDistance, 0, 0);
            }
            else //dash to the left
            {
                transform.localPosition += new Vector3(-dashDistance, 0, 0);
            }
            nextDash = Time.time + dashCooldown; //nextDash becomes greater than Time so cannot be done until time passes it
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

    public void OnDeath()
    {
        Destroy(this);
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            OnDeath(); 
        }
    }
}
