using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BRTG_ClassLibrary;

public class ManageGamev2 : MonoBehaviour
{
    #region Unity Variables
    public GameObject StartScreenObjects;
    public GameObject MainGameObjects;
    public GameObject MapObjects;
    public InputField playerInput;
    public Text gameOutput;
    string basestring;
    int score = 0;
    int highscore = 100;
    decimal distance = 0;
    ulong timeHr;
    byte timeMin;
    byte timeSec;
    bool newgame;

    Player player;
    TheBackrooms backrooms;
    Wanderer curWanderer;
    Entity curEntity;
    public GameObject[] minimap = new GameObject[64];
    public GameObject mapTile;
    public GameObject Map;
    public List<GameObject> MapTiles = new List<GameObject>();
    bool mapOpen = false;
    int timer = 5;
    long[] curCord = new long[2];
    long highX = 1;
    long highY = 1;
    long lowX = -1;
    long lowY = -1;

    string[] randomFlavourText = new string[15]
{
        "You thought you heard something, but it was probably nothing.",
        "Wait... have you been here before?",
        "The moistness of the carpet is starting to get to you.",
        "The lights flickered for a moment, but nothing else happened.",
        "You see a door, but when you blinked it vanished.",
        "Insane scribblings are found across the walls.",
        "You miss home.",
        "The mold is starting to look tasty.",
        "You see a flight of stairs, but when you look up them, you see yourself looking back.",
        "What's that smell?",
        "How long has it been?",
        "You found a computer, but it doesn't work.",
        "It was beyond recognition.",
        "Lorem Ipsum",
        " Infinite office\n Roaming for eternity\n Who can say you won't?"
};
    #endregion
    void Start()
    {
        Application.targetFrameRate = 60;
        highscore = PlayerPrefs.GetInt("savedHighScore");
    }
    public void StartGame()
    {
        score = 0;
        StartScreenObjects.SetActive(false);
        MapObjects.SetActive(false);
        MainGameObjects.SetActive(true);
        newgame = true;
        distance = 0;
        timeHr = 0;
        timeMin = 0;
        timeSec = 0;
        backrooms = new TheBackrooms();
        player = new Player();
        curWanderer = null;
        curEntity = null;
        curCord[0] = 0; curCord[1] = 0;
        PlayerMove();
    }

