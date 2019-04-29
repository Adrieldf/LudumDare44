using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public static Upgrades Instance;
    private List<UpgradeTypes> UpgradesList = new List<UpgradeTypes>();
    public GameObject upgradesPanel;
    public TextMeshProUGUI[] UpgradesOption;
    private int option1;
    private int option2;
    private int option3;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpgradesList.Add(new UpgradeTypes("Beserker", "+ Attack", "- Health", UpSubTypes.PlusAttack, UpSubTypes.MinusHealth));
        UpgradesList.Add(new UpgradeTypes("Hit Hard", "+ Attack", "- Speed", UpSubTypes.PlusAttack, UpSubTypes.MinusSpeed));
        UpgradesList.Add(new UpgradeTypes("Tanky boi", "+ Health", "- Speed", UpSubTypes.PlusHealth, UpSubTypes.MinusSpeed));
        UpgradesList.Add(new UpgradeTypes("Healthy", "+ Health", "- Attack", UpSubTypes.PlusHealth, UpSubTypes.MinusAttack));
        UpgradesList.Add(new UpgradeTypes("What?", "+ Health", "- Health", UpSubTypes.PlusHealth, UpSubTypes.MinusHealth));
        UpgradesList.Add(new UpgradeTypes("Run Sam Run", "+ Speed", "- Health", UpSubTypes.PlusSpeed, UpSubTypes.MinusHealth));
        UpgradesList.Add(new UpgradeTypes("Run Fast", "+ Speed", "- Attack", UpSubTypes.PlusSpeed, UpSubTypes.MinusAttack));
    }
    public void ShowUpgrades()
    {
        RollOptions();
        upgradesPanel.SetActive(true);
        MusicGameController.Instance.PlayPowerUpMusic();
    }
    public void onClickUpgrade(int upgrade)
    {
        Debug.Log(upgrade);
        int index = 0;
        if (upgrade == 1)
            index = option1;
        else if (upgrade == 2)
            index = option2;
        else
            index = option3;

        switch (UpgradesList[index].Plus)
        {
            case UpSubTypes.PlusAttack:
                Character.Instance.ChangeAttack(1);
                break;
            case UpSubTypes.PlusHealth:
                Character.Instance.ChangeHealth(1);
                break;
            case UpSubTypes.PlusSpeed:
                Character.Instance.ChangeSpeed(1);
                break;
        }
        switch (UpgradesList[index].Minus)
        {
            case UpSubTypes.MinusAttack:
                Character.Instance.ChangeAttack(-1);
                break;
            case UpSubTypes.MinusHealth:
                Character.Instance.ChangeHealth(-1);
                break;
            case UpSubTypes.MinusSpeed:
                Character.Instance.ChangeSpeed(-1);
                break;
        }
        upgradesPanel.SetActive(false);
        MusicGameController.Instance.PlayBattleMusic();
    }
    public void RollOptions()
    {
        option1 = getRandomNumber(-1, UpgradesList.Count);
        option2 = getRandomNumber(option1, UpgradesList.Count);
        option3 = getRandomNumber(option2, UpgradesList.Count);
        if (option3 == option1)
            option3 = getRandomNumber(option1, UpgradesList.Count);

        UpgradesOption[0].text = UpgradesList[option1].Title;
        UpgradesOption[1].text = UpgradesList[option1].TextPlus;
        UpgradesOption[2].text = UpgradesList[option1].TextMinus;
        UpgradesOption[3].text = UpgradesList[option2].Title;
        UpgradesOption[4].text = UpgradesList[option2].TextPlus;
        UpgradesOption[5].text = UpgradesList[option2].TextMinus;
        UpgradesOption[6].text = UpgradesList[option3].Title;
        UpgradesOption[7].text = UpgradesList[option3].TextPlus;
        UpgradesOption[8].text = UpgradesList[option3].TextMinus;
    }
    public int getRandomNumber(int lastNumber, int max)
    {
        int number = Random.Range(0, max);
        if (lastNumber != -1 && number != lastNumber)
            return number;
        else
            getRandomNumber(number, max);

        return number;
    }
    public struct UpgradeOption
    {
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Plus;
        public TextMeshProUGUI Minus;
    }
    public class UpgradeTypes
    {
        public string Title { get; set; }
        public string TextPlus { get; set; }
        public string TextMinus { get; set; }
        public UpSubTypes Plus { get; set; }
        public UpSubTypes Minus { get; set; }

        public UpgradeTypes(string title, string textPlus, string textMinus, UpSubTypes plus, UpSubTypes minus)
        {
            Title = title;
            TextPlus = textPlus;
            TextMinus = textMinus;
            Plus = plus;
            Minus = minus;
        }
    }
    public enum UpSubTypes
    {
        PlusHealth = 0,
        PlusAttack = 1,
        PlusSpeed = 2,
        MinusHealth = 3,
        MinusAttack = 4,
        MinusSpeed = 5,
    }
}
