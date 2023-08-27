using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuilder
{
    #region Fields and properties

    private BaseCharacter characterPrefab;

    private Vector3 position = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;

    private SOCharacterData characterData;

    #endregion

    #region Public Methods

    public CharacterBuilder FromPrefab(BaseCharacter prefab)
    {
        characterPrefab = prefab;
        return this;
    }

    public CharacterBuilder WithPositon(Vector3 newPosition)
    {
        position = newPosition;
        return this;
    }

    public CharacterBuilder WithRotation(Quaternion newRotation)
    {
        rotation = newRotation;
        return this;
    }

    public CharacterBuilder WithCharacterData(SOCharacterData characterData)
    {
        this.characterData = characterData;
        return this;
    }

    public BaseCharacter BuildCharacter()
    {
        BaseCharacter instance = Object.Instantiate(characterPrefab, position, rotation);
        instance.UpdateCharacterData(characterData);
        return instance;
    }

    #endregion
}
