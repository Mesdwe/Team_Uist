using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : GenericSingletonClass<Player>
{
    public int rp;

    public event Action OnRPChanged;


    public void SetShip(Ship ship)
    {
        ship.OnArrival += HandleRPChanged; //watch out
    }

    public void HandleRPChanged()
    {
        UpdateRP();
    }
    public void UpdateRP()
    {
        rp++;
        OnRPChanged?.Invoke();
    }

}
