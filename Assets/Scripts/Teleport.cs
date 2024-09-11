using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject ObjectAsTargetPlace;
    public Vector3 place;
    public float gettingTransparentTime = 1.0f;
    public float moveTime = 1.0f;
    public GameObject ObjectToTransfer; // if it is - the function TeleportObject transfers it, not it's argument

    private void Start()
    {
        if (place == Vector3.zero && ObjectAsTargetPlace != null)
        {
            place = ObjectAsTargetPlace.transform.position;
        }
    }
    public void SetPlace(Transform p)
    {
        place = p.position;
    }

    public void TeleportObject(GameObject obj)
    {
        if (ObjectToTransfer != null)
        {
            Debug.Log("here");
            StartCoroutine(TeleportMe(ObjectToTransfer));
        }
        else
        {
            StartCoroutine(TeleportMe(obj));
        }
    }

    

    private IEnumerator MovingCoroutine(GameObject obj)
    {
        float currentTime = 0.0f;
        Vector3 beginCoordinates = obj.transform.position;
        while (currentTime < moveTime)
        {
            Vector3 coordinate = Vector3.Lerp(beginCoordinates, place, currentTime / moveTime);
            obj.transform.position = coordinate;
            currentTime += Time.deltaTime;
            yield return null;
        }
        
    }

    private IEnumerator VanishingCoroutine(GameObject obj, float targetOpacity)
    {
        
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        float currentTime = 0.0f;
        float beginningOpacity = spriteRenderer.color.a;
        while(currentTime < gettingTransparentTime)
        {
            float currentOpacity = Mathf.Lerp(beginningOpacity, targetOpacity, currentTime / gettingTransparentTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentOpacity);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator TeleportMe(GameObject obj)
    {
        yield return StartCoroutine(VanishingCoroutine(obj, 0.0f));
        obj.GetComponent<Rigidbody2D>().simulated = false;
        // obj.SetActive(false);
        yield return StartCoroutine(MovingCoroutine(obj));
        obj.GetComponent<Rigidbody2D>().simulated = true;
        // obj.SetActive(true);
        yield return StartCoroutine(VanishingCoroutine(obj, 1.0f)); 
    }
}
