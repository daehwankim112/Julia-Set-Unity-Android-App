using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour {

    public Material mat;
    public Vector2 pos;
    public float scale;

    private void UpdateShaer()
    {
        float aspect = (float)Screen.width / (float)Screen.height; //These are integer so it should be casted to float otherwise, it will be truncated.

        float scaleX = scale;
        float scaleY = scale;

        if (aspect > 1f)
        {
            scaleY /= aspect;
        }
        else
        {
            scaleX *= aspect;
        }

        mat.SetVector("_Area", new Vector4(pos.x, pos.y, scaleX, scaleY));
    }

    private void HandleInputs()
    {
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            scale *= .99f;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            scale *= 1.01f;
        }
    }

    // The difference between update and fixed update is that the fixed update execute fixed amount of update per seconds
    //while normal update depends on how fast your computer is.
    void FixedUpdate()
    {
        HandleInputs();
        UpdateShaer();
    }
}
