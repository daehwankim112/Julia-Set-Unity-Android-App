using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour {

    public Material mat;
    public Vector2 pos;
    public float scale;
    public float timespeed = 0.001f;

    private float diff = 0;
    private Vector2 smoothPos;
    private float smoothScale;
    private Vector2 firstTouch;

    private void UpdateShaer()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, .05f);
        smoothScale = Mathf.Lerp(smoothScale, scale, .05f);

        float aspect = (float)Screen.width / (float)Screen.height; //These are integer so it should be casted to float otherwise, it will be truncated.

        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspect > 1f)
            scaleY /= aspect;
        else
            scaleX *= aspect;

        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
    }

    private void HandleInputs()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            scale *= .99f;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            scale *= 1.01f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= .01f*scale;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += .01f * scale;
        }
        if (Input.GetKey(KeyCode.W))
        {
            pos.y += .01f * scale;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= .01f * scale;
        }
    }

    private Vector2 PositionDelta(Touch touch)
    {
        //not moved
        //if (touch.phase != TouchPhase.Moved)
            return Vector2.zero;

        //Delta

    }

    // The difference between update and fixed update is that the fixed update execute fixed amount of update per seconds
    //while normal update depends on how fast your computer is.
    void FixedUpdate()
    {
        //HandleInputs();
        UpdateShaer();
        if ( mat.GetFloat("_TimePass") == 1 ) {
            diff += timespeed;
        }
        Shader.SetGlobalFloat("color_diff", diff);
        
        if (Input.touchCount >= 1)
        {
            
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstTouch = Input.GetTouch(0).position;
            }
                
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //Vector of movement from first touch to new touch position is calculated
                Vector2 movement = new Vector2(10*(Input.GetTouch(0).position.x - firstTouch.x) / ((float)Screen.width), 6*(Input.GetTouch(0).position.y - firstTouch.y) / ((float)Screen.height));
                pos -= movement;
                firstTouch = Input.GetTouch(0).position; //First touch is being updated (following) new touch position

            }
        }
    }
}
