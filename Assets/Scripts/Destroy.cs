using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    public void DestroyIt(GameObject obj)
    {
        Destroy(obj);
    }
}
