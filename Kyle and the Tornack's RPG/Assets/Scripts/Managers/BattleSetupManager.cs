using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class BattleSetupManager : MonoBehaviour
{
    public static BattleSetupManager Instance;

    //Unit Select Screen
    [SerializeField] private GameObject CharacterSelectUI;
    [SerializeField] private GameObject buttonTemplate;
    private List<ScriptableUnit> heroes;
    private bool HeroUIGenerated = false;
    public List<ScriptableUnit> SelectedUnits;

    public TMP_Text NoHeroes;
    public GameObject LessThanSix;

    //Position Select Screen
    [SerializeField] private GameObject PositionSelectUI;
    private bool HeroesSpawned = false;
    [SerializeField] private CameraMoveControl camControl;
    [SerializeField] private CameraZoomControl camZoom;

    public BaseUnit SelectedUnit;

    private void Awake()
    {
        Instance = this;
        heroes = Resources.LoadAll<ScriptableUnit>("Units/Heroes").ToList();
    }

    public void GenButtons()
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            if (heroes[i].DeadorInjured == false)
            {
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);

                button.name = heroes[i].CharName;
                button.GetComponent<UnitSelectButton>().SetButtonDetails(heroes[i].CharName, heroes[i].CharPortrait, heroes[i]);
                button.transform.SetParent(buttonTemplate.transform.parent, false);
            }
        }
    }

    public void SelectHeroes()
    {
        if(HeroUIGenerated == false)
        {
            CharacterSelectUI.SetActive(true);
            GenButtons();
        }
    }

    public void ChoosePositions()
    {
        if(SelectedUnits.Count == 0)
        {
            StartCoroutine(NoHeroesTextFade());
        }
        else if(SelectedUnits.Count < 6)
        {
            LessThanSix.SetActive(true);
        }
        else
        {
            CharacterSelectUI.SetActive(false);
            GameManager.Instance.ChangeState(GameManager.GameState.SelectStartPositions);
        }
    }

    public IEnumerator NoHeroesTextFade()
    {
        NoHeroes.enabled = true;
        NoHeroes.color = new Color(NoHeroes.color.r, NoHeroes.color.g, NoHeroes.color.b, 1);

        yield return new WaitForSeconds(1);

        while(NoHeroes.color.a > 0f)
        {
            NoHeroes.color = new Color(NoHeroes.color.r, NoHeroes.color.g, NoHeroes.color.b, NoHeroes.color.a - (Time.deltaTime / 0.3f));
            yield return null;
        }
        NoHeroes.enabled = false;
    }

    public void DontStart()
    {
        LessThanSix.SetActive(false);
    }

    public void ConfirmStart()
    {
        LessThanSix.SetActive(false);
        CharacterSelectUI.SetActive(false);
        GameManager.Instance.ChangeState(GameManager.GameState.SelectStartPositions);
    }

    public void SelectStartingPositions()
    {
        if(HeroesSpawned != true)
        {
            PositionSelectUI.SetActive(true);
            camControl.enabled = true;
            camZoom.enabled = true;

            foreach(ScriptableUnit Hero in SelectedUnits)
            {
                var spawnedHero = Instantiate(Hero.UnitPrefab);
                var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

                randomSpawnTile.SetUnit(spawnedHero);
            }
        }
        else
        {

        }
    }

    public void SetSelectedUnit(BaseUnit unit)
    {
        SelectedUnit = unit;
    }

    public void StartBattle()
    {

    }
}
