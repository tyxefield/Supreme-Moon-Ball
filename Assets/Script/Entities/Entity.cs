using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public GameObject ent;
    public float x;
    public float y;
    public float z;
    public float rot;

    public bool removed;

    public Entity(float x, float y, float z, float rot)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.rot = rot;
    }

    public void Move(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;

        if (ent != null)
            ent.transform.position = VectorData.Set(x, y, z);

        return;
    }
}