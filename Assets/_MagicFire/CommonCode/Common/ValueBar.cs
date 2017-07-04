namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;
    using System;

    public class ValueBar : IValueSlidable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">初始主值</param>
        /// <param name="upperLimit">初始上限值</param>
        /// <param name="lowerLimit">初始下限值</param>
        public ValueBar(int value = 0, int upperLimit = 0, int lowerLimit = 0)
        {
            _value = value;
            _upperLimit = upperLimit;
            _lowerLimit = lowerLimit;
        }
        /// <summary>
        /// 主值
        /// </summary>
        private int _value;
        /// <summary>
        /// 上限值
        /// </summary>
        private int _upperLimit;
        /// <summary>
        /// 下限值
        /// </summary>
        private int _lowerLimit;

        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value >= _upperLimit)
                {
                    _value = _upperLimit;
                }
                else if (value <= _lowerLimit)
                {
                    _value = _lowerLimit;
                }
                else
                {
                    _value = value;
                }
            }
        }

        public int UpperLimit
        {
            get
            {
                return _upperLimit;
            }

            set
            {
                if (value <= _lowerLimit)
                {
                    _upperLimit = _value = _lowerLimit;
                }
                else if (value <= _value)
                {
                    _value = _upperLimit = value;
                }
                else
                {
                    _upperLimit = value;
                }
            }
        }

        public int LowerLimit
        {
            get
            {
                return _lowerLimit;
            }

            set
            {
                if (value >= _upperLimit)
                {
                    _lowerLimit = _value = _upperLimit;
                }
                else if (value >= _value)
                {
                    _lowerLimit = _value = value;
                }
                else
                {
                    _lowerLimit = value;
                }
            }
        }

        public float Percentage
        {
            get
            {
                if (Math.Abs((_upperLimit - _lowerLimit)) <= 0)
                {
                    return 0;
                }
                return (float)(_value - _lowerLimit) / (_upperLimit - _lowerLimit);
            }
        }

        public void Drop(int dropAmount)
        {
            Value -= dropAmount;
        }

        public void Recover(int recoverAmount)
        {
            Value += recoverAmount;
        }

        public void EnlargeUpperLimit(int enlargeAmount)
        {
            UpperLimit += enlargeAmount;
        }

        public void ShrinkUpperLimit(int shrinkAmount)
        {
            UpperLimit -= shrinkAmount;
        }
    }

}