using UnityEngine;

[CreateAssetMenu(fileName = "LabOpener", menuName = "Data/Player/LabOpener", order = 2)]
public class LabOpener : ScriptableObject
{
    public bool labIsOpen;
    public bool labIsTransitioning;
}