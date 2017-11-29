//-----------------------------------------------------------------------
// <copyright file="Operation.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

namespace FeatureToggle
{
    public class Operation
    {
        public double First { get; set; }

        public double Second { get; set; }

        public double Result { get; set; }

        public Operation For(double result) =>
            new Operation
            {
                First = First,
                Second = Second,
                Result = result
            };
    }
}