using NUnit.Framework;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public sealed class ExplorationManager : MonoBehaviour
{
    GameObject currentRoom;
    GameObject leftRoom;
    GameObject rightRoom;
    Array roomTypeWeight;
    Dictionary<int, RoomType> roomTypeDictionary;
    public int lastRoomNumber{ get; private set; } = 0;
    const int addedWeight = 1;

    private PlayerMover playerMover;
    
    [SerializeField] GameObject combatPrefab;
    [SerializeField] GameObject pactPrefab;

    [SerializeField] AlphaTweening crossfade;

    [SerializeField] float luckyModifier;
    [SerializeField] float unluckyModifier;

    [SerializeField] Sprite spritePactRoom3;
    [SerializeField] Sprite spritePactRoom2;
    [SerializeField] Sprite spritePactRoom1;

    [SerializeField] Sprite spriteCombatRoom3;
    [SerializeField] Sprite spriteCombatRoom2;
    [SerializeField] Sprite spriteCombatRoom1;

    public GameObject GetRoom()
    {
        return currentRoom;
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

        int minimumWeight = int.MaxValue;

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
            IncreaseWeight(type);

            int index = 0;
            foreach (int weight in roomTypeWeight) 
            {
                minimumWeight = Math.Min(weight, minimumWeight);
            }

            foreach (int weight in roomTypeWeight) // Reset weight
            {
                roomTypeWeight.SetValue(weight - minimumWeight + 1, index);
                index++;
            }

            switch (type)
            {
                case RoomType.COMBAT:
                    room = Instantiate(combatPrefab);
                    room.GetComponent<CombatRoom>().roomNumber = lastRoomNumber;
                    if (lastRoomNumber == 1)
                    {
                        this.GetComponent<CombatRoom>().GetComponent<SpriteRenderer>().sprite = spriteCombatRoom2;
                    }
                    else if (lastRoomNumber == 2)
                    {
                        this.GetComponent<CombatRoom>().GetComponent<SpriteRenderer>().sprite = spriteCombatRoom3;
                    }
                    break;

                case RoomType.PACT:
                    room = Instantiate(pactPrefab);
                    room.GetComponent<PactRoom>().roomNumber = lastRoomNumber;
                    if (lastRoomNumber == 1)
                    {
                        this.GetComponent<PactRoom>().GetComponent<SpriteRenderer>().sprite = spritePactRoom2;
                    }
                    else if (lastRoomNumber == 2)
                    {
                        this.GetComponent<PactRoom>().GetComponent<SpriteRenderer>().sprite = spritePactRoom3;
                    }
                    break;
                
                default:
                    throw new Exception("Room type generated improperly.");
            }


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
        currentRoom.transform.position = new UnityEngine.Vector3(0, 0, 0);
        currentRoom.GetComponent<Room>().OnEnter();
        playerMover.SetPosition(currentRoom.GetComponent<Room>().enterPos);
        playerMover.SetTarget(currentRoom.GetComponent<Room>().stayPos, MoveState.STAY);
    }

    public void WalkTowardsNextDoor()
    {
        playerMover.SetTarget(currentRoom.GetComponent<Room>().exitPos, MoveState.EXIT);
    }

    public void RoomFade()
    {
        crossfade.gameObject.SetActive(true);
        crossfade.In();
        
        crossfade.OnInEnd += () =>
        {
            ExitRoom();
            GenerateRoom();
            crossfade.OnInEnd = null;
        };

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
        crossfade.Out();
        GameManager.Instance.playerManager.GetSystem<PlayerStats>(out PlayerStats stats);
        float percent = stats.maxLife * 0.2f;
        stats.Heal(percent);

        crossfade.OnOutEnd += () =>
        {
            crossfade.gameObject.SetActive(false);
            crossfade.OnOutEnd = null;
        };

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
        
        EnterRoom();
    }

    void Start()
    {
        GameManager.Instance.playerManager.GetSystem<PlayerMover>(out playerMover);

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
        playerMover.SetPosition(currentRoom.GetComponent<Room>().enterPos);
        playerMover.OnExitMovementEnd += RoomFade;
        
        currentRoom.GetComponent<Room>().Initialize();

        // Add to next rooms
        lastRoomNumber++;
        ///////////////////////

        EnterRoom();
    }
}
