using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        float health = this.GetComponent<target>().health;
        if (health <= 0)
        {
            SceneManager.LoadScene("Win Screen");
        }
            
    }

    public void change()
    {
        SceneManager.LoadScene("Win Screen");
    }
}
