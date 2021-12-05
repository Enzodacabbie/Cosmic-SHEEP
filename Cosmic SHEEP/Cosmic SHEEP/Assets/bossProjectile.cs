using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossProjectile : MonoBehaviour
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
        speed = 40f;
        lifetime = 6.0f;
        targetPosition = target.transform.position;
        destVector = (targetPosition - transform.position).normalized;
        StartCoroutine(die(lifetime));
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
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