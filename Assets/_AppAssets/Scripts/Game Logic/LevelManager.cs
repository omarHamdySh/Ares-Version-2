using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ClickStateMachine), typeof(RoomManger), typeof(CharacterManager))]
public class LevelManager : MonoBehaviour
{
    private static LevelManager _Instance;

    public GameEvent OnDragOn;
    public GameEvent OnDragOff;

    public GameObject Environment;
    public RoomManger roomManager;
    public CharacterManager characterManager;
    public ClickStateMachine controllersStateMachine;
    public TimlineController presentationManager;

    [SerializeField] private GameObject charPrefab;
    public Transform hippernationRoom;
    public bool Testing;
    public GameObject FPSGraphTools;
    #region Map Boundries
    public Transform TopBoundry;
    public Transform BottomBoundry;
    public Transform LeftBoundry;
    public Transform RightBoundry;
    public Transform FrontBoundry;
    public Transform BackBoundry;
    #endregion
    int charindex = 0;

    public static LevelManager Instance
    {
        get { return _Instance; }
    }

    private void Awake()
    {
        /** Order of methods calling is critical**/
        Init();
        calculateRoomsBounds();
        CreateCharForStaticRooms();

        if (_Instance == null)
        {
            _Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //asdfasdfasdfadsf
        //Debug.Log((roomsBounds[Environment.transform.GetChild(0).gameObject].size.x + roomsBounds[Environment.transform.GetChild(0).gameObject].size.y) / 2 - 2.5f);
        //
    }

    private void Init()
    {
        foreach (Transform roomTransform in Environment.transform)
        {
            roomManager.rooms.Add(new Room(roomTransform.gameObject));
        }
    }

    public void CreateCharForStaticRooms()
    {
        foreach (KeyValuePair<Room, Bounds> entry in roomManager.roomsBounds)
        {
            var id = entry.Key.roomGameObject.name;

            if (!PlayerPrefs.HasKey(id + " CharNum"))
            {
                PlayerPrefs.SetInt(id + " CharNum", 0);
            }

            int num = PlayerPrefs.GetInt(id + " CharNum");

            for (int i = 0; i < num; i++)
            {
                var roomEntity = entry.Key.roomGameObject.GetComponentInChildren<RoomEntity>();
                roomEntity.roomGameObject = entry.Key.roomGameObject;
                CreateChar(roomEntity);
            }
            entry.Key.roomGameObject.GetComponentInChildren<RoomEntity>().IsFirstTime = false;
        }
    }

    public void CreateNewChar()
    {
        createCharacter();
    }
    public void createCharacter()
    {
        // Declare character
        Character character;

        // Create Instantiation Pos for the new character before creation time
        Vector3 pos = new Vector3(hippernationRoom.position.x, hippernationRoom.position.y, charPrefab.transform.position.z);

        // Instantiate the Character.
        GameObject characterGameObject = Instantiate(charPrefab, pos, Quaternion.identity) as GameObject;

        // Add the physical character reference to the logical character
        characterManager.addNewCharacter(character = characterGameObject.GetComponent<CharacterEntity>().character);
    }

    public void CreateChar(RoomEntity roomEntity)
    {
        // Declare character
        Character character;

        // Create Instantiation Pos for the new character before creation time
        Vector3 pos = new Vector3(hippernationRoom.position.x, hippernationRoom.position.y, charPrefab.transform.position.z);

        // Instantiate the Character.
        GameObject characterGameObject = Instantiate(charPrefab, pos, Quaternion.identity) as GameObject;

        // Add the physical character reference to the logical character
        characterManager.addNewCharacter(character = characterGameObject.GetComponent<CharacterEntity>().character);

        // This for don't allow any char to open the training tutorial UI but not the Char generated from save data for training
        if (!roomEntity.transform.parent.name.Equals("TrainningRoom"))
        {
            characterGameObject.GetComponent<CharacterEntity>().isFristTime = false;
        }

        // Naming the character at the creation time
        characterGameObject.name = charindex.ToString();

        charindex++; // used in the naming process to increement the name value

        //print(characterGameObject.name + "  " + roomEntity.transform.parent.name);


        if (roomEntity.transform.parent.name.Equals("HibernationRoom"))
        {
            //If the contianer room of the character is hibernation room determine the entrance statically to the bottom entrance
            characterGameObject.GetComponent<CharacterEntity>().character.containerEntrance = roomEntity.leftEntrance;
        }

        //// Search for a free job in the intended room
        //if (roomManager.getRoomWithGameObject(roomEntity.roomGameObject).searchForFreeJob())
        //{// Get one of the free jobs
        //    roomManager.getRoomWithGameObject(roomEntity.roomGameObject).getRandomVacantJob(character);
        //}

        //The next line of code is critical just give it a deep look before changing it.
        // Double check that the contianerRoom reference inside the character instance is not equal to null;
        //character.container = character.job.jobRoom;


        moveCharacterManuallyToRoom(roomEntity, character, characterGameObject);

    }

    public void moveCharacterManuallyToRoom(RoomEntity roomEntity, Character character, GameObject characterGameObject)
    {
        character.container = roomEntity.roomGameObject;
        if (character.container != null)
        {
            Slot s = roomEntity.mySlot;
            characterGameObject.GetComponent<CharController>().GenerateFollowPathWayPoins(
                s.MySlotManger.transform.GetSiblingIndex(),
                s.MyDir,
                s.transform.GetSiblingIndex(),
                roomEntity
                );
            characterGameObject.GetComponent<CharController>().MoveInPath();
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var room in roomManager.roomsBounds.Values)
        {
            Gizmos.DrawWireCube(room.center, room.size);
        }
    }
    public void setDragModeOn()
    {
        OnDragOn.Raise();
    }
    public void setDragModeOff()
    {
        OnDragOff.Raise();
    }

    #region Deprecated 
    //public void calculateRoomsBounds()
    //{

    //    //    foreach (Transform room in Environment.transform.GetChildren())
    //    //    {
    //    //        var renderers = room.GetChild(0).GetChild(0).GetComponentsInChildren<MeshRenderer>();
    //    //        Bounds bounds = new Bounds(renderers[0].bounds.center, new Vector3(0, 0, 0));
    //    //        foreach (var renderer in renderers)
    //    //        {
    //    //            if (renderer != renderers[0])
    //    //                bounds.Encapsulate(renderer.bounds);
    //    //        }
    //    //        roomsBounds.Add(room.gameObject, bounds);
    //    //    }
    //    //}
    //}
    #endregion 

    public void calculateRoomsBounds()
    {//sadfasdfadsf
        foreach (Room room in roomManager.rooms)
        {
            var renderer = room.roomGameObject.GetComponentInChildren<Renderer>();
            //Bounds bounds = new Bounds(renderers[0].bounds.center, new Vector3(0, 0, 0));
            //foreach (var renderer in renderers)
            //{
            //    if (renderer != renderers[0])
            //        bounds.Encapsulate(renderer.bounds);
            //}
            if (!roomManager.rooms.Contains(room))
            {
                roomManager.rooms.Add(room);
            }
            roomManager.roomsBounds.Add(room, renderer.bounds);
        }
    }

    public void CalculateThisRoomBounds(Room room)
    {//asdfsadfadsf
     //var renderers = room.roomGameObject.transform.GetChild(0)
     //.GetChild(1).gameObject.GetComponentsInChildren<MeshRenderer>();

        //Bounds bounds = new Bounds(renderers[0].bounds.center, new Vector3(0, 0, 0));
        //foreach (var renderer in renderers)
        //{
        //    if (renderer != renderers[0])
        //        bounds.Encapsulate(renderer.bounds);
        //}
        var renderer = room.roomGameObject.GetComponentInChildren<Renderer>();
        if (!roomManager.rooms.Contains(room))
        {
            roomManager.rooms.Add(room);
        }
        roomManager.roomsBounds.Add(room, renderer.bounds);
    }

    public GameObject getCharacterPhysicalContainer(GameObject charcterGameObject)
    {

        foreach (KeyValuePair<Room, Bounds> entry in roomManager.roomsBounds)
        {
            if (entry.Value.Contains(charcterGameObject.transform.position))
            {
                return entry.Key.roomGameObject;
            }
        }
        return null; //this is impossible to happen. if it happens it would be a deal breaker glitch
    }
    public void OnSecondChange()
    {//Called each real second
        this.characterManager.OnSecondChange();
        this.roomManager.OnSecondChange();
    }
    public void OnMinuteChange()
    {//Called each real Minute
        this.characterManager.OnMinuteChange();
        this.roomManager.OnMinuteChange();
    }
    public void OnGameHourChange()
    {//Called each Game Hour
        this.characterManager.OnGameHourChange();
        this.roomManager.OnGameHourChange();
    }
    public void OnGameDayChange()
    {// Called each Game Day
        this.characterManager.OnGameDayChange();
        this.roomManager.OnGameDayChange();
    }
    public void toggleTesting()
    {
        Testing = !Testing;
        FPSGraphTools.SetActive(Testing);
    }
}
