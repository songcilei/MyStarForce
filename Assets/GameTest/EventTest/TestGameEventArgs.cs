using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

namespace StarForce
{
    public class TestGameEventArgs : BaseEventArgs
    {
        //event ID
        public static readonly int EventId = typeof(TestGameEventArgs).GetHashCode();

        //entity id  这个可以不写 这个就是内容
        public int EntityId
        {
            get;
            private set;
        }
        
        //初始化
        public TestGameEventArgs()
        {
            EntityId = -1;
        }

        public override int Id {
            get
            {
                return EventId;
            }
        }

        //从pool 内获取
        public static TestGameEventArgs Create(int entityId,object userData = null)
        {
            TestGameEventArgs testGameEventArgs = ReferencePool.Acquire<TestGameEventArgs>();
            testGameEventArgs.EntityId = entityId;
            return testGameEventArgs;
        }


        public override void Clear()
        {
            EntityId = -1;
        }

        
    }
}

