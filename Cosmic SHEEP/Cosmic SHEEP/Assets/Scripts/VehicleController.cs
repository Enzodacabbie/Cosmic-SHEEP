using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VehicleController : MonoBehaviour
{
    public float givenAmount;
    public float health;
    public float maxHealth;
    public float nextDash;
    public float dashCooldown;
    public float dashDistance;
    public float dashTime;
    public float maxSpeed;
    public float minSpeed;
    public float baseSpeed;
   
    public bool  moveable;
    public bool  hittable;

    public CinemachineDollyCart cart;
    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cart = this.GetComponentInParent<CinemachineDollyCart>();

        nextDash = 0f;
        dashCooldown = 3f;
        dashDistance = 5f;
        dashTime = 1f;
        baseSpeed = cart.m_Speed;
        maxSpeed = 8f;
        minSpeed = 2f;

        moveable = true;
        hittable = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        float dy = Input.GetAxis("Vertical");
        float dx = Input.GetAxis("Horizontal");
        float dashx = Input.GetAxis("Dashing");
        float movement = Input.GetAxis("Boost");

        if(Mathf.Abs(dy)> 0.001f && moveable == true) //if we are moving vertically
        {
            transform.localPosition += new Vector3(0, dy*givenAmount*Time.deltaTime, 0);
            StayInBoundsY();
        }
        if (Mathf.Abs(dx) > 0.001f && moveable == true) //if we are moving horizontally
        {
            transform.localPosition += new Vector3(dx*givenAmount*Time.deltaTime, 0, 0);
            StayInBoundsX();
        }

        if(Mathf.Abs(dashx) > 0.001f && Time.time > nextDash) //if there is dash input and cooldown is done
        {
            if(dashx > 0.0f) //dash to the right
            {
                StartCoroutine(SpinRight(dashTime));
            }
            else //dash to the left
            {
                StartCoroutine(SpinLeft(dashTime));
            }
            nextDash = Time.time + dashCooldown; //nextDash becomes greater than Time so cannot be done until time passes it
            StayInBoundsX();
        }

        if(Mathf.Abs(movement) > 0.001f)
        {
            changeSpeed(movement);
        }
        if (cart.m_Speed != baseSpeed && Mathf.Abs(movement) == 0)
        {
            resetSpeed();
        }
    }

    void changeSpeed(float boostSpeed)
    {
        if (boostSpeed > 0.0f)
        {
            if (cart.m_Speed < maxSpeed)
            {
                cart.m_Speed += 0.1f;
                changeCameraZoom(90);
            }
        }

        else
        {
            if (cart.m_Speed > minSpeed)
            {
                cart.m_Speed -= 0.1f;
                changeCameraZoom(40);
            }
               
        }
    }

    void resetSpeed()
    {
        if (cart.m_Speed < baseSpeed)
            cart.m_Speed += 0.1f;
        else
            cart.m_Speed -= 0.1f;
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

    void changeCameraZoom(float zoom)
    {
        if (zoom > 60)
        {
            if (cam.fieldOfView < 90)
                cam.fieldOfView += (300 * Time.deltaTime);
        }
        else if (zoom < 60)
        {
            if (cam.fieldOfView > 40)
            {
                cam.fieldOfView -= (300 * Time.deltaTime);
            }
        }
        else
        {
            cam.fieldOfView = 60;
        }
        
        //cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, cam.fieldOfView + 500 * Time.deltaTime, 90*Time.deltaTime);
    }

    public void OnDeath()
    {
        Destroy(this);
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) {
            OnDeath(); 
        }
    }

    IEnumerator SpinRight(float time)
    {
        hittable = false;
        Vector3 endPosition = new Vector3(transform.localPosition.x+5f, transform.position.y, transform.position.z);
        float savedHealth = health; 
        float i = 0.0f;

        health = 1000.0f;
        while (transform.position != endPosition &&  i < time)
        {
            i += Time.deltaTime;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, endPosition, 20f * Time.deltaTime);
            transform.Rotate(0f, 0f, 360f * time * Time.deltaTime, Space.Self);
           
            yield return null;
        }
        health = savedHealth;
        hittable = true;
    }

    IEnumerator SpinLeft(float time)
    {
        hittable = false;
        Vector3 endPosition = new Vector3(transform.localPosition.x - 5f, transform.position.y, transform.position.z);
        float savedHealth = health;
        float i = 0.0f;

        health = 1000.0f;
        while (transform.position != endPosition && i < time)
        {
            i += Time.deltaTime;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, endPosition, 20f * Time.deltaTime);
            transform.Rotate(0f, 0f, 360f * time * Time.deltaTime, Space.Self);

            yield return null;
        }
        health = savedHealth;
        hittable = true;
    }

    
}
