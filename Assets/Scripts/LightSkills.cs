using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSkills : MonoBehaviour
{
    public void SpeedUpShips(Ship ship)
    {
        ship.SpeedUp();
    }
}
public enum LightAbilities
{
    Light,
    Heal,
    Barrier,
    Stun
}

