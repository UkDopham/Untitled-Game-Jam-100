using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _emotionSprites = new List<Sprite>();

    public Sprite GetSpriteByEmotion(Emotion emotion)
    {
        int count = (int)emotion;
        
        if(count >= this._emotionSprites.Count)
        {
            return null;
        }

        if (emotion == Emotion.None)
        {
            return null;
        }

        return this._emotionSprites[count];
    }
}
