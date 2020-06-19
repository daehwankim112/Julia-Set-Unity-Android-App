using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    public Canvas InstructionCanvas;
    private bool closed = false;

    void FixedUpdate()
    {
        if ( ! closed )
        {
            if(Input.touchCount >= 1)
            {
                InstructionCanvas.sortingOrder = -2;
                closed = true;
            }
            
        }
    }
}
