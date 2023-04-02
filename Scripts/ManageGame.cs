using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
    #region Game Var
    public GameObject StartScreenObjects;
    public GameObject MainGameObjects;
    public string playercommand;
    public InputField playerInput;
    public Text gameOutput;
    public bool newgame;
    public string basestring;
    public bool encounterpresent;
    public int timer = 5;
    #endregion
    #region Player Var
    public int Score;
    public int highscore;
    public int blood;
    public int sweat;
    public int tears;
    public int grit;
    public int sight;
    public int sound;
    public float weight;
    public float distance;
    public int timeSec;
    public int timeMin;
    public int timeHr;
    public bool[] condition = new bool[29];
    //00 - Bleeding     01 - Bruised        02 - Broken Bones       03 - Unconscious        04 - Dead
    //05 - Tired        06 - Exhausted      07 - Confused           08 - Shocked            09 - Distressed
    //10 - Delusional   11 - Insane         12 - Happy              13 - Estatic            14 - Sad
    //15 - Depressed    16 - Angry          17 - Furious            18 - Scared             19 - Terrified
    //20 - Blind        21 - Deaf           22 - Mute               23 - Poisoned           24 - Thirsty
    //25 - Parched      26 - Hungry         27 - Starving           28 - Embarrased
    public string[] conditionName = new string[29]
    {
        "Bleeding", "Bruised", "Bones Broken", "Unconscious", "Dead",
        "Tired", "Exhausted", "Confused", "Shocked", "Distressed",
        "Delusional", "Insane", "Happy", "Estatic", "Sad",
        "Depressed", "Angry", "Furious", "Scared", "Terrified",
        "Blind", "Deaf", "Mute", "Poisoned", "Thirsty",
        "Parched", "Hungry", "Starving", "Embarrased"
    };
    public int[] inventory = new int[49];
    //00 - First Aid    01 - Knife          02 - Machete            03 - Pistol             04 - Pistol Ammo
    //05 - Crossbow     06 - Crossbow Ammo  07 - Brass Knuckles     08 - Water Gun          09 - Watch
    //10 - Water Bottle 11 - Water % Full   12 - Energy Bar         13 - Nuts               14 - Chocolate
    //15 - Potato Chips 16 - Raw Meat       17 - Cooked Meat        18 - Unknown            19 - Sleeping Bag
    //20 - Shirt        21 - Jeans          22 - Pants              23 - Hat                24 - Vest
    //25 - Jacket       26 - Shoes          27 - Socks              28 - Poncho             29 - Gloves
    //30 - Ski Mask     31 - Coat           32 - Knee Pads          33 - NV Goggles         34 - Armour
    //35 - Backpack     36 - Flashlight     37 - Matches            38 - Plush              39 - Mirror
    //40 - Paint Brush  41 - Comb           42 - Paper Clip         43 - Toothpaste         44 - Crayons
    //45 - Dice         46 - Yarn           47 - Nail               48 - Photo
    public string[] inventoryName = new string[49]
    {
        "First Aid Kit", "Knife", "Machete", "Pistol", "Pistol Ammo",
        "Crossbow", "Crossbow Ammo", "Brass Knuckles", "Water Gun", "Watch",
        "Water Bottle", "Water % Full", "Energy Bar", "Nuts", "Chocolate",
        "Potato Chips", "Raw Meat", "Cooked Meat", "Unknown", "Sleeping Bag",
        "Shirt", "Jeans", "Pants", "Hat", "Vest",
        "Jacket", "Shoes", "Socks", "Poncho", "Gloves",
        "Ski Mask", "Coat", "Knee Pads", "Night Vision Goggles", "Combat Armour",
        "Backpack", "Flashlight", "Matches", "Plush", "Mirror",
        "Paint Brush", "Comb", "Paper Clip", "Toothpaste", "Crayons",
        "Dice", "Yarn", "Nail", "Photo"
    };
    public int encountedItems;
    public int encountedWanderers;
    public int encountedEntities;
    public int selectedItem;
    public bool autoplay;
    public bool isCheating;
    public bool[] cheats = new bool[5];
    // inf Blood | inf Sweat | inf Tears | No Weight | No Conditions
    #endregion
    #region Room Var
    public int eventtimer;
    public int eventtype;
    //00 - No Event     01 - Blackout       02 - Deafing Sound      03 - Random Item        04 - Corpse
    //05 - Friend       06 - Foe            07 - Deathmoth          08 - Duller             09 - Hound
    public int totalexits;
    public int targetexits;
    public int[] exits = new int[4];
    //00 - North        01 - East           02 - South              03 - West
    public string[] directions = new string[4] { "North", "East", "South", "West"};
    //Max 2 each Min 2 total
    #endregion
    #region Wanderer Var
    public int newWanderID;
    public string newWanderName;
    public int newWanderBlood;
    public int newWanderSweat;
    public int newWanderPronoun;
    public int[] recruitID = new int[10];
    public string[] recruitName = new string[10];
    public int[] recruitBlood = new int[10];
    public int[] recruitPronoun = new int[10];
    #endregion
    #region Entity Var
    public int entityBlood;
    public int entitySweat;
    public int entityGrit;
    #endregion
    #region Random Pull Var
    public int randomNumber;
    public string[] randomName = new string[50] {
        "Harvey Winchester", "Adelia Blakesley", "Ely Pemberton", "Dina Dick", "Carter Kingson", "Ronnie Miller", "Jenny Tolbert", "Babette Jack", "Maura Colt", "Virgee Beasley"
      , "Misti Michaels", "George Pierce", "Doris Clayton", "Vanessa Gibbs", "Karissa Blackburn", "Calla Wilkie", "Phillis Pickering", "Rickie Timberlake", "Prosper Glass", "Shleby Bowman"
      , "Winthrop Haywood", "Daniel Derby", "Lynda Landon", "Christal Poindexter", "Jenni Woodhams", "Dirk Dallas", "Cammie Eady", "Trix Blackflame", "Millard Haden", "Frieda Farmer"
      , "Audrey Christison", "Trish Kidd", "Bailey Jervis", "Esther Dobbs", "Eldon Hilton", "Derek Russell", "Shelby Humphrey", "Tyron Clarkson", "Nik Lambborn", "Clem Young"
      , "Connor Douglass", "Erin Keur", "Joseph Simon", "Benjamin De Cler", "Ethan Brook Rodney", "Ethan Bowler", "Ally Wollerman", "Boston Zeng", "Roxie Cunningham", "John Smith"};
    public int[] pronouns = new int[50]
    {
        1, 2, 1, 2, 1, 1, 2, 2, 2, 2
      , 1, 1, 2, 2, 2, 1, 1, 1, 2, 2
      , 1, 1, 2, 1, 2, 1, 2, 2, 1, 2
      , 2, 2, 1, 1, 1, 1, 2, 1, 1, 1
      , 1, 3, 1, 1, 1, 1, 2, 1, 2, 1
    };
    //1 - He/Him 2 - She/Her 3 - They/Them
    public string[] randomFlavourText = new string[10]
    {
        "You thought you heard something, but it was probably nothing.",
        "Wait... have you been here before?",
        "The moistness of the carpet is starting to get to you.",
        "The lights flickered for a moment, but nothing else happened.",
        "You see a door, but when you blinked it vanished.",
        "Insane scribblings are found across the walls.",
        "You're starting to miss home.",
        "The mold is starting to look tasty.",
        "You see a flight of stairs, but when you look up them, you see yourself looking back.",
        "What's that smell?"
    };
    #endregion

    public void Start()
    {
        Application.targetFrameRate = 60;
        highscore = PlayerPrefs.GetInt("savedHighScore");
    }

    public void NewGame()
    {
        //Show Correct Game Objects
        isCheating = false;
        for (int i = 0; i < cheats.Length; i++)
        {
            cheats[i] = false;
        }
        Score = 0;
        highscore = PlayerPrefs.GetInt("savedHighScore");
        autoplay = false;
        encounterpresent = false;
        StartScreenObjects.SetActive(false);
        MainGameObjects.SetActive(true);
        #region Reset Player Stats
        blood = 100;
        sweat = 0;
        tears = 0;
        grit = 0;
        sight = 100;
        sound = 0;
        weight = 0;
        distance = 0;
        timeSec = 0;
        timeMin = 0;
        timeHr = 0;
        for (int i = 0; i < condition.Length; i++)
        {
            condition[i] = false;
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = 0;

        }
        encountedItems = 0;
        encountedWanderers = 0;
        encountedEntities = 0;
        #endregion
        eventtimer = 10;
        eventtype = 0;
        totalexits = 0;
        newgame = true;
        for (int i = 0; i < exits.Length; i++)
        {
            exits[i] = 0;
        }
        #region Reset Wanderer Stats
        newWanderID = -1;
        newWanderName = "";
        newWanderBlood = 100;
        newWanderSweat = 0;
        newWanderPronoun = 3;
        for (int i = 0; i < 10; i++)
        {
            recruitID[i] = -1;
            recruitName[i] = "";
            recruitBlood[i] = 100;
            recruitPronoun[i] = 3;
        }
        entityBlood = 100;
        entitySweat = 0;
        entityGrit = 0;
        #endregion
        #region Vary Stats
        blood -= GenerateNumber(0, 6);
        sweat += GenerateNumber(0, 6);
        tears += GenerateNumber(5, 11);
        sight -= GenerateNumber(0, 6);
        sound += GenerateNumber(10, 16);
        grit += 4;
        weight += 0.4f;
        weight = float.Parse(System.Math.Round(weight, 1).ToString());
        //Extra Clothes
        randomNumber = GenerateNumber(1, 11);
        if (randomNumber == 10)
        {
            grit++;
            weight += 0.1f;
            weight = float.Parse(System.Math.Round(weight, 1).ToString());
        }
        //Extra Item
        randomNumber = GenerateNumber(1, 51);
        if (randomNumber == 50)
        {
            randomNumber = GenerateNumber(0, 50);
            inventory[randomNumber]++;
        }
        //Extra Condition
        randomNumber = GenerateNumber(1, 1001);
        if (randomNumber == 1000)
        {
            do
            {
                randomNumber = GenerateNumber(0, 30);
            }
            while (randomNumber == 4);
            condition[randomNumber] = true;
        }
        #endregion
        GenerateNewRoom();
    }

    public void GenerateNewRoom()
    {
        #region Cheats
        if (cheats[0])
        {
            blood = 100;
        }
        if (cheats[1])
        {
            sweat = 0;
        }
        if (cheats[2])
        {
            tears = 0;
        }
        if (cheats[3])
        {
            weight = 1;
        }
        if (cheats[4])
        {
            for (int i = 0; i < condition.Length; i++)
            {
                if (i != 4)
                {
                    condition[i] = false;
                }
            }
        }
        #endregion
        #region Calculate Score
        Score = Mathf.FloorToInt((distance + (101 - tears) + 
            (101 - sweat) + (encountedItems) + (10 * encountedEntities) + 
            (5 * encountedWanderers)) + (timeHr));
        #endregion
        #region Time and Space
        //Output Time
        gameOutput.text = "[";
        if (inventory[9] > 0)
        {
            gameOutput.text += timeHr + ":" + timeMin + ":" + timeSec + "]\n";
        }
        else
        {
            gameOutput.text += "??:??:??]\n";
        }
        //Output Distance
        gameOutput.text += "[" + distance + "] miles travelled\n";
        gameOutput.text += "[B: " + blood + " | S: " + sweat + " | T: " + tears + "]\n";
        //Flavour Text
        if (newgame)
        {
            gameOutput.text += "You have entered the Backrooms and are doomed to wander these halls for the rest of your days. " +
                "Might as well try to survive as long as you can.\nType 'help' or 'h' to see what you can do.\n";
            newgame = false;
        }
        randomNumber = GenerateNumber(1, 11);
        if (randomNumber == 10)
        {
            randomNumber = GenerateNumber(0, 10);
            gameOutput.text += randomFlavourText[randomNumber] + "\n";
        }
        #endregion
        #region Generate Room
        //Generate The Room Exits
        do
        {
            randomNumber = GenerateNumber(0, 3);
            targetexits = GenerateNumber(0, 4);
            if (randomNumber == 0 && exits[targetexits] != 0)
            {
                exits[targetexits]--;
                totalexits--;
            }
            else if (randomNumber == 2 && exits[targetexits] != 2)
            {
                exits[targetexits]++;
                totalexits++;
            }
        } while (totalexits < 2 || totalexits > 8);
        gameOutput.text += "Out of all the exits in the room, there's...\n";
        for (int i = 0; i < exits.Length; i++)
        {
            if (exits[i] > 0)
            {
                gameOutput.text += exits[i] + " to the " + directions[i] + ".\n";
            }
        }
        #endregion
        #region New Stats and Effects
        //Hostile Encounters
        if (eventtype == 6)
        {
            newWanderSweat += 25;
            blood--;
            if (newWanderSweat >= 100)
            {
                eventtype = 0;
                encounterpresent = false;
                gameOutput.text += "The hostile wanderer left.\n";
            }
        }
        if (eventtype == 7)
        {
            entitySweat += 20;
            blood--;
            if (entitySweat >= 100)
            {
                eventtype = 0;
                encounterpresent = false;
                gameOutput.text += "The Deathmoth lost you in the endless halls.\n";
            }
        }
        if (eventtype == 8)
        {
            entitySweat += 100;
            blood--;
            if (entitySweat >= 100)
            {
                eventtype = 0;
                encounterpresent = false;
                gameOutput.text += "The Duller ran away.\n";
            }
        }
        if (eventtype == 9)
        {
            entitySweat += 10;
            blood--;
            if (entitySweat >= 100)
            {
                eventtype = 0;
                encounterpresent = false;
                gameOutput.text += "The Hound lost your scent and left to find more prey.\n";
            }
        }
        //Add Sweat
        randomNumber = GenerateNumber(1, 11);
        if (randomNumber == 10)
        {
            gameOutput.text += "You started to sweat more";
            if (sweat == 100)
            {
                blood--;
                gameOutput.text += ", but it simply added to the pain.\n";
            }
            else
            {
                sweat++;
                if (weight >= 50 && inventory[9] == 0)
                {
                    sweat++;
                }
                if (weight >= 75 && inventory[9] != 0)
                {
                    sweat++;
                }
                gameOutput.text += ".\n";
            }
            if (sweat < 50)
            {
                condition[5] = false;
                condition[6] = false;
            }
            else if (sweat >= 50 && sweat < 75)
            {
                condition[5] = true;
                condition[6] = false;
                gameOutput.text += "You are Tired.\n";
                if (inventory[12] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[12] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[13] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[13] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[14] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[14] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[15] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[15] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[16] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[16] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[17] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[17] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[18] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[18] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
            }
            else if (sweat >= 75)
            {
                condition[5] = false;
                condition[6] = true;
                gameOutput.text += "You are Exhausted.\n";
                if (inventory[12] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[12] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[13] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[13] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[14] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[14] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[15] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[15] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[16] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[16] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[17] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[17] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
                else if (inventory[18] > 0)
                {
                    gameOutput.text += "You ate " + inventoryName[18] + " to gain some energy.";
                    sweat = GenerateNumber(0, 11);
                }
            }
        }
        if (sweat > 100)
        {
            sweat = 100;
        }
        //Add Tears
        randomNumber = GenerateNumber(1, 101);
        if (randomNumber == 100)
        {
            gameOutput.text += "Your sanity started to drain";
            if (tears == 100)
            {
                blood--;
                gameOutput.text += ", but the insanity hurt you more.\n";
            }
            else
            {
                tears++;
                gameOutput.text += ".\n";
            }
            if (tears < 25)
            {
                condition[9] = false;
                condition[10] = false;
                condition[11] = false;
            }
            else if (tears >= 25 && tears < 50)
            {
                condition[9] = true;
                condition[10] = false;
                condition[11] = false;
                gameOutput.text += "You are Distressed.\n";
            }
            else if (tears >= 50 && tears < 75)
            {
                condition[9] = false;
                condition[10] = true;
                condition[11] = false;
                gameOutput.text += "You are Delusional.\n";
            }
            else if (tears >= 75)
            {
                condition[9] = false;
                condition[10] = false;
                condition[11] = true;
                gameOutput.text += "You are Insane.\n";
            }
        }
        if (tears > 100)
        {
            tears = 100;
        }
        //Ditch Clothes
        randomNumber = GenerateNumber(1, 1001);
        if (randomNumber == 1000)
        {
            gameOutput.text += "Some clothes were starting to smell";
            if (grit == 0)
            {
                gameOutput.text += ", but it was " +
                    "actually just you.\n";
            }
            else
            {
                grit--;
                weight -= 0.1f;
                weight = float.Parse(System.Math.Round(weight, 1).ToString());
                gameOutput.text += ", so you threw them away.\n";
            }
            if (grit == 0)
            {
                condition[28] = true;
                gameOutput.text += "You felt Embarrased.\n";
            }
        }
        if (grit > 0)
        {
            condition[28] = false;
        }
        //Change Lights
        randomNumber = GenerateNumber(1, 11);
        if (randomNumber == 1 && condition[20] == false)
        {
            gameOutput.text += "The lights got a little dimmer.\n";
            if (sight > 4)
            {
                sight -= GenerateNumber(1, 6);
            }
            if (sight < 25)
            {
                gameOutput.text += "It is too dark to see";
                if (inventory[33] > 0 || inventory[36] > 0)
                {
                    gameOutput.text += ", but you lit the way.\n";
                    sight = GenerateNumber(95, 101);
                }
                else
                {
                    gameOutput.text += ".\n";
                }
            }
        }
        else if (randomNumber == 2 && condition[20] == false)
        {
            gameOutput.text += "The lights got a little brighter.\n";
            if (sight < 196)
            {
                sight += GenerateNumber(1, 6);
            }
            if (sight > 175)
            {
                gameOutput.text += "It is too bright to see again. You were Blinded.\n";
                condition[20] = true;
                sight = 0;
            }
        }
        //Change Sound
        randomNumber = GenerateNumber(1, 11);
        if (randomNumber == 1 && condition[21] == false)
        {
            gameOutput.text += "The buzz got a little quieter.\n";
            if (sound > 4)
            {
                sound -= GenerateNumber(1, 3);
            }
        }
        else if (randomNumber == 2 && condition[21] == false)
        {
            gameOutput.text += "The buzz got a little louder.\n";
            if (sound < 196)
            {
                sound += GenerateNumber(1, 3);
            }
            if (sound > 190)
            {
                gameOutput.text += "It is too loud to hear again. You were Deafened.\n";
                condition[21] = true;
                sound = 0;
            }
        }
        //Bleeding
        if (condition[0])
        {
            blood--;
            gameOutput.text += "Your Bleeding worsens.\n";
            randomNumber = GenerateNumber(1, 101);
            if (randomNumber == 100)
            {
                condition[0] = false;
                gameOutput.text += "Your bleeding has stopped.\n";
            }

        }
        //Poisoned
        if (condition[23])
        {
            blood--;
            gameOutput.text += "Your Poison worsens.\n";
            randomNumber = GenerateNumber(1, 101);
            if (randomNumber == 100)
            {
                condition[23] = false;
                gameOutput.text += "Your poison has stopped.\n";
            }

        }
        //Emotion 
        randomNumber = GenerateNumber(1, 101);
        if (randomNumber == 100)
        {
            if (((condition[13] || condition[15]) || (condition[17] || condition[19])))
            {
                condition[13] = false;
                condition[15] = false;
                condition[17] = false;
                condition[19] = false;
                gameOutput.text += "Your high emotion has faded.\n";
            }
            if (condition[12])
            {
                condition[12] = false;
                condition[13] = true;
                gameOutput.text += "You are " + conditionName[13] + ".\n";
            }
            else if (condition[14])
            {
                condition[14] = false;
                condition[15] = true;
                gameOutput.text += "You are " + conditionName[15] + ".\n";
            }
            else if (condition[16])
            {
                condition[16] = false;
                condition[17] = true;
                gameOutput.text += "You are " + conditionName[17] + ".\n";
            }
            else if (condition[18])
            {
                condition[18] = false;
                condition[19] = true;
                gameOutput.text += "You are " + conditionName[19] + ".\n";
            }
            if (!( ((condition[12] && condition[13]) && (condition[14] && condition[15])) && ((condition[16] && condition[17]) && (condition[18] && condition[19]))))
            {
                randomNumber = GenerateNumber(6, 10);
                condition[randomNumber * 2] = true;
                gameOutput.text += "You are " + conditionName[randomNumber * 2] + ".\n";
            }
        }
        //Thirst
        if ((timeHr != 0 && timeHr % 24 == 0) &&
            ( (timeMin == 0) && (timeSec > 0 && timeSec < 10)))
        {
            if (inventory[11] < 10)
            {
                if (!condition[24] && !condition[25])
                {
                    condition[24] = true;
                    condition[25] = false;
                    gameOutput.text += "You are Thirsty.\n";
                }
                else if (condition[24] && !condition[25])
                {
                    condition[24] = false;
                    condition[25] = true;
                    gameOutput.text += "You are Parched.\n";
                }
                else if (condition[25])
                {
                    condition[24] = false;
                    condition[25] = true;
                    condition[4] = true;
                    blood = 0;
                }
            }
            else
            {
                inventory[11] -= 10;
                gameOutput.text += "You stayed hydrated.\n";
            }
        }
        //Hunger
        if ((timeHr != 0 && timeHr % 168 == 0) &&
            ((timeMin == 0) && (timeSec > 0 && timeSec < 10)))
        {
            //12-17 inventory
            //26-27 condition
            if (inventory[12] + inventory[13] + inventory[14] + inventory[15] + inventory[16] + inventory[17] == 0)
            {
                if (!condition[26] && !condition[27])
                {
                    condition[26] = true;
                    condition[27] = false;
                    gameOutput.text += "You are Hungry.\n";
                }
                else if (condition[26] && !condition[27])
                {
                    condition[26] = false;
                    condition[27] = true;
                    gameOutput.text += "You are Starving.\n";
                }
                else if (condition[27])
                {
                    condition[26] = false;
                    condition[27] = true;
                    condition[4] = true;
                    blood = 0;
                }
            }
            else
            {
                for (int i = 12; i < 18; i++)
                {
                    if (inventory[i] != 0)
                    {
                        inventory[i]--;
                    }
                }
                gameOutput.text += "You fought off your hunger.\n";
            }
        }
        //Dead
        if (blood < 1)
        {
            condition[4] = true;
        }
        if (condition[4])
        {
            gameOutput.text += "You are Dead, type 'end' to end the game.\n";
        }
        //Unconscious
        if (blood >= 45 && blood < 50)
        {
            condition[3] = true;
        }
        if (condition[3])
        {
            condition[3] = false;
            for (int i = 0; i < 8; i++)
            {
                GenerateNewRoom();
            }
            sweat = GenerateNumber(0, 6);
            blood = GenerateNumber(40, 45);
            gameOutput.text += "You were unconscious for some time.\n";
        }
        //Time and Space
        distance += 0.01f;
        distance = float.Parse(System.Math.Round(distance, 2).ToString());
        timeSec += GenerateNumber(5, 11);
        if (inventory[9] == 0)
        {
            timeSec += GenerateNumber(5, 46);
        }
        while (timeSec > 59)
        {
            timeSec -= 60;
            timeMin++;
        }
        while (timeMin > 59)
        {
            timeMin -= 60;
            timeHr++;
        }
        #endregion
        #region Events
        //Events
        if (eventtype > 0 && eventtype < 6)
        {
            eventtype = 0;
        }
        if (eventtype == 0)
        {
            eventtimer--;
            if (eventtimer == 0)
            {
                eventtype = GenerateNumber(0, 10);
                eventtimer = GenerateNumber(10, 101);
                condition[7] = false;
                condition[8] = false;
            }
        }
        //Blackout
        if (eventtype == 1)
        {
            sight = GenerateNumber(0, 5);
            gameOutput.text += "The lights have suddenly gone out, you have temporarily been blinded.\n";
        }
        //Deafening Sound
        else if (eventtype == 2)
        {
            sound = GenerateNumber(95, 101);
            condition[21] = true;
            gameOutput.text += "A scream echoes through the halls, bursting your ears and making you deaf.\n";
        }
        //Random Item
        else if (eventtype == 3)
        {
            FindItem();
        }
        //Corpse
        else if (eventtype == 4)
        {
            gameOutput.text += "You find a corpse lying on the ground, they might have something in their pockets.\n";
            condition[8] = true;
        }
        //Friend
        else if (eventtype == 5)
        {
            if (encountedWanderers != 1000)
            {
                randomNumber = GenerateNumber(0, 50);
                newWanderID = randomNumber;
                newWanderName = randomName[randomNumber];
                newWanderBlood = 100;
                newWanderSweat = 0;
                newWanderPronoun = pronouns[randomNumber];
                encountedWanderers++;
                gameOutput.text += "You run into another person after what feels like too long. They seem to be friendly and " + ApplyOtherPronouns(newWanderPronoun) + " says "
                    + ApplyPronouns(newWanderPronoun) + " name is " + newWanderName + ".\n";
            }
        }
        if (encounterpresent == false)
        {
                    //Foe
            if (eventtype == 6)
            {
                if (encountedWanderers != 1000)
                {
                    encounterpresent = true;
                    randomNumber = GenerateNumber(0, 50);
                    newWanderID = randomNumber;
                    newWanderName = randomName[randomNumber];
                    newWanderBlood = 100;
                    newWanderSweat = 0;
                    newWanderPronoun = pronouns[randomNumber];
                    encountedWanderers++;
                    gameOutput.text += "You run into another person after what feels like too long. They don't seem to be too friendly and when " + ApplyOtherPronouns(newWanderPronoun) +
                        " starts to look at you, you get the sudden urge to run away.\n";
                }
            }
            //Deathmoth
            else if (eventtype == 7)
            {
                if (encountedEntities != 100)
                {
                    encounterpresent = true;
                    entityBlood = 100;
                    entitySweat = 25;
                    entityGrit = 0;
                    encountedEntities++;
                    gameOutput.text += "You encounter a Deathmoth. Your best choice might be to run away.\n";
                }
            }
            //Duller
            else if (eventtype == 8)
            {
                if (encountedEntities != 100)
                {
                    encounterpresent = true;
                    entityBlood = 1;
                    entitySweat = 99;
                    entityGrit = 100;
                    encountedEntities++;
                    gameOutput.text += "You encounter a Duller. It should run away soon.\n";
                    condition[7] = true;
                }
            }
            //Hound
            else if (eventtype == 9)
            {
                if (encountedEntities != 100)
                {
                    encounterpresent = true;
                    entityBlood = 50;
                    entitySweat = 0;
                    entityGrit = 10;
                    encountedEntities++;
                    gameOutput.text += "You encounter a Hound. Your best choice might be to fight back.\n";
                }
            }
        }
        #endregion
        #region Win
        if (distance >= 600000000 && !isCheating)
        {
            gameOutput.text += "You've done it. You've reached the end of the Backrooms. The door ahead should lead you back to reality.\n" +
                "The future ahead is unclear but you pass through into the unknown.\n\n" +
                "THE END\n\n" +
                "Type 'end' to finish the game.";
        }
        #endregion
        basestring = gameOutput.text;
    }

    public void FindItem()
    {
        randomNumber = 1;
        while (randomNumber == 1 && encountedItems != 10000)
        {
            do
            {
                randomNumber = GenerateNumber(0, 49);
            } while (randomNumber == 11);
            inventory[randomNumber]++;
            weight += 0.1f;
            weight = float.Parse(System.Math.Round(weight, 2).ToString());
            if (randomNumber == 3)
            {
                inventory[4] += 18;
            }
            if (randomNumber == 4)
            {
                inventory[4] += 17;
            }
            if (randomNumber == 5)
            {
                inventory[6] += 4;
            }
            if (randomNumber == 6)
            {
                inventory[6] += 3;
            }
            if (randomNumber == 10 || randomNumber == 8)
            {
                inventory[11] += 100;
            }
            //Clothes
            if (randomNumber >= 20 && randomNumber <= 34)
            {
                inventory[randomNumber]--;
                grit++;
                if ((randomNumber == 24 || randomNumber == 28) || randomNumber == 31)
                {
                    grit++;
                }
                if (randomNumber == 30 || randomNumber == 32)
                {
                    grit += 4;
                }
                if (randomNumber == 33)
                {
                    inventory[33]++;
                }
                if (randomNumber == 34)
                {
                    grit += 24;
                }
            }
            encountedItems++;
            gameOutput.text += "You find a " + inventoryName[randomNumber] + ".\n";
            randomNumber = GenerateNumber(1, 3);
        }
    }

    public void PlayerAction()
    {
        PlayerCommand(playerInput.text.ToLower());
    }

    public void PlayerCommand(string command)
    {
        #region Manual
        if (command == "h" || command == "help")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Commands:\n" +
                "help / h - Shows you this text.\n" +
                "back / b - Sends you back to the room information.\n" +
                "walk / w - Wander into another room.\n" +
                "stats / s - See your stats, such as blood, sweat, and tears.\n" +
                "inventory / i - See what items you have available to you.\n" +
                "use / u then a number - Use an item in your inventory.\n" +
                "loot / l - When near a corpse, take everything it has.\n" +
                "talk / t then a number - Talk to friendly wanderers or to recruits you've already met. (Use numbers for talking to recruits)\n" +
                "fight / f - Use your weapons to do what you must.\n" +
                "run / r - When escaping something unwanted, it gives a better chance of it losing your trail.\n" +
                "\nFor more information, type 'Page 2' about Player Statistics.";
        }
        if (command == "page 2")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Player Statistics:\n" +
                "Blood - Represents your health and wellbeing. Once this reaches 0, you are Dead.\n" +
                "Sweat - Represents your stamina and vigour. Once this reaches 100, you will start to take exhaustion damage.\n" +
                "Tears - Represents your sanity and mental fortitude. Once this reaches 100, you will be insane and start to take damage.\n" +
                "Grit - Represents your defence and protection. Keep this as high as possible by wearing clothes.\n" +
                "Sight - Represents how well you can see. Too dark means temporary Blindness while Too bright means permenant Blindness.\n" +
                "Sound - Represents how loud your surroundings are. Being too loud will make you deaf.\n" +
                "Weight - Represents how much you're carrying. If you hold too much without a Backpack, you will Sweat more.\n" +
                "Distance - Shows how far you've come. Measured in miles.\n" +
                "Time - Shows how many of your days you've already counted.\n" +
                "Conditions - There are many to be inflicted with.\n" +
                "Items - Scattered throughout the Backrooms, can help you in survival.\n" +
                "Final Goal: Traverse the entire 600'000'000 miles of the Backrooms.\n" +
                "\nFor more information, type 'Page 3' about Conditions Part 1.";
        }
        if (command == "page 3")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Conditions Part 1:\n" +
                "1.  Bleeding - Inflicted from battle. Causes loss of Blood until a First Aid Kit is used or sometimes on its own.\n" +
                "2.  Bruised - Inflicted from battle. Causes no additional harm.\n" +
                "3.  Broken Bones - Inflicted from battle. Causes no additional harm.\n" +
                "4.  Unconscious - Inflicted from battle or being too Exhausted. Time will pass and Blood will be lost.\n" +
                "5.  Dead - It will always come. It marks the end of your journey.\n" +
                "6.  Tired - Caused by high Sweat, has no other effect.\n" +
                "7.  Exhausted - Caused by high Sweat, final warning before taking Exhaustion damage.\n" +
                "8.  Confused - Caused when encountering someting.\n" +
                "9.  Shocked - Caused when seeing a corpse.\n" +
                "10. Distressed - Caused by high Tears, has no other effect.\n" +
                "\nFor more information, type 'page 4' about Conditions Part 2.";
        }
        if (command == "page 4")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Conditions Part 2:\n" +
                "11. Delusional - Caused by high Tears, has no other effect.\n" +
                "12. Insane - Caused by high Tears, final warning before taking mental damage.\n" +
                "13. Happy - Caused at random. Has no effect.\n" +
                "14. Estatic - Caused at random. Has no effect.\n" +
                "15. Sad - Caused at random. Has no effect.\n" +
                "16. Depressed - Caused at random. Has no effect.\n" +
                "17. Angry - Caused at random. Has no effect.\n" +
                "18. Furious - Caused at random. Has no effect.\n" +
                "19. Scared - Caused at random. Has no effect.\n" +
                "20. Terrified - Caused at random. Has no effect.\n" +
                "\nFor more information, type 'page 5' about Conditions Part 3.";
        }
        if (command == "page 5")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Conditions Part 3:\n" +
                "21. Blind - Caused from battle or the lights. You cannot see.\n" +
                "22. Deaf - Caused from the environment. You cannot hear.\n" +
                "23. Mute - Rarely inflicted from battle. You cannot talk to others.\n" +
                "24. Poisoned - Sometimes caused by eating raw meat, makes you lose blood.\n" +
                "25. Thirsty - Caused by not drinking.\n" +
                "26. Parched - Caused by not drinking, final chance before death.\n" +
                "27. Hungry - Caused by not eating.\n" +
                "28. Starving - Caused by not eating, final chance before death.\n" +
                "29. Embarrased - Caused by lack of clothes. Has no other effect.\n" +
                "\nFor more information, type 'page 6' about Items Part 1.";
        }
        if (command == "page 6")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Items Part 1:\n" +
                "1.  First Aid Kit - Used to gain up to 50 Blood and stop Bleeding.\n" +
                "2.  Knife - (Weapon) Used in battle.\n" +
                "3.  Machete - (Weapon) Used in battle.\n" +
                "4.  Pistol - (Weapon) Used in battle.\n" +
                "5.  Pistol Ammo - (Ammo) Used for pistols.\n" +
                "6.  Crossbow - (Weapon) Used in battle.\n" +
                "7.  Crossbow Bolts - (Ammo) Used for crossbows.\n" +
                "8.  Brass Knuckles - (Weapon) Used in battle.\n" +
                "9.  Water Gun - (Weapon?) Used in battle.\n" +
                "10. Pocket Watch - Used to keep track of time.\n" +
                "\nFor more information, type 'page 7' about Items Part 2";
        }
        if (command == "page 7")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Items Part 2:\n" +
                "11. Water Bottle - Used to stop dehydration.\n" +
                "12. Energy Bar - (Food) Used to stop starvation.\n" +
                "13. Nuts - (Food) Used to stop starvation.\n" +
                "14. Chocolate - (Food) Used to stop starvation.\n" +
                "15. Potato Chips - (Food) Used to stop starvation.\n" +
                "16. Raw Meat - (Food) Used to stop starvation but can inflict poison.\n" +
                "17. Cooked Meat - (Food) Used to stop starvation.\n" +
                "18. Edible(?) Food - (Food) Used to stop starvation and other effects.\n" +
                "19. Sleeping Bag - Used to pass time and reset Sweat.\n" +
                "20. Shirt - (Clothes) Used to increase Grit.\n" +
                "\nFor more information, type 'page 8' about Items Part 3";
        }
        if (command == "page 8")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Items Part 3:\n" +
                "21. Jeans - (Clothes) Used to increase Grit.\n" +
                "22. Pants - (Clothes) Used to increase Grit.\n" +
                "23. Hat - (Clothes) Used to increase Grit.\n" +
                "24. Vest - (Clothes) Used to increase Grit.\n" +
                "25. Jacket - (Clothes) Used to increase Grit.\n" +
                "26. Shoes - (Clothes) Used to increase Grit.\n" +
                "27. Socks - (Clothes) Used to increase Grit.\n" +
                "28. Poncho - (Clothes) Used to increase Grit.\n" +
                "29. Gloves - (Clothes) Used to increase Grit.\n" +
                "30. Ski Mask - (Clothes) Used to increase Grit.\n" +
                "\nFor more information, type 'page 9' about Items Part 4";
        }
        if (command == "page 9")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Items Part 4:\n" +
                "31. Coat - (Clothes) Used to increase Grit.\n" +
                "32. Knee Pads - (Clothes) Used to increase Grit.\n" +
                "33. Night Vision Goggles - (Clothes) Used to increase Grit and see in dark places.\n" +
                "34. Combat Armour - (Clothes) Used to greatly increase Grit.\n" +
                "35. Backpack - Allows more weight to be carried.\n" +
                "36. Flashlight - Allows you to see in dark places.\n" +
                "37. Matches - Allows you to see in dark places.\n" +
                "38. Plush - No purpose.\n" +
                "39. Mirror - No purpose.\n" +
                "40. Paint Brush - No purpose.\n" +
                "\nFor more information, type 'page 10' about Items Part 5";
        }
        if (command == "page 10")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Items Part 5:\n" +
                "41. Comb - No purpose.\n" +
                "42. Paper Clip - No purpose.\n" +
                "43. Toothpaste - No purpose.\n" +
                "44. Crayons - No purpose.\n" +
                "45. Dice - No purpose.\n" +
                "46. Yarn - No purpose.\n" +
                "47. Nail - No purpose.\n" +
                "48. Photo  - No purpose.\n" +
                "\nTo return to the Backrooms, type 'back'.";
        }
        #endregion
        #region Commands
        if (command == "b" || command == "back")
        {
            gameOutput.text = basestring;
        }
        if ((command == "w" || command == "walk") && !condition[4])
        {
            GenerateNewRoom();
        }
        if (command == "s" || command == "stats")
        {
            gameOutput.text = "Stats:\n\n" +
                "Blood - " + blood + "\n" +
                "Sweat - " + sweat + "\n" +
                "Tears - " + tears + "\n" +
                "Grit - " + grit + "\n" +
                "Sight - " + sight + "\n" +
                "Sound - " + sound + "\n" +
                "Weight - " + weight + "\n" +
                "Conditions - ";
            for (int i = 0; i < condition.Length; i++)
            {
                if (condition[i])
                {
                    gameOutput.text += conditionName[i] + " ";
                }
            }
        }
        if (command == "i" || command == "inventory")
        {
            gameOutput.text = "Inventory:\n\n";
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] > 0)
                {
                    if (i == 10)
                    {
                        gameOutput.text += (i + 1) + ". " + inventory[11] + "% Full " + inventory[i] + "x " + inventoryName[i] + "\n";
                    }
                    else if (i == 11)
                    {
                        //do nothing
                    }
                    else
                    {
                        gameOutput.text += (i + 1) + ". " + inventory[i] + "x " + inventoryName[i] + "\n";
                    }
                }
            }
        }
        if (command.Contains("u ") && !condition[4])
        {
            if (command.Length == 3)
            {
                //1-9
                selectedItem = int.Parse(command[command.Length - 1].ToString());
            }
            else if (command.Length == 4)
            {
                //10-49
                selectedItem = int.Parse(command[command.Length - 2].ToString() + command[command.Length - 1].ToString());
            }
            selectedItem--;
            //First Aid 0
            if (selectedItem == 0 && inventory[0] > 0)
            {
                blood += 50;
                if (blood > 100)
                {
                    blood = 100;
                }
                basestring += "You used the First Aid Kit.\n";
                inventory[0]--;
                condition[0] = false;
                condition[1] = false;
                condition[2] = false;
                condition[23] = false;
            }
            //Water 10
            if (selectedItem == 10 && inventory[11] > 9)
            {
                condition[24] = false;
                condition[25] = false;
                sweat -= 5;
                if (sweat < 0)
                {
                    sweat = 0;
                }
                basestring += "You drank water.\n";
                inventory[11] -= 10;
            }
            //Food 12-18 (16 Raw Meat & 18 Unknown) 
            if ((selectedItem >= 12 && selectedItem <= 18) && inventory[selectedItem] > 0)
            {
                condition[26] = false;
                condition[27] = false;
                sweat -= 25;
                if (sweat < 0)
                {
                    sweat = 0;
                }
                basestring += "You ate " + inventoryName[selectedItem] + ".\n";
                inventory[selectedItem]--;
                if (selectedItem == 16)
                {
                    randomNumber = GenerateNumber(0, 4);
                    if (randomNumber == 0)
                    {
                        condition[23] = true;
                        basestring += "You are Poisoned.\n";
                    }
                }
                if (selectedItem == 18)
                {
                    for (int i = 0; i < condition.Length; i++)
                    {
                        randomNumber = GenerateNumber(0, 2);
                        if (randomNumber == 0)
                        {
                            condition[i] = false;
                        }
                        else
                        {
                            condition[i] = true;
                        }
                    }
                    basestring += "That was a bad idea.\n";
                }
            }
            //Sleeping Bag (19)
            if (selectedItem == 19 && inventory[19] > 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    GenerateNewRoom();
                }
                sweat = GenerateNumber(0, 6);
                gameOutput.text += "You slept for some time.\n";
            }
            //Matches (37)
            if (selectedItem == 37 && inventory[37] > 0)
            {
                inventory[37]--;
                sight = GenerateNumber(95, 101);
                basestring += "You used a Match.\n";
            }
        }
        if (command.Contains("use ") && !condition[4])
        {
            if (command.Length == 5)
            {
                //1-9
                selectedItem = int.Parse(command[command.Length - 1].ToString());
            }
            else if (command.Length == 6)
            {
                //10-49
                selectedItem = int.Parse(command[command.Length - 2].ToString() + command[command.Length - 1].ToString());
            }
            selectedItem--;
            //First Aid 0
            if (selectedItem == 0 && inventory[0] > 0)
            {
                blood += 50;
                if (blood > 100)
                {
                    blood = 100;
                }
                basestring += "You used the First Aid Kit.\n";
                inventory[0]--;
                condition[0] = false;
                condition[1] = false;
                condition[2] = false;
                condition[23] = false;
            }
            //Water 10
            if (selectedItem == 10 && inventory[11] > 9)
            {
                condition[24] = false;
                condition[25] = false;
                sweat -= 5;
                if (sweat < 0)
                {
                    sweat = 0;
                }
                basestring += "You drank water.\n";
                inventory[11] -= 10;
            }
            //Food 12-18 (16 Raw Meat & 18 Unknown) 
            if ((selectedItem >= 12 && selectedItem <= 18) && inventory[selectedItem] > 0)
            {
                condition[26] = false;
                condition[27] = false;
                sweat -= 25;
                if (sweat < 0)
                {
                    sweat = 0;
                }
                basestring += "You ate " + inventoryName[selectedItem] + ".\n";
                inventory[selectedItem]--;
                if (selectedItem == 16)
                {
                    randomNumber = GenerateNumber(0, 4);
                    if (randomNumber == 0)
                    {
                        condition[23] = true;
                        basestring += "You are Poisoned.\n";
                    }
                }
                if (selectedItem == 18)
                {
                    for (int i = 0; i < condition.Length; i++)
                    {
                        randomNumber = GenerateNumber(0, 2);
                        if (randomNumber == 0)
                        {
                            condition[i] = false;
                        }
                        else
                        {
                            condition[i] = true;
                        }
                    }
                    basestring += "That was a bad idea.\n";
                }
            }
            //Sleeping Bag (19)
            if (selectedItem == 19 && inventory[19] > 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    GenerateNewRoom();
                }
                sweat = GenerateNumber(0, 6);
                gameOutput.text += "You slept for some time.\n";
            }
            //Matches (37)
            if (selectedItem == 37 && inventory[37] > 0)
            {
                inventory[37]--;
                sight = GenerateNumber(95, 101);
                basestring += "You used a Match.\n";
            }
        }
        if ((command == "l" || command == "loot") && eventtype == 4)
        {
            for (int i = 0; i < 5; i++)
            {
                FindItem();
                weight += 0.1f;
                weight = float.Parse(System.Math.Round(weight, 2).ToString());
                grit++;
                eventtype = 0;
            }
        }
        if ((command == "t" || command == "talk") && (eventtype == 5 && !condition[22]))
        {
            gameOutput.text += "You have a conversation with " + newWanderName + " and " + ApplyOtherPronouns(newWanderPronoun) + " decides to join you.\n";
            if (recruitID[9] != -1)
            {
                gameOutput.text += "Unfortunately, you had to leave " + newWanderName + "behind. You wish them luck.\n";
            }
            for (int i = 0; i < recruitID.Length; i++)
            {
                if (recruitID[i] == -1)
                {
                    recruitID[i] = newWanderID;
                    recruitName[i] = newWanderName;
                    recruitPronoun[i] = newWanderPronoun;
                    recruitBlood[i] = 100;
                    gameOutput.text += "Use 't " + (i + 1) + "' to talk to them anytime.\n";
                    break;
                }
            }
            eventtype = 0;
        }
        if (command.Contains("t ") && (command.Length > 2 && !condition[22]))
        {
            if (command.Length == 3)
            {
                selectedItem = int.Parse(command[command.Length - 1].ToString());
            }
            else if (command.Length == 4)
            {
                selectedItem = int.Parse(command[command.Length - 2].ToString() + command[command.Length - 1].ToString());
            }
            selectedItem--;
            if (recruitID[selectedItem] != -1)
            {
                gameOutput.text += "You talked to " + recruitName[selectedItem] + ".\n";
                tears -= 25;
                if (tears < 0)
                {
                    tears = 0;
                }
                condition[9] = false;
                condition[10] = false;
                condition[11] = false;
            }
        }
        if (command.Contains("talk ") && command.Length > 5)
        {
            if (command.Length == 6)
            {
                selectedItem = int.Parse(command[command.Length - 1].ToString());
            }
            else if (command.Length == 7)
            {
                selectedItem = int.Parse(command[command.Length - 2].ToString() + command[command.Length - 1].ToString());
            }
            selectedItem--;
            if (recruitID[selectedItem] != -1)
            {
                gameOutput.text += "You talked to " + recruitName[selectedItem] + ".\n";
                tears -= 25;
                if (tears < 0)
                {
                    tears = 0;
                }
                condition[9] = false;
                condition[10] = false;
                condition[11] = false;
            }
        }
        if ((command == "f" || command == "fight") && ((eventtype >= 5 && eventtype <= 9) && !condition[4]))
        {
            if (eventtype == 5 || eventtype == 6)
            {
                newWanderBlood -= ((1 + inventory[1] * 5) + (inventory[2] * 10) + (inventory[3] * 1) + (inventory[5] * 5) + (inventory[7] * 5));
                if (newWanderBlood < 0)
                {
                    newWanderBlood = 0;
                }
                inventory[4] -= inventory[3];
                inventory[6] -= inventory[5];
                if (inventory[4] <= 0)
                {
                    inventory[3] = 0;
                }
                if (inventory[6] <= 0)
                {
                    inventory[5] = 0;
                }
                gameOutput.text += "You inflicted " + ((1 + inventory[1] * 5) + (inventory[2] * 10) + (inventory[3] * 1) + (inventory[5] * 5) + (inventory[7] * 5)) +
                    " damage onto " + newWanderName + ".\n";

                randomNumber = GenerateNumber(1, 11) - grit;
                if (randomNumber < 0)
                {
                    randomNumber = 0;
                }
                blood -= randomNumber;
                gameOutput.text += "You lost " + randomNumber + " Blood.\n";

                if (newWanderBlood == 0)
                {
                    gameOutput.text += "You defeated the wanderer and you can now loot the corpse.\n";
                    eventtype = 4;
                    encounterpresent = false;
                }
            }
            else if ((eventtype == 7 || eventtype == 8) || eventtype == 9)
            {
                entityBlood -= ((1 + inventory[1] * 5) + (inventory[2] * 10) + (inventory[3] * 1) + (inventory[5] * 5) + (inventory[7] * 5)) - entityGrit;
                if (entityBlood < 0)
                {
                    entityBlood = 0;
                }
                inventory[4] -= inventory[3];
                inventory[6] -= inventory[5];
                if (inventory[4] <= 0)
                {
                    inventory[3] = 0;
                }
                if (inventory[6] <= 0)
                {
                    inventory[5] = 0;
                }
                gameOutput.text += "You inflicted " + ((1 + inventory[1] * 5) + (inventory[2] * 10) + (inventory[3] * 1) + (inventory[5] * 5) + (inventory[7] * 5) - entityGrit) +
                    " damage onto it.\n";

                if (eventtype == 7)
                {
                    randomNumber = GenerateNumber(10, 21) - grit;
                    if (randomNumber < 0)
                    {
                        randomNumber = 0;
                    }
                    blood -= randomNumber;
                    gameOutput.text += "You lost " + randomNumber + " Blood.\n";
                }
                if (eventtype == 8)
                {
                    randomNumber = GenerateNumber(0, 2) - grit;
                    if (randomNumber < 0)
                    {
                        randomNumber = 0;
                    }
                    blood -= randomNumber;
                    gameOutput.text += "You lost " + randomNumber + " Blood.\n";
                }
                if (eventtype == 9)
                {
                    randomNumber = GenerateNumber(25, 76
) - grit;
                    if (randomNumber < 0)
                    {
                        randomNumber = 0;
                    }
                    blood -= randomNumber;
                    gameOutput.text += "You lost " + randomNumber + " Blood.\n";
                }

                if (entityBlood == 0)
                {
                    gameOutput.text += "You defeated the entity and you can now loot the corpse.\n";
                    eventtype = 4;
                    encounterpresent = false;
                }
            }
            if (blood < 0)
            {
                blood = 0;
                condition[4] = true;
            }
            randomNumber = GenerateNumber(1, 101);
            if (randomNumber == 1)
            {
                condition[0] = true;
                gameOutput.text += "You inflicted Bleeding.\n";
            }
            if (randomNumber == 2)
            {
                condition[1] = true;
                gameOutput.text += "You inflicted Bruises.\n";
            }
            if (randomNumber == 3)
            {
                condition[2] = true;
                gameOutput.text += "You inflicted Broken Bones.\n";
            }
            if (randomNumber == 4)
            {
                condition[20] = true;
                gameOutput.text += "You inflicted Blindness from battle.\n";
            }
            if (randomNumber == 5)
            {
                condition[22] = true;
                gameOutput.text += "You became Mute from injuries sustained in battle.\n";
            }
        }
        if ((command == "r" || command == "run") && ((eventtype >= 5 && eventtype <= 9) && !condition[4]))
        {
            if (eventtype == 6)
            {
                newWanderSweat += 25;
                sweat += 5;
            }
            if (eventtype >= 7 && eventtype <= 9)
            {
                entitySweat += 25;
                sweat += 5;
            }
            GenerateNewRoom();
            gameOutput.text += "You try to lose your foe.\n";
        }
        if (command == "auto" || command == "a")
        {
            if (autoplay)
            {
                autoplay = false;
            }
            else
            {
                autoplay = true;
            }
        }
        if (command == "end")
        {
            if (Score > highscore && !isCheating)
            {
                PlayerPrefs.SetInt("savedHighScore", Score);
                highscore = PlayerPrefs.GetInt("savedHighScore");
            }
            if (distance >= 600000000 && !isCheating)
            {
                gameOutput.text = "CONGRATULATIONS! YOU ESCAPED THE BACKROOMS!";
            }
            else
            {
                gameOutput.text = "";
            }
            gameOutput.text += "GAME OVER\n\n" +
                "Final Score: " + Score + "00\n" +
                "High Score: " + highscore + "00\n" +
                "Type 'new' to start a new game.";
        }
        if (command == "new")
        {
            NewGame();
        }
        if (command == "reset")
        {
            gameOutput.text += "High Score Reset";
            PlayerPrefs.SetInt("savedHighScore", 0);
        }
        #endregion
        #region Cheats
        if (command == "unstoppable")
        {
            isCheating = true;
            cheats[0] = true;
            gameOutput.text += "Infinite Blood Activated.\n";
        }
        if (command == "speedrun")
        {
            isCheating = true;
            cheats[1] = true;
            gameOutput.text += "No Sweat Activated.\n";
        }
        if (command == "thick head")
        {
            isCheating = true;
            cheats[2] = true;
            gameOutput.text += "No Tears Activated.\n";
        }
        if (command == "weightless")
        {
            isCheating = true;
            cheats[3] = true;
            gameOutput.text += "No Weight Activated.\n";
        }
        if (command == "cure all")
        {
            isCheating = true;
            cheats[4] = true;
            gameOutput.text += "No Conditions Activated.\n";
        }
        #endregion
    }

    //His Her Their
    public string ApplyPronouns(int egg)
    {
        if (egg == 1)
        {
            return "his";
        }
        else if (egg == 2)
        {
            return "her";
        }
        else if (egg == 3)
        {
            return "their";
        }
        return "ERROR: Pronouns not Found.";
    }
    //He She They
    public string ApplyOtherPronouns(int egg)
    {
        if (egg == 1)
        {
            return "he";
        }
        else if (egg == 2)
        {
            return "she";
        }
        else if (egg == 3)
        {
            return "they";
        }
        return "ERROR: Pronouns not Found.";
    }

    public int GenerateNumber(int min, int max)
    {
        //Min is Inclusive, Max is Exclusive
        return int.Parse(Random.Range(min, max).ToString());
    }

    public void Update()
    {
        if (autoplay)
        {
            timer--;
            if (timer <= 0)
            {
                if (eventtype >= 4)
                {
                    autoplay = false;
                }
                else
                {
                    PlayerCommand("w");
                }
                timer = 1;
            }
        }
    }
}
