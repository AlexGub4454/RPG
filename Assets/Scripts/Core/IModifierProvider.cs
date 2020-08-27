using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifierProvider 
{
    IEnumerable<float> GetProviders(Stat stat);
}
