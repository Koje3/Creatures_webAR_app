using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
