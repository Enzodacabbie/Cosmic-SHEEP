using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float health;
    public float speed;
    public float minSpeed;
    public float damageDealt;
    
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        speed = 3;
        minSpeed = 1;
        damageDealt = 20;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, distanceMoved); //move towards the player

        Vector3 toTarget = (target.transform.position - transform.position);
        

        if (Vector3.Distance(transform.position, target.transform.position) < 4) //if near the player, slow down
        {
            speed = minSpeed;
        }

        if (Vector3.Dot(toTarget, transform.forward) -15 > 0) //this checks if the asteroid is behind the player (checks actually 4 units behind player)
        {
            OnDeath();
        }
            

    }    

    void takeDamage(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var obj = collision.gameObject.GetComponent<VehicleController>();
            obj.OnDeath();
            OnDeath();
        }
    }
}
