using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public Vector3 targetPosition;
    public Vector3 destVector;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        //Physics.IgnoreLayerCollision(6, 6, true);
        speed = 30f;
        lifetime = 6.0f;
        targetPosition = target.transform.position + new Vector3(0, 0, 5);
        destVector = (targetPosition - transform.position).normalized;
        StartCoroutine(die(lifetime));
    }

    private void Awake()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        //float distanceMoved = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, distanceMoved);
        transform.position += destVector * speed * Time.deltaTime;
    }


    IEnumerator die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var obj = collision.gameObject.GetComponent<VehicleController>();
            obj.OnDeath();
        }
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
