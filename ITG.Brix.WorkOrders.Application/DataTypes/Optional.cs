﻿using System;

namespace ITG.Brix.WorkOrders.Application.DataTypes
{
    public struct Optional<T>
    {
        private readonly T _value;

        public bool HasValue { get; private set; }

        public T Value
        {
            get
            {
                if (HasValue)
                {
                    return _value;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public Optional(T value)
        {
            _value = value;
            HasValue = true;
        }
    }
}
