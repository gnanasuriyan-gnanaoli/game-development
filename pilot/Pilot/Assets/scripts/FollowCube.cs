using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCube : MonoBehaviour
{
    public Transform cube;
    public Vector3 offset;
    void Update()
    {
        transform.position = cube.position + offset;
    }


}
