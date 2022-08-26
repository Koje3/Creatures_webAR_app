using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour
{
    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        if (materials.Length > 0)
            GetComponent<SkinnedMeshRenderer>().material = materials[Random.Range(0, materials.Length)];
    }

}
