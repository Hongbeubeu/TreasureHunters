using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    public CharacterAction characterAction;
    [SerializeField] public Rigidbody2D body;
    [SerializeField] private GroundChecker groundChecker;

    public bool IsOnGround()
    {
        return groundChecker.GetOnGround();
    }
}