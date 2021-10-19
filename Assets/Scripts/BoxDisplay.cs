
using UnityEngine;


public class BoxDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject Eye;
    void Start()
    {
        this.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Eye.transform.position.y >= this.transform.position.y)
        {
            GetComponent<Renderer>().material.color = Color.green - new Color32(0, 0, 0, 200);
        }
    }
}
