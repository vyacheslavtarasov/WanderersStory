using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowImage : MonoBehaviour
{
    public void Show()
    {
        FindObjectOfType<ImageFullScreen>(true).gameObject.SetActive(true);
    }

    public void Hide()
    {
        FindObjectOfType<ImageFullScreen>(true).gameObject.SetActive(false);
    }
}
