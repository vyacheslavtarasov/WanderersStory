using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    Coroutine a;
    // Start is called before the first frame update
    private IEnumerator First()
    {

        yield return null;
        StopCoroutine(a);
        yield return null;
        yield return null;
    }

    private IEnumerator Second()
    {

        yield return null;
        yield return null;

    }
    void Start()
    {
        a = StartCoroutine(First());
        // a = StartCoroutine(Second());
    }
    
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.lossyScale);
        
    }


}
