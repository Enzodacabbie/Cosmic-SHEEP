
using UnityEngine;

public class SciFiWeapon : MonoBehaviour
{
    public bool isFiring;
    public float damage = 1f;
    public float range = 100f;
    public float fireRate = .5f;

    //public Camera fpsCam;
    public GameObject barrel;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactEffect;
    public TrailRenderer tracerEffect;
   
    RaycastHit hit;
    Ray ray;

    private float nextTimeToFire = 5f;
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            AudioSource gunSound = this.GetComponent<AudioSource>();
            gunSound.PlayOneShot(gunSound.clip, 0.5f);
        }
    }
    void Shoot()
    {
        muzzleFlash.Play();
        
        ray.origin = barrel.transform.position;
        ray.direction = barrel.transform.forward;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);

        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.DrawLine(barrel.transform.position, hit.point, Color.red, 2f);
            Debug.Log(hit.transform.name);

            target t = hit.transform.GetComponent<target>();
            if(t != null)
            {
                t.TakeDamage(damage);
            }
            impactEffect.transform.position = hit.point;
            impactEffect.transform.forward = hit.normal;
            impactEffect.Emit(1);
            tracer.transform.position = hit.point;
       
        }
    }
}
