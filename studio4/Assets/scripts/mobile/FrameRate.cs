using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    public void Sixty()
    {
        Application.targetFrameRate = 60;
    }

    public void Thirty()
    {
        Application.targetFrameRate = 30;
    }

    public void OneFortyFour()
    {
        Application.targetFrameRate = 144;
    }

}
