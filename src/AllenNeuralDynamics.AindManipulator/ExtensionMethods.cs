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
}