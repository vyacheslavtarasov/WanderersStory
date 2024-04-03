using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject Prefab2Spawn;
    public Transform TransformComponent;
    public bool inheritScale = true;

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
}
