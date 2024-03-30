using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 Target;

    public void TeleportObject(GameObject obj)
    {
        obj.transform.position = Target;
    }
}
