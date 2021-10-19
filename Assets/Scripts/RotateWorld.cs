using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
{
    [SerializeField]
    GameObject world;

    [SerializeField]
    GameObject eye;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        world.transform.RotateAround(eye.transform.position, Vector3.up, Time.deltaTime * 10);
    }
}
