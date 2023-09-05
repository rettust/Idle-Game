using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("file storage config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    [Header("debug")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;


    [Header("autosave config")]
    [SerializeField] private float autoSaveTimeSeconds = 60f;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;
    private Coroutine autoSaveCoroutine;
    public int autoSaveFrequency = 5;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        if (disableDataPersistence)
        {
            Debug.LogWarning("data persistence disabled (change bool on DPM if this isnt intended)");
        }
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
        // start up the auto saving coroutine
        // TODO: for some reason when the autosave autosaves it resets all GameData back to its NewGame() state. wtf?
        // if (autoSaveCoroutine != null)
        // {
        //     StopCoroutine(autoSaveCoroutine);
        // }
        // autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void NewGame()
    {
        fileDataHandler.Delete();
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }
        this.gameData = fileDataHandler.Load();

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (this.gameData == null)
        {
            Debug.Log("no data found; start a new game");
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }

    public void SaveGame()
    {
        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }

        // if we don't have any data to save, log a warning here
        if (this.gameData == null)
        {
            Debug.LogWarning("no save data found - start a new game first");
            return;
        }


        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }


        // save data to file
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
        }
    }
}