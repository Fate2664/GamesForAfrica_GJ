using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum StatType { attackDamage, attackRange, attackSpeed, bulletCount, bulletSpeed, spreadAngle, bulletSize, moveSpeed }
    const float baseUpgradeValue=1.5f;
    //starting stats
    public const float baseAttackDamage=10;
    public const float baseAttackRange = 2;
    public const float baseAttackSpeed = 1;
    public const float baseBulletCount=3;
    public const float baseBulletSpeed = 5;
    public const float baseSpreadAngle=24;
    public const float baseBulletSize=5;
    public const float baseMoveSpeed=3;
    public const float baseMoney=0;

    //Attack
    public float money;
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
                UpgradeSpreadAngle();
                break;
            case StatType.moveSpeed:
                UpgradeMoveSpeed();
                break;
        }
    }
    private void UpgradeAttackDamage()
    {
        attackDamage += baseAttackDamage * baseUpgradeValue;
    }

    private void UpgradeAttackRange()
    {
        attackRange += baseAttackRange * baseUpgradeValue;
    }

    private void UpgradeAttackSpeed()
    {
        attackSpeed += baseAttackSpeed * baseUpgradeValue;
    }

    private void UpgradeBulletCount()
    {
        bulletCount += 1;
    }

    private void UpgradeBulletSpeed()
    {
        bulletSpeed += baseBulletSpeed * baseUpgradeValue;
    }

    private void UpgradeSpreadAngle()
    {
        spreadAngle += (bulletSize - baseBulletSize) * 3f;
    }

    private void UpgradeBulletSize()
    {
        bulletSize += 1;
    }

    private void UpgradeMoveSpeed() 
    {
        moveSpeed += baseMoveSpeed * baseUpgradeValue;
    }

}
