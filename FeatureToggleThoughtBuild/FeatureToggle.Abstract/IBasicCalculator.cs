//-----------------------------------------------------------------------
// <copyright file="IBasicCalculator.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

namespace FeatureToggle.Calculator
{
    public interface IBasicCalculator
    {
        double Add(double first, double second);

        double Sub(double first, double second);
    }
}