using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSetupManager : MonoBehaviour
{
    public static BattleSetupManager Instance;

    [Header("Unit Select Screen")]
    [SerializeField] private GameObject CharacterSelectUI;
    [SerializeField] private GameObject buttonTemplate;
    private List<ScriptableUnit> heroes;
    private bool HeroUIGenerated = false;
    public List<ScriptableUnit> SelectedUnits;

    [Space]
    [Header("Poppup UI")]
    public TMP_Text NoHeroes;
    public GameObject LessThanSix;

    [Space]
    [Header("Position Select Screen")]
    [SerializeField] private GameObject PositionSelectUI;
    private bool HeroesSpawned = false;
    [SerializeField] private CameraMoveControl camControl;
    [SerializeField] private CameraZoomControl camZoom;

    //CharacterInfoUI
    [Space]
    [Header("CharacterInfo")]
    [SerializeField] private Image InfoPortrait;
    [SerializeField] private TMP_Text VigorDisp;
    [SerializeField] private TMP_Text StaminaDisp;
    [SerializeField] private TMP_Text PowerDisp;
    [SerializeField] private TMP_Text SkillDisp;
    [SerializeField] private TMP_Text ArcaneDisp;
    [SerializeField] private TMP_Text WillDisp;
    
    [Space]
    [Header("Gameplay Values")]
    public BaseUnit SelectedUnit;
    [Space]
    //For enemy units, we shall have a list of enemies and a tile paired together
    public List<ScriptableUnit> Enemies;
    public List<Tile> EnemySpawnTile;

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
                GameObject button = Instantiate(buttonTemplate);
                button.SetActive(true);

                button.name = heroes[i].CharName;
                button.GetComponent<UnitSelectButton>().SetButtonDetails(heroes[i].CharName, heroes[i].CharPortrait, heroes[i]);
                button.transform.SetParent(buttonTemplate.transform.parent, false);
            }
        }
        HeroUIGenerated = true;
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

    public void CharacterInfo(Sprite Portrait ,float Vigor, float Stamina, float Power, float Skill, float Arcane, float Will)
    {
        InfoPortrait.sprite = Portrait;
        VigorDisp.text = "Vigor: " + Vigor;
        StaminaDisp.text = "Stamina: " + Stamina;
        PowerDisp.text = "Power: " + Power;
        SkillDisp.text = "Skill: " + Skill;
        ArcaneDisp.text = "Arcane: " + Arcane;
        WillDisp.text = "Will: " + Will;
    }

    public void SetSelectedUnit(BaseUnit unit)
    {
        if(SelectedUnit != null)
        {
            UnselectUnit();
        }
        SelectedUnit = unit;
        SelectedUnit.UnitSelected();
    }

    public void UnselectUnit()
    {
        SelectedUnit.UnitDeselected();
        SelectedUnit = null;
    }

    public void SelectStartingPositions()
    {
        if (HeroesSpawned != true)
        {
            PositionSelectUI.SetActive(true);
            camControl.enabled = true;
            camZoom.enabled = true;

            foreach (ScriptableUnit Hero in SelectedUnits)
            {
                var spawnedHero = Instantiate(Hero.UnitPrefab);
                spawnedHero.UnitData = Hero;
                spawnedHero.SetStats();
                var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

                randomSpawnTile.SetUnit(spawnedHero);

                spawnedHero.Init = (int)(spawnedHero.Skill + Random.Range(1, 10));
                BattleManager.Instance.AllUnits.Add(spawnedHero);
            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                var spawnedEnemy = Instantiate(Enemies[i].UnitPrefab);
                spawnedEnemy.UnitData = Enemies[i];
                spawnedEnemy.SetStats();
                EnemySpawnTile[i].SetUnit(spawnedEnemy);
                spawnedEnemy.Init = (int)(spawnedEnemy.Skill + Random.Range(1, 10));
                BattleManager.Instance.AllUnits.Add(spawnedEnemy);
            }

            HeroesSpawned = true;
        }
    }

    public void StartBattle()
    {
        PositionSelectUI.SetActive(false);
        GridManager.Instance.GridView();
        BattleManager.Instance.CreateInitiativeOrder();
        GameManager.Instance.ChangeState(GameManager.GameState.CombatLoop);
    }
}
