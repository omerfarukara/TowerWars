public enum GameState
{
    Idle,
    Play,
    Finish
}

public enum MotionType
{
    Move,
    LocalMove,
    Scale,
    Jump
}

public enum ShakeType
{
    Position,
    Rotation,
    Scale
}

public enum ValueType
{
    Constant,
    Range
}

public enum BorderType
{
    Rectangle,
    Circle
}

public enum OrderType
{
    Ordered,
    Random
}

public enum BelongsTo
{
    Player,
    Enemy
}