using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifetime;
    public Unit owner;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherunit = other.gameObject.GetComponent<Unit>();
        if (otherunit != null && otherunit != owner)
        {
            otherunit.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
