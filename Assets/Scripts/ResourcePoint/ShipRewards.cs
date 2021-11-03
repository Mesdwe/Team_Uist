using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RewardData", menuName = "ScriptableObjects/ShipRewardData")]
public class ShipRewards : ScriptableObject
{
    public int rewardLevelCount;
    public int[] rewards;
    public int defaultReward = 0;
    public int GetRewards(float healthPct)
    {
        //TEMP
        int reward = rewards[defaultReward];
        if (healthPct >= 1f)
        {
            reward = rewards[0];
        }
        else if (healthPct > 0.75f)
        {
            reward = rewards[1];
        }
        else if (healthPct > 0.5f)
        {
            reward = rewards[2];
        }
        else if (healthPct > 0.25f)
        {
            reward = rewards[3];
        }
        else if (healthPct > 0.01f)
        {
            reward = rewards[4];
        }
        else if (healthPct <= 0f)
        {
            reward = rewards[5];
        }
        return reward;
    }
}
