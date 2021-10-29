using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemies;
    public Vector3 spawnvalues;
    public float spawnWait;
    public float spawnMostwait;
    public float spawnLeastwait;
    public int current_enemy = 0;
    public int max_enemies;
    public int startWait;
    public bool stop;

    int randEnemy;
    void Start()
    {
        StartCoroutine(waitSpawner());
        
    }

    // Update is called once per frame
    void Update()
    {
       
        spawnWait = Random.Range(spawnLeastwait, spawnMostwait);

        if (current_enemy >= max_enemies)
        {
            stop = true;
        }
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while(!stop)
        {
            randEnemy = Random.Range(0, 2);

            Vector3 spawnPosition = new Vector3(Random.Range(-spawnvalues.x, spawnvalues.x), 1, Random.Range(0, 6*(spawnvalues.z)));

            Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 20), gameObject.transform.rotation);
            current_enemy++;

            yield return new WaitForSeconds(spawnWait);
            
        }
    }
}
