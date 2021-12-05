using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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
    public GameObject thing;


    // Start is called before the first frame update
    void Start()
    {
        cart = this.GetComponentInParent<CinemachineDollyCart>();

        speed = 10;
        nextDash = 0f;
        dashCooldown = 3f;
        dashDistance = 5f;
        dashTime = 1f;
        baseSpeed = cart.m_Speed;
        maxSpeed = 15f;
        minSpeed = 5f;
        health = 100;
        maxHealth = 200;
        lookSpeed = 500;

        moveable = true;
        hittable = true;
        canBoost = true;

        cart.m_Position = PlayerData.lastCheckpoint;
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
            move(dx, dy, speed);
           
        }
        if (Mathf.Abs(dx) > 0.001f && moveable == true) //if we are moving horizontally
        {
            move(dx, dy, speed);
            Lean(thing, dx, 80, 0.1f);
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
        Vector3 look = new Vector3(x, y, 1.0f);
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(look), Mathf.Deg2Rad * 50.0f);
        StayInBounds();
    }

    void Lean(GameObject x, float dx, float leanLimit, float time)
    {
        Vector3 target = transform.localEulerAngles;
        x.transform.localEulerAngles = new Vector3(target.x, target.y, Mathf.LerpAngle(target.z, -dx * leanLimit, time));
    }

    void changeSpeed(float boostSpeed)
    {
        if (boostSpeed > 0.0f)
        {
            if (cart.m_Speed < maxSpeed)
            {
                cart.m_Speed += 0.1f;
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 90, 0.1f);
            }
        }
        else
        {
            if (cart.m_Speed > minSpeed)
            {
                cart.m_Speed -= 0.1f;
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 40, 0.1f);
            }
               
        }
    }

    void resetSpeed()
    {
        if (cart.m_Speed < baseSpeed)   //the player is boosting
        {
            cart.m_Speed += 0.01f;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 0.1f);
        }
        else {
            cart.m_Speed -= 0.01f;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 0.1f);
        }
            
        
    }

    //This method clamps the player position to the ends of the screen
    void StayInBounds()
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp01(position.x);
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
    }

    public void OnDeath()
    {
        Scene currentLevel = SceneManager.GetActiveScene();
        print(currentLevel.name);
        if(currentLevel.name == "Level 3")
        {
            if(cart.m_Position >= PlayerData.levelThreeCheckpoint)
                PlayerData.lastCheckpoint = PlayerData.levelThreeCheckpoint;
            if (cart.m_Position >= PlayerData.levelThreeCheckpoint2)
                PlayerData.lastCheckpoint = PlayerData.levelThreeCheckpoint2;
        }
        else if(currentLevel.name == "Level_ 1")
        {
            if (cart.m_Position >= PlayerData.levelOneCheckpoint)
                PlayerData.lastCheckpoint = PlayerData.levelOneCheckpoint;
        }
        

        PlayerData.lives -= 1;
        if(PlayerData.lives <= 0)
        {
            SceneManager.LoadScene("Game Over.unity");
        }
       
        SceneManager.LoadScene(currentLevel.name);
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
        moveable = false;
        float savedHealth = health;
        float i = 0.0f;
        Vector3 endPosition = new Vector3(transform.localPosition.x+5f, transform.position.y, transform.position.z);
        
        health = 1000.0f;
        while (transform.position != endPosition &&  i < time)
        {
            Physics.IgnoreLayerCollision(0, 6, true);
            i += Time.deltaTime;
            transform.Rotate(0f, 0f, 360f * time * Time.deltaTime, Space.Self);
            yield return null;
        }
        Physics.IgnoreLayerCollision(0, 6, false);

        health = savedHealth;
        moveable = true;
        hittable = true;
        canBoost = true;
    }

    IEnumerator SpinLeft(float time) //does a 360 rotation to the left and sets health to 1000 while doing so
    {

        hittable = false;
        canBoost = false;
        moveable = false;
        float savedHealth = health;
        float i = 0.0f;
        Vector3 endPosition = new Vector3(transform.localPosition.x - 5f, transform.position.y, transform.position.z);

        while (transform.position != endPosition && i < time)
        {
            Physics.IgnoreLayerCollision(0, 6, true);
            i += Time.deltaTime;
            transform.Rotate(0f, 0f, 360f * time * Time.deltaTime, Space.Self);
            yield return null;
        }
        Physics.IgnoreLayerCollision(0, 6, false);

        health = savedHealth;
        moveable = true;
        hittable = true;
        canBoost = true;
    }

    
}
