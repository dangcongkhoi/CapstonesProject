using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    protected Vector3 _posLastfame;
    public Camera _UICamera;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _posLastfame = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            var delta = Input.mousePosition - _posLastfame;
            _posLastfame = Input.mousePosition;
            var axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
            transform.rotation = Quaternion.AngleAxis(delta.magnitude* 0.1f, axis) * transform.rotation;
        }
    }
}
