using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject Prefab2Spawn;
    public Transform TransformComponent;
    public bool inheritScale = true;
    public bool inheritRotation = false;

    [ContextMenu("spawn")]
    public GameObject Spawn()
    {
        var instance = Instantiate(Prefab2Spawn, TransformComponent.position, Quaternion.identity);
        if (inheritScale)
        {
            instance.transform.localScale = TransformComponent.lossyScale;
        }
        return instance;
    }

    public void SpawnNoReturn()
    {
        Quaternion rotationToBe = Quaternion.identity;
        if (inheritRotation)
        {
            rotationToBe = TransformComponent.rotation;
        }
        var instance = Instantiate(Prefab2Spawn, TransformComponent.position, rotationToBe);
        if (inheritScale)
        {
            instance.transform.localScale = TransformComponent.lossyScale;
        }
        return;
    }
}
