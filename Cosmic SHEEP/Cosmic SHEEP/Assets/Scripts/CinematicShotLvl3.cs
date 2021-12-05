using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicShotLvl3 : MonoBehaviour
{
    public GameObject thing; 
    // Start is called before the first frame update
    void Start()
    {
        thing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        thing.SetActive(true);
        StartCoroutine(panShot());
    }

    IEnumerator panShot()
    {
        return null;
    }
}
