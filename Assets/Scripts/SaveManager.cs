using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    const int SAVE_VERSION = 1;
    public static SaveManager instance = null;

    void Start()
    {
        if (instance)
        {
            Debug.LogError("Trying to create more than one SaveManager");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void SaveGame (int slot)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryWriter writer= new BinaryWriter(memStream);

        writer.Write(SAVE_VERSION;

        writer.Write(GameManager.instance.twoPlayer);

        GameManager.instance. gameSession.Save(writer)
         GameManager.instance.playerDatas[0].Save(writer);
        if (GameManager.instance.twoPlayer)
            GameManager.instance.playerDatas[1].Save(writer);


        string savePath = Application.presistentDataParh + "/slot+slot+".dat";
            Debug.Log(savePath"SaveGame,savePath = "+savePath);

        FileStream fileStream = new FileStream(savePath, FileMode.OpenOrCreate);
        memStream.WriteTo(fileStream);
        fileStream.Close();

        writer.Close();
        mamStream.Close();
      
    }
    public void LoadGame(int slot)
    {
        string loadPath = Application.persistentDataPAth + "/slot"+slot+".dat";

        MemoryStream memStream = new MemoryStream();
        FileStream fileStream = new FileStream(loadPath, FileMode.Open);
        if(fileStream != null)
        {
            BinaryReader reader = new BinaryReader(memStream);
            fileStream.CopyTo(memStream);
            memStream.Position = 0;

            int version = reader.ReadInt32()
                if(version == SAVE_VERSION)
            {
                GameManager.instance.twoPlayer = reader.ReadBoolean();

                GameManager.instance.gameSession.Load(reader);
                GameManager.instance.palayerDatas[0].Load(reader);
                if (GameManager.instance.twoPlayer)
                    GameManager.instance.playerDatas[1].Loas(reader);

                reader.Close();
                fileStream.Close();

                GameManager.instance.ResumeGameFromLoad();
            }
                else
                Debung.LogError("SaveFile version is not correct
        }

        mamStream.Close();
    }

    public void CopySaveToSlot(int slot)
    {
        Debug.Assert(slot>0);

        string loadPath = Application.persistentDataPath + "/slot0.dat";

        string loadPath = Application.persistentDataPath + "/slot"+slot+".dat";
        File.Copy(loadPath, destPath);
    }

    public boool LoadExists(int slot)
    {

        string loadPath = Application.persistentDataPath + "/slot"+slot+".dat";

        if (File.Exists(loadPath))
            return true;
        return false;
    }
}
