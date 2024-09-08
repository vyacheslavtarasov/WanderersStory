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
        if (DemoUp.active)
        {
            MainObjectList[0].active = false;
            MainObjectList[1].active = true;
            DemoUp.SetActive(false);
        }
        else
        {
            MainObjectList[0].active = true;
            MainObjectList[1].active = false;
            DemoDown.SetActive(false);
        }

        int rnd1 = Random.Range(2, 3+1);
        MainObjectList[rnd1].active = true;
        
        int rnd2 = Random.Range(4, 5+1);
        MainObjectList[rnd2].active = true;

    }

    public void Swap()
    {
        foreach(GameObject obj in MainObjectList)
        {
            if (obj.active)
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
