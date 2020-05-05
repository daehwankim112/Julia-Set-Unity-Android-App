using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour {

    public Material mat;
    public Vector2 pos;
    public float scale;
    public float timespeed = 0.001f;

    private float timeDiff = 0;
    private float magicDiff = 0;
    private Vector2 smoothPos;
    private float smoothScale;
    private Vector2 firstTouch;
    private float ZoomedBy;
    private float scaleX, scaleY;
    private static bool paused;
    private int TapCount;
    private float MaxDubbleTapTime;
    private float NewTimeUntilDoubleTap;
    private bool waitingForSecondTap;

    void Start()
    {
        TapCount = 0;
        MaxDubbleTapTime = 0.7f;
        waitingForSecondTap = false;
    }

    private void UpdateShaer()
    {
        //These are for smoothing user interactions. Put these back if fps is good.
        //smoothPos = Vector2.Lerp(smoothPos, pos, .09f);
        //smoothScale = Mathf.Lerp(smoothScale, scale, .05f);

        float aspect = (float)Screen.width / (float)Screen.height; //These are integer so it should be casted to float otherwise, it will be truncated.

        scaleX = scale;
        scaleY = scale;

        if (aspect > 1f)
            scaleY /= aspect;
        else
            scaleX *= aspect;

        mat.SetVector("_Area", new Vector4(pos.x, pos.y, scaleX, scaleY));
    }

    private void DoubleTap()
    {
        //print(Time.time);
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            //print(touch.phase);

            if (touch.phase == TouchPhase.Ended && Time.time > NewTimeUntilDoubleTap - MaxDubbleTapTime + 0.1f)
            {
                TapCount += 1;
                //print("TapCount added. TapCount: " + TapCount + " Time: " + Time.time);
                if ( TapCount == 1 )
                    waitingForSecondTap = true;
                else
                    waitingForSecondTap = false;
            }
            //print("TapCount: " + TapCount + "waitingForSecondTap: " + waitingForSecondTap);
            if (TapCount == 1 && waitingForSecondTap)
            {
                NewTimeUntilDoubleTap = Time.time + MaxDubbleTapTime;
                waitingForSecondTap = false;
            }
            else if (TapCount == 2 && Time.time <= NewTimeUntilDoubleTap)
            {
                //Double tapped. Pause the game
                /*
                PauseSetting.GameIsPaused = true;
                PauseSetting.GameStatueChanger = true;*/
                PauseSetting.ChangeGameStatue(1);
                print("Double Tapped!");
                    

                waitingForSecondTap = false;
                TapCount = 0;
            }
            //print("NewTimeUntilDoubleTap: " + NewTimeUntilDoubleTap + " Time: " + Time.time);
        }
        if (Time.time > NewTimeUntilDoubleTap && TapCount > 0)
        {
            TapCount = 0;
            waitingForSecondTap = false;
            //print("Timeout! Time: " + Time.time + "NewTime: " + NewTimeUntilDoubleTap);
        }
    }

    // The difference between update and fixed update is that the fixed update execute fixed amount of update per seconds
    //while normal update depends on how fast your computer is.
    void FixedUpdate()
    {
        //Check if game is paused.
        paused = PauseSetting.GameIsPaused;

        //HandleInputs();
        UpdateShaer();
        if (!paused)
        {
            DoubleTap();
        }
        
        if ( mat.GetFloat("_TimePass") == 1 ) {
            timeDiff += timespeed;
        }
        Shader.SetGlobalFloat("color_diff", timeDiff);
        if (mat.GetFloat("_Magic") == 1)
        {
            magicDiff += timespeed;
        }
        Shader.SetGlobalFloat("magic_diff", magicDiff);

        //Scroll
        if (Input.touchCount >= 1 && !paused )
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstTouch = Input.GetTouch(0).position;
            }
                
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //Vector of movement from first touch to new touch position is calculated
                Vector2 movement = new Vector2(scale * (Input.GetTouch(0).deltaPosition.x) / ((float)Screen.width) / 2, 
                    scaleY * (Input.GetTouch(0).deltaPosition.y) / ((float)Screen.height) / 2);

                pos -= movement;
            }

            //Pinch
            if (Input.touchCount >= 2)
            {
                var pos1 = Input.GetTouch(0).position;
                var pos2 = Input.GetTouch(1).position;
                var pos1b = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
                var pos2b = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;

                //Calculate Zoom
                //var zoom;

                float BeforeScale = Vector2.Distance(pos1b, pos2b) / 4;
                float AfterScale = Vector2.Distance(pos1, pos2) / 4;
                ZoomedBy = BeforeScale / AfterScale;
                scale *= ZoomedBy;
            }
        }
    }
}
