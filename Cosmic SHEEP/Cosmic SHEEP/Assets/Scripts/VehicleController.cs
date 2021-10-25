using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VehicleController : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float nextDash;
    public float dashCooldown;
    public float dashDistance;
    public float dashTime;
    public float maxSpeed;
    public float minSpeed;
    public float baseSpeed;
    public float lookSpeed;
   
    public bool  moveable;
    public bool  hittable;
    public bool  canBoost;

    public CinemachineDollyCart cart;
    public Camera cam;
    public GameObject aimTarget;


    // Start is called before the first frame update
    void Start()
    {
        cart = this.GetComponentInParent<CinemachineDollyCart>();

        speed = 20;
        nextDash = 0f;
        dashCooldown = 3f;
        dashDistance = 5f;
        dashTime = 1f;
        baseSpeed = cart.m_Speed;
        maxSpeed = 8f;
        minSpeed = 2f;
        health = 100;
        maxHealth = 200;
        lookSpeed = 500;

        moveable = true;
        hittable = true;
        canBoost = true;
        
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
            lookAtTarget(aimTarget.transform.position, dx, dy, lookSpeed);
            move(dx, dy, speed);
        }
        if (Mathf.Abs(dx) > 0.001f && moveable == true) //if we are moving horizontally
        {
            lookAtTarget(aimTarget.transform.position, dx, dy, lookSpeed);
            move(dx, dy, speed);
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
            StayInBounds();
        }

        if(Mathf.Abs(movement) > 0.001f && canBoost == true)
        {
            changeSpeed(movement);
        }
        if (cart.m_Speed != baseSpeed && Mathf.Abs(movement) == 0)
        {
            resetSpeed();
        }
    }

    void move(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        StayInBounds();
    }

    void lookAtTarget(Vector3 pos, float x, float y, float lookspeed)
    {
        Quaternion look = Quaternion.LookRotation(aimTarget.transform.position);
        aimTarget.transform.localPosition = new Vector3(x*2, (y+1)*2, 6);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, look, Mathf.Deg2Rad * lookSpeed * Time.deltaTime);
        transform.LookAt(aimTarget.transform);
    }

    void Lean()
    {

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
        if (cart.m_Speed < baseSpeed) //the player is boosting
            cart.m_Speed += 0.01f;
        else
            cart.m_Speed -= 0.01f;    //the player is breaking
    }

    //This method clamps the player position to the ends of the screen
    void StayInBounds() 
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp01(position.x);
        position.y = Mathf.Clamp01(position.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    void StayInBounds(GameObject x)
    {
        Vector3 position = Camera.main.WorldToViewportPoint(x.transform.position);
        position.x = Mathf.Clamp01(position.x);
        position.y = Mathf.Clamp01(position.y);
        x.transform.position = Camera.main.ViewportToWorldPoint(position);
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

    IEnumerator SpinRight(float time) //does a 360 rotation to the right and sets health 1000 while doing so
    {
        hittable = false;
        canBoost = false;
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
        canBoost = true;
    }

    IEnumerator SpinLeft(float time) //does a 360 rotation to the left and sets health to 1000 while doing so
    {
        hittable = false;
        canBoost = false;
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
        canBoost = true;
    }

    
}
