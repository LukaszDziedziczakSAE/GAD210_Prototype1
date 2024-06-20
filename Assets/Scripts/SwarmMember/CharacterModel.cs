using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    [SerializeField] GameObject malePeasant;
    [SerializeField] GameObject femalePeasant;
    [SerializeField] Material type1;
    [SerializeField] Material type2;
    [SerializeField] Material type3;

    private void Start()
    {
        int isMale = Random.Range(0, 2);
        SkinnedMeshRenderer meshRenderer;
        if (isMale == 1)
        {
            malePeasant.SetActive(true);
            femalePeasant.SetActive(false);
            meshRenderer = malePeasant.GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            malePeasant.SetActive(false);
            femalePeasant.SetActive(true);
            meshRenderer = femalePeasant.GetComponent<SkinnedMeshRenderer>();
        }

        int type = Random.Range(0, 3);
        switch (type)
        {
            case 0:
                meshRenderer.material = type1;
                break;

            case 1:
                meshRenderer.material = type2;
                break;

            case 2:
                meshRenderer.material = type3;
                break;
        }
    }

}