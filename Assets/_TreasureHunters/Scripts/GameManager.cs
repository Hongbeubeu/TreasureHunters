using Ultilities.Core.Runtime.Singleton;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override void Init()
    {
    }

    public void Test()
    {
        Debug.Log("test");
    }
}