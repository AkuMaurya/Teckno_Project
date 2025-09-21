using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Text Score, PlayerHealth, Xp;
    int score = 0;
    public GameObject MainMenu;
    public GameObject Retry;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetScore(int _score)
    {
        if(_score == 0) score = 0;
        else score += _score;
        Score.text = "Score: " + score;
    }

    public void SetXp(int xp)
    {
        Xp.text = "Xp: "+ xp;
    }

    public void SetHealth(int health)
    {
        PlayerHealth.text = "Health: "+ health;
    }
    // Update is called once per frame
    public void QuitApplication()
    {
        Application.Quit();
    }
}
