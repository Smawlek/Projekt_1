using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtonController : MonoBehaviour
{
    public TextMeshProUGUI versionText;

    // Start is called before the first frame update
    void Start()
    {
        versionText.text = "v. " + GameInfo.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
	{
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
