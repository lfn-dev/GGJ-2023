using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }
}
