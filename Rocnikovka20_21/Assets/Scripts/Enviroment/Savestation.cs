using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class Savestation : MonoBehaviour
{
    public float timeStep = 0.1f;

    public List<Sprite> sprites = new List<Sprite>();

    public Transform respawnPoint;
    public Transform interactionBubble;

    public AudioSource audio;

    private float startTime;

    private int activeSprite = 0;

    private bool isInteractionActive = false;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        interactionBubble.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Animation();

        if(InputManager.GetButtonDown("Interaction") && isInteractionActive)
		{
            audio.Play();

            StartCoroutine(TurnOffAudio());
            SaveLocation();
		}
    }

    private IEnumerator TurnOffAudio()
    {
        yield return new WaitForSeconds(2f);

        audio.Stop();
    }

    private void SaveLocation()
	{
        player.GetComponent<PlayerInfo>().SaveSavestation(transform);
	}

    private void Animation()
	{
        if (Time.time - startTime >= timeStep)
        {
            activeSprite++;

            if(activeSprite == sprites.Count)
			{
                activeSprite = 0;
			}

            transform.GetComponent<SpriteRenderer>().sprite = sprites[activeSprite];

            startTime = Time.time;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
		{
            player = col.transform;

            isInteractionActive = true;

            interactionBubble.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isInteractionActive = false;

            interactionBubble.gameObject.SetActive(false);
        }
    }
}
