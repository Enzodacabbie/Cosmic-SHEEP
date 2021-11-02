using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public Vector3 targetPosition;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = target.transform.position;
        StartCoroutine(die(lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Destroy(this);
    }
}
