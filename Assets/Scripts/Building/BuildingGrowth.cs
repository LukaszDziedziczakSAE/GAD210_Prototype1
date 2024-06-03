using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrowth : MonoBehaviour
{
    private float buildingHeight;
    private float buildingScale;
    [SerializeField] private float buildRate = 1f;

    public float CurrentProgress {  get; private set; }

    private void SetOriginalValues()
    {
        buildingHeight = transform.localPosition.y;
        buildingScale = transform.localScale.y;
    }

    private void SetBuildingTransforms(float progress)
    {
        Vector3 position = transform.localPosition;
        position.y = Mathf.Lerp(buildingHeight / 10, buildingHeight, progress);
        transform.localPosition = position;

        Vector3 scale = transform.localScale;
        scale.y = Mathf.Lerp(buildingScale / 10, buildingScale, progress);
        transform.localScale = scale;
    }

    public void BuildBuilding()
    {
        CurrentProgress += buildRate * Time.deltaTime;
        SetBuildingTransforms(CurrentProgress);
    }
}
