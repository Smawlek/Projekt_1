using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidController : MonoBehaviour
{
    public int damage = 5;

    public Transform voidCheckPoint;

    private PlayerAnimationsController playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            if(voidCheckPoint != null)
			{
                playerAnim = obj.GetComponent<PlayerAnimationsController>();
                playerAnim.FadeInBS();

                StartCoroutine(CompleteTeleport(obj));
            }
        }
    }

    private IEnumerator CompleteTeleport(Collider2D obj)
	{
        yield return new WaitForSecondsRealtime(1f);

        obj.gameObject.GetComponent<PlayerInfo>().TakeDamage(damage);

        obj.transform.position = voidCheckPoint.position;

        yield return new WaitForSecondsRealtime(1f);

        playerAnim.FadeOutBS();
    }

    public void SaveCheckpoint(Transform checkpoint)
	{
        voidCheckPoint = checkpoint;
	}
}
