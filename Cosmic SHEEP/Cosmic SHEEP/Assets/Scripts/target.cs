
using UnityEngine;

public class target : MonoBehaviour
{
    public float health = 50f;
    public GameObject Explosion;
   

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Explode();
        }
    }

    void Explode()
    {
        GameObject impactGO = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
