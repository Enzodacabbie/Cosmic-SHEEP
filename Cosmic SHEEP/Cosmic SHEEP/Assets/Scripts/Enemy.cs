using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    void Start()
    {
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
        if (Vector3.Distance(transform.position, target.transform.position) < range)
            canShoot = true;
        else
            canShoot = false;
    }

    public void getEnemyType()
    {
        if (this.gameObject.tag == "Square")
        {
            type = 1;
            print("we are square");
        }
            
        else if (this.gameObject.tag == "Triangle")
            type = 2;
    }

    IEnumerator shoot(float time)
    {
        //determine how many projectiles to shoot
        if (type == 1)
            shootLimit = 4;
        else if (type == 2)
            shootLimit = 3;

        while (health > 0)
        {
            

            float i = 0;
            while (i < shootLimit)
            {
                if (canShoot == true)
                {
                    
                }
                var enemyProjectile = Instantiate(projectile, transform.position + new Vector3(0, 0, -2), transform.rotation);
                enemyProjectile.GetComponent<Projectile>().target = target;
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
