using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] public UI_Swarm swarm;
    [SerializeField] public UI_Resources resources;
    [SerializeField] public UI_SelectionIndicator selectionIndicator;
    [SerializeField] public UI_BuildMenu buildMenu;
    [SerializeField] public UI_PauseMenu pauseMenu;
    [SerializeField] UI_MultiplicationSelector multiplicationSelector;
    [SerializeField] List<ResourceIconPair> ResourceIconPairs = new List<ResourceIconPair>();

    public static UI_Swarm Swarm => Instance.swarm;
    public static UI_Resources Resources => Instance.resources;
    public static UI_SelectionIndicator SelectionIndicator => Instance.selectionIndicator;
    public static UI_BuildMenu BuildMenu => Instance.buildMenu;
    public static UI_MultiplicationSelector MultiplicationSelector => Instance.multiplicationSelector;
    public static UI_PauseMenu PauseMenu => Instance.pauseMenu;

    [System.Serializable]
    public class ResourceIconPair
    {
        public Resource.EType Type;
        public Texture Icon;
    }    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (swarm == null) swarm = GetComponentInChildren<UI_Swarm>();
    }

    private void Start()
    {
        selectionIndicator.gameObject.SetActive(false);
        buildMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
    }

    public static Texture ResourceIcon(Resource.EType type)
    {
        foreach (ResourceIconPair pair in Instance.ResourceIconPairs)
        {
            if (pair.Type == type) return pair.Icon;
        }
        return null;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        Time.timeScale = pauseMenu.gameObject.activeSelf ? 0 : 1;
    }
}
