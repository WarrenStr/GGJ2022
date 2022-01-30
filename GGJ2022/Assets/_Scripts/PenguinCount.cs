using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguinCount : MonoBehaviour
{
    
    public Text penguinCounter;

    public Image timerRadial;

    public void SetPenguin(int penguins)
    {
        penguinCounter.text = penguins.ToString();
    }

    public void PenguinRecharge(float timer)
    {
        timerRadial.fillAmount = timer;
    }

}
