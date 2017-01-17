using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
sealed public class AI_Path : MonoBehaviour
{
    public int AmountOfPositions;

    public int NewAmountOfPositions;

    public List<Vector2> PositionList;

    private List<Vector2> _EnemyPositionList;


    public List<Vector2> GetAlliedPath()
    {

        return PositionList;
    }

    public List<Vector2> GetAlliedResourcePath(int SpotNumberToTheMine)
    {
        List<Vector2> resourcePathList = new List<Vector2>();
        for (int i = 0; i < SpotNumberToTheMine; i++)
        {
            resourcePathList.Add(PositionList[i]);
        }
        return PositionList;
    }

    public List<Vector2> GetEnemyPath()
    {
        ReverseAlliedPathList();

        return _EnemyPositionList;
    }

    public List<Vector2> GetEnemyResourcePath(int SpotNumberToTheMine)
    {
        List<Vector2> enemyResourcePathList = new List<Vector2>();
        ReverseAlliedPathList();

        for (int i = 0; i < SpotNumberToTheMine; i++)
        {
            enemyResourcePathList.Add(_EnemyPositionList[i]);
        }
        return enemyResourcePathList;
    }

    private void ReverseAlliedPathList()
    {
        List<Vector2> reversedPositionList = PositionList;
        reversedPositionList.Reverse();
        _EnemyPositionList = reversedPositionList;
    }
}
