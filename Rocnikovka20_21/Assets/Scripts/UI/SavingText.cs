using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavingText : MonoBehaviour
{
    public float timeStep = 0.1f;

    public TextMeshProUGUI text;

    public List<string> texts = new List<string>();

    private float startTime;

    private int activeText = 0;

    // Start is called before the first frame update
    void Start()
    {
        text.text = texts[0];

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= timeStep)
        {
            activeText++;

            if (activeText == texts.Count)
            {
                activeText = 0;
            }

            text.text = texts[activeText];

            startTime = Time.time;
        }
    }
}
