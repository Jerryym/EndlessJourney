using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
	protected override void Awake()
	{
		base.Awake();
		base.m_patrolState = new BoarPatrolState();
		base.m_chaseState = new BoarChaseState();
	}
}
