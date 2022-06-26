using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSetupManager : MonoBehaviour
{
    public static BattleSetupManager Instance;

    [SerializeField] private GameObject buttonTemplate;
    private List<ScriptableUnit> units;
    private List<ScriptableUnit> heroes;

    private void Awake()
    {
        Instance = this;
        units = Resources.LoadAll<ScriptableUnit>("Units/Heroes").ToList();
    }

    private void Start()
    {

        for (int i = 0; i <= units.Count; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.name = units[i].CharName;
            button.GetComponent<ButtonListButton>().SetButtonDetails(units[i].CharName, units[i].CharPortrait);
            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }

    public void GenButtons()
    {

    }

    public void SelectHeroes()
    {

    }

    public void PositionHeroes()
    {

    }
}
