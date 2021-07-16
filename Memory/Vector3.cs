using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tem.TemClass
{
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct Vector3 {
        [FieldOffset(0)]
        public float X;
        [FieldOffset(4)]
        public float Y;
        [FieldOffset(8)]
        public float Z;

        public Vector3(float x, float y, float z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public Vector3(float x) {
            this.X = x;
            this.Y = x;
            this.Z = x;
        }
        public Vector3(string s) {
            string[] values = s.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length >= 2) {
                this.X = float.Parse(values[0], CultureInfo.CreateSpecificCulture("en-US"));
                this.Y = float.Parse(values[1], CultureInfo.CreateSpecificCulture("en-US"));
                this.Z = float.Parse(values[2], CultureInfo.CreateSpecificCulture("en-US"));
            } else {
                this.X = this.Y = this.Z = -1.0f;
            }
        }
        public override bool Equals(object obj) {
            return obj is Vector3 temp && temp.X == X && temp.Y == Y;
        }
        public static bool operator ==(Vector3 one, Vector3 two) {
            return one.X == two.X && one.Y == two.Y;
        }
        public static bool operator !=(Vector3 one, Vector3 two) {
            return one.X != two.X || one.Y != two.Y;
        }
        public static Vector3 operator -(Vector3 one, Vector3 two) {
            return new Vector3() { X = one.X - two.X, Y = one.Y - two.Y };
        }
        public static Vector3 operator +(Vector3 one, Vector3 two) {
            return new Vector3() { X = one.X + two.X, Y = one.Y + two.Y };
        }
        public static Vector3 operator *(Vector3 one, Vector3 two) {
            return new Vector3() { X = one.X * two.X, Y = one.Y * two.Y };
        }
        public static Vector3 operator *(Vector3 one, int two) {
            return new Vector3() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector3 operator *(int two, Vector3 one) {
            return new Vector3() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector3 operator *(Vector3 one, float two) {
            return new Vector3() { X = one.X * two, Y = one.Y * two };
        }
        public static Vector3 operator *(float two, Vector3 one) {
            return new Vector3() { X = one.X * two, Y = one.Y * two };
        }
        public static float operator !(Vector3 one) {
            return (float)Math.Sqrt(one.X * one.X + one.Y * one.Y);
        }
        public Vector2 ToVector2() {
            return new Vector2(this.X, this.Y);
        }
        public override string ToString() {
            return this.X.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.Y.ToString(CultureInfo.CreateSpecificCulture("en-US")) + ", " + this.Z.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        }
        public override int GetHashCode() {
            return (int)(X * Y * Z);
        }
    }
}
