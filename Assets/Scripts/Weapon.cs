using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class Weapon : Item {
    public GameObject dcUp;
    public GameObject dcDown;
    public GameObject dcLeft;
    public GameObject dcRight;

    public float attack;
}
