using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CharacterSelect : NetworkBehaviour
{
    public Character[] characters = default;
    public int selectedCharacter = 0;
    [SerializeField] GameObject CharacterDisplay;
    [SerializeField] Transform CharacterBannerParent;
    private List<GameObject> characterInstances = new List<GameObject>();
    public Transform spawnpos1;
    public Transform spawnpos2;
    public Text chooseNameText;


    public override void OnStartClient()
    {
            foreach (var character in characters)
            {
                GameObject characterInstance =
                    Instantiate(character.CharacterBanner, CharacterBannerParent);

                characterInstance.SetActive(false);

                characterInstances.Add(characterInstance);
            }
        
        characterInstances[selectedCharacter].SetActive(true);
        CharacterDisplay.SetActive(true);
    }
    
    public void NextCharacter()
    {
        characterInstances[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characterInstances[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        CmdSelect(selectedCharacter);
        CharacterDisplay.SetActive(false);
    }

    [Command(requiresAuthority = false)]
    public void CmdSelect(int CharacterIndex, NetworkConnectionToClient sender = null)
    {
        Transform start = spawnpos1;
        GameObject characterInstance = Instantiate(characters[CharacterIndex].ChracterGameplay, start.position, start.rotation);
        NetworkServer.Spawn(characterInstance, sender);
        Debug.Log("Dota2");
    } 
    public void PreviousCharacter()
    {
        characterInstances[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter <0)
        {
            selectedCharacter += characters.Length;
        }
        characterInstances[selectedCharacter].SetActive(true);
    }
}
