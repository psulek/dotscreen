#nullable enable

using System;

// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable once CheckNamespace
// ReSharper disable UnusedMember.Global
namespace ScaleHQ.DotScreen
{
    /// <summary>
    /// Represents the size of a rectangular region with an ordered pair of width and height.
    /// </summary>
    [Serializable]
    public struct SizeD : IEquatable<SizeD>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref='SizeD'/> class.
        /// </summary>
        // ReSharper disable once UnassignedReadonlyField
        public static readonly SizeD Empty;

        private double _width;
        private double _height;

        /// <summary>
        /// Initializes a new instance of the <see cref='SizeD'/> class from the specified
        /// existing <see cref='SizeD'/>.
        /// </summary>
        public SizeD(SizeD size)
        {
            _width = size._width;
            _height = size._height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='SizeD'/> class from the specified
        /// <see cref='PointD'/>.
        /// </summary>
        public SizeD(PointD pt)
        {
            _width = pt.X;
            _height = pt.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='SizeD'/> class from the specified dimensions.
        /// </summary>
        public SizeD(double width, double height)
        {
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Tests whether two <see cref='SizeD'/> objects are identical.
        /// </summary>
        public static bool operator ==(SizeD sz1, SizeD sz2) => sz1.Width == sz2.Width && sz1.Height == sz2.Height;

        /// <summary>
        /// Tests whether two <see cref='SizeD'/> objects are different.
        /// </summary>
        public static bool operator !=(SizeD sz1, SizeD sz2) => !(sz1 == sz2);

        /// <summary>
        /// Converts the specified <see cref='SizeD'/> to a <see cref='PointD'/>.
        /// </summary>
        public static explicit operator PointD(SizeD size) => new PointD(size.Width, size.Height);

        /// <summary>
        /// Tests whether this <see cref='SizeD'/> has zero width and height.
        /// </summary>
        public readonly bool IsEmpty => _width == 0.0 && _height == 0.0;

        /// <summary>
        /// Represents the horizontal component of this <see cref='SizeD'/>.
        /// </summary>
        public double Width
        {
            readonly get => _width;
            set => _width = value;
        }

        /// <summary>
        /// Represents the vertical component of this <see cref='SizeD'/>.
        /// </summary>
        public double Height
        {
            readonly get => _height;
            set => _height = value;
        }

        /// <summary>
        /// Performs vector addition of two <see cref='SizeD'/> objects.
        /// </summary>
        public static SizeD Add(SizeD sz1, SizeD sz2) => new SizeD(sz1.Width + sz2.Width, sz1.Height + sz2.Height);

        /// <summary>
        /// Contracts a <see cref='SizeD'/> by another <see cref='SizeD'/>.
        /// </summary>
        public static SizeD Subtract(SizeD sz1, SizeD sz2) => new SizeD(sz1.Width - sz2.Width, sz1.Height - sz2.Height);

        /// <summary>
        /// Tests to see whether the specified object is a <see cref='SizeD'/>  with the same dimensions
        /// as this <see cref='SizeD'/>.
        /// </summary>
        public readonly override bool Equals(object? obj)
        {
            return obj is SizeD other && Equals(other);
        }

        public readonly bool Equals(SizeD other)
        {
            return _width.Equals(other._width) && _height.Equals(other._height);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_width.GetHashCode() * 397) ^ _height.GetHashCode();
            }
        }

        public readonly PointD ToPointD() => (PointD)this;

        // public readonly Size ToSize() => new Size(unchecked((int)Width), unchecked((int)Height));

        /// <summary>
        /// Creates a human-readable string that represents this <see cref='SizeD'/>.
        /// </summary>
        public readonly override string ToString() => $"{{Width={_width}, Height={_height}}}";
    }
}