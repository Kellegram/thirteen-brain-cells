using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region singleton

    public static PlayerManager instance;
    public MainCam orbit;
    private float timer = 0f;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    
    public GameObject player;
    public GameObject map;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    void LateUpdate()
    {
        if (timer < 5f)
            timer += Time.deltaTime;
        else
            orbit.setOrbit(false);
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        orbit.setOrbit(true);
    }

}
