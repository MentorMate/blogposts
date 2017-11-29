//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System;
using FeatureToggle.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.Controllers
{
    /// <summary>The application default MVC controller.</summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        private readonly IBasicCalculator _basicCalculator;
        private readonly IExtendedCalculator _extendedCalculator;

        public HomeController(
            IBasicCalculator basicCalculator = null,
            IExtendedCalculator extendedCalculator = null)
        {
            _basicCalculator = basicCalculator;
            _extendedCalculator = extendedCalculator;
        }

        /// <summary>The application default page..</summary>
        public IActionResult Index() =>
            GetView(new Operation() { First = 6, Second = 3 });

        /// <summary>Add</summary>
        public IActionResult Add(Operation data) =>
            CalculatorAction(data, _basicCalculator.Add);

        /// <summary>Subtraction</summary>
        public IActionResult Sub(Operation data) =>
            CalculatorAction(data, _basicCalculator.Sub);

        /// <summary>Multiply</summary>
        public IActionResult Multiply(Operation data) =>
            CalculatorAction(data, _extendedCalculator.Multiply);

        /// <summary>Devide</summary>
        public IActionResult Devide(Operation data) =>
            CalculatorAction(data, _extendedCalculator.Devide);

        private IActionResult CalculatorAction(Operation data, Func<double, double, double> action)
        {
            data.Result = action(data.First, data.Second);
            return GetView(data);
        }

        private IActionResult GetView(Operation data)
        {
            ViewBag.IsBasicCalculatorEnabled = _basicCalculator != null;
            ViewBag.IsExtendedCalculatorEnabled = _extendedCalculator != null;
            return View("Index", data);
        }
    }
}