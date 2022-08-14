using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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

    //CharacterInfoUI
    [SerializeField] private Image InfoPortrait;
    [SerializeField] private TMP_Text VigorDisp, StaminaDisp, StrengthDisp, SkillDisp, IntelligenceDisp, FaithDisp, WillpowerDisp;
    
    public BaseUnit SelectedUnit;
    public Color SelectedColor = new Color(88, 153, 255, 255);
    public Color UnitMovedColor = new Color(103, 103, 103, 255);
    public Color DefaultColor = new Color(255, 255, 255, 255);

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

    public void CharacterInfo(Sprite Portrait ,float Vigor, float Stamina, float Strength, float Skill, float Intelligence, float Faith, float Willpower)
    {
        InfoPortrait.sprite = Portrait;
        VigorDisp.text = "Vigor: " + Vigor;
        StaminaDisp.text = "Stamina: " + Stamina;
        StrengthDisp.text = "Strength: " + Strength;
        SkillDisp.text = "Skill: " + Skill;
        IntelligenceDisp.text = "Intelligence: " + Intelligence;
        FaithDisp.text = "Faith: " + Faith;
        WillpowerDisp.text = "Willpower: " + Willpower;
    }

    public void SetSelectedUnit(BaseUnit unit)
    {
        if(SelectedUnit != null)
        {
            UnselectUnit();
        }
        SelectedUnit = unit;
        SelectedUnit.gameObject.GetComponent<SpriteRenderer>().color = SelectedColor;
    }

    public void UnselectUnit()
    {
        SelectedUnit.gameObject.GetComponent<SpriteRenderer>().color = DefaultColor;
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
            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                var spawnedEnemy = Instantiate(Enemies[i].UnitPrefab);
                spawnedEnemy.UnitData = Enemies[i];
                spawnedEnemy.SetStats();
                EnemySpawnTile[i].SetUnit(spawnedEnemy);
            }

            HeroesSpawned = true;
        }
    }

    public void StartBattle()
    {
        //When this is clicked it...

        //Turn off position select ui DONE
        //Changes all the tiles in the grid manager to not use their grid view DONE
        //Changes game state to players turn DONE
        //Spawns enemy units 

        //Afterwards we need a combat loop manager to replace this battlesetupmanager as it has run its functions... Find what relies on this and figure out a work around.

        //LETS GO
        PositionSelectUI.SetActive(false);
        GridManager.Instance.GridView();

        GameManager.Instance.ChangeState(GameManager.GameState.PlayerTurn);
    }
}
