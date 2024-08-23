using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

namespace StarForce
{
    public class ProductAddEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ProductAddEventArgs).GetHashCode();

        public int EntityId
        {
            get;
            private set;   
        }

        public ProductBase m_ProductBase;

        public ProductAddEventArgs()
        {
            EntityId = -1;
        }


        public static ProductAddEventArgs Create(ProductBase productBase)
        {
            ProductAddEventArgs productAddEventArgs = ReferencePool.Acquire<ProductAddEventArgs>();

            productAddEventArgs.m_ProductBase = productBase;
            return productAddEventArgs;
        }


        public override void Clear()
        {
            EntityId = -1;
            
        }

        public override int Id {
            get
            {
                return EventId;
            }
        }
    }
}

