using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSizeValues : MonoBehaviour
{
    public Text width;
    public Text height;

    
    public void SetValues()
    {
        var generator = FindObjectOfType<MazeGenerator>().GetComponent<MazeGenerator>();

        var widthValue = Convert.ToInt32(width.text);
        var heightValue = Convert.ToInt32(height.text);
        if (widthValue > 0 && widthValue < 51)
        {
            generator.width = widthValue;
        }
        else
        {
            throw new Exception("Choose width value between 1 and 50!");
        }
        if (heightValue > 0 && heightValue < 51)
        {
            generator.height = heightValue;
        }
        else
        {
            throw new Exception("Choose height value between 1 and 50!");
        }
    }
}
