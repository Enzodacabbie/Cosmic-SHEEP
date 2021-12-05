using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setUnactive : MonoBehaviour
{
    public GameObject unactiveObject;

    private void OnTriggerEnter(Collider player)
    {
        unactiveObject.SetActive(false);
    }
}