    void Update()
    {
        if (!StartScreenObjects.active)
        {
            timer--;
        }
        if (timer < 0)
        {
            if (player.Blood > 0)
            {
                if (Input.GetKey("up"))
                {
                    curCord[1]++;
                    timer = 30;
                    PlayerMove();
                }
                if (Input.GetKey("left"))
                {
                    curCord[0]--;
                    timer = 30;
                    PlayerMove();
                }
                if (Input.GetKey("down"))
                {
                    curCord[1]--;
                    timer = 30;
                    PlayerMove();
                }
                if (Input.GetKey("right"))
                {
                    curCord[0]++;
                    timer = 30;
                    PlayerMove();
                }
            }
            if (Input.GetKey(KeyCode.LeftControl) && !mapOpen)
            {
                MapObjects.SetActive(true);
                LoadMaxiMap();
                timer = 30;
                mapOpen = true;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && mapOpen)
            {
                MapObjects.SetActive(false);
                timer = 30;
                mapOpen = false;
            }
        }
    }
    void PlayerMove()
    {
        for (uint i = 0; i < RandomGen.Next(); i++)
        {
            RandomGen.Next();
        }
        player.UpdateHuman();
        foreach (Human human in player.playerFriends)
        {
            human.UpdateHuman();
        }
        if (curWanderer != null)
        {
            curWanderer = null;
        }
        if (curEntity != null)
        {
            player.Blood--;
            player.Sweat++;
            curEntity.Sweat += 5;
            if (curEntity.Sweat >= 100)
            {
                curEntity = null;
            }
        }
        WriteDesc();
        if (curCord[0] > highX)
        {
            highX = curCord[0];
        }
        if (curCord[0] < lowX)
        {
            lowX = curCord[0];
        }
        if (curCord[1] > highY)
        {
            highY = curCord[1];
        }
        if (curCord[1] < lowY)
        {
            lowY = curCord[1];
        }
        backrooms.LoadRoom(curCord[0], curCord[1]);
        ApplyEvent(backrooms.currentE);
        foreach (Room room in backrooms.roomsList)
        {
            room.ResetEvent();
        }
        LoadMiniMap();
        CalcDist();
        timeSec += (byte)RandomGen.Next(5, 30);
        CalcTime();
        CalcScore();
        if (mapOpen)
        {
            LoadMaxiMap();
        }
    }
    public void PlayerCommand()
    {
        string command = playerInput.text;
        if (player.Blood <= 0)
        {
            if (score > highscore)
            {
                PlayerPrefs.SetInt("savedHighScore", score);
                highscore = PlayerPrefs.GetInt("savedHighScore");
            }
            gameOutput.text += "GAME OVER\n\n" +
    "Final Score: " + (score*10) + "\n" +
    "High Score: " + (highscore*10) + "\n" +
    "Type 'new' to start a new game.";
        }
        #region Manual
        if (command == "h" || command == "help")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
            "Commands:\n" +
            "help / h - Shows you this text.\n" +
            "back / b - Sends you back to the room information.\n" +
            "walk / w - Wander into another room (Can also use arrow keys).\n" +
            "stats / s - See your stats, such as blood, sweat, and tears.\n" +
            "inventory / i - See what items you have available to you.\n" +
            "use / u (Item Name) - Use an item in your inventory.\n" +
            "talk / t then a number - Talk to friendly wanderers or to recruits you've already met. (Use names for talking to recruits)\n" +
            "fight / f - Use your weapons to do what you must.\n" +
            "Press CONTROL to show/hide the map\n" +
            "\nFor more information, type 'Page 2' about Player Statistics.";
        }
        if (command == "page 2")
        {
            gameOutput.text = "Welcome to the Help Section.\n" +
                "Player Statistics:\n" +
                "Blood - Represents your health and wellbeing. Once this reaches 0, you are Dead.\n" +
                "Sweat - Represents your stamina and vigour. Once this reaches 100, you will start to take exhaustion damage.\n" +
                "Tears - Represents your sanity and mental fortitude. Once this reaches 100, you will be insane and start to take damage.\n" +
                "Power - Represents your strength and fighting capability.\n" +
                "Grit - Represents your defence and protection. Keep this as high as possible by wearing clothes.\n" +
                "Sight - Represents how well you can see. Too dark means temporary Blindness while Too bright means permenant Blindness.\n" +
                "Sound - Represents how loud your surroundings are. Being too loud will make you deaf.\n" +
                "Weight - Represents how much you're carrying. If you hold too much without a Backpack, you will Sweat more.\n" +
                "Hunger - You'll need food to survive or you'll starve.\n" +
                "Thirst - Water and other liquids are also needed to live.\n" +
                "There are also multiple conditions to be afflicted with.\n\n" +
                "Final Goal: Traverse the entire 600'000'000 miles of the Backrooms.\n" +
                "\nTo return to the game, type back.";
        }
        #endregion
        #region Commands
        if (command == "b" || command == "back")
        {
            gameOutput.text = basestring;
        }
        if (player.Blood > 0)
        {
            if (command == "w" || command == "walk")
            {
                curCord[1]++;
                timer = 30;
                PlayerMove();
            }
            string itemchoice;
            if (command.Length > 4)
            {
                if (command.Substring(0, 2) == "u ")
                {
                    itemchoice = command.Substring(2);
                    foreach (Item item in player.inventory)
                    {
                        if (item.Name == itemchoice)
                        {
                            item.UseItem(player);
                            player.inventory.Remove(item);
                            break;
                        }
                    }
                }
                if (command.Substring(0, 4) == "use ")
                {
                    itemchoice = command.Substring(4);
                    foreach (Item item in player.inventory)
                    {
                        if (item.Name == itemchoice)
                        {
                            item.UseItem(player);
                            player.inventory.Remove(item);
                            break;
                        }
                    }
                }
            }
            if ((command == "t" || command == "talk") && !player.humanConditions.Mute)
            {
                if (curWanderer != null)
                {
                    gameOutput.text += "You talked to " + curWanderer.FirstName + ".\n";
                    curWanderer.Favor += (sbyte)RandomGen.Next(5, 10);
                    player.Tears = (int)RandomGen.Next(0, 5);
                    if (curWanderer.Favor >= 75)
                    {
                        gameOutput.text += curWanderer.FirstName + " has decided to join you.\n";
                        player.playerFriends.Add(curWanderer);
                        curWanderer = null;
                    }
                }
                else if (curWanderer == null && player.playerFriends.Count > 0)
                {
                    gameOutput.text += "You talked to " + player.playerFriends[(int)RandomGen.Next(0, (uint)(player.playerFriends.Count - 1))];
                    player.Tears = (int)RandomGen.Next(0, 5);
                    foreach (Human human in player.playerFriends)
                    {
                        human.Tears = (int)RandomGen.Next(0, 5);
                    }
                }
            }
            if (command == "f" || command== "fight")
            {
                int damage;
                if (curWanderer != null)
                {
                    damage = player.Power - curWanderer.Grit;
                    if (damage <= 0)
                    {
                        damage = 1;
                    }
                    curWanderer.Favor = -100;
                    curWanderer.Blood -= damage;
                    gameOutput.text += curWanderer.FirstName + " lost " + damage + " Blood.\n";

                    damage = curWanderer.Power - player.Grit;
                    if (damage <= 0)
                    {
                        damage = 1;
                    }
                    player.Blood -= damage;
                    gameOutput.text += "You lost " + damage + " Blood.\n";

                    if (curWanderer.Blood <= 0)
                    {
                        gameOutput.text += "You defeated " + curWanderer.FirstName + ".\n";
                        curWanderer = null;
                    }
                }
                if (curEntity != null)
                {
                    damage = player.Power - curEntity.Grit;
                    if (damage <= 0)
                    {
                        damage = 1;
                    }
                    curEntity.Blood -= damage;
                    gameOutput.text += curEntity.Name + " lost " + damage + " Blood.\n";

                    damage = curEntity.Power - player.Grit;
                    if (damage <= 0)
                    {
                        damage = 1;
                    }
                    player.Blood -= damage;
                    gameOutput.text += "You lost " + damage + " Blood.\n";

                    if (curEntity.Blood <= 0)
                    {
                        gameOutput.text += "You defeated " + curEntity.Name + ".\n";
                        curEntity = null;
                    }
                }
            }
        }
        if (command == "s" || command == "stats")
        {
            gameOutput.text = "Stats:\n\n" +
                "Blood - " + player.Blood + "\n" +
                "Sweat - " + player.Sweat + "\n" +
                "Tears - " + player.Tears + "\n" +
                "Power - " + player.Power + "\n" +
                "Grit - " + player.Grit + "\n" +
                "Hunger - " + player.Hunger + "\n" +
                "Thirst - " + player.Thirst + "\n" + 
                "Sight - " + player.Sight + "\n" +
                "Sound - " + player.Sound + "\n" +
                "Weight - " + player.CurWeight + "\n" +
                "Conditions -\n";
            gameOutput.text += (player.humanConditions.Bleeding) ? " Bleeding\n" : "";
            gameOutput.text += (player.humanConditions.Bruised) ? " Bruised\n" : "";
            gameOutput.text += (player.humanConditions.BrokenBones) ? " Broken Bones\n" : "";
            gameOutput.text += (player.humanConditions.Unconscious) ? " Unconscious\n" : "";
            gameOutput.text += (player.humanConditions.Happy) ? " Happy\n" : "";
            gameOutput.text += (player.humanConditions.Sad) ? " Sad\n" : "";
            gameOutput.text += (player.humanConditions.Angry) ? " Angry\n" : "";
            gameOutput.text += (player.humanConditions.Scared) ? " Scared\n" : "";
            gameOutput.text += (player.humanConditions.Blind) ? " Blind\n" : "";
            gameOutput.text += (player.humanConditions.Deaf) ? " Deaf\n" : "";
            gameOutput.text += (player.humanConditions.Mute) ? " Mute\n" : "";
            gameOutput.text += (player.humanConditions.Poisoned) ? " Poisoned\n" : "";
            gameOutput.text += (player.humanConditions.Embarrased) ? " Embarrased\n" : "";
            gameOutput.text += (player.humanConditions.Addicted) ? " Addicted\n" : "";
            gameOutput.text += (player.humanConditions.Encumbered) ? " Encumbered\n" : "";
        }
        if (command == "i" || command == "inventory")
        {
            gameOutput.text = "Inventory:\n\n";
            foreach (Item item in player.inventory)
            {
                gameOutput.text += item.Name + "\n";
            }
        }
        if (command == "new")
        {
            StartGame();
        }
        if (command == "reset")
        {
            gameOutput.text += "High Score Reset";
            PlayerPrefs.SetInt("savedHighScore", 0);
        }
        #endregion
    }
    void LoadMiniMap()
    {
        int count = 0;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (backrooms.vRoomData[x, y] == true)
                {
                    minimap[count].SetActive(true);
                }
                else
                {
                    minimap[count].SetActive(false);
                }
                count++;
            }
        }
    }
    void LoadMaxiMap()
    {
        foreach (GameObject obj in MapTiles)
        {
            Destroy(obj);
        }
        MapTiles.Clear();
        float xScale = 5/(float)(highX - lowX);
        float yScale = 5/(float)(highY - lowY);
        float zScale = (xScale <= yScale) ? xScale : yScale;
        Map.transform.localPosition = new Vector3(566.5f, -350f, 0f);
        Map.transform.localScale = new Vector3(zScale, zScale, 1f);
        foreach (Room room in backrooms.roomsList)
        {
            GameObject newTile = Instantiate(mapTile, Map.transform);
            newTile.transform.localPosition = new Vector3(room.Coordinates[0] * 105, room.Coordinates[1] * 105, 0f);
            MapTiles.Add(newTile);
        }
    }
    void WriteDesc()
    {
        //Output Time
        gameOutput.text = "[";
        bool gotWatch = false;
        foreach (Item item in player.inventory)
        {
            if (item.Name == "Watch")
            {
                gotWatch = true;
            }
        }
        if (gotWatch)
        {
            gameOutput.text += timeHr + ":" + timeMin + ":" + timeSec + "]\n";
        }
        else
        {
            gameOutput.text += "??:??:??]\n";
        }
        //Output Distance
        gameOutput.text += "[" + distance + "] miles travelled\n";
        gameOutput.text += "[B: " + player.Blood + " | S: " + player.Sweat + " | T: " + player.Tears + "]\n";
        //Flavour Text
        if (newgame)
        {
            gameOutput.text += "You have entered the Backrooms and are doomed to wander these halls for the rest of your days. " +
                "Might as well try to survive as long as you can.\nType 'help' or 'h' to see what you can do.\n";
            newgame = false;
        }
        if (RandomGen.Next() == 1)
        {
            gameOutput.text += randomFlavourText[RandomGen.Next(1, 15)-1] + "\n";
        }
        //Ending
        if (distance >= 600000000)
        {
            gameOutput.text += "You've done it. You've reached the end of the Backrooms. The door ahead should lead you back to reality.\n" +
                "The future ahead is unclear but you pass through into the unknown.\n\n" +
                "THE END\n\n" +
                "Type 'end' to finish the game.";
        }
        basestring = gameOutput.text;
    }
    void CalcDist()
    {
        distance = 0;
        foreach(Room room in backrooms.roomsList)
        {
            decimal roomSpace = 0;
            string temp = Convert.ToString((long)room.RoomData, 2).PadLeft(64, '0');
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == '1')
                {
                    roomSpace++;
                }
            }
            distance += roomSpace * 0.001m;
        }
    }
    void CalcTime()
    {
        while (timeSec >= 60)
        {
            timeMin++;
            timeSec -= 60;
        }
        while (timeMin >= 60)
        {
            timeHr++;
            timeMin -= 60;
        }
    }
    void CalcScore()
    {
        score = Mathf.FloorToInt(((int)distance + (101 - player.Tears) +
(101 - player.Sweat) + (10 * player.playerFriends.Count) + ((int)timeHr)));
    }
    void ApplyEvent(byte eventID)
    {
        switch (eventID)
        {
            case 1: //Blackout
                player.Sight = 0;
                foreach (Human human in player.playerFriends)
                {
                    human.Sight = 0;
                }
                gameOutput.text += "There was a blackout and you cannot see.\n";
                break;
            case 2: //Deafening Sound
                player.Sound = 175;
                foreach (Human human in player.playerFriends)
                {
                    human.Sound = 175;
                }
                gameOutput.text += "The lights buzz increases to a near-deafening level.\n";
                break;
            case 3: //Time Skip
                timeHr += RandomGen.Next(100, 1000);
                gameOutput.text += "Everything just felt older.\n";
                break;
            case 4: //Spacial Displacement
                curCord[0] += RandomGen.Next(5, 100);
                curCord[0] -= RandomGen.Next(5, 100);
                curCord[1] += RandomGen.Next(5, 100);
                curCord[1] -= RandomGen.Next(5, 100);
                gameOutput.text += "Nothing looks familiar anymore.\n";
                break;
            case 5: //Free Item
                player.AddNewItem();
                gameOutput.text += "You found a " + player.inventory[player.inventory.Count-1].Name + ".\n";
                break;
            case 6: //Corpse
                gameOutput.text += "You found a corpse on the floor.\n";
                for (int i = 0; i < RandomGen.Next(5, 10); i++)
                {
                    player.AddNewItem();
                    gameOutput.text += "You found a " + player.inventory[player.inventory.Count - 1].Name + ".\n";
                }
                break;
            case 7: //Human Encounter
                if (curWanderer == null & curEntity == null)
                {
                    curWanderer = new Wanderer();
                    gameOutput.text += "You found another human.\n";
                }
                break;
            case 8: //Entity Encounter
                if (curWanderer == null & curEntity == null)
                {
                    curEntity = new Entity();
                    gameOutput.text += "You encountered " + curEntity.Name + ".\n";
                }
                break;
        }
    }
}
