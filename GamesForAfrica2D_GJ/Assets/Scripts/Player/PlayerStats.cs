using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum StatType { attackDamage, attackRange, attackSpeed, bulletCount, bulletSpeed, spreadAngle, bulletSize, moveSpeed }
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

    public void UpgradeStat(StatType statType)
    {
        switch (statType)
        {
            case StatType.attackDamage:
                UpgradeAttackDamage();
                break;
            case StatType.attackRange:
                UpgradeAttackRange();
                break;
            case StatType.attackSpeed:
                UpgradeAttackSpeed();
                break;
            case StatType.bulletCount:
                UpgradeBulletCount();
                break;
            case StatType.bulletSpeed:
                UpgradeBulletSpeed();
                break;
            case StatType.spreadAngle:
                UpgradeSpreadAngle();
                break;
            case StatType.bulletSize:
                UpgradeBulletSize();
                break;
            case StatType.moveSpeed:
                UpgradeMoveSpeed();
                break;
        }
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
