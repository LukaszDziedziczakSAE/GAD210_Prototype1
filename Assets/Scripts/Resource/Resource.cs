using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource : IEquatable<Resource>
{
    [field: SerializeField] public EType Type {  get; private set; }
    [field: SerializeField] public int Amount { get; private set; }

    public Resource(EType type)
    {
        Type = type;
        Amount = 1;
    }

    public enum EType
    {
        None,
        Wood,
        Stone,
        WoodPlanks
    }

    public void Add(int amount)
    {
        Amount += amount;
    }

    public void Remove(int amount)
    {
        Amount -= amount;
    }

    public void Set(int amount)
    {
        Amount = amount;
    }

    public bool Equals(Resource other)
    {
        return Type == other.Type;
    }
}
