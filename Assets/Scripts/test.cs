using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    Coroutine a;
    // Start is called before the first frame update
    private IEnumerator First()
    {
        Debug.Log("first0");
        yield return null;
        Debug.Log("first1");
        StopCoroutine(a);
        Debug.Log("first2");
        yield return null;
        Debug.Log("first3");
        yield return null;
    }

    private IEnumerator Second()
    {
        Debug.Log("second0");
        yield return null;
        Debug.Log("second1");
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
