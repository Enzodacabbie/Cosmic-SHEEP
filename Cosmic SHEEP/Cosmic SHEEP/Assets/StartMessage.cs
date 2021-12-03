using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMessage : MonoBehaviour
{
    public GameObject message;
    // Start is called before the first frame update
    void Start()
    {
        message.SetActive(false);
    }
    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            message.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(4.5f);
        Destroy(message);
        Destroy(gameObject);
    }
    
}
