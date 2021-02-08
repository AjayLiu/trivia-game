using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [HideInInspector]
    public float hearts = 3f;
    const float MAX_HEARTS = 3f;
    Image[] heartImgs;
    // Start is called before the first frame update
    void Start()
    {
        heartImgs = GetComponentsInChildren<Image>();
    }

    public void SetHearts(float newHearts)
    {
        hearts = Mathf.Clamp(newHearts, 0, MAX_HEARTS);
        int wholeHearts = Mathf.FloorToInt(hearts);
        for (int i = 0; i < wholeHearts; i++)
        {
            heartImgs[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < 3 - wholeHearts; i++)
        {
            heartImgs[3 - i - 1].gameObject.SetActive(false);
        }

        //set leftover heart
        float remainder = hearts - wholeHearts;
        if (remainder != 0)
        {
            heartImgs[wholeHearts].fillAmount = hearts - wholeHearts;
            heartImgs[wholeHearts].gameObject.SetActive(true);
        }
    }
}
