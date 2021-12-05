using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject activeObject;

    private void OnTriggerEnter(Collider player)
    {
        activeObject.SetActive(true);
    }
}
