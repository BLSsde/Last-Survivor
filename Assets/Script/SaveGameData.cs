using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveGameData : MonoBehaviour
{
    public static SaveGameData instance { get; private set; }

    //What we want to save
    public int currentAvatar;
    public int currentMap;
    public int coins;
    public int finishes;
    public int currentLevel = 1;

    public bool[] avatarUnlocked = new bool[5] { true, false, false, false,false};

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            
            currentAvatar = data.currentAvatar;
            currentMap = data.currentMap;
            
            coins = data.coins;
            finishes = data.finishes;
            currentLevel = data.currentLevel;
            avatarUnlocked = data.avatarUnlocked;

            if(avatarUnlocked == null)
            {
                avatarUnlocked = new bool[5] { true, false, false, false, false};
            }

            if(currentLevel == 0)
            {
                currentLevel = 1;
            }

            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

   
        data.currentAvatar = currentAvatar;
        data.currentMap = currentMap;
     
        data.coins = coins;
        data.finishes = finishes;
        data.currentLevel = currentLevel;
        data.avatarUnlocked = avatarUnlocked;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
class PlayerData_Storage
{
    public int currentAvatar;
    public int currentMap;
    public int coins;
    public int finishes;
    public int currentLevel;
    public bool[] avatarUnlocked;
}