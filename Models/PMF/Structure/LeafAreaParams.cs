namespace Models.PMF.Struct
{
    /// <summary>Birch, Hammer bell shaped curve parameters</summary>
    public class LeafAreaParams
    {
        /// <summary>Eqn 14 calc x0 - position of largest leaf</summary>
        public double AX0I { get; set; }

        /// <summary>TODO</summary>
        public double AX0S { get; set; }

        /// <summary>TODO</summary>
        public double X0Main { get; set; }

        /// <summary>TODO</summary>
        public double AMaxI { get; set; }

        /// <summary>TODO</summary>
        public double AMaxS { get; set; }

        /// <summary>TODO</summary>
        public double AMaxMain { get; set; }

        /// <summary>TODO</summary>
        public double A0 { get; set; }

        /// <summary>TODO</summary>
        public double A1 { get; set; }

        /// <summary>TODO</summary>
        public double A2 { get; set; }

        /// <summary>TODO</summary>
        public double B0 { get; set; }

        /// <summary>TODO</summary>
        public double B1 { get; set; }

        /// <summary>TODO</summary>
        public double B2 { get; set; }

        /// <summary>TODO</summary>
        public double LeafNoCorrection { get; set; }

        /// <summary>TODO</summary>
        public double LargestLeafPlateau { get; set; }
    }
}
