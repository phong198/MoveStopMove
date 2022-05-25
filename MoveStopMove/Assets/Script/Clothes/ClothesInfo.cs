using UnityEngine;

[CreateAssetMenu(fileName = "New Clothes", menuName = "Clothes")]
public class ClothesInfo : ScriptableObject
{
    [Header("Head Position")]
    public GameObject[] HeadPosition;

    [Header("Back Position")]
    public GameObject[] BackPosition;

    [Header("Tail Position")]
    public GameObject[] TailPosition;

    [Header("FreeHand Position")]
    public GameObject[] FreeHandPosition;

    [Header("Pants")]
    public Material[] PantsMaterials;

    [Header("Skin")]
    public Material[] SkinMaterials;
}
