using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float firerate;
    public float health;
    public float maxhealth;
    public GameObject projectile;
    public float last_shot;
    // Start is called before the first frame update
    void Start()
    {
        last_shot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void OnDeath()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void Fire(float damage, float lifetime, float speed, Vector3 direction)
    {
        if (last_shot + firerate > Time.time) return;
        Debug.Log("fired");
        GameObject proj = Instantiate(projectile, transform.position + direction.normalized * 0.5f, Quaternion.LookRotation(direction));
        var projcontroller = proj.GetComponent<ProjectileController>();
        projcontroller.damage = damage;
        projcontroller.lifetime = lifetime;
        projcontroller.speed = speed;
        projcontroller.owner = this;
        last_shot = Time.time;
    }


}
