using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSetting : MonoBehaviour
{

    public Material mat;

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

}
