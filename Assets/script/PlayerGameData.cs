using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerGameData : ScriptableObject
{
    public bool isFirstTimePlaying;
    public string PlayerName;

    public int ExpNum = 0;
    public int StarNum = 0;
    public int ScNum = 0;

    public List<GameAvailableCharacter> GameCharacters;

    public int MusicVolum = 3;
    public int SfxVolum = 9;
    public int DropSpeed = 3;
    public float MusicTimeOffset = 0;
    public bool isMvOn = true;

    public List<GameAvailableCharacter> GetAvailableCharacter()
    {
        List<GameAvailableCharacter> MeAvailableCharacter = new List<GameAvailableCharacter>();

        for(int i = 0; i < GameCharacters.Count; i++)
        {
            if (GameCharacters[i].MeAvailable)
            {
                MeAvailableCharacter.Add(GameCharacters[i]);
            }
        }
        return MeAvailableCharacter;
    }


    public void SetData(GameSceneManager gameSceneManager)
    {
        isFirstTimePlaying = gameSceneManager.isFirstTimePlaying;

        MusicVolum = gameSceneManager.MusicVolum;
        SfxVolum = gameSceneManager.SfxVolume;
        DropSpeed = gameSceneManager.DropSpeed;
        MusicTimeOffset = gameSceneManager.MusicTimeOffset;
        isMvOn = gameSceneManager.isMvOn;

        ExpNum = gameSceneManager.Economy_Exp;
        StarNum = gameSceneManager.Economy_Star;
        ScNum = gameSceneManager.Economy_ScNum;
    }
}


[System.Serializable]
public class GameAvailableCharacter
{
    public CharacterSO Character;
    public bool MeAvailable;
}