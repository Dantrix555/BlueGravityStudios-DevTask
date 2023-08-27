using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersInstaller : IInstaller
{
    #region Fields and Properties

    [Header("Characters Prefab")]
    [SerializeField]
    private BaseCharacter characterPrefab;
    [SerializeField]
    private BaseCharacter npcPrefab;

    [Header("Characters Data")]
    [SerializeField]
    private SOCharacterData mainCharacterData;
    [SerializeField]
    private SOCharacterData[] npcsData;

    [Space(5)]
    [Header("Characters Location")]
    [SerializeField]
    private Transform playerSpawnTransform;
    [SerializeField]
    private Transform[] npcsSpawnTransform;

    public SOCharacterData soCharacterData => mainCharacterData;

    private CharacterBuilder charactersBuilder;

    private BaseCharacter playerController;
    private List<BaseCharacter> npcControllersList;

    #endregion

    #region IInstaller Override Methods

    public override void Install(ServiceLocator serviceLocator)
    {
        serviceLocator.RegisterService(this);
        SpawnCharacters();
    }

    #endregion

    #region Public Methods

    public void UpdatePlayerData(SOEquipableData equipableData)
    {
        mainCharacterData.EquipableHairData = equipableData.EquipableType == EquipableType.Hair ? equipableData : mainCharacterData.EquipableHairData;
        mainCharacterData.EquipableHatData = equipableData.EquipableType == EquipableType.Hat ? equipableData : mainCharacterData.EquipableHatData;
        mainCharacterData.EquipableOutfitData = equipableData.EquipableType == EquipableType.Outfit ? equipableData : mainCharacterData.EquipableOutfitData;

        playerController.UpdateCharacterData(mainCharacterData);
    }

    public SOEquipableData GetEquipableData(EquipableType equipableType)
    {
        switch (equipableType)
        {
            case EquipableType.Hat:
                return mainCharacterData.EquipableHatData;
            case EquipableType.Outfit:
                return mainCharacterData.EquipableOutfitData;
            case EquipableType.Hair:
                return mainCharacterData.EquipableHairData;
        }

        Debug.LogError(string.Concat("Required equipable of type ", equipableType.ToString(), " not found, returning null"));
        return null;
    }

    public void CheckIfSoldPartShouldBeRemoved(SOEquipableData removedEquipableData)
    {
        if (removedEquipableData == mainCharacterData.EquipableHatData || removedEquipableData == mainCharacterData.EquipableOutfitData)
            playerController.UpdateCharacterRemovedPart(removedEquipableData.EquipableType);
    }

    #endregion

    #region Inner Methods

    private void SpawnCharacters()
    {
        playerController = BuildCharacter(playerSpawnTransform, mainCharacterData, characterPrefab);

        npcControllersList = new List<BaseCharacter>();

        for (int i = 0; i < npcsData.Length; i++)
            npcControllersList.Add(BuildCharacter(npcsSpawnTransform[i % npcsSpawnTransform.Length], npcsData[i], npcPrefab));
    }

    private BaseCharacter BuildCharacter(Transform characterTransform, SOCharacterData characterData, BaseCharacter prefab)
    {
        charactersBuilder = new CharacterBuilder().FromPrefab(prefab).WithPositon(characterTransform.position)
            .WithRotation(characterTransform.rotation).WithCharacterData(characterData);

        return charactersBuilder.BuildCharacter();
    }

    #endregion

}
