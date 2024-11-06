using Unity.VisualScripting;
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

    public Enemy[] enemies = new Enemy[20];

    void Start()
    {
        enemies[0].enemyName = "Imp";
        enemies[0].enemyMaxHp = 30;
        enemies[0].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[0].enemyDef = 2;
        enemies[0].enemyAtk = 5;
        enemies[0].xpRwd = 5;
        enemies[0].goldRwd = 2;

        enemies[1].enemyName = "Ogre";
        enemies[1].enemyMaxHp = 100;
        enemies[1].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[1].enemyDef = 4;
        enemies[1].enemyAtk = 20;
        enemies[1].xpRwd = 10;
        enemies[1].goldRwd = 4;

        enemies[2].enemyName = "Wight";
        enemies[2].enemyMaxHp = 150;
        enemies[2].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[2].enemyDef = 4;
        enemies[2].enemyAtk = 30;
        enemies[2].xpRwd = 20;
        enemies[2].goldRwd = 6;

        enemies[3].enemyName = "Ghost";
        enemies[3].enemyMaxHp = 200;
        enemies[3].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[3].enemyDef = 4;
        enemies[3].enemyAtk = 40;
        enemies[3].xpRwd = 50;
        enemies[3].goldRwd = 8;

        enemies[4].enemyName = "Hill Giant";
        enemies[4].enemyMaxHp = 500;
        enemies[4].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[4].enemyDef = 10;
        enemies[4].enemyAtk = 50;
        enemies[4].xpRwd = 75;
        enemies[4].goldRwd = 10;

        enemies[5].enemyName = "Drider";
        enemies[5].enemyMaxHp = 600;
        enemies[5].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[5].enemyDef = 10;
        enemies[5].enemyAtk = 60;
        enemies[5].xpRwd = 100;
        enemies[5].goldRwd = 12;

        enemies[6].enemyName = "Stone Giant";
        enemies[6].enemyMaxHp = 700;
        enemies[6].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[6].enemyDef = 10;
        enemies[6].enemyAtk = 70;
        enemies[6].xpRwd = 125;
        enemies[6].goldRwd = 14;

        enemies[7].enemyName = "Chain Devil";
        enemies[7].enemyMaxHp = 750;
        enemies[7].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[7].enemyDef = 10;
        enemies[7].enemyAtk = 80;
        enemies[7].xpRwd = 150;
        enemies[7].goldRwd = 16;

        enemies[8].enemyName = "Treant";
        enemies[8].enemyMaxHp = 800;
        enemies[8].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[8].enemyDef = 20;
        enemies[8].enemyAtk = 90;
        enemies[8].xpRwd = 175;
        enemies[8].goldRwd = 18;

        enemies[9].enemyName = "Guardian Naga";
        enemies[9].enemyMaxHp = 950;
        enemies[9].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[9].enemyDef = 20;
        enemies[9].enemyAtk = 100;
        enemies[9].xpRwd = 200;
        enemies[9].goldRwd = 20;

        enemies[10].enemyName = "Djinni";
        enemies[10].enemyMaxHp = 1000;
        enemies[10].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[10].enemyDef = 20;
        enemies[10].enemyAtk = 110;
        enemies[10].xpRwd = 225;
        enemies[10].goldRwd = 22;

        enemies[11].enemyName = "Arch Mage";
        enemies[11].enemyMaxHp = 1050;
        enemies[11].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[11].enemyDef = 20;
        enemies[11].enemyAtk = 120;
        enemies[11].xpRwd = 250;
        enemies[11].goldRwd = 24;

        enemies[12].enemyName = "Adult White Dragon";
        enemies[12].enemyMaxHp = 1150;
        enemies[12].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[12].enemyDef = 25;
        enemies[12].enemyAtk = 130;
        enemies[12].xpRwd = 275;
        enemies[12].goldRwd = 26;

        enemies[13].enemyName = "Ice Devil";
        enemies[13].enemyMaxHp = 1250;
        enemies[13].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[13].enemyDef = 30;
        enemies[13].enemyAtk = 140;
        enemies[13].xpRwd = 300;
        enemies[13].goldRwd = 28;

        enemies[14].enemyName = "Purple Worm";
        enemies[14].enemyMaxHp = 1400;
        enemies[14].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[14].enemyDef = 30;
        enemies[14].enemyAtk = 150;
        enemies[14].xpRwd = 325;
        enemies[14].goldRwd = 30;

        enemies[15].enemyName = "Iron Golem";
        enemies[15].enemyMaxHp = 1600;
        enemies[15].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[15].enemyDef = 30;
        enemies[15].enemyAtk = 160;
        enemies[15].xpRwd = 350;
        enemies[15].goldRwd = 32;

        enemies[16].enemyName = "Adult Red Dragon";
        enemies[16].enemyMaxHp = 1700;
        enemies[16].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[16].enemyDef = 30;
        enemies[16].enemyAtk = 170;
        enemies[16].xpRwd = 375;
        enemies[16].goldRwd = 34;

        enemies[17].enemyName = "Dragon Turtle";
        enemies[17].enemyMaxHp = 1800;
        enemies[17].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[17].enemyDef = 30;
        enemies[17].enemyAtk = 180;
        enemies[17].xpRwd = 400;
        enemies[17].goldRwd = 36;

        enemies[18].enemyName = "Balor";
        enemies[18].enemyMaxHp = 1900;
        enemies[18].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[18].enemyDef = 30;
        enemies[18].enemyAtk = 190;
        enemies[18].xpRwd = 450;
        enemies[18].goldRwd = 38;

        enemies[19].enemyName = "Lich";
        enemies[19].enemyMaxHp = 200000;
        enemies[19].enemyCurrentHp = enemies[0].enemyMaxHp;
        enemies[19].enemyDef = 350;
        enemies[19].enemyAtk = 1250;
        enemies[19].xpRwd = 500;
        enemies[19].goldRwd = 400;

    }

}