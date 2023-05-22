namespace Assets.PixelHeroes.Scripts
{
    /// <summary>
    /// Animation state. The same parameter controls animation transitions.
    /// </summary>
    public enum AnimationState
    {
        Idle,
        Ready,
        Walking,
        Running,
        Jumping,
        Blocking,
        Crawling,
        Dead
    }
}