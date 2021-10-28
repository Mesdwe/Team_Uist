using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : GenericSingletonClass<Player>
{
    public int rp;

    public event Action OnRPChanged;
    public event Action<bool,int> OnShipChanged;
    public int shipSaved;
    public int shipLost;

    public void SetShip(Ship ship)
    {
        ship.OnArrival += HandleRPChanged; //watch out
        ship.OnArrival += SavedShip;
        ship.OnDeath += LostShip;
    }

    public void HandleRPChanged(Ship ship)
    {
        UpdateRP(ship.shipRewards.GetRewards(ship.GetCurrentHealthPct()));

    }
    public void SavedShip(Ship ship)
    {
        shipSaved++;
        OnShipChanged?.Invoke(true,shipSaved);
    }

    public void LostShip(Ship ship)
    {
        shipLost++;
        OnShipChanged?.Invoke(false,shipLost);
    }
    public void UpdateRP(int rpChange)
    {
        rp += rpChange;
        OnRPChanged?.Invoke();
    }

}
