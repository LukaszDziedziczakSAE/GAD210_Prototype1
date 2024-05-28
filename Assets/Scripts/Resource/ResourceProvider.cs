using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProvider : Location
{
    [field: SerializeField, Header("Resource Provider")] public float JobDuration { get; private set; } = 1f;
    [field: SerializeField] public Resource.EType ResourceType { get; private set; }
}
