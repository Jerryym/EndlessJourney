using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		Character attacker = GetComponent<Character>();
		if (attacker)
		{
			collision.GetComponent<Character>()?.TakeDamage(attacker);
		}
	}
}
