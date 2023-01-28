using System;
using System.Drawing;

#nullable enable

// ReSharper disable UnusedMember.Global
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable once CheckNamespace
namespace ScaleHQ.DotScreen
{
    /// <summary>
    /// Stores the location and size of a rectangular region.
    /// </summary>
    [Serializable]
    public struct RectangleD : IEquatable<RectangleD>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref='RectangleD'/> class.
        /// </summary>
        // ReSharper disable once UnassignedReadonlyField
        public static readonly RectangleD Empty;

        private double _x;
        private double _y;
        private double _width;
        private double _height;

        /// <summary>
        /// Initializes a new instance of the <see cref='RectangleD'/> class with the specified location
        /// and size.
        /// </summary>
        public RectangleD(double x, double y, double width, double height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public RectangleD(Rectangle rectangle): this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {}

        /// <summary>
        /// Creates a new <see cref='RectangleD'/> with the specified location and size.
        /// </summary>
        public static RectangleD From(double left, double top, double right, double bottom) =>
            new RectangleD(left, top, right - left, bottom - top);

        public PointD Location
        {
            readonly get => new PointD(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the size of this <see cref='RectangleD'/>.
        /// </summary>
        public SizeD Size
        {
            readonly get => new SizeD(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='RectangleD'/>.
        /// </summary>
        public double X
        {
            readonly get => _x;
            set => _x = value;
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='RectangleD'/>.
        /// </summary>
        public double Y
        {
            readonly get => _y;
            set => _y = value;
        }

        /// <summary>
        /// Gets or sets the width of the rectangular region defined by this <see cref='RectangleD'/>.
        /// </summary>
        public double Width
        {
            readonly get => _width;
            set => _width = value;
        }

        /// <summary>
        /// Gets or sets the height of the rectangular region defined by this <see cref='RectangleD'/>.
        /// </summary>
        public double Height
        {
            readonly get => _height;
            set => _height = value;
        }

        /// <summary>
        /// Gets the x-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='RectangleD'/> .
        /// </summary>
        public readonly double Left => X;

        /// <summary>
        /// Gets the y-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='RectangleD'/>.
        /// </summary>
        public readonly double Top => Y;

        /// <summary>
        /// Gets the x-coordinate of the lower-right corner of the rectangular region defined by this
        /// <see cref='RectangleD'/>.
        /// </summary>
        public readonly double Right => X + Width;

        /// <summary>
        /// Gets the y-coordinate of the lower-right corner of the rectangular region defined by this
        /// <see cref='RectangleD'/>.
        /// </summary>
        public readonly double Bottom => Y + Height;

        /// <summary>
        /// Tests whether this <see cref='RectangleD'/> has a <see cref='Width'/> or a <see cref='Height'/> of 0.
        /// </summary>
        public readonly bool IsEmpty => Width <= 0 || Height <= 0;

        /// <summary>
        /// Tests whether <paramref name="obj"/> is a <see cref='RectangleD'/> with the same location and
        /// size of this <see cref='RectangleD'/>.
        /// </summary>
        public readonly override bool Equals(object? obj)
        {
            return obj is RectangleD other && Equals(other);
        }

        /// <summary>
        /// Tests whether two <see cref='RectangleD'/> objects have equal location and size.
        /// </summary>
        public static bool operator ==(RectangleD left, RectangleD right) =>
            left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;

        /// <summary>
        /// Tests whether two <see cref='RectangleD'/> objects differ in location or size.
        /// </summary>
        public static bool operator !=(RectangleD left, RectangleD right) => !(left == right);

        /// <summary>
        /// Determines if the specified point is contained within the rectangular region defined by this
        /// <see cref='Rectangle'/> .
        /// </summary>
        public readonly bool Contains(double x, double y) => X <= x && x < X + Width && Y <= y && y < Y + Height;

        /// <summary>
        /// Determines if the specified point is contained within the rectangular region defined by this
        /// <see cref='Rectangle'/> .
        /// </summary>
        public readonly bool Contains(PointD pt) => Contains(pt.X, pt.Y);

        /// <summary>
        /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within
        /// the rectangular region represented by this <see cref='Rectangle'/> .
        /// </summary>
        public readonly bool Contains(RectangleD rect) =>
            X <= rect.X && rect.X + rect.Width <= X + Width && Y <= rect.Y && rect.Y + rect.Height <= Y + Height;

        public readonly bool Equals(RectangleD other)
        {
            return _x.Equals(other._x) && _y.Equals(other._y) && _width.Equals(other._width) && _height.Equals(other._height);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _x.GetHashCode();
                hashCode = (hashCode * 397) ^ _y.GetHashCode();
                hashCode = (hashCode * 397) ^ _width.GetHashCode();
                hashCode = (hashCode * 397) ^ _height.GetHashCode();
                return hashCode;
            }
        }


        public readonly RectangleF ToRectangleF()
        {
            return RectangleF.FromLTRB((float)Left, (float)Top, (float)Right, (float)Bottom);
        }

        /// <summary>
        /// Converts the <see cref='Location'/> and <see cref='Size'/>
        /// of this <see cref='RectangleD'/> to a human-readable string.
        /// </summary>
        public readonly override string ToString() => $"{{X={X}, Y={Y}, Width={Width}, Height={Height}}}";

        /// <summary>
        /// Converts the specified <see cref='Rectangle'/> to a
        /// <see cref='RectangleD'/>.
        /// </summary>
        public static implicit operator RectangleD(Rectangle r) => new RectangleD(r.X, r.Y, r.Width, r.Height);
    }
}