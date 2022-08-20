using UnityEngine;
using System.Collections;

namespace Ultilities.Core.Runtime.Pool
{
    public interface IFastPoolItem
    {
        void OnFastInstantiate();
        void OnFastDestroy();
    }
}