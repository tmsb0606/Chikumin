using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJampable
{
    public IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        yield return null;
    }
}
