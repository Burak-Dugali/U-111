using System.Collections.Generic;
using UnityEngine;


namespace Dialogue
{
    [CreateAssetMenu(menuName = "Data/Dialogue/Dialogue")]
    public class DialogueCreator : ScriptableObject
    {
        public List<string> Texts;
        public Sprite NPCAsset;
    }
}
