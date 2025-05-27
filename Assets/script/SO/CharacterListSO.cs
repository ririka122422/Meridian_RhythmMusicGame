using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[SerializeField]
public class CharacterListSO : ScriptableObject
{
    public List<CharacterSO> CharacterList;
}
