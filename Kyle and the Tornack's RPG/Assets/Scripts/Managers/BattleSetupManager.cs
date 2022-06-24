using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSetupManager : MonoBehaviour
{
    public static BattleSetupManager Instance;

    private List<ScriptableUnit> units;
    private void Awake()
    {
        Instance = this;

        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

}
