using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] public UI_Swarm swarm;
    [SerializeField] public UI_Resources resources;
    [SerializeField] public UI_SelectionIndicator selectionIndicator;

    public static UI_Swarm Swarm => Instance.swarm;
    public static UI_Resources Resources => Instance.resources;
    public static UI_SelectionIndicator SelectionIndicator => Instance.selectionIndicator;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (swarm == null) swarm = GetComponentInChildren<UI_Swarm>();
    }

    private void Start()
    {
        selectionIndicator.gameObject.SetActive(false);
    }
}