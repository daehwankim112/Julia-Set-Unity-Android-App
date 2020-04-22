using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour {

    public Material mat;
    public Vector2 pos;
    public float scale;

    // Update is called once per frame
    void Update() {
        float aspect = (float)Screen.width / (float)Screen.height; //These are integer so it should be casted to float otherwise, it will be truncated.

        float scaleX = scale;
        float scaleY = scale;

        if ( aspect > 1f ) {
            scaleY /= aspect;
        }
        else {
            scaleX *= aspect;
        }

        mat.SetVector("_Area", new Vector4(pos.x, pos.y, scaleX, scaleY));
    }
}
