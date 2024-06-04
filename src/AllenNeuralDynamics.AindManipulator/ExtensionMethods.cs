namespace AllenNeuralDynamics.AindManipulator {
    public partial class ManipulatorPosition
    {
        public static ManipulatorPosition operator +(ManipulatorPosition el1, ManipulatorPosition el2)
        {
            return new ManipulatorPosition()
            {
                X = el1.X + el2.X,
                Y1 = el1.Y1 + el2.Y1,
                Y2 = el1.Y2 + el2.Y2,
                Z = el1.Z + el2.Z
            };
        }

        public static ManipulatorPosition operator -(ManipulatorPosition el1, ManipulatorPosition el2)
        {
            return new ManipulatorPosition()
            {
                X = el1.X - el2.X,
                Y1 = el1.Y1 - el2.Y1,
                Y2 = el1.Y2 - el2.Y2,
                Z = el1.Z - el2.Z
            };
        }

        public static ManipulatorPosition operator *(ManipulatorPosition el1, float gain)
        {
            return new ManipulatorPosition()
            {
                X = el1.X * gain,
                Y1 = el1.Y1 * gain,
                Y2 = el1.Y2 * gain,
                Z = el1.Z * gain
            };
        }
        public static ManipulatorPosition operator *(ManipulatorPosition el1, ManipulatorPosition el2)
        {
            return new ManipulatorPosition()
            {
                X = el1.X * el2.X,
                Y1 = el1.Y1 * el2.Y1,
                Y2 = el1.Y2 * el2.Y2,
                Z = el1.Z * el2.Z
            };

        }
    }

    public enum AindManipulatorAxis
    {
        None = Axis._0,
        X = Axis._3,
        Y1 = Axis._1,
        Y2 = Axis._2,
        Z = Axis._4
    }

    public class AindManipulatorPosition
    {
        public double X;
        public double Y1;
        public double Y2;
        public double Z;

        public AindManipulatorPosition()
        {
            X = 0;
            Y1 = 0;
            Y2 = 0;
            Z = 0;
        }

        public AindManipulatorPosition(double y1, double y2, double x, double z)
        {
            X = x;
            Y1 = y1;
            Y2 = y2;
            Z = z;
        }

        public AindManipulatorPosition(ManipulatorPosition pos)
        {
            X = pos.X;
            Y1 = pos.Y1;
            Y2 = pos.Y2;
            Z = pos.Z;
        }

        public ManipulatorPosition ToManipulatorPosition()
        {
            return new ManipulatorPosition()
            {
                X = X,
                Y1 = Y1,
                Y2 = Y2,
                Z = Z
            };
        }
    }



}