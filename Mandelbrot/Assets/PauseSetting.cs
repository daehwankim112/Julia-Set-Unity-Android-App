using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSetting : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public static bool GameStatueChanger = false;
    public static float Order;
    public Material mat;
    public Canvas PausedCanvas;
    
    void Start()
    {
        Order = PausedCanvas.sortingOrder;
    }


    void Update()
    {
        if ( Order == 1 && ! GameIsPaused)
        {
            PausedCanvas.sortingOrder = (int) Order;
            GameIsPaused = true;
        }
    }

    /*
    public void Resume()
    {
        if (GameIsPaused)
        {
            PausedCanvas.sortingOrder = sortorder;
            GameIsPaused = false;
        }
    }

    public void Pause()
    {
        if (!GameIsPaused && GameStatueChanger)
        {
            PausedCanvas.sortingOrder = sortorder;
            GameIsPaused = true;
        }
    }*/

    public static void ChangeGameStatue( float order)
    {
        if (Order != order)
            Order = order;
    }
    
    public void Resume()
    {
        PausedCanvas.sortingOrder = -1;
        Order = -1;
        GameIsPaused = false;
    }
    /*
    public void Pause()
    {
        if (! GameIsPaused && GameStatueChanger)
        {
            PausedCanvas.sortingOrder = 1;
            GameIsPaused = true;
        }
    }*/

    public void SetRed(float red)
    {
        mat.SetFloat("_R", red);
    }

    public void SetGreen(float green)
    {
        mat.SetFloat("_G", green);
    }

    public void SetBlue(float blue)
    {
        mat.SetFloat("_B", blue);
    }

    public void SetC_Real(float real)
    {
        mat.SetFloat("_C_real", real);
    }

    public void SetC_Imaginary(float imaginary)
    {
        mat.SetFloat("_C_imaginary", imaginary);
    }

    public void SetTime(bool timepass)
    {
        if ( timepass )
            mat.SetFloat("_TimePass", 1);
        else
            mat.SetFloat("_TimePass", 0);
    }

    public void SetMagic(bool magic)
    {
        if (magic)
            mat.SetFloat("_Magic", 1);
        else
            mat.SetFloat("_Magic", 0);
    }

}
