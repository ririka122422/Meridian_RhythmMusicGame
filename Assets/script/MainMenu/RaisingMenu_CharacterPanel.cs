using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaisingMenu_CharactorPanal : MonoBehaviour
{
    private GameAudioManager _gameAudioManager;
    public MainManu_RaisingMenu _RaisingMenu;

    public CharacterSO CharacterSO;

    [SerializeField] private Image actorIcon;
    [SerializeField] private Text actorLevel;
    [SerializeField] private Button openDetails;

    private void Start()
    {
        _gameAudioManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameAudioManager>();

        openDetails.onClick.AddListener(OnClickOpenDetails);
    }

    public void AutoSetup(CharacterSO characterSO)
    {
        CharacterSO = characterSO;
        actorIcon.sprite = CharacterSO.icon;
        actorLevel.text = $"Lv.{CharacterSO.Level}";
    }

    public void OnClickOpenDetails()
    {
        _gameAudioManager.PlayOnClickButtonSfx();

        _RaisingMenu.ShowCharacterDetails(this);
    }

    private void OnDestroy()
    {
        openDetails.onClick.RemoveAllListeners();
    }
}
