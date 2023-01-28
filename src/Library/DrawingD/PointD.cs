using System;

#nullable enable

// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable once CheckNamespace
// ReSharper disable UnusedMember.Global
namespace ScaleHQ.DotScreen
{
    /// <summary>
    /// Represents an ordered pair of x and y coordinates that define a point in a two-dimensional plane.
    /// </summary>
    [Serializable]
    public struct PointD : IEquatable<PointD>
    {
        /// <summary>
        /// Creates a new instance of the <see cref='PointD'/> class with member data left uninitialized.
        /// </summary>
        // ReSharper disable once UnassignedReadonlyField
        public static readonly PointD Empty;

        private double _x;
        private double _y;

        /// <summary>
        /// Initializes a new instance of the <see cref='PointD'/> class with the specified coordinates.
        /// </summary>
        public PointD(double x, double y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref='PointD'/> is empty.
        /// </summary>
        public readonly bool IsEmpty => _x == 0.0 && _y == 0.0;

        /// <summary>
        /// Gets the x-coordinate of this <see cref='PointD'/>.
        /// </summary>
        public double X
        {
            readonly get => _x;
            set => _x = value;
        }

        /// <summary>
        /// Gets the y-coordinate of this <see cref='PointD'/>.
        /// </summary>
        public double Y
        {
            readonly get => _y;
            set => _y = value;
        }


        public readonly override bool Equals(object? obj)
        {
            return obj is PointD other && Equals(other);
        }

        public readonly bool Equals(PointD other)
        {
            return _x.Equals(other._x) && _y.Equals(other._y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_x.GetHashCode() * 397) ^ _y.GetHashCode();
            }
        }

        public readonly override string ToString() => $"{{X={_x}, Y={_y}}}";
    }
}