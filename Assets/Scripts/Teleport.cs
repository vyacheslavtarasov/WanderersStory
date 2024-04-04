using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform place;

    public void SetPlace(Transform p)
    {
        place = p;
    }

    public void TeleportObject(GameObject obj)
    {
        obj.transform.position = place.transform.position;
    }
}
