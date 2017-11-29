//-----------------------------------------------------------------------
// <copyright file="IExtendedCalculator.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

namespace FeatureToggle.Calculator
{
    public interface IExtendedCalculator
    {
        double Multiply(double first, double second);

        double Devide(double first, double second);
    }
}