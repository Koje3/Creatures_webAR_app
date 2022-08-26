using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeToARView : MonoBehaviour
{
    public UnityEvent changeToAR;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToggleARView());
    }

    public IEnumerator ToggleARView()
    {
        yield return new WaitForSeconds(1);
        changeToAR.Invoke();
    }

}
