using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [field: SerializeField] public EState State {  get; private set; }
    [field: SerializeField] public Location Location { get; private set; }
    [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }

    [Header("Placement")]
    [SerializeField] Material goodPlacement;
    [SerializeField] Material badPlacement;

    Material buildingMaterial;
    int inTrigger;

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
        placed
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

        Housing housing = GetComponent<Housing>();
        if (housing != null) housing.OnPlacement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Location>() != null) inTrigger++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Location>() != null) inTrigger--;
    }
}
