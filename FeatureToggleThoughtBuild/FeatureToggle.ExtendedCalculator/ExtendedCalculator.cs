//-----------------------------------------------------------------------
// <copyright file="ExtendedCalculator.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

namespace FeatureToggle.Calculator
{
    public class ExtendedCalculator : IExtendedCalculator
    {
        public double Devide(double first, double second) =>
            first / second;

        public double Multiply(double first, double second) =>
            first * second;
    }
}