using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class characterDatabase : ScriptableObject
{
    public character[] character;

    public int characterCount
    { get { return character.Length; } }

    public character GetCharacter(int index)
    { return character[index]; }
}
