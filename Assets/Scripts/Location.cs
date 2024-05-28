using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [field: SerializeField, Header("Location")] public BoxCollider Collider { get; private set; }
    [SerializeField] float localAreaSize;
    [field: SerializeField] public string JobName { get; private set; }


    public Vector3 LocalAreaPosition
    {
        get
        {
            float xSize = 0;
            float zSize = 0;
            if (!Collider.isTrigger)
            {
                xSize = (Collider.size.x/2) + localAreaSize;
                zSize = (Collider.size.z/2) + localAreaSize;
            }
            else
            {
                xSize = (Collider.size.x / 2);
                zSize = (Collider.size.z / 2);
            }
            //print("xSize=" + xSize + ", zSize=" + zSize);

            float randomX = Random.Range(transform.position.x - xSize, transform.position.x + xSize);
            float randomZ = Random.Range(transform.position.z - zSize, transform.position.z + zSize);
            Vector3 position = new Vector3(randomX, transform.position.y, randomZ);

            int attempts = 0;
            while (PositionInColider(position) || !PositionClear(position))
            {
                if (++attempts >= 100) return Vector3.zero;
                randomX = Random.Range(transform.position.x - xSize, transform.position.x + xSize);
                randomZ = Random.Range(transform.position.z - zSize, transform.position.z + zSize);
                position = new Vector3(randomX, transform.position.y, randomZ);
            }

            return position;
        }
    }

    public bool PositionInColider(Vector3 position)
    {
        if (Collider.isTrigger) return false;
        return position.x > (transform.position.x - Collider.size.x) && position.x < (transform.position.x + Collider.size.x) &&
            position.z > (transform.position.z - Collider.size.z) && position.z < (transform.position.z + Collider.size.z);
    }

    public bool PositionClear(Vector3 position)
    {
        if (Physics.SphereCast(position, 0.5f, transform.up, out RaycastHit hit, 0.5f, Game.SwarmLayer))
        {
            return false;
        }
        return true;
    }
}
