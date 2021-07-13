using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable_Object", menuName = "Scriptable Objects/Player Data")]
public class PlayerData_Object : ScriptableObject
{
    // public int intV; 
    // public float floatV;
    // public bool booleanV;
    //
    // public GameObject gameObjectV;
    public float speed;
    public float attackCooldown;
    public float defenceCooldown;
    public GameObject attackPrefab;
    public GameObject defencePrefab;
    public GameObject MixAttackPrefab;
    public GameObject VoidKillParticlePrefab;
    public GameObject attackDisableParticlePrefab; 
}
