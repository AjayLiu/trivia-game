using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [HideInInspector]
    public int hearts = 3;
    Image[] heartImgs;
    // Start is called before the first frame update
    void Start()
    {
        heartImgs = GetComponentsInChildren<Image>();
    }

    public void SetHearts(int newHearts)
    {
        hearts = newHearts;
        for (int i = 0; i < hearts; i++)
        {
            heartImgs[i].color = Color.red;
        }
        for (int i = 0; i < 3 - hearts; i++)
        {
            heartImgs[3 - i - 1].color = Color.white;
        }
    }
}
