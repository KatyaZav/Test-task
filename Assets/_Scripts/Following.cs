using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    [SerializeField] private Transform _follow;
    [SerializeField] private Vector3 _offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = _follow.position + _offset;
        //transform.LookAt(_follow);
    }
}
