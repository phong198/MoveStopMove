using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSkin", menuName = "Skin")]
public class CharacterSkins : ScriptableObject
{
    [Header("Skin Color")]
    public List<Material> SkinColor;
}
