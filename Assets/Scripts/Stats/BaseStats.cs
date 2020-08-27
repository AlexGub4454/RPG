using RPG.Core;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [SerializeField]
    [Range(0, 3)]
    private int startLevel = 0;
    [SerializeField]
    private CharacterClass characterClass;
    [SerializeField]
    Progression progression = null;
    private int currentLevel = -1;
    [SerializeField] GameObject FVXLevelUp;
    public event Action onLevelUp;
    private void Start()
    {
        if (gameObject.tag != "Player") return;
        currentLevel = CalculateLevel();
        Experience experience = GetComponent<Experience>();
        if (experience != null)
        {
            experience.onGainedExperience += UpdateLevel;
        }
    }
    public float GetAdditiveModifier(Stat stat)
    {
        float total=0;
        foreach(IModifierProvider modifierProvider in GetComponents<IModifierProvider>())
        {
            foreach(float modifier in modifierProvider.GetProviders(stat))
            {
                total += modifier;
            }
        }
        return total;
    }
    private void UpdateLevel()
    {
        if (gameObject.tag != "Player") return;
        int newLevel = CalculateLevel();
        if (newLevel > currentLevel)
        {
            currentLevel = newLevel;
            LevelUpEffect();
            onLevelUp();
        }
    }
    private void LevelUpEffect()
    {
        Instantiate(FVXLevelUp, transform);
    }
    private int GetLevel()
    {
        if (currentLevel < 0)
        {
            currentLevel = CalculateLevel();
        }
        return currentLevel;
    }
    public float GetStat(Stat stat)
    {

        return progression.GetStat(stat,characterClass, startLevel);
    }
 
    public int CalculateLevel()
    {
        float currentExp = (float)GetComponent<Experience>().CaptureState();
        int maxLevel = progression.GetLevel(Stat.ExperienceToLevelUp, characterClass);
        for(int currentLevel = 0; currentLevel < maxLevel; currentLevel++)
        {
            float levelXp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, currentLevel);
            if (levelXp > currentExp)
            {
                return currentLevel;
            }
        }
        return maxLevel;
    }
}
