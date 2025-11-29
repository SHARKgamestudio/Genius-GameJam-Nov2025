using NUnit.Framework;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public sealed class ExplorationManager : MonoBehaviour
{
    private static ExplorationManager _instance;

    [SerializeField] int roomAmount;
    // Player* player;
    GameObject currentRoom;
    GameObject leftRoom;
    GameObject rightRoom;
    Array roomTypeWeight;
    Dictionary<int, RoomType> roomTypeDictionary;
    int lastRoomNumber = 0;
    const int addedWeight = 10;
    [SerializeField] GameObject combatPrefab;
    [SerializeField] GameObject pactPrefab;

    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;

    [SerializeField] GameObject fadeLayer;

    [SerializeField] float luckyModifier;
    [SerializeField] float unluckyModifier;

    public GameObject GetRoom()
    {
        return currentRoom;
    }

    public int GetRoomNumber()
    { 
        return lastRoomNumber - roomAmount;
    }

    public static ExplorationManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ExplorationManager();
        }
        return _instance;
    }

    void IncreaseWeight(RoomType _ignoreType)
    {
        int type = 0;
        foreach(int weight in roomTypeWeight)
        {
            if (roomTypeDictionary[type] == _ignoreType)
            {
                type++;
                continue;
            }

            roomTypeWeight.SetValue(weight + addedWeight, type);
            type++;
        }
    }

    void GenerateRoom()
    {
        GameObject room;

        int luckyRoom = UnityEngine.Random.Range(0, 2);

        for (int i = 0; i < 2; i++)
        {
            // Pull from random room type
            int totalWeight = 0;
            foreach (int weight in roomTypeWeight)
            {
                totalWeight += weight;
            }
            float rand = UnityEngine.Random.value;
            int currentWeight = 0;
            int roomType = 0;
            RoomType type = RoomType._COUNT;
            foreach (int weight in roomTypeWeight)
            {
                currentWeight += weight;
                if (rand < (float)currentWeight / (float)totalWeight)
                {
                    type = roomTypeDictionary[roomType];
                    break;
                }
                roomType++;
            }

            switch (type)
            {
                case RoomType.COMBAT:
                    room = Instantiate(combatPrefab);
                    room.transform.position = new UnityEngine.Vector3(0, 0, 5);
                    room.AddComponent<CombatRoom>().roomNumber = lastRoomNumber;
                    break;

                case RoomType.PACT:
                    room = Instantiate(pactPrefab);
                    room.transform.position = new UnityEngine.Vector3(0, 0, 5);
                    room.AddComponent<PactRoom>();
                    break;

                case RoomType._COUNT:
                default:
                    throw new Exception("Room type generated improperly.");
                    break;
            }

            IncreaseWeight(type);

            if(i == luckyRoom)
            {
                room.GetComponent<Room>().luckModifier = luckyModifier;
            }
            else
            {
                room.GetComponent<Room>().luckModifier = unluckyModifier;
            }

            switch (i)
            {
                case 0:
                    leftRoom = room;
                    break;
                case 1:
                    rightRoom = room;
                    break;
            }
        }

        lastRoomNumber++;
    }

    void ExitRoom()
    {
        currentRoom.GetComponent<Room>().OnExit();

        // Show room selection

    }
    void EnterRoom()
    {
        currentRoom.GetComponent<Room>().OnEnter();
        currentRoom.transform.position = UnityEngine.Vector3.zero;
        switch(currentRoom.GetComponent<Room>().GetRoomType())
        {
            case RoomType.COMBAT:
                // combat manager
                break;
            case RoomType.PACT:
                // PATC MANAGER?????
                break;
        }
    }

    public void GoNextRoom()
    {
        ExitRoom();

        // Layer fade in
        fadeLayer.GetComponent<FadeLayer>().SetTarget(new Color(0, 0, 0, 1));

        // Show room UI
        leftButton.GetComponent<Button>().interactable = true;
        rightButton.GetComponent<Button>().interactable = true;
        leftButton.GetComponent<FadeImage>().ResetTarget();
        rightButton.GetComponent<FadeImage>().ResetTarget();

        GenerateRoom();

        // Switch game state to selection

    }

    public void ChooseFirstRoom()
    {
        NextRoom(true);
    }
    public void ChooseSecondRoom()
    {
        NextRoom(false);
    }

    public void NextRoom(bool _left)
    {
        // Layer fade out
        fadeLayer.GetComponent<FadeLayer>().SetTarget(new Color(0, 0, 0, 0));
        // Hide room UI
        leftButton.GetComponent<FadeImage>().SetTarget(new Color(1, 1, 1, 0));
        rightButton.GetComponent<FadeImage>().SetTarget(new Color(1, 1, 1, 0));
        leftButton.GetComponent<Button>().interactable = false;
        rightButton.GetComponent<Button>().interactable = false;

        if(_left)
        {
            currentRoom = leftRoom;
            Destroy(rightRoom);
        }
        else
        {
            currentRoom = rightRoom;
            Destroy(leftRoom);
        }

        currentRoom.GetComponent<Room>().OnEnter();
        currentRoom.transform.position = UnityEngine.Vector3.zero;
        switch (currentRoom.GetComponent<Room>().GetRoomType())
        {
            case RoomType.COMBAT:
                // combat manager
                break;
            case RoomType.PACT:
                // PATC MANAGER?????

                break;
        }

        // Fade in
        EnterRoom();
    }

    void Start()
    {
        Button lButton = leftButton.GetComponent<Button>();
        lButton.onClick.AddListener(ChooseFirstRoom);
        Button rButton = rightButton.GetComponent<Button>();
        rButton.onClick.AddListener(ChooseSecondRoom);

        // Hide room UI
        leftButton.GetComponent<FadeImage>().SetColor(new Color(0, 0, 0, 0));
        rightButton.GetComponent<FadeImage>().SetColor(new Color(0, 0, 0, 0));
        lButton.interactable = false;
        rButton.interactable = false;

        fadeLayer.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

        /// Room generation ///
        roomTypeDictionary = new Dictionary<int, RoomType>();

        // Done by hand... mb future self
        roomTypeDictionary[0] = RoomType.COMBAT;
        roomTypeDictionary[1] = RoomType.PACT;

        roomTypeWeight = Array.CreateInstance(typeof(int), (int)RoomType._COUNT);
        for (int i = 0; i < (int)RoomType._COUNT; i++)
        {
            roomTypeWeight.SetValue(1, i);
        }

        // First room is pre-generated as a Pact room
        currentRoom = Instantiate(pactPrefab);
        currentRoom.transform.position = new UnityEngine.Vector3(0, 0, 5);
        currentRoom.AddComponent<PactRoom>();
        IncreaseWeight(RoomType.PACT);

        // Init room
        currentRoom.GetComponent<Room>().Initialize();

        // Add to next rooms
        lastRoomNumber++;
        ///////////////////////

        EnterRoom();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            GoNextRoom();
        }
    }

}
