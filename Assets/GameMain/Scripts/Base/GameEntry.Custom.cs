//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace StarForce
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }

        public static HPBarComponent HPBar
        {
            get;
            private set;
        }

        public static ShopComponent Shop
        {
            get;
            private set;
        }
        
        public static BattleComponent BattleSystem
        {
            get;
            private set;
        }

        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            HPBar = UnityGameFramework.Runtime.GameEntry.GetComponent<HPBarComponent>();
            Shop = UnityGameFramework.Runtime.GameEntry.GetComponent<ShopComponent>();
            BattleSystem = UnityGameFramework.Runtime.GameEntry.GetComponent<BattleComponent>();
        }
    }
}
