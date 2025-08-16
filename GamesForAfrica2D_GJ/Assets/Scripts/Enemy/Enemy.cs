using UnityEngine;

public class Enemy : MonoBehaviour
{
    float HP;
    public float MaxHP;
    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damage)
    {
        HP -= damage;
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
