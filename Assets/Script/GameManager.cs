using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject cubePrefab;
    public GameObject enemyPrefab, playerPrefab;
    List <Vector2> cubesPos = new List<Vector2>();
    int maxCubes = 100;
    int attempts = 0;
    private NavMeshSurface surface;
    public WaveManager waveManager;
    public Texture2D cursorTexture;
    public UIController uiController;
    public 
    Vector2 cursorPosition;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        GenerateObsticals(); // generating some Randome Obsticals
        BuildNavMesh(); // Building NavMesh
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SpawnPlayer();
        ChangeCursor();
        SpawnEnemies();
    }

    void ChangeCursor()
    {
        //cursorPosition = new Vector2(cursorTexture.width/2, cursorTexture.height/2);
        //Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
    void BuildNavMesh()
    {
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void GenerateObsticals()
    {
        while (cubesPos.Count < maxCubes && attempts < maxCubes * 10) // safety limit
        {
            int x = Random.Range(-50, 50);
            int y = Random.Range(-50, 50);
            Vector3 pos = new Vector3(x, 0.5f, y);
            if (!cubesPos.Contains(pos)) // check if already used
            {
                Instantiate(cubePrefab, pos, Quaternion.identity);
                cubesPos.Add(pos);
            }
            attempts++;
        }
    }

    void SpawnEnemies()
    {
        waveManager.StartWave();
    }
    public void GameOver()
    {
        Time.timeScale= 0;
        uiController.Retry.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        uiController.Retry.SetActive(false);
        PlayerStats.Instance.InitializeValues();
        waveManager.DeleteAllChildren();
    }

    public void MainMenu()
    {
        uiController.Retry.SetActive(false);
        Retry();
        uiController.MainMenu.SetActive(true);
    }

}
