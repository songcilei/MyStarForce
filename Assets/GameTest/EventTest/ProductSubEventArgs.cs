using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using StarForce;
using UnityEngine;

namespace StarForce
{
    //self event
    public class ProductSubEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ProductSubEventArgs).GetHashCode();

        public int EntityId
        {
            get;
            private set;
        }

        public ProductBase m_ProductBase;

        public ProductSubEventArgs ()
        {
            EntityId = -1;
        }

        public override int Id
        {
            get { return EventId; }
        }

        public static ProductSubEventArgs Create(int entityId,object userData = null)
        {
            ProductSubEventArgs productSubEventArgs = ReferencePool.Acquire<ProductSubEventArgs>();
            productSubEventArgs.EntityId = entityId;
            return productSubEventArgs;
        }
    
        public static ProductSubEventArgs Create(ProductBase productBase)
        {
            ProductSubEventArgs productSubEventArgs = ReferencePool.Acquire<ProductSubEventArgs>();
            productSubEventArgs.m_ProductBase = productBase;
            return productSubEventArgs;
        }

        public override void Clear()
        {
            EntityId = -1;
        }

    }
}

