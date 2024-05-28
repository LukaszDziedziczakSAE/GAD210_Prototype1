using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Building : MonoBehaviour
{
    [field: SerializeField] public BuildingConfig[] Buildables {  get; private set; }
}
