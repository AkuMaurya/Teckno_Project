using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int level = 1,score = 0;
    public int currentExp = 0;
    public int expToNextLevel = 100;
    public int health = 100;
    public GameManager manager;
    public UIController uiController;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        manager = GameObject.Find("Plane").GetComponent<GameManager>();
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        InitializeValues();
    }
    public void InitializeValues()
    {
        level = 1;
        currentExp = 0;
        score = 0;
        expToNextLevel = 100;
        health = 100;
        uiController.SetScore(score);
        uiController.SetXp(currentExp);
        uiController.SetHealth(health);

    }
    public void GainExp(int amount)
    {
        currentExp += amount;
        uiController.SetXp(currentExp);
        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentExp = 0;
        expToNextLevel += 50; // increase requirement
        health += 20;         // buff player
        uiController.SetHealth(health);
        Debug.Log("Level Up! New Level: " + level);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        uiController.SetHealth(health);
        Debug.Log("PlayerH:" + health);
        if (health <= 0)
        {
            manager.GameOver();
        }
    }
}
