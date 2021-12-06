
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class target : MonoBehaviour
{
    public float health = 50f;
    public GameObject Explosion;


    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {

            if (this.GetComponent<Enemy>() != null)
            {
                this.GetComponent<Enemy>().health = -100;
            }
            StartCoroutine(Explode());


        }
    }

    IEnumerator Explode()
    {
        GameObject impactGO = Instantiate(Explosion, transform.position, transform.rotation);

        Destroy(gameObject);
        if (gameObject.tag == "Boss")
        {
            SceneManager.LoadScene("Win Screen");
        }
            
        yield return new WaitForSeconds(2);
        Destroy(impactGO);
    }
}