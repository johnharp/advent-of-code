using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnotScript : MonoBehaviour
{
    public Vector3 TargetPosition;
    public float MoveSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetPosition != null)
        {
            float step = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(
                transform.position, TargetPosition, step);
        }
    }
}
