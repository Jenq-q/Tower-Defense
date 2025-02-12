using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public EnemyType type;
    public float health = 100f;
    public float speed = 10f;
    public float damage = 1f;
    public float rotationSpeed = 5f;
    public int scoreValue = 10;
    public Color color = Color.red;
}