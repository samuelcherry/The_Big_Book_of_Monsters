using UnityEngine;


public class EnemyObjects : MonoBehaviour
{

    [System.Serializable]
    public struct Enemy
    {
        public string enemyName;
        public float enemyMaxHp;
        public float enemyCurrentHp;
        public float enemyDef;
        public float enemyAtk;
        public float xpRwd;
        public float goldRwd;
    }

}