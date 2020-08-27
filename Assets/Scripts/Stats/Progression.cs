using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Stat
{
    Health,Experience,ExperienceToLevelUp,DamageRate
}

[CreateAssetMenu(fileName ="Progression",menuName ="Stats/New Progression",order =0)]
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionClass[] progressions;
    Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookUpTable = null;

    [Serializable]
    public class ProgressionClass
    {
        [SerializeField] public CharacterClass character;
        [SerializeField] public ProgressionStats[] progressionStats;
    }
    [Serializable]
    public class ProgressionStats
    {
       [SerializeField] public Stat stat;
       [SerializeField] public float[] levels;
    }

    public float GetStat(Stat stat,CharacterClass characterClass,int level)
    {
        BuildLookUp();

        if (lookUpTable.ContainsKey(characterClass)) 
        {
            if (lookUpTable[characterClass].ContainsKey(stat))
            {
                if (lookUpTable[characterClass][stat].Length > level)
                    return lookUpTable[characterClass][stat][level];
                else return lookUpTable[characterClass][stat][0];
            }
        }
       
        return 20;
    }
    public int GetLevel(Stat stat, CharacterClass characterClass)
    {
        BuildLookUp();
        return lookUpTable[characterClass][stat].Length;
    }
    private void BuildLookUp()
    {
        if (lookUpTable != null) return;
        lock(this){
            lookUpTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (var progression in progressions)
            {
                lookUpTable.Add(progression.character, new Dictionary<Stat, float[]>());
                foreach (ProgressionStats progressionStats in progression.progressionStats)
                {
                   
                    lookUpTable[progression.character].Add(progressionStats.stat, progressionStats.levels);

                }

            } 
        }
    }

}
