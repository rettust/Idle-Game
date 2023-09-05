using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

[CreateAssetMenu(fileName = "LootTable", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    [SerializeField] private List<ItemData> _items;
    [System.NonSerialized] private bool isInitialized = false;
    private float _totalWeight;
    string[] prefixArr = File.ReadAllLines(@"G:\Unity\PirateMulana\Idle Game\Assets\Random\text & csv\prefix.txt");
    string[] suffixArr = File.ReadAllLines(@"G:\Unity\PirateMulana\Idle Game\Assets\Random\text & csv\suffix.txt");

    private void Initialize()
    {
        if (!isInitialized)
        {
            _totalWeight = _items.Sum(item => item.weight);
            isInitialized = true;
        }
    }
    public ItemData GetRandomItem()
    {
        Initialize();
        float diceRoll = Random.Range(0f, _totalWeight);

        // cycles through item list
        foreach (var item in _items)
        {
            // if item weight is >= diceroll, take the matching item
            if(item.weight >= diceRoll)
            {
                item.iLvl = Random.Range(item.minIlvl, item.maxIlvl);
                item.attributes = GenerateRandomAttributes(item);
                item.itemName = prefixArr[Random.Range(0, prefixArr.Length-1)] + " " + item.baseItemName + " " + suffixArr[Random.Range(0, suffixArr.Length-1)];
                return item;
            }

            // cycles to next item if no match, increments item id
            diceRoll -= item.weight;
        }
            // shouldnt ever get here unless something went wrong
            Debug.Log("something went sideways");
            return null;


    }
    private List<Attributes> GenerateRandomAttributes(ItemData item)
    {
        List<Attributes> attributes = new List<Attributes>();
        List<Attributes> possibleAttributes = new List<Attributes>();
        int numAttributes = Random.Range(1, 6);
        possibleAttributes.Add(new Attributes("Health", item.iLvl*10, item.iLvl*20, default));
        possibleAttributes.Add(new Attributes("Strength", item.iLvl+1, item.iLvl+5, default));
        possibleAttributes.Add(new Attributes("Defense", item.iLvl+1, item.iLvl+5, default));
        possibleAttributes.Add(new Attributes("Speed", item.iLvl+1, item.iLvl+5, default));
        possibleAttributes.Add(new Attributes("Credit Production", item.iLvl+1, item.iLvl+5, default));
        possibleAttributes.Add(new Attributes("Ship Speed", item.iLvl+1, item.iLvl+5, default));



        for(int i = 0; i < numAttributes; i++){
            int index = Random.Range(0, possibleAttributes.Count);
            Attributes attribute = possibleAttributes[index];
            int value = Random.Range(attribute.minValue, attribute.maxValue);
            attributes.Add(new Attributes(attribute.name, attribute.minValue, attribute.maxValue, value));
            possibleAttributes.RemoveAt(index);
        }

        return attributes;
    }
}

// use this to create the actual items & their relative % weight & potential statrolls
[System.Serializable]
public class ItemData
{
    public string itemName;
    public string itemType;
    public string baseItemName;
    public Rarity itemRarity;
    [HideInInspector] public List<string> attributeMods;
    [HideInInspector] public List<float> attributeValues;
    public int minIlvl;
    public int maxIlvl;
    public int iLvl;
    public float weight;
    [HideInInspector] public List<Attributes> attributes;

    // if i plan on using actual item icons later on this is where it would go
    //public Sprite sprite
}


public class Rarity
{
    private int crappyWeight = 400;
    private int commonWeight = 800;
    private int magicalWeight = 200;
    private int rareWeight = 50;
    private int epicWeight = 25;
    private int legendaryWeight = 5;


    private int RollDemBones(){
        int totalRarityWeight = crappyWeight + commonWeight + magicalWeight + rareWeight + epicWeight + legendaryWeight;
        int rollRarity = Random.Range(0, totalRarityWeight);
        return rollRarity;
    }

}

public class Attributes
{
    public string name;
    public int minValue;
    public int maxValue;
    public int value;

    public Attributes(string name, int minValue, int maxValue, int value)
    {
        this.name = name;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.value = value;
    }
}


