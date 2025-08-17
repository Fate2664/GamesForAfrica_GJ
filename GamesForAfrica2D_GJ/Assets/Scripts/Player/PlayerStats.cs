using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    const float baseUpgradeValue=1.5f;
    //starting stats
    const float baseAttackDamage=10;
    const float baseAttackRange = 2;
    const float baseAttackSpeed = 1;
    const float baseBulletCount=3;
    const float baseBulletSpeed = 5;
    const float baseSpreadAngle=15;
    const float baseBulletSize=1;
    const float baseMoveSpeed=3;
    //Attack
    public float attackDamage;
    public float attackRange;
    public float attackSpeed;
    public float bulletCount;
    public float bulletSpeed;
    public float spreadAngle;
    public float bulletSize;
    //Movement
    public float moveSpeed;
    private void Awake()
    {
        ResetStats();
    }
    void ResetStats()
    {
        attackDamage = baseAttackDamage;
        attackRange = baseAttackRange;
        attackSpeed = baseAttackSpeed;
        bulletCount = baseBulletCount;
        bulletSpeed = baseBulletSpeed;
        spreadAngle = baseSpreadAngle;
        bulletSize = baseBulletSize;
        moveSpeed = baseMoveSpeed;
    }
    public void UpgradeAttackDamage()
    {
        attackDamage += baseAttackDamage * baseUpgradeValue;
    }

    public void UpgradeAttackRange()
    {
        attackRange += baseAttackRange * baseUpgradeValue;
    }

    public void UpgradeAttackSpeed()
    {
        attackSpeed += baseAttackSpeed * baseUpgradeValue;
    }

    public void UpgradeBulletCount()
    {
        bulletCount += 1;
    }

    public void UpgradeBulletSpeed()
    {
        bulletSpeed += baseBulletSpeed * baseUpgradeValue;
    }

    public void UpgradeSpreadAngle()
    {
        spreadAngle += 5;
    }

    public void UpgradeBulletSize()
    {
        bulletSize += baseBulletSize * baseUpgradeValue;
    }

    public void UpgradeMoveSpeed()
    {
        moveSpeed += baseMoveSpeed * baseUpgradeValue;
    }

}
