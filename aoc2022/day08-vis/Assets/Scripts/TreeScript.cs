using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public int Height = 0;
    public int Row = 0;
    public int Col = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Col-50, 0, Row-50);
        transform.position += Vector3.up * (Height) / 2;
        transform.localScale += Vector3.up * Height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
