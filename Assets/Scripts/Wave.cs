using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Wave Data")]
public class Wave : ScriptableObject
{
    public List<GameObject> enemies; // List of enemy prefabs for this wave
    public float spawnInterval = 2f; // Time interval between enemy spawns
    public float duration = 30f; // Duration of the wave in seconds

}
