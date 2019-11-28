using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{

    private int health = 1000;

    public void Damage(int dmg)
    {
        health -= dmg;
    }
}
