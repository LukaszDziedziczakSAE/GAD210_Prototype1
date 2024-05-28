using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [field: SerializeField] public Player_Selection Selection { get; private set; }
    [field: SerializeField] public InputReader Input { get; private set; }
    [field: SerializeField] public Camera Camera { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
