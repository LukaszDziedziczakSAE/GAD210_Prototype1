using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] LayerMask swarmLayer;
    [SerializeField] LayerMask locationLayer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public static LayerMask SwarmLayer => Instance.swarmLayer;
    public static LayerMask LocationLayer => Instance.locationLayer;
}
