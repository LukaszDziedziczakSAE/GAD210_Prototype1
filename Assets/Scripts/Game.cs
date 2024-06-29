using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] LayerMask swarmLayer;
    [SerializeField] LayerMask locationLayer;
    [SerializeField] LayerMask terrainLayer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public static LayerMask SwarmLayer => Instance.swarmLayer;
    public static LayerMask LocationLayer => Instance.locationLayer;
    public static LayerMask TerrainLayer => Instance.terrainLayer;

    public static void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
