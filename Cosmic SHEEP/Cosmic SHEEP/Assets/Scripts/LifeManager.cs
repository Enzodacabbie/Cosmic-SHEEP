using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    private float _lives;
    public Text LiveNumber;

    private void Start()
    {
        _lives = PlayerData.lives;
        LiveNumber.text = "Lives: "+ _lives;
    }

    private void Update()
    {
        _lives = PlayerData.lives;
        LiveNumber.text = "Lives: " + _lives;
    }
    public float Lives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            LiveNumber.text = Lives.ToString();
        }
    }

}
