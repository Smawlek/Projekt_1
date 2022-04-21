using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
	{
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
	}
#endif

    public int min;
    public int max;
    public int current;

    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
	{
        float currentOffset = current - min;
        float maxOffset = max - min;
        float filledAmount = currentOffset / maxOffset;

        mask.fillAmount = filledAmount;
	}

    public void SetValues(int minimum, int maximum, int current)
	{
        min = minimum;
        max = maximum;
        this.current = current;
	}
}
