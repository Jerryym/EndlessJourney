using System;
using UnityEngine;

[Serializable]//实现可序列化
public class PlayerModel
{
	/// <summary>
	/// 基础属性
	/// </summary>
	[field: SerializeField]
	public PlayerBasicStats playerBasic = null;

	/// <summary>
	/// 战斗属性
	/// </summary>
	[field: SerializeField]
	public PlayerCombatStats playerCombat = null;

	/// <summary>
	/// 移动配置
	/// </summary>
	[field: SerializeField]
	public PlayerMovementConfig playerMovement = null;

	/// <summary>
	/// 能力配置
	/// </summary>
	[field: SerializeField]
	public PlayerAbilityConfig playerAbility = null;

	public PlayerModel() 
	{
		playerBasic = new PlayerBasicStats();
		playerCombat = new PlayerCombatStats();
		playerMovement = new PlayerMovementConfig();
		playerAbility = new PlayerAbilityConfig();
	}

	/// <summary>
	/// 受到伤害
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(float damage)
	{
		//闪避判定
		float rEvasionChance = playerCombat.Evasion / (playerCombat.Evasion + 150f);
		float rRandValue = UnityEngine.Random.value;
		if (rRandValue < rEvasionChance)
		{
			return;
		}

		//伤害计算
		float rDamageReductionRate = playerCombat.Defence / (playerCombat.Defence + 10f);
		float rFinalDamage = damage * (1f - rDamageReductionRate);
		rFinalDamage = Mathf.Max(0f, rFinalDamage);

		//生命值扣除
		playerBasic.Health = Mathf.Max(playerBasic.Health - rFinalDamage, 0.0f);
	}

	/// <summary>
	/// 判断当前体力值是否足够执行滑铲
	/// </summary>
	/// <returns>如果当前体力大于或等于滑铲消耗的体力，则返回true；否则返回false</returns>
	public bool CanSlide()
	{
		return playerBasic.Stamina >= playerMovement.SlideCost;
	}

}
