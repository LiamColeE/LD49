using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManifold : MonoBehaviour
{
    
    void Start()
    {
        int randomScene = Random.Range(1, SceneManager.sceneCount+1);
        SceneManager.LoadScene(randomScene, LoadSceneMode.Single);
    }
}
