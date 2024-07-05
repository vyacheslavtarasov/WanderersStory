using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Hero>().GetComponent<Teleport>().ObjectToTransfer = FindObjectOfType<Hero>().gameObject;
        
    }

    public void Teleport()
    {
        FindObjectOfType<Hero>().GetComponent<Teleport>().place = transform.position;
        FindObjectOfType<Hero>().GetComponent<Teleport>().TeleportObject(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
