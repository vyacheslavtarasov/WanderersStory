using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{

    public string SortingLayer = "Default";
    public int OrderInLayer = 0;

    public Transform FollowObject;

    public Vector3 CameraPosition_1;
    public Vector3 BackgroundPosition_1;

    public Vector3 CameraPosition_2;
    public Vector3 BackgroundPosition_2;


    public float kX = 0;
    public float kY = 0;

    [ContextMenu("Calculate KX KY")]
    public void CalculateKXKY()
    {
        kX =  (BackgroundPosition_2.x - BackgroundPosition_1.x) / (CameraPosition_2.x - CameraPosition_1.x);
        kY =  (BackgroundPosition_2.y - BackgroundPosition_1.y) / (CameraPosition_2.y - CameraPosition_1.y);
    }
    void Start()
    {
        if (kX == 0)
        {
            kX = (BackgroundPosition_2.x - BackgroundPosition_1.x) / (CameraPosition_2.x - CameraPosition_1.x);
        }
        if (kY == 0)
        {
            kY = (BackgroundPosition_2.y - BackgroundPosition_1.y) / (CameraPosition_2.y - CameraPosition_1.y);
        }

        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRenderer = child.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = SortingLayer;
            spriteRenderer.sortingOrder = OrderInLayer;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(
            (FollowObject.position.x - CameraPosition_1.x) * kX + BackgroundPosition_1.x,
            (FollowObject.position.y - CameraPosition_1.y) * kY + BackgroundPosition_1.y, 
            transform.position.z);
    }
}
