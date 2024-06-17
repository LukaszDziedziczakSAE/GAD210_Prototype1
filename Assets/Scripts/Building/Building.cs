using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Building : MonoBehaviour
{
    [field: SerializeField] public EState State {  get; private set; }
    [field: SerializeField] public Location Location { get; private set; }
    [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
    [SerializeField] Transform buildingModel;

    [Header("Placement")]
    [SerializeField] Material goodPlacement;
    [SerializeField] Material badPlacement;

    Material buildingMaterial;
    int inTrigger;
    float buildCompletion;

    float completedHeight;
    float completedScale;

    private void Update()
    {
        if (State == EState.placing)
        {
            transform.position = Player.Instance.Building.GroundPositionUnderMouse;
            SetPlacementMaterial();
        }
    }

    public enum EState
    {
        none,
        placing,
        placed,
        complete
    }

    public void SetToPlacingState()
    {
        this.Location.Collider.isTrigger = true;
        buildingMaterial = MeshRenderer.material;
        State = EState.placing;
    }

    public bool PlacementGood
    {
        get
        {
            return inTrigger == 0;
        }
    }

    private void SetPlacementMaterial()
    {
        if (PlacementGood)
        {
            MeshRenderer.material = goodPlacement;
        }
        else
        {
            MeshRenderer.material = badPlacement;
        }
    }

    public void SetPlacedState()
    {
        this.Location.Collider.isTrigger = false;
        MeshRenderer.material = buildingMaterial;
        State = EState.placed;

        buildCompletion = 0;
        completedHeight = buildingModel.localPosition.y;
        completedScale = buildingModel.localScale.y;
        ShowBuildCompletion();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Location>() != null) inTrigger++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Location>() != null) inTrigger--;
    }

    public void AddToBuildingCompletion(float amount)
    {
        if (State != EState.placed) return;
        //Debug.Log("Adding to building completing = " + amount);

        buildCompletion = Mathf.Min(1,  buildCompletion + amount);

        if (buildCompletion >= 1)
        {
            Housing housing = GetComponent<Housing>();
            if (housing != null) housing.IncreaseSwarmSize();
            State = EState.complete;
            Debug.Log(name + " finished building");
        }

        ShowBuildCompletion();
        //Debug.Log("Current completiong = " + buildCompletion);
    }

    private void ShowBuildCompletion()
    {
        float currentHeight = Mathf.Lerp(0.1f, completedHeight, buildCompletion);
        float currentScale = Mathf.Lerp(0.1f, completedScale, buildCompletion);

        buildingModel.localPosition = new UnityEngine.Vector3(buildingModel.localPosition.x, currentHeight, buildingModel.localPosition.z);
        buildingModel.localScale = new UnityEngine.Vector3(buildingModel.localScale.x, currentScale, buildingModel.localScale.z);
    }

}
