using System.Collections.Generic;
using UnityEngine;

public class ShipsController : MonoBehaviour
{
    public PlayerShip PlayerShip { get; set; }
    public List<AIShip> Ships { get; private set; }

    public static ShipsController Instance;

    private void Start()
    {
        Ships = new List<AIShip>();
        Instance = this;
    }

    public void AddShip(AIShip ship)
    {
        Ships.Add(ship);
    }

    public void Remove(AIShip ship)
    {
        Ships.Remove(ship);
    }
}