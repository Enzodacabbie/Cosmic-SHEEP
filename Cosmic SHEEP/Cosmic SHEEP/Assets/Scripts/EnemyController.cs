using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Unit
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Fire(10, 8, 15, player.transform.position - transform.position);
            yield return new WaitForSeconds(firerate);
        }
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
