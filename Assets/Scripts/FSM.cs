using System.Collections.Generic;
using UnityEngine;

public class FSM<TEnum, TOwner> : MonoBehaviour
{
    protected Dictionary<TEnum, IState<TOwner>> dicState = new();
    protected StateMachine<TOwner> sm;
}
