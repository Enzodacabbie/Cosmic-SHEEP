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
    }

    private void Awake()
    {
        speed = 30f;
        lifetime = 7.0f;
        targetPosition = target.transform.position;
        StartCoroutine(die(lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, distanceMoved);
    }


    IEnumerator die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var obj = collision.gameObject.GetComponent<VehicleController>();
            obj.OnDeath();
        }
        Destroy(this);
    }
}
