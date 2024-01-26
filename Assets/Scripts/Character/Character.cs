using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Enum defining different statistics
public enum Statistic
{
    Life,
    Energy,
    Damage,
    Armor,
    AttackSpeed,
    MoveSpeed,
    HealthRegeneration
}

// Serializable class representing a statistic with a value
[Serializable]
public class StatsValue
{
    public Statistic statisticType;
    public bool typeFloat;
    public int integer_value;

    public float float_value;

    // Constructor to initialize a StatsValue
    public StatsValue(Statistic statisticType, int value = 0)
    {
        this.statisticType = statisticType;
        this.integer_value = value;
    }

    public StatsValue(Statistic statisticType, float float_value)
    {
        this.statisticType = statisticType;
        this.float_value = float_value;
        typeFloat = true;
    }
}

// Serializable class containing a collection of StatsValues
[Serializable]
public class StatsGroup
{
    public List<StatsValue> stats;

    // Constructor initializing the StatsGroup and its values
    public StatsGroup()
    {
        stats = new List<StatsValue>();
    }

    // Method to initialize default stats
    public void Init()
    {
        stats.Add(new StatsValue(Statistic.Life, 100));
        stats.Add(new StatsValue(Statistic.Energy, 100));
        stats.Add(new StatsValue(Statistic.Damage, 25));
        stats.Add(new StatsValue(Statistic.Armor, 5));
        stats.Add(new StatsValue(Statistic.AttackSpeed, 1f));
        stats.Add(new StatsValue(Statistic.MoveSpeed, 1F));
        stats.Add(new StatsValue(Statistic.HealthRegeneration, 1f));
    }

    // Method to get a specific StatsValue by statistic type
    internal StatsValue Get(Statistic statisticToGet)
    {
        return stats[(int)statisticToGet];
    }

    public void Sum(StatsValue toAdd)
    {
        StatsValue statsValue = stats[(int)toAdd.statisticType];

        if (toAdd.typeFloat == true)
        {
            statsValue.float_value += toAdd.float_value;
        }
        else
        {
            statsValue.integer_value += toAdd.integer_value;
        }
    }

    public void Subtract(StatsValue toSubtract)
    {
        StatsValue statsValue = stats[(int)toSubtract.statisticType];

        if (toSubtract.typeFloat == true)
        {
            statsValue.float_value -= toSubtract.float_value;
        }
        else
        {
            statsValue.integer_value -= toSubtract.integer_value;
        }
    }
}

// Enum defining different attributes
public enum Attribute
{
    Strength,
    Dexterity,
    Intelligence
}

// Serializable class representing an attribute with a value
[Serializable]
public class AttributeValue
{
    public Attribute attributeType;
    public int value;

    // Constructor to initialize an Attributes Value
    public AttributeValue(Attribute attributeType, int value = 0)
    {
        this.attributeType = attributeType;
        this.value = value;
    }
}

// Serializable class containing a collection of Attribute Values
[Serializable]
public class AttributeGroup
{
    public List<AttributeValue> attributeValues;

    // Constructor initializing the Attribute Group
    public AttributeGroup()
    {
        attributeValues = new List<AttributeValue>();
    }

    // Method to initialize default attribute values
    public void Init()
    {
        attributeValues.Add(new AttributeValue(Attribute.Strength));
        attributeValues.Add(new AttributeValue(Attribute.Dexterity));
        attributeValues.Add(new AttributeValue(Attribute.Intelligence));
    }
}

// Serializable class representing a value pool (like character's health)
[Serializable]
public class ValuePool
{
    public StatsValue maxValue;
    public int currentValue;

    // Constructor to initialize a ValuePool with a maximum value
    public ValuePool(StatsValue maxValue)
    {
        this.maxValue = maxValue;
        this.currentValue = maxValue.integer_value;
    }

    internal void FullRestore()
    {
        currentValue = maxValue.integer_value;
    }

    public void Restore(int v)
    {
        currentValue += v;
        if(currentValue > maxValue.integer_value)
        {
            currentValue = maxValue.integer_value;
        }
    }
}

// Main class representing a character
public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] AttributeGroup attributes; // Collection of attributes for the character
    [SerializeField] StatsGroup stats; // Collection of statistics for the character
    public ValuePool lifePool; // Health value pool for the character
    public ValuePool energyPool;
    public bool isDead;

    private void Start()
    {
        // Initialization of attribute and stats groups
        attributes = new AttributeGroup();
        attributes.Init();

        stats = new StatsGroup();
        stats.Init();

        // Initializing the character's life and Energy pool with its maximum value
        lifePool = new ValuePool(stats.Get(Statistic.Life));
        energyPool = new ValuePool(stats.Get(Statistic.Energy));
    }

    private void Update()
    {
        LifeRegeneration();
    }

    float lifeRegen;
    private void LifeRegeneration()
    {
        lifeRegen += Time.deltaTime * stats.Get(Statistic.HealthRegeneration).float_value;
        if (lifeRegen > 1f)
        {
            Heal(1);
            lifeRegen -= 1f;
        }
    }

    private void Heal(int v)
    {
        lifePool.Restore(v);
    }

    public void TakeDamage(int damage)
    {
        // Applying defense calculation
        damage = ApplyDefence(damage);

        // Reducing character's health
        lifePool.currentValue -= damage;

        // Checking if the character is dead
        CheckDeath();
    }

    // Method to apply defense based on character's armor
    private int ApplyDefence(int damage)
    {
        damage -= stats.Get(Statistic.Armor).integer_value;

        if (damage <= 0)
        {
            damage = 1;
        }

        return damage;
    }

    internal void Restore()
    {
        lifePool.FullRestore();
        isDead = false;
    }

    // Method to check if the character is dead
    private void CheckDeath()
    {
        if (lifePool.currentValue <= 0)
        {
            isDead = true;
            GetComponent<CharacterDefeatHandler>().Defeated();

            AIEnemy aiEnemy = GetComponent<AIEnemy>();
            if (aiEnemy != null)
            {
                aiEnemy.OnEnemyDeath();
            }
        }
    }

    // Method to get a specific statistic value
    public StatsValue TakeStats(Statistic statisticToGet)
    {
        return stats.Get(statisticToGet);
    }

    public void AddStats(List<StatsValue> statsValues)
    {
        for(int i = 0; i < statsValues.Count; i++)
        {
            StatsAdd(statsValues[i]);
        }
    }

    private void StatsAdd(StatsValue statsValue)
    {
        stats.Sum(statsValue);
    }

    public void SubtractStats(List<StatsValue> statsValues)
    {
        for(int i = 0; i < statsValues.Count; i++)
        {
            StatsSubtract(statsValues[i]);
        }
    }

    private void StatsSubtract(StatsValue statsValue)
    {
        stats.Subtract(statsValue);
    }

    public int GetDamage()
    {
        int damage = TakeStats(Statistic.Damage).integer_value;
        return damage;
    }
}
