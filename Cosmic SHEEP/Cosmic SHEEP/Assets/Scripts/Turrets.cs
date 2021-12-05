using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    // Start is called before the first frame update

    public float health;
    public float shootSpeed;
    public float maxHealth;
    public float range; //distance that enemy will begin shooting
    public float type; //this refers to type of enemy, 1 is square, 2 is triangle
    public float shootLimit; //the amount of projectiles it will fire at once
    public bool canShoot; //if the enemy is close enough to shoot

    public GameObject projectile;
    public GameObject target;
    public AudioSource shootSound;

    void Start()
    {
        Physics.IgnoreLayerCollision(7, 6, true);
        getEnemyType();
        range = 20f;
        health = 100.0f;
        shootSpeed = 4f;
        maxHealth = 150;
        canShoot = false;
        StartCoroutine(shoot(shootSpeed));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void getEnemyType()
    {
        if (this.gameObject.tag == "Square")
        {
            type = 1;
        }
        else if (this.gameObject.tag == "Triangle")
            type = 2;

        else if (this.gameObject.tag == "Hexagon")
            type = 3;
        else if (this.gameObject.tag == "Staff")
            type = 5;
        else
            type = 4;
    }

    IEnumerator shoot(float time)
    {

        //determine how many projectiles to shoot
        if (type == 1)
            shootLimit = 4;
        else if (type == 2)
            shootLimit = 3;
        else if (type == 3)
            shootLimit = 6;
        else if (type == 4)
            shootLimit = 1;
        else if (type == 5)
            shootLimit = 12;

        while (health > 0)
        {
            
            float i = 0;
            while (i < shootLimit)
            {
                var enemyProjectile = Instantiate(projectile, transform.position + new Vector3(0, 0, -2), transform.rotation);
                enemyProjectile.GetComponent<Projectile>().target = target;
                enemyProjectile.GetComponent <bossProjectile>().target = target;
                if (shootSound != null)
                {
                    shootSound.PlayOneShot(shootSound.clip, 0.7f);
                }

                yield return new WaitForSeconds(0.2f);
                i++;
            }

            yield return new WaitForSeconds(time);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var obj = collision.gameObject.GetComponent<VehicleController>();
            obj.OnDeath();
        }
    }
}