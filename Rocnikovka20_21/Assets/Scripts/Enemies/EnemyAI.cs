using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool knocked = false;
	public bool isFacingRight = true;

	public EnemyInfo enemyInfo;

    public void IsKnocked(bool knocked)
	{
		this.knocked = knocked;
	}

	public bool GetFacingRight()
	{
		return isFacingRight;
	}

	public void SetFacingRight(bool isFacingRight)
	{
		this.isFacingRight = isFacingRight;
	}

	public void SetAI(EnemyInfo enemyInfo)
	{
		this.enemyInfo = enemyInfo;
	}
}
