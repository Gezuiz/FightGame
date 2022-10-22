using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "CharacterSelection/Character")]

public class Character : ScriptableObject
{
    [SerializeField] private GameObject characterBanner = default;
    [SerializeField] private GameObject characterGameplay = default;

    public GameObject CharacterBanner => characterBanner;
    public GameObject ChracterGameplay => characterGameplay;
}

