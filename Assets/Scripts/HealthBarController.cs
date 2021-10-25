using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private HealthBar healthBarPrefab;
    private Dictionary<Ship, HealthBar> healthBars = new Dictionary<Ship, HealthBar>();

    private void Awake()
    {
        Ship.OnHealthAdded += AddHealthBar;
        Ship.OnHealthRemoved += RemoveHealthBar;
    }

    private void AddHealthBar(Ship ship)
    {
        if (healthBars.ContainsKey(ship) == false)
        {
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthBars.Add(ship, healthBar);
            healthBar.SetShip(ship);
        }
    }

    private void RemoveHealthBar(Ship ship)
    {
        if (healthBars.ContainsKey(ship))
        {
            if (healthBars[ship].gameObject != null)
                Destroy(healthBars[ship].gameObject);
            healthBars.Remove(ship);
        }
    }

    private void OnDestroy()
    {
        Ship.OnHealthAdded -= AddHealthBar;

        Ship.OnHealthRemoved -= RemoveHealthBar;
    }
}
