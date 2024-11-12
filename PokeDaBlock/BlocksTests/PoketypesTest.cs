using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokeTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PokeTypesTests
{

    [TestClass]
    public class PoketypesTest
    {

        [TestMethod]
        public void GetTypes()
        {
            PokeType.SummonTypelist();
            PokeType.AssembleTypes();
        }
        [TestMethod]
        public void TestWeaknessesAddedUp()
        {
            List<PokeType> expectedWeakto = new List<PokeType>() { PokeType.Fire, PokeType.Ground };
            List<PokeType> expectedQuadWeak = new List<PokeType>() { PokeType.Fighting };
            List<PokeType> expectedImmune = new List<PokeType>() { PokeType.Ghost, PokeType.Poison };
            List<PokeType> actualWeakto = PokeType.WeaknessAddedUp(PokeType.Normal, PokeType.Steel).WeakTo;
            List<PokeType> actualQuadWeak = PokeType.WeaknessAddedUp(PokeType.Normal, PokeType.Steel).QuadWeak;
            List<PokeType> actualImmune = PokeType.WeaknessAddedUp(PokeType.Normal, PokeType.Steel).Immune;

            CollectionAssert.AreEqual(expectedWeakto, actualWeakto);
            CollectionAssert.AreEqual(expectedQuadWeak, actualQuadWeak);
            CollectionAssert.AreEqual(expectedImmune, actualImmune);
        }
        [TestMethod]
        public void TestSuperEffectiveCoverage()
        {
            List<PokeType> expected = new List<PokeType>() { PokeType.Flying, PokeType.Water };
            List<PokeType> actual = PokeType.SuperEffectiveCoverage(PokeType.Normal, PokeType.Electric).MixedCoverage;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSummonList()
        {
           
            PokeType expect = PokeType.Electric;
            PokeType actual = PokeType.SummonTypelist()[3];
            Assert.AreEqual(expect, actual);
        }
        [TestMethod]
        public void TestName()
        {
            string result = PokeType.ChooseType("water").Name;

            Assert.AreEqual("Water", result);
        }
        [TestMethod]
        public void TestFormDEST()
        {

            List<PokeType> actual = PokeType.FormDSET(PokeType.ChooseType("Electric"));
            List<PokeType> expected = new List<PokeType>() { PokeType.ChooseType("Flying"), PokeType.ChooseType("Water") };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetDEST()
        {
            List<PokeType> expected = new List<PokeType>() { PokeType.ChooseType("Flying"), PokeType.ChooseType("Water") };

            List<PokeType> actual = PokeType.ChooseType("Electric").GetDSET();

            CollectionAssert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected, PokeType.ChooseType("Electric").DealsSuperEffectiveTo);
        }
        [TestMethod]
        public void TestGetWt()
        {

            List<PokeType> expected = new List<PokeType> { PokeType.ChooseType("Ground") };

            List<PokeType> actual = PokeType.Electric.WeakTo;

            CollectionAssert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected, PokeType.Electric.GetWT());
        }
        [TestMethod]
        public void TestGetRES()
        {
            string expected = PokeType.ElectricGet().Resistances[0].Name;

            string actual = "Flying";
            List<PokeType> collectedExpected = new List<PokeType> { { PokeType.Flying },{ PokeType.Water } };
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(collectedExpected, PokeType.ElectricGet().GetRES() );
        }
        [TestMethod]
        public void TestChooseType()
        {
            PokeType electric = PokeType.ElectricGet();

            PokeType other = new PokeType();
            PokeType actual = PokeType.ChooseType("electric");

            Assert.AreEqual(electric, actual);
        }
        [TestMethod]
        public void TestChooseType2()
        {
            PokeType electric = PokeType.ChooseType("electric");

            PokeType other = new PokeType();
            PokeType actual = PokeType.ChooseType("electric");

            Assert.AreEqual(electric, actual);
        }
        [TestMethod]
        public void TestChooseType3()
        {
           
           

           

            Assert.AreEqual("Ground", PokeType.Electric.WeakTo[0].Name);

        }
    }
}



