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
        speed = 6;
        minSpeed = 3;
        damageDealt = 20;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, distanceMoved);

        if (Vector3.Distance(transform.position, target.transform.position) < 4)
        {
            speed = minSpeed;
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
            print("here");
            var obj = collision.gameObject.GetComponent<VehicleController>();
            obj.takeDamage(damageDealt);
            print(obj.health);
            OnDeath();
        }
    }
}
