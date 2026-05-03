using UnityEngine;

//

/*
ギミック案
避けやすい攻撃 > 隙が大きい攻撃
- タケノコ、単独・連続
- レーザー、単独・連続
- ゲロビ
- 
*/

public class E_BossCubeA : Enemy
{


    protected override void E_Start()
    {
        
    }

    protected override void E_Update(float dtime)
    {
        
    }

    protected override void E_Death()
    {
        
    }



    public override Vector3 GetPosition()
    {
        return transform.position;
    }

    public override Transform GetTransform()
    {
        return transform;
    }
}
