using System;
using UnityEngine;

[Serializable]
public class SpriteAndEmotion
{
    [SerializeField] private Sprite defaultFaceSprite;
    [SerializeField] private RuntimeAnimatorController faceAnimation;

    public Sprite DefaultFaceSprite => defaultFaceSprite;
    public RuntimeAnimatorController FaceAnimation => faceAnimation;
}
