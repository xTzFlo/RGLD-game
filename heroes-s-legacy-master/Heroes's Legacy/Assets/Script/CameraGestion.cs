using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraGestion : MonoBehaviour
{
    
    // Start is called before the first frame update
    private bool isBoss_scene = false;
    public GameObject camObject;
    void Start()
    {
        camObject = GameObject.FindGameObjectWithTag("CameraPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game_multi_02" || SceneManager.GetActiveScene().name == "Game_multi_04.unity" || SceneManager.GetActiveScene().name == "Game_multi_06" && !isBoss_scene)
        {
            isBoss_scene = true;
            camObject.SetActive(false);

        }
        else if (SceneManager.GetActiveScene().name != "Game_multi_06" && SceneManager.GetActiveScene().name != "Game_multi_04.unity" && SceneManager.GetActiveScene().name != "Game_multi_02" && isBoss_scene)
        {
            isBoss_scene = false;
            camObject.SetActive(true);
        }
    }
}
