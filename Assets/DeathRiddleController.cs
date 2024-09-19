using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRiddleController : MonoBehaviour
{

    public List<GameObject> MainObjectList = new List<GameObject>();

    public GameObject DemoUp;
    public GameObject DemoDown;

    public void StartRiddle()
    {
        if (DemoUp.activeSelf)
        {
            MainObjectList[0].SetActive(false);
            MainObjectList[1].SetActive(true);
            DemoUp.SetActive(false);
        }
        else
        {
            MainObjectList[0].SetActive(true);
            MainObjectList[1].SetActive(false);
            DemoDown.SetActive(false);
        }

        int rnd1 = Random.Range(2, 3+1);
        MainObjectList[rnd1].SetActive(true);

        int rnd2 = Random.Range(4, 5+1);
        MainObjectList[rnd2].SetActive(true);

    }

    public void Swap()
    {
        foreach(GameObject obj in MainObjectList)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
}
