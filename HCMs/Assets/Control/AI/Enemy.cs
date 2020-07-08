public class Enemy
{
    private EnemyBehaviore _enmBeha;
    public Enemy(EnemyBehaviore behaviore)
    {
        _enmBeha = behaviore;
    }
    // 敵の動きを呼び出す
    // アップデートみたいな感じで運用
    public void Move()
    {
        _enmBeha.Run();
    }
}
