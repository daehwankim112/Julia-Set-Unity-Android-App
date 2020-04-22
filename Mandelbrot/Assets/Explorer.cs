using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{

    public Material mat;
    public Vector2 pos;
    public float scale;

    // Update is called once per frame
    void Update(){
        mat.SetVector("_Area", new Vector4(pos.x, pos.y, scale, scale));
    }
}
