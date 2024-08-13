using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AssignHeroToFollowCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private Hero _hero;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _hero = FindObjectOfType<Hero>();
        _camera.Follow = _hero.transform;
    }

    public void AssignHero()
    {

        _camera = GetComponent<CinemachineVirtualCamera>();
        _hero = FindObjectOfType<Hero>();
        _camera.Follow = _hero.transform;

    }
}
