using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Location
{
    public EState State {  get; private set; }

    public enum EState
    {
        none,
        placing,
        placed
    }
}
