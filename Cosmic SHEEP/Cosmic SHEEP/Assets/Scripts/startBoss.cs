using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBoss : MonoBehaviour
{
    float speed = 15;

    public GameObject weapons;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.gameObject != null)
        {
            if (boss.GetComponent<target>().health <= 0)
            {
                weapons.SetActive(false);
                SceneManager.LoadScene("Game Over 1");
                //StartCoroutine(endGame());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        weapons.SetActive(true);
        this.GetComponent<AudioSource>().Play();
        StartCoroutine(rise(70));
    }

    IEnumerator rise(float time)
    {
        Vector3 targetPos = weapons.transform.position + new Vector3(0, 15, 0);
        float step = speed * Time.deltaTime;
        float i = 0;
        while (i < time)
        {
            weapons.transform.position = Vector3.MoveTowards(weapons.transform.position, targetPos, step);
            i += 0.1f;
        }
        return null;
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("player test");
    }
}
