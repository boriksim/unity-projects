using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fractal : MonoBehaviour
{
    Transform point1;
    Transform point2;
    Transform point3;
    Transform point4;
    Transform point5;

    public Transform point;
    public GameObject prefab;

    Vector3 cords;

    public void Start()
    {
        point1 = GameObject.FindGameObjectWithTag("1").GetComponent<Transform>();
        point2 = GameObject.FindGameObjectWithTag("2").GetComponent<Transform>();
        point3 = GameObject.FindGameObjectWithTag("3").GetComponent<Transform>();
        point4 = GameObject.FindGameObjectWithTag("4").GetComponent<Transform>();
        point5 = GameObject.FindGameObjectWithTag("5").GetComponent<Transform>();
    }


    void FixedUpdate()
    {
        point = gameObject.GetComponent<Transform>();
        cords = new Vector3(point.transform.position.x, point.transform.position.y, point.transform.position.z);
        int r = Random.Range(0, 6);
        if (r == 0)
        {
            cords = (point1.position + cords) / 2;
        }
        if (r == 1)
        {
            cords = (point2.position + cords) / 2;
        }
        if (r == 2)
        {
            cords = (point3.position + cords) / 2;
        }
        if (r == 3)
        {
            cords = (point4.position + cords) / 2;
        }
        if (r == 4)
        {
            cords = (point5.position + cords) / 2;
        }
        Instantiate(prefab, cords, Quaternion.identity);
        Destroy(this);
    }
}
