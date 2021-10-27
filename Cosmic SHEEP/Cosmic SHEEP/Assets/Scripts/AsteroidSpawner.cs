 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float spawnSpeed;
    public float distance;
    public bool canSpawn;

    public GameObject spawnObject;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 30 && canSpawn == true)
        {
            StartCoroutine(Spawn(4));
        }
    }

    IEnumerator Spawn(float time)
    {
        canSpawn = false;
        var asteroid = Instantiate(spawnObject, transform.position+ new Vector3(Random.value*2, Random.value*3, Random.value *4 -2), transform.rotation);
        asteroid.GetComponent<Asteroid>().target = target;
        yield return new WaitForSeconds(time);
        canSpawn = true;
    }
}
